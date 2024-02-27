using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.Intrinsics;
using eft_dma_radar.Source.Misc;
using Offsets;

namespace eft_dma_radar
{
    public class RegisteredPlayers
    {
        private readonly ulong _base;
        private readonly ulong _listBase;
        private readonly Stopwatch _regSw = new();
        private readonly Stopwatch _healthSw = new();
        private readonly Stopwatch _posSw = new();
        private readonly ConcurrentDictionary<string, Player> _players =
            new(StringComparer.OrdinalIgnoreCase);

        private int _localPlayerGroup = -100;

        private bool IsAtHideout
        {
            get => Memory.InHideout;
        }

        #region Getters
        public ReadOnlyDictionary<string, Player> Players { get; }
        public int PlayerCount
        {
            get
            {
                // ask<int> GetPlayerCountAsync()
                var players = GetPlayerCountAsync();
                players.Wait();
                return players.Result;
            }
        }

        public async Task<int> GetPlayerCountAsync()
        {
            int maxAttempts = 5;
            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                try
                {
                    var count = Memory.ReadValue<int>(_base + Offsets.UnityList.Count);
                    if (count < 1 || count > 1024)
                    {
                        //clear all variables
                        _players.Clear();
                        return -1;
                    }
                    return count;
                }
                catch (DMAShutdown)
                {
                    throw; // Specific handling for DMAShutdown
                }
                catch (Exception ex) when (attempt < maxAttempts - 1)
                {
                    await Task.Delay(1000); // Asynchronous delay
                }
                catch
                {
                    // Log exception or handle it as needed
                    return -1; // Indicate an error condition
                }
            }
            return -1; // Default error return
        }



        #endregion

        /// <summary>
        /// RegisteredPlayers List Constructor.
        /// </summary>
        public RegisteredPlayers(ulong baseAddr)
        {
            _base = baseAddr;
            Players = new(_players); // update readonly ref
            _listBase = Memory.ReadPtr(_base + 0x0010);
            _regSw.Start();
            _healthSw.Start();
            _posSw.Start();
        }

        // Example of a helper method
        private (string, string) GetPlayerIdFromBase(ulong playerBase)
        {
            // Logic to extract player ID from player base
            // Return the player ID
            //Debug.WriteLine($"PlayerBase: {playerBase:X}");
            var classNamePtr = Memory.ReadPtrChain(playerBase, Offsets.UnityClass.Name);
            var classNameString = Memory.ReadString(classNamePtr, 64).Replace("\0", string.Empty);
            //classNameString = classNameString.Replace("\0", string.Empty);
            //Debug.WriteLine($"ClassName: {classNameString}");

            string id;
            string className = classNameString;
            //ulong localPlayerProfile = 0;
            // If classname = ClientPlayer use [Class] EFT.Player
            // If classname = NextObservedPlayer use [Class] EFT.NextObservedPlayer.ObservedPlayerView

            if (classNameString == "ClientPlayer" || classNameString == "LocalPlayer" || classNameString == "HideoutPlayer") // [Class] EFT.Player : MonoBehaviour, IPlayer, GInterface58CF, GInterface58CA, GInterface58D4, GInterface590B, GInterfaceB734, IDissonancePlayer
            {
                //Local player
                var localPlayerProfile = Memory.ReadPtr(playerBase + Offsets.Player.Profile);
                var localPlayerID = Memory.ReadPtr(localPlayerProfile + Offsets.Profile.Id);

                var localPlayerIDStr = Memory.ReadUnityString(localPlayerID);
                id = localPlayerIDStr;
                
            }
            //else if (classNameString == "HideoutPlayer"){
            //    //Local player
            //    var localPlayerProfile = Memory.ReadPtr(playerBase + Offsets.Player.Profile);
            //    var localPlayerID = Memory.ReadPtr(localPlayerProfile + Offsets.Profile.Id);

            //    var localPlayerIDStr = Memory.ReadUnityString(localPlayerID);
            //    id = localPlayerIDStr;
            //}
            else if (classNameString == "ObservedPlayerView") //[Class] EFT.NextObservedPlayer.ObservedPlayerView : MonoBehaviour, IPlayer
            {
                //All other players
                var ObservedPlayerView = playerBase;
                var profileIDprt = Memory.ReadPtr(ObservedPlayerView + Offsets.ObservedPlayerView.ID);
                var profileID = Memory.ReadUnityString(profileIDprt);
                id = profileID;
            }
            else
            {
                id = null;
            }

            return (id, className);
        }

        private void ProcessPlayer(int index, ScatterReadMap scatterMap, HashSet<string> registered)
        {
            //Console.WriteLine($"UpdateList: Processing player {index + 1}.");

            try
            {
                if (!scatterMap.Results[index][0].TryGetResult<MemPointer>(out var playerBase)) return;
                //var playerBase = (ulong)scatterMap.Results[index][0].Result;
                var playerProfile = 0ul;
                (string playerId, string className) = GetPlayerIdFromBase(playerBase);
                if (playerId.Length != 24 && playerId.Length != 36) throw new ArgumentOutOfRangeException("id"); // Ensure valid ID length
                                if (_players.TryGetValue(playerId, out var player))
                {
                    if (className == "ClientPlayer" || className == "LocalPlayer" || className == "HideoutPlayer")
                    {
                        playerProfile = Memory.ReadPtr(playerBase + Offsets.Player.Profile);
                    }
                    else if (className == "ObservedPlayerView")
                    {
                        //Console.WriteLine($"Player '{player.Name}' is observed player.");
                        playerProfile = Memory.ReadPtr(playerBase);
                        //var healthController = Memory.ReadPtr(playerProfile + 0x80 +0xE8);
                    }
                    else
                    {
                        //Console.WriteLine($"Player '{player.Name}' is unknown player.");
                        //set playerProfile to 0
                        playerProfile = 0;
                        return;
                    }
                    
                    if (player.ErrorCount > 100) // Erroring out a lot? Re-Alloc
                    {
                        Program.Log(
                            $"WARNING - Existing player '{player.Name}' being re-allocated due to excessive errors..."
                        );
                        ReallocPlayer(playerId, playerBase, playerProfile);
                    }
                    else if (player.Base != playerBase) // Base address changed? Re-Alloc
                    {
                        Program.Log(
                            $"WARNING - Existing player '{player.Name}' being re-allocated due to new base address..."
                        );
                        ReallocPlayer(playerId, playerBase, playerProfile);
                    }
                    else // Mark active & alive
                    {
                        player.IsActive = true;
                        player.IsAlive = true;
                    }
                }
                else
                {
                    if (className == "ClientPlayer" || className == "LocalPlayer" || className == "HideoutPlayer") {
                        playerProfile = Memory.ReadPtr(playerBase + Offsets.Player.Profile);
                    }
                    else if (className == "ObservedPlayerView")
                    {
                        playerProfile = Memory.ReadPtr(playerBase);
                    }
                    else
                    {
                        playerProfile = 0;
                        return;
                    }
                    var newplayer = new Player(playerBase, playerProfile, null, className); // allocate new player object
                    if (
                        newplayer.Type is PlayerType.LocalPlayer
                        && _players.Any(x => x.Value.Type is PlayerType.LocalPlayer)
                    )
                    {
                        // Don't allocate more than one LocalPlayer on accident
                    }
                    else
                    {
                        if (_players.TryAdd(playerId, newplayer))
                            Program.Log($"Player '{newplayer.Name}' allocated.");
                    }
                }

                registered.Add(playerId); // Mark player as registered

            }
            catch (Exception ex)
            {
                Program.Log($"ERROR processing player at index {index}: {ex}");
            }

            void ReallocPlayer(string id, ulong newPlayerBase, ulong newPlayerProfile)
            {
                try
                {
                    var player = new Player(newPlayerBase, newPlayerProfile, _players[id].Position); // alloc
                    _players[id] = player; // update ref to new object
                    Program.Log($"Player '{player.Name}' Re-Allocated successfully.");
                }
                catch (Exception ex)
                {
                    throw new Exception($"ERROR re-allocating player '{_players[id].Name}': ", ex);
                }
            }
        }

        private bool ShouldSkipUpdate()
        {
            if (_regSw.ElapsedMilliseconds < 750)
            {
                //Debug.WriteLine("UpdateList: Update skipped, less than 750ms elapsed.");
                return true;
            }
            return false;
        }

        private void MarkInactivePlayers(HashSet<string> registered)
        {
            foreach (var player in _players)
            {
                if (player.Value.Type is PlayerType.LocalPlayer) {
                    if (!registered.Contains(player.Key))
                    {
                        player.Value.LastUpdate = true;
                    }
                }
                else if (!registered.Contains(player.Key))
                {
                    player.Value.IsActive = false;
                }
            }
        }

        private ScatterReadMap InitializeScatterRead(int count)
        {
            var scatterMap = new ScatterReadMap(count);
            var round1 = scatterMap.AddRound();
            for (int i = 0; i < count; i++) {
                round1.AddEntry<MemPointer>(i, 0, _listBase + Offsets.UnityListBase.Start + (uint)(i * 0x8));
            }
            scatterMap.Execute();

            return scatterMap;
        }

        #region UpdateList
        /// <summary>
        /// Updates the ConcurrentDictionary of 'Players'
        /// </summary>
        /// 
        public async Task UpdateListAsync()
        {
            if (ShouldSkipUpdate())
                return;

            try
            {
                int count = await GetPlayerCountAsync();
                var registered = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                var scatterMap = InitializeScatterRead(count);

                for (int i = 0; i < count; i++)
                {
                    ProcessPlayer(i, scatterMap, registered);
                }

                MarkInactivePlayers(registered);
                _regSw.Restart();
            }
            catch (DMAShutdown)
            {
                throw;
            }
            catch (RaidEnded)
            {
                Console.WriteLine("TEEEEST");
            }
            catch (Exception ex)
            {
                Program.Log($"CRITICAL ERROR - RegisteredPlayers Loop FAILED: {ex}");
            }
        }
        #endregion

        #region UpdateAllPlayers
        /// <summary>
        /// Updates all 'Player' values (Position,health,direction,etc.)
        /// </summary>
        ///
        public void UpdateAllPlayers()
        {

            if (IsAtHideout)
            {
                Debug.WriteLine("In Hideout, not updating players.");
                return;
            }
            try
            {
                var players = _players
                    .Select(x => x.Value)
                    .Where(x => x.IsActive && x.IsAlive)
                    .ToArray();
                if (players.Length == 0)
                    return; // No players
                if (_localPlayerGroup == -100) // Check if current player group is set
                {
                    var localPlayer = _players
                        .FirstOrDefault(x => x.Value.Type is PlayerType.LocalPlayer)
                        .Value;
                    if (localPlayer is not null)
                    {
                        _localPlayerGroup = localPlayer.GroupID;
                    }
                }
                //bool checkHealth = _healthSw.ElapsedMilliseconds > 250; // every 250 ms
                bool checkPos =
                    _posSw.ElapsedMilliseconds > 10000 && players.Any(x => x.IsHumanActive); // every 10 sec & at least 1 active human player
                var scatterMap = new ScatterReadMap(players.Length);
                var round1 = scatterMap.AddRound();
                ScatterReadRound round2 = null;
                if (checkPos) // allocate and add extra rounds to map
                {
                    round2 = scatterMap.AddRound();
                    
                }
                for (int i = 0; i < players.Length; i++)
                {
                    var player = players[i];
                    if (player.LastUpdate) // player may be dead/exfil'd
                    {
                        if (player.Type == PlayerType.LocalPlayer) {
                            var corpse = round1.AddEntry<MemPointer>(i, 6, player.CorpsePtr);
                        }
                    } else {
                        var rotation = round1.AddEntry<Vector2>(i, 0,
                            (player.Type == PlayerType.LocalPlayer || player.Type == PlayerType.AIOfflineScav) ?
                                player.MovementContext + Offsets.MovementContext.Rotation :
                                player.MovementContext + Offsets.ObserverdPlayerMovementContext.Rotation);

                        var posAddr = player.TransformScatterReadParameters;
                        var indices = round1.AddEntry<List<int>>(i, 1, posAddr.Item1, posAddr.Item2 * 4);
                        var vertices = round1.AddEntry<List<Vector128<float>>>(i, 2, posAddr.Item3, posAddr.Item4 * 16);

                        if (checkPos) {
                            var hierarchy = round1.AddEntry<MemPointer>(i, 3, player.TransformInternal, null, Offsets.TransformInternal.Hierarchy);
                            var indicesAddr = round2?.AddEntry<MemPointer>(i, 4, hierarchy, null, Offsets.TransformHierarchy.Indices);
                            var verticesAddr = round2?.AddEntry<MemPointer>(i, 5, hierarchy, null, Offsets.TransformHierarchy.Vertices);
                        }
                    }
 
                    //{
                    //    if (player.Type is PlayerType.LocalPlayer)
                    //    {
                    //        var rotation = round1.AddEntry<Vector2>(i,0,player.MovementContext + Offsets.MovementContext.Rotation);
                    //        var posAddr = player.TransformScatterReadParameters;
                    //        var indices = round1.AddEntry<List<int>>(i,1,posAddr.Item1,posAddr.Item2*4);
                    //        var vertices = round1.AddEntry<List<Vector128<float>>>(i,2,posAddr.Item3,posAddr.Item4*16);
                    //        if (checkPos && player.IsHumanActive)
                    //        {
                    //            var hierarchy = round1.AddEntry<MemPointer>(i,3,player.TransformInternal,null,Offsets.TransformInternal.Hierarchy);
                    //            var indicesAddr = round2?.AddEntry<MemPointer>(i,4,hierarchy,null,Offsets.TransformHierarchy.Indices);
                    //            var verticesAddr = round2?.AddEntry<MemPointer>(i,5,hierarchy,null,Offsets.TransformHierarchy.Vertices);
                    //        }
                    //    }
                    //    else if (player.Type is PlayerType.AIScav) {
                    //        //If offline - var rotation = round1.AddEntry<Vector2>(i,0,player.MovementContext + Offsets.MovementContext.Rotation);
                    //        //If online - var rotation = round1.AddEntry<Vector2>(i,0,player.MovementContext + Offsets.ObserverdPlayerMovementContext.Rotation);
                    //        var rotation = round1.AddEntry<Vector2>(i,0,player.MovementContext + Offsets.ObserverdPlayerMovementContext.Rotation);

                    //        var posAddr = player.TransformScatterReadParameters;
                    //        var indices = round1.AddEntry<List<int>>(i,1,posAddr.Item1,posAddr.Item2*4);
                    //        var vertices = round1.AddEntry<List<Vector128<float>>>(i,2,posAddr.Item3,posAddr.Item4*16);
                    //        if (checkPos)
                    //        {
                    //            var hierarchy = round1.AddEntry<MemPointer>(i,3,player.TransformInternal,null,Offsets.TransformInternal.Hierarchy);
                    //            var indicesAddr = round2?.AddEntry<MemPointer>(i,4,hierarchy,null,Offsets.TransformHierarchy.Indices);
                    //            var verticesAddr = round2?.AddEntry<MemPointer>(i,5,hierarchy,null,Offsets.TransformHierarchy.Vertices);

                    //        }
                    //    }
                    //    else if (player.Type is PlayerType.AIOfflineScav)
                    //    {
                    //        var rotation = round1.AddEntry<Vector2>(i,0,player.MovementContext + Offsets.ObserverdPlayerMovementContext.Rotation);

                    //        var posAddr = player.TransformScatterReadParameters;
                    //        var indices = round1.AddEntry<List<int>>(i,1,posAddr.Item1,posAddr.Item2*4);
                    //        var vertices = round1.AddEntry<List<Vector128<float>>>(i,2,posAddr.Item3,posAddr.Item4*16);
                    //        if (checkPos)
                    //        {
                    //            var hierarchy = round1.AddEntry<MemPointer>(i,3,player.TransformInternal,null,Offsets.TransformInternal.Hierarchy);
                    //            var indicesAddr = round2?.AddEntry<MemPointer>(i,4,hierarchy,null,Offsets.TransformHierarchy.Indices);
                    //            var verticesAddr = round2?.AddEntry<MemPointer>(i,5,hierarchy,null,Offsets.TransformHierarchy.Vertices);

                    //        }
                    //    }
                    //    else {
                    //        var rotation = round1.AddEntry<Vector2>(i,0,player.MovementContext + Offsets.ObserverdPlayerMovementContext.Rotation);
                    //        var posAddr = player.TransformScatterReadParameters;
                    //        var indices = round1.AddEntry<List<int>>(i,1,posAddr.Item1,posAddr.Item2*4);
                    //        var vertices = round1.AddEntry<List<Vector128<float>>>(i,2,posAddr.Item3,posAddr.Item4*16);
                    //        if (checkPos)
                    //        {
                    //            var hierarchy = round1.AddEntry<MemPointer>(i,3,player.TransformInternal,null,Offsets.TransformInternal.Hierarchy);
                    //            var indicesAddr = round2?.AddEntry<MemPointer>(i,4,hierarchy,null,Offsets.TransformHierarchy.Indices);
                    //            var verticesAddr = round2?.AddEntry<MemPointer>(i,5,hierarchy,null,Offsets.TransformHierarchy.Vertices);

                    //        }
                    //    }
                    //}
                }
                scatterMap.Execute();
                for (int i = 0; i < players.Length; i++)
                {
                    var player = players[i];

                    if (_localPlayerGroup != -100 && player.GroupID != -1 && player.IsHumanHostile) { // Teammate check
                        if (player.GroupID == _localPlayerGroup)
                            player.Type = PlayerType.Teammate;
                    }

                    if (player.LastUpdate) // player may be dead/exfil'd
                    {
                        if (player.Type is PlayerType.LocalPlayer)
                        {
                            if (scatterMap.Results[i][6].TryGetResult<MemPointer>(out var corpsePtr))
                            {
                                player.IsAlive = false;
                            }
                            player.IsActive = false;
                            player.LastUpdate = false;
                        }
                    }
                    else
                    {
                        bool posOK = true;
                        if (checkPos && player.IsHumanActive) // Position integrity check for active human players
                        {

                            scatterMap.Results[i][2].TryGetResult<MemPointer>(out var i4);
                            scatterMap.Results[i][3].TryGetResult<MemPointer>(out var i5);
                            if (i4 != 0x0 && i5 != 0x0)
                            {
                                var indicesAddr = i4;
                                var verticesAddr = i5;
                                if (player.IndicesAddr != indicesAddr || player.VerticesAddr != verticesAddr)
                                {
                                    Program.Log(
                                        $"WARNING - Transform has changed for Player '{player.Name}'"
                                    );
                                    player.SetPosition(null); // alloc new transform
                                    posOK = false; // Don't try update pos with old vertices/indices
                                }
                            }
                        }
                        bool p1 = true;
                        if (player.Type is PlayerType.Default)
                        {
                            continue;
                        }
                        if (player.IsLocalPlayer)
                        {
                            var rotation = scatterMap.Results[i][0].TryGetResult<Vector2>(out var rot);
                            bool p2 = player.SetRotation(rot);

                            var indices = scatterMap.Results[i][1].TryGetResult<List<int>>(out var ind);
                            var vertices = scatterMap.Results[i][2].TryGetResult<List<Vector128<float>>>(out var vert);
                            var posBufs = new object[2]
                            {
                                ind,
                                vert
                            };
                            bool p3 = true;
                            if (posOK)
                            {
                                p3 = player.SetPosition(posBufs);
                            }
                            if (p1 && p2 && p3)
                                player.ErrorCount = 0;
                            else
                                player.ErrorCount++;
                        }
                        else {
                            var rotation = scatterMap.Results[i][0].TryGetResult<Vector2>(out var rot);
                            bool p2 = player.SetRotation(rot);

                            var indices = scatterMap.Results[i][1].TryGetResult<List<int>>(out var ind);
                            var vertices = scatterMap.Results[i][2].TryGetResult<List<Vector128<float>>>(out var vert);
                            var posBufs = new object[2]
                            {
                                ind,
                                vert
                            };
                            bool p3 = true;
                            if (posOK)
                            {
                                p3 = player.SetPosition(posBufs);
                            }
                            if (p1 && p2 && p3)
                                player.ErrorCount = 0;
                            else
                                player.ErrorCount++;
                        }
                    }
                    
                }
                if (checkPos)_posSw.Restart();
            }
            
            catch (Exception ex)
            {
                Program.Log($"CRITICAL ERROR - UpdateAllPlayers Loop FAILED: {ex}");
            }
        }

        #endregion
    }
}
