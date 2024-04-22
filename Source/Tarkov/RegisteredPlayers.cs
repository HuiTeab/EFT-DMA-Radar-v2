using eft_dma_radar.Source.Misc;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.Intrinsics;

namespace eft_dma_radar
{
    public class RegisteredPlayers
    {
        private readonly ulong _base;
        private readonly ulong _listBase;
        private readonly Stopwatch _regSw = new();
        private readonly Stopwatch _healthSw = new();
        private readonly Stopwatch _posSw = new();
        private readonly ConcurrentDictionary<string, Player> _players = new(StringComparer.OrdinalIgnoreCase);

        private int _localPlayerGroup = -100;

        #region Getters
        public ReadOnlyDictionary<string, Player> Players { get; }

        private bool IsAtHideout
        {
            get => Memory.InHideout;
        }

        public int PlayerCount
        {
            get
            {
                const int maxAttempts = 5;
                for (int attempt = 0; attempt < maxAttempts; attempt++)
                {
                    try
                    {
                        var count = Memory.ReadValue<int>(this._base + Offsets.UnityList.Count);
                        //Program.Log($"[RegisteredPlayers] [PlayerCount] - Registered Players: {count}");
                        if (count < 1 || count > 1024)
                        {
                            this._players.Clear();
                            return -1;
                        }
                        return count;
                    }
                    catch (DMAShutdown)
                    {
                        throw;
                    }
                    catch (Exception ex) when (attempt < maxAttempts - 1)
                    {
                        Program.Log($"ERROR - GetPlayerCountAsync attempt {attempt + 1} failed: {ex}");
                        Thread.Sleep(1000);
                    }
                }
                return -1;
            }
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

        #region UpdateList

        /// <summary>
        /// Updates the ConcurrentDictionary of 'Players'
        /// </summary>
        public void UpdateList()
        {
            if (_regSw.ElapsedMilliseconds < 500) return; // Update every 500ms
            try
            {
                var count = PlayerCount;
                if (count < 1 || count > 1024)
                    throw new RaidEnded();

                var registered = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                var scatterMap = new ScatterReadMap(count);
                var round1 = scatterMap.AddRound();
                var round2 = scatterMap.AddRound();
                var round3 = scatterMap.AddRound();
                var round4 = scatterMap.AddRound();
                var round5 = scatterMap.AddRound();
                var round6 = scatterMap.AddRound();
                for (int i = 0; i < count; i++)
                {
                    var r1 = round1.AddEntry<ulong>(i, 0, _listBase + Offsets.UnityListBase.Start + (uint)(i * 0x8));
                    var r2 = round2.AddEntry<ulong>(i, 1, r1, null, 0x0);
                    var r3 = round3.AddEntry<ulong>(i, 2, r2, null, 0x0);
                    var r4 = round4.AddEntry<ulong>(i, 3, r3, null, 0x48);
                    var r5 = round5.AddEntry<string>(i, 4, r4, 64);
                    var r6 = round2.AddEntry<ulong>(i, 5, r1, null, Offsets.Player.Profile);
                    var r7 = round3.AddEntry<ulong>(i, 6, r6, null, Offsets.Profile.Id);
                    var r8 = round4.AddEntry<ulong>(i, 7, r1, null, Offsets.ObservedPlayerView.ID);
                    var r9 = round3.AddEntry<ulong>(i, 8, r1, null, Offsets.Player.Corpse);

                }
                scatterMap.Execute();


                for (int i = 0; i < count; i++)
                {
                    var playerProfilePtr = 0UL;
                    var profileIDPtr = 0UL;
                    scatterMap.Results[i][0].TryGetResult<ulong>(out var playerBase);
                    if (playerBase == 0) continue;
                    scatterMap.Results[i][4].TryGetResult<string>(out var className);
                    if (className == "ObservedPlayerView")
                    {
                        scatterMap.Results[i][7].TryGetResult<ulong>(out profileIDPtr);
                    }
                    else {
                        scatterMap.Results[i][5].TryGetResult<ulong>(out playerProfilePtr);
                        scatterMap.Results[i][6].TryGetResult<ulong>(out profileIDPtr);
                    }
                    if (scatterMap.Results[i][8].TryGetResult<ulong>(out var corpsePtr))
                    {
                        if (corpsePtr != 0)
                        {
                            var PlayerProfileID = Memory.ReadUnityString(Memory.ReadPtr(corpsePtr + 0x140));
                            if (_players.TryGetValue(PlayerProfileID, out var players))
                            {
                                players.LastUpdate = true;
                            }
                        }
                    }
                    if (profileIDPtr == 0)
                    {
                        Program.Log($"ERROR - ProfileIDPtr is NULL for Player '{className}'");
                        continue;
                    }
                    var profileID = Memory.ReadUnityString(profileIDPtr);
                    registered.Add(profileID);

                    if (!_players.TryGetValue(profileID, out var player))
                    {
                        player = new Player(playerBase, playerProfilePtr, null, className);
                        _players[profileID] = player;
                        Program.Log($"Player '{player.Name}' Registered successfully.");
                    }
                    else if (player.Base != playerBase)
                    {
                        ReallocPlayer(profileID, playerBase, profileIDPtr);
                    }
                }

                var inactivePlayers = _players.Where(x => !registered.Contains(x.Key) && x.Value.IsActive);
                foreach (var player in inactivePlayers)
                {
                    player.Value.LastUpdate = true;
                }
            }
            catch (DMAShutdown)
            {
                throw;
            }
            catch (RaidEnded)
            {
                throw;
            }
            catch (Exception ex)
            {
                Program.Log($"ERROR - UpdateList failed: {ex}");
            }
            finally
            {
                _regSw.Restart();
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

        #endregion UpdateList

        #region UpdateAllPlayers

        /// <summary>
        /// Updates all 'Player' values (Position,health,direction,etc.)
        /// </summary>
        public void UpdateAllPlayers()
        {

            if (IsAtHideout)
            {
                return;
            }
            try
            {
                var players = this._players
                    .Select(x => x.Value)
                    .Where(x => x.IsActive && x.IsAlive)
                    .ToArray();

                if (players.Length == 0)
                {
                    //Console.WriteLine("No players found.");
                    return;
                }
                //Program.Log($"[RegisteredPlayers] [UpdateAllPlayers] - Registered Players: {players.Length}");
                if (this._localPlayerGroup == -100) // Check if current player group is set
                {
                    var localPlayer = this._players
                        .FirstOrDefault(x => x.Value.Type is PlayerType.LocalPlayer)
                        .Value;
                    if (localPlayer is not null)
                    {
                        this._localPlayerGroup = localPlayer.GroupID;
                    }
                }
                bool checkHealth = this._healthSw.ElapsedMilliseconds > 500; // every 250 ms
                bool checkPos = this._posSw.ElapsedMilliseconds > 10000 && players.Any(x => x.IsHumanActive); // every 10 sec & at least 1 active human player
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
                        var corpse = round1.AddEntry<MemPointer>(i, 6, player.CorpsePtr);
                    } 
                    else {
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
                        if (checkHealth && player.IsHostileActive) {
                            var health = round1.AddEntry<int>(i, 7, player.HealthController + 0xD8);
                        }
                    }
                }

                scatterMap.Execute();

                for (int i = 0; i < players.Length; i++)
                {
                    var player = players[i];

                    if (this._localPlayerGroup != -100 && player.GroupID != -1 && player.IsHumanHostile && player.GroupID == this._localPlayerGroup) { // Teammate check
                            player.Type = PlayerType.Teammate;
                    }

                    if (player.LastUpdate) // player may be dead/exfil'd
                    {
                        if (scatterMap.Results[i][6].TryGetResult<MemPointer>(out var corpsePtr))
                        {
                            if (corpsePtr != null)
                            {
                                Console.WriteLine($"{player.Name} => {corpsePtr}");
                                player.IsAlive = false;
                            }
                        }
                        player.IsActive = false;
                        player.LastUpdate = false;
                    }
                    else
                    {
                        bool posOK = true;
                        if (checkPos && player.IsHumanActive)
                        {
                            scatterMap.Results[i][2].TryGetResult<MemPointer>(out var i4);
                            scatterMap.Results[i][3].TryGetResult<MemPointer>(out var i5);
                            if (i4 != 0x0 && i5 != 0x0)
                            {
                                var indicesAddr = i4;
                                var verticesAddr = i5;
                                if (player.IndicesAddr != indicesAddr || player.VerticesAddr != verticesAddr)
                                {
                                    Program.Log($"WARNING - Transform has changed for Player '{player.Name}'");
                                    player.SetPosition(null);
                                    posOK = false;
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
                        else
                        {
                            var rotation = scatterMap.Results[i][0].TryGetResult<Vector2>(out var rot);
                            bool p2 = player.SetRotation(rot);

                            var indices = scatterMap.Results[i][1].TryGetResult<List<int>>(out var ind);
                            var vertices = scatterMap.Results[i][2].TryGetResult<List<Vector128<float>>>(out var vert);

                            if (checkHealth && player.Type != PlayerType.Teammate)
                            {
                                var health = scatterMap.Results[i][7].TryGetResult<int>(out var hp);
                                player.SetHealth(hp);
                            }

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

                            player.SetKDAsync();

                            if (p1 && p2 && p3)
                                player.ErrorCount = 0;
                            else
                                player.ErrorCount++;
                        }
                    }
                }

                if (checkHealth)
                    this._healthSw.Restart();

                if (checkPos)
                    this._posSw.Restart();
            }
            
            catch (Exception ex)
            {
                Program.Log($"CRITICAL ERROR - UpdateAllPlayers Loop FAILED: {ex}");
            }
        }
        #endregion
    }
}
