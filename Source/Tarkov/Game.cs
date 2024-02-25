using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace eft_dma_radar
{

    /// <summary>
    /// Class containing Game (Raid) instance.
    /// </summary>
    public class Game
    {
        private readonly ulong _unityBase;
        private GameObjectManager _gom;
        private ulong _localGameWorld;
        private LootManager _lootManager;
        private RegisteredPlayers _rgtPlayers;
        private bool _inHideout = false;
        private GrenadeManager _grenadeManager;
        private ExfilManager _exfilManager;
        private volatile bool _inGame = false;
        private volatile bool _loadingLoot = false;
        private volatile bool _refreshLoot = false;
        private volatile string _mapName = string.Empty;
        private volatile bool _isScav = false;
        private ulong _fpscamera;
        private ulong _opticcamera;
        private QuestManager _questManager;
        
        #region Getters
        public bool InGame
        {
            get => _inGame;
        }
        // in InHideout means local game world not false and registered players is 1
        public bool InHideout
        {
            get => _inHideout;
        }
        public bool IsScav
        {
            get => _isScav;
        }
        public string MapName
        {
            get => _mapName;
        }
        public int PlayerSide
        {
            get => 0;
        }
        public bool LoadingLoot
        {
            get => _loadingLoot;
        }
        public ReadOnlyDictionary<string, Player> Players
        {
            get => _rgtPlayers?.Players;
        }
        public LootManager Loot
        {
            get => _lootManager;
        }
        public ReadOnlyCollection<Grenade> Grenades
        {
            get => _grenadeManager?.Grenades;
        }
        public ReadOnlyCollection<Exfil> Exfils
        {
            get => _exfilManager?.Exfils;
        }
        public ulong FpsCamera
        {
            get => _fpscamera;
        }
        public ulong OpticCamera
        {
            get => _opticcamera;
        }
        #endregion

        /// <summary>
        /// Game Constructor.
        /// </summary>
        public Game(ulong unityBase)
        {
            _unityBase = unityBase;
        }

        #region GameLoop
        /// <summary>
        /// Main Game Loop executed by Memory Worker Thread.
        /// It manages the updating of player list and game environment elements like loot, grenades, and exfils.
        /// </summary>
        public async Task GameLoop()
        {
            try
            {
                // Update the list of players and their states
                await UpdatePlayersAsync();

                // Update game environment elements such as loot and exfils
                if (_inGame && !InHideout )
                {
                    UpdateGameEnvironment();
                }
                //if registered players is -1, then we are died or exfilled
                if (_rgtPlayers.PlayerCount == -1)
                {
                    throw new RaidEnded();
                }
            }
            catch (DMAShutdown)
            {
                // Handle DMA shutdown scenarios
                HandleDMAShutdown();
            }
            catch (RaidEnded e)
            {
                HandleRaidEnded(e);
            }
            catch (Exception ex)
            {
                // Handle any unexpected exceptions that occur during the game loop
                HandleUnexpectedException(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Updates the list of players and their current states.
        /// </summary>
        private async Task UpdatePlayersAsync()
        {
            // Update the list of registered players
            //_rgtPlayers.UpdateList();

            await _rgtPlayers.UpdateListAsync();
            // Update the state of each player (e.g., location, health)
            _rgtPlayers.UpdateAllPlayers();
        }
        /// <summary>
        /// Method to get map name using local game world
        /// </summary>
        /// <returns></returns>
        private void GetMapName()
        {
            //If in hideout, map name is empty
            if (_inHideout)
            {
                _mapName = string.Empty;
                return;
            }
            var classNamePtr = Memory.ReadPtrChain(_localGameWorld, Offsets.UnityClass.Name);
            var classNameString = Memory.ReadString(classNamePtr, 64).Replace("\0", string.Empty);
            if (classNameString == "ClientLocalGameWorld") {
                var mapNamePrt = Memory.ReadPtrChain(_localGameWorld, new uint[] { 0x148, 0x550 });
                var mapName = Memory.ReadUnityString(mapNamePrt);
                _mapName = mapName;
            } else {
                var mapNamePrt = Memory.ReadPtr(_localGameWorld + 0x40);
                var mapName = Memory.ReadUnityString(mapNamePrt);
                _mapName = mapName;
            }
        }

        /// <summary>
        /// Updates miscellaneous game environment elements like loot, grenades, and exfils.
        /// </summary>
        private void UpdateGameEnvironment()
        {
            // This method encapsulates the logic for updating various elements in the game
            // such as loot positions, grenade statuses, and exfil points
            UpdateMisc();
        }

        /// <summary>
        /// Handles the scenario when DMA shutdown occurs.
        /// </summary>
        private void HandleDMAShutdown()
        {
            _inGame = false;
            // Additional logic to handle DMA shutdown
        }

        /// <summary>
        /// Handles the scenario when the raid ends.
        /// </summary>
        /// <param name="e">The RaidEnded exception instance containing details about the raid end.</param>
        private void HandleRaidEnded(RaidEnded e)
        {
            Program.Log("Raid has ended!");
            _inGame = false;
            //Clear all game objects
            _lootManager = null;
            _rgtPlayers = null;
            _grenadeManager = null;
            _exfilManager = null;
            _mapName = string.Empty;
            _fpscamera = 0;
            _opticcamera = 0;
            _localGameWorld = 0;
            //Wait before trying to get game world again
            Thread.Sleep(5000);
        }

        /// <summary>
        /// Handles unexpected exceptions that occur during the game loop.
        /// </summary>
        /// <param name="ex">The exception instance that was thrown.</param>
        private void HandleUnexpectedException(Exception ex)
        {
            Program.Log($"CRITICAL ERROR - Raid ended due to unhandled exception: {ex}");
            _inGame = false;
            // Additional logic to handle unexpected exceptions
        }

        /// <summary>
        /// Waits until Raid has started before returning to caller.
        /// </summary>
        /// 
        public async Task WaitForGameAsync()
        {
            //GetCamera();
            while (!(GetGOM() && await GetLGWAsync())) //&& GetCamera()
            {
                _inGame = false;
                await Task.Delay(500);
            }
            //GetCamera();
            Program.Log("Raid has started!");
            _inGame = true;
        }


        /// <summary>
        /// Helper method to locate Game World object.
        /// </summary>
        private ulong GetObjectFromList(ulong activeObjectsPtr, ulong lastObjectPtr, string objectName)
        {
            var activeObject = Memory.ReadValue<BaseObject>(Memory.ReadPtr(activeObjectsPtr));
            var lastObject = Memory.ReadValue<BaseObject>(Memory.ReadPtr(lastObjectPtr));
            if (activeObject.obj != 0x0 && lastObject.obj == 0x0)
            {
                // Add wait for lastObject to be populated
                Program.Log("Waiting for lastObject to be populated...");
                while (lastObject.obj == 0x0)
                {
                    lastObject = Memory.ReadValue<BaseObject>(Memory.ReadPtr(lastObjectPtr));
                    Thread.Sleep(1000);
                }
            }

            if (activeObject.obj != 0x0)
            {
                while (activeObject.obj != 0x0 && activeObject.obj != lastObject.obj)
                {
                    var objectNamePtr = Memory.ReadPtr(activeObject.obj + Offsets.GameObject.ObjectName);
                    var objectNameStr = Memory.ReadString(objectNamePtr, 64);
                    if (objectNameStr.Contains(objectName, StringComparison.OrdinalIgnoreCase))
                    {
                        Program.Log($"Found object {objectNameStr}");
                        return activeObject.obj;
                    }

                    activeObject = Memory.ReadValue<BaseObject>(activeObject.nextObjectLink); // Read next object
                }
            }
            if (lastObject.obj != 0x0)
            {
                var objectNamePtr = Memory.ReadPtr(lastObject.obj + Offsets.GameObject.ObjectName);
                var objectNameStr = Memory.ReadString(objectNamePtr, 64);
                if (objectNameStr.Contains(objectName, StringComparison.OrdinalIgnoreCase))
                {
                    Program.Log($"Found object {objectNameStr}");
                    return lastObject.obj;
                }
            }
            Program.Log($"Couldn't find object {objectName}");
            return 0;
        }

        public static ulong GetComponentFromGameObject(ulong gameObject, string componentName)
        {
            var component = Memory.ReadPtr(gameObject + 0x30);
            // Loop through a fixed range of potential component slots
            for (int i = 0x8; i < 0x100; i += 0x10) 
            {
                var componentPtr = Memory.ReadPtr(component + (ulong)i);
                if (componentPtr == 0) continue;
                var fieldsPtr = Memory.ReadPtr(componentPtr + 0x28);
                componentPtr = Memory.ReadPtr(componentPtr + 0x28);
                var classNamePtr = Memory.ReadPtrChain(fieldsPtr, Offsets.UnityClass.Name);
                var className = Memory.ReadString(classNamePtr, 64).Replace("\0", string.Empty);
                //Console.WriteLine($"GetComponentFromGameObject: {className}");
                if (className.Contains(componentName, StringComparison.OrdinalIgnoreCase))
                {
                    return componentPtr;
                }
            }
            return 0;
        }

        /// <summary>
        /// Gets Game Object Manager structure.
        /// </summary>
        private bool GetGOM()
        {
            try
            {
                var addr = Memory.ReadPtr(_unityBase + Offsets.ModuleBase.GameObjectManager);
                _gom = Memory.ReadValue<GameObjectManager>(addr);
                Program.Log($"Found Game Object Manager at 0x{addr.ToString("X")}");
                return true;
            }
            catch (DMAShutdown) { throw; }
            catch (Exception ex)
            {
                throw new GameNotRunningException($"ERROR getting Game Object Manager, game may not be running: {ex}");
            }
        }

        /// <summary>
        /// Gets Camera Object Manager structure.
        /// </summary>
        private bool GetCamera()
        {
            try
            {
                var addr = Memory.ReadPtr(_unityBase + 0x0179F500);
                for (int i = 0; i < 500; i++)
                {
                    var allCameras = Memory.ReadPtr(addr + 0x0);
                    var camera = Memory.ReadPtr(allCameras + (ulong)i * 0x8);

                    if (camera != 0)
                    {
                        var cameraObject = Memory.ReadPtr(camera + Offsets.GameObject.ObjectClass);
                        var cameraNamePtr = Memory.ReadPtr(cameraObject + Offsets.GameObject.ObjectName);

                        var cameraName = Memory.ReadString(cameraNamePtr, 64).Replace("\0", string.Empty);
                        if (cameraName.Contains("BaseOpticCamera(Clone)", StringComparison.OrdinalIgnoreCase))
                        {
                            _opticcamera = cameraObject;
                        }
                        if (cameraName.Contains("FPS Camera", StringComparison.OrdinalIgnoreCase))
                        {
                            _fpscamera = cameraObject;
                        }
                        if (_opticcamera != 0 && _fpscamera != 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (DMAShutdown) { throw; }
            catch (Exception ex)
            {
                throw new GameNotRunningException($"ERROR getting Camera, game may not be running: {ex}");
            }
            return false;
        }

        /// <summary>
        /// public static function to turn thermalvision on and off
        /// </summary>
        /// 
        public static void ThermalVision(bool on)
        {
            if (Memory.FpsCamera != 0)
            {
                var fpsThermalComponent = GetComponentFromGameObject(Memory.FpsCamera, "ThermalVision");

                var thermalOn = Memory.ReadValue<bool>(fpsThermalComponent + 0xE0);
                if (on) {
                    if (!thermalOn)
                    {
                        Memory.WriteValue(fpsThermalComponent + 0xE0, true);
                        Memory.WriteValue(fpsThermalComponent + 0xE1, false);
                        Memory.WriteValue(fpsThermalComponent + 0xE2, false);
                        Memory.WriteValue(fpsThermalComponent + 0xE3, false);
                        Memory.WriteValue(fpsThermalComponent + 0xE4, false);
                        Memory.WriteValue(fpsThermalComponent + 0xE5, false);
                    }

                }
                else
                {
                    if (thermalOn)
                    {
                        Memory.WriteValue(fpsThermalComponent + 0xE0, false);
                        Memory.WriteValue(fpsThermalComponent + 0xE1, true);
                        Memory.WriteValue(fpsThermalComponent + 0xE2, true);
                        Memory.WriteValue(fpsThermalComponent + 0xE3, true);
                        Memory.WriteValue(fpsThermalComponent + 0xE4, true);
                        Memory.WriteValue(fpsThermalComponent + 0xE5, true);
                    }
                }
            }
        }

        public static void ThermalVisionOptic(bool on)
        {
            if (Memory.OpticCamera != 0)
            {
                ulong opticComponent = 0;
                ulong opticThermalVision = 0;
                var component = Memory.ReadPtr(Memory.OpticCamera + 0x30);
                for (int i = 0x8; i < 0x100; i += 0x10) 
                {
                    var fields = Memory.ReadPtr(component + (ulong)i);
                    if (fields == 0) continue;
                    var fieldsPtr_ = Memory.ReadPtr(fields + 0x28);
                    var classNamePtr = Memory.ReadPtrChain(fieldsPtr_, Offsets.UnityClass.Name);
                    var className = Memory.ReadString(classNamePtr, 64).Replace("\0", string.Empty);
                    if (className == "ThermalVision")
                    {

                        opticComponent = fields;
                        opticThermalVision = Memory.ReadPtr(fieldsPtr_ + 0x28);
                        break;
                    }
                }
                if (on)
                {
                    Memory.WriteValue(opticComponent + 0x38, true);
                    Memory.WriteValue(opticThermalVision + 0xE0, true);
                }
                else
                {
                    Memory.WriteValue(opticComponent + 0x38, false);
                    Memory.WriteValue(opticThermalVision + 0xE0, false);
                }
            }
        }

        /// <summary>
        /// public static function to turn nightvision on and off
        /// </summary>
        ///     
        public static void NightVision(bool on)
        {
            if (Memory.FpsCamera != 0)
            {
                var nightVisionComponent = GetComponentFromGameObject(Memory.FpsCamera, "NightVision");
                var nightVisionOn = Memory.ReadValue<bool>(nightVisionComponent + 0xEC);
                if (on)
                {
                    if (!nightVisionOn)
                    {
                        Memory.WriteValue(nightVisionComponent + 0xEC, true);
                    }
                }
                else
                {
                    if (nightVisionOn)
                    {
                        Memory.WriteValue(nightVisionComponent + 0xEC, false);
                    }
                }
            }
        }

        /// <summary>
        /// Gets Local Game World address.
        /// </summary>
        private async Task<bool> GetLGWAsync()
        {
            int retryInterval = 500; // Time in milliseconds to wait before retrying
            int maxRetries = 5000; // Maximum number of retries
            int retryCount = 0;

            while (retryCount < maxRetries)
            {
                try
                {
                    ulong activeNodes = Memory.ReadPtr(_gom.ActiveNodes);
                    ulong lastActiveNode = Memory.ReadPtr(_gom.LastActiveNode);
                    var gameWorld = GetObjectFromList(activeNodes, lastActiveNode, "GameWorld");
                    if (gameWorld == 0)
                    {
                        Program.Log("Unable to find GameWorld Object, likely not in raid. Retrying...");
                        await Task.Delay(retryInterval);
                        retryCount++;
                        continue;
                    }

                    _localGameWorld = Memory.ReadPtrChain(gameWorld, Offsets.GameWorld.To_LocalGameWorld);
                    if (_localGameWorld == 0)
                    {
                        Program.Log("ERROR - Local Game World is null. Retrying...");
                        await Task.Delay(retryInterval);
                        retryCount++;
                        continue;
                    }
                    var localPlayer = Memory.ReadPtr(_localGameWorld + Offsets.LocalGameWorld.MainPlayer);
                    var localPlayerInfo = Memory.ReadPtrChain(localPlayer, new uint[] {Offsets.Player.Profile, Offsets.Profile.PlayerInfo});
                    var localPlayeSide = Memory.ReadValue<int>(localPlayerInfo + Offsets.PlayerInfo.PlayerSide);
                    if (localPlayeSide == 4)
                    {
                        _isScav = true;
                    }
                    else
                    {
                        _isScav = false;
                    }
                    var localGameWorldClassnamePtr = Memory.ReadPtrChain(_localGameWorld, Offsets.UnityClass.Name);
                    var localGameWorldClassName = Memory.ReadString(localGameWorldClassnamePtr, 64).Replace("\0", string.Empty);
                    var localPlayerClassnamePtr = Memory.ReadPtrChain(localPlayer, Offsets.UnityClass.Name);
                    var classNameString = Memory.ReadString(localPlayerClassnamePtr, 64).Replace("\0", string.Empty);
                    //Hideout handling
                    if (classNameString == "HideoutPlayer")
                    {
                        var rgtPlayers = localPlayer;
                        _rgtPlayers = new RegisteredPlayers(rgtPlayers);
                        _inHideout = true;
                        //Should skip loot, grenades, exfils, etc. But should still keep gearmanager
                        return true;
                    }
                    //Online handling
                    else if (classNameString == "ClientPlayer" || classNameString == "LocalPlayer" && localGameWorldClassName != "ClientLocalGameWorld")
                    {
                        _inHideout = false;
                        var rgtPlayers = new RegisteredPlayers(Memory.ReadPtr(_localGameWorld + Offsets.LocalGameWorld.RegisteredPlayers));
                        // retry if player count is 0
                        if (rgtPlayers.PlayerCount == 0)
                        {
                            Console.WriteLine("Player count is 0. Retrying...");
                            await Task.Delay(retryInterval);
                            retryCount++;
                            continue;
                        }

                        _rgtPlayers = rgtPlayers;
                        return true; // Successful exit
                    }
                    //Offline handling
                    else if (localGameWorldClassName == "ClientLocalGameWorld") {
                        _inHideout = false;
                        var rgtPlayers = new RegisteredPlayers(Memory.ReadPtr(_localGameWorld + Offsets.LocalGameWorld.RegisteredPlayers));
                        if (rgtPlayers.PlayerCount == 0)
                        {
                            Console.WriteLine("Player count is 0. Retrying...");
                            await Task.Delay(retryInterval);
                            retryCount++;
                            continue;
                        }
                        _rgtPlayers = rgtPlayers;
                        return true; // Successful exit
                    }
                    else {
                        await Task.Delay(retryInterval);
                        retryCount++;
                        continue;
                    }
                }
                catch (DMAShutdown)
                {
                    throw; // Propagate the DMAShutdown exception upwards
                }
                catch (Exception ex)
                {
                    Program.Log($"ERROR getting Local Game World: {ex}. Retrying...");
                    await Task.Delay(retryInterval);
                    retryCount++;
                    continue;
                }
            }

            Program.Log("Maximum retry attempts reached. Unable to retrieve Local Game World.");
            return false; // Indicate failure after maximum retries
        }



        /// <summary>
        /// Loot, grenades, exfils,etc.
        /// </summary>
        private void UpdateMisc()
        {
            if (_questManager is null)
            {
                try
                {
                    var questManager = new QuestManager(_localGameWorld);
                    _questManager = questManager; // update ref
                }
                catch (Exception ex)
                {
                    Program.Log($"ERROR loading QuestManager: {ex}");
                }
            }
            if (_lootManager is null || _refreshLoot)
            {
                _loadingLoot = true;
                try
                {
                    var loot = new LootManager(_localGameWorld);
                    _lootManager = loot; // update ref
                    _refreshLoot = false;
                }
                catch (Exception ex)
                {
                    Program.Log($"ERROR loading LootEngine: {ex}");
                }
                GetCamera();
                _loadingLoot = false;
            }
            if (_mapName == string.Empty)
            {
                try
                {
                    GetMapName();
                }
                catch (Exception ex)
                {
                    Program.Log($"ERROR getting map name: {ex}");
                }
            }
            if (_grenadeManager is null)
            {
                try
                {
                    var grenadeManager = new GrenadeManager(_localGameWorld);
                    _grenadeManager = grenadeManager; // update ref
                }
                catch (Exception ex)
                {
                    Program.Log($"ERROR loading GrenadeManager: {ex}");
                }
            }
            else _grenadeManager.Refresh(); // refresh via internal stopwatch
            if (_exfilManager is null)
            {
                try
                {
                    var exfils = new ExfilManager(_localGameWorld);
                    _exfilManager = exfils; // update ref
                }
                catch (Exception ex)
                {
                    Program.Log($"ERROR loading ExfilController: {ex}");
                }
            }
            else _exfilManager.Refresh(); // periodically refreshes (internal stopwatch)
        }
        public void RefreshLoot()
        {
            if (_inGame)
            {
                _refreshLoot = true;
            }
        }
        #endregion
    }

    #region Exceptions
    public class GameNotRunningException : Exception
    {
        public GameNotRunningException()
        {
        }

        public GameNotRunningException(string message)
            : base(message)
        {
        }

        public GameNotRunningException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class RaidEnded : Exception
    {
        public RaidEnded()
        {

        }

        public RaidEnded(string message)
            : base(message)
        {
        }

        public RaidEnded(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    #endregion
}
