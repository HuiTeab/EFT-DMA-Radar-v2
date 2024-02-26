using System;
using System.Diagnostics;
using System.Numerics;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Text;
using eft_dma_radar.Source.Misc;

namespace eft_dma_radar
{
    /// <summary>
    /// Class containing Game Player Data.
    /// </summary>
    public class Player
    {
        private static readonly FileSystemWatcher _watchlistMonitor;
        private static readonly object _watchlistLock = new();
        private static readonly ConcurrentStack<PlayerHistoryEntry> _history = new();
        private static Dictionary<string, int> _groups = new(StringComparer.OrdinalIgnoreCase);

        private readonly Stopwatch _posRefreshSw = new();
        private readonly Stopwatch _kdRefreshSw = new();
        private readonly object _posLock = new(); // sync access to this.Position (non-atomic)
        private readonly GearManager _gearManager;
        private Transform _transform;

        #region PlayerProperties
        /// <summary>
        /// Player is a PMC Operator.
        /// </summary>
        public bool IsPmc { get; }
        /// <summary>
        /// Player is a Local PMC Operator.
        /// </summary>
        public bool IsLocalPlayer { get; }
        /// <summary>
        /// Player is Alive/Not Dead.
        /// </summary>
        public volatile bool IsAlive = true;
        /// <summary>
        /// Player is Active (has not exfil'd).
        /// </summary>
        public volatile bool IsActive = true;
        /// <summary>
        /// Account UUID for Human Controlled Players.
        /// </summary>
        public string AccountID { get; }
        /// <summary>
        /// Player name.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Player Level (Based on experience).
        /// </summary>
        public int Lvl { get; } = 0;
        /// <summary>
        /// MemberCategory of Player Account (Developer,Sherpa,etc.) null if ordinary account/eod.
        /// </summary>
        public string Category { get; }
        /// <summary>
        /// Player's Kill/Death Average
        /// </summary>
        public float KDA { get; private set; } = -1f;
        /// <summary>
        /// Group that the player belongs to.
        /// </summary>
        public int GroupID { get; } = -1;
        /// <summary>
        /// Type of player unit.
        /// </summary>
        public PlayerType Type { get; set; }
        /// <summary>
        /// Player's current health (sum of all 7 body parts).
        /// </summary>
        public int Health { get; private set; } = -1;
        private Vector3 _pos = new Vector3(0, 0, 0); // backing field
        /// <summary>
        /// Player's Unity Position in Local Game World.
        /// </summary>
        /// 
        private LootManager Loot
        {
            get => Memory.Loot;
        }
        public Vector3 Position // 96 bits, cannot set atomically
        {
            get
            {
                lock (_posLock)
                {
                    return _pos;
                }
            }
            private set
            {
                lock (_posLock)
                {
                    _pos = value;
                }
            }
        }
        /// <summary>
        /// Cached 'Zoomed Position' on the Radar GUI. Used for mouseover events.
        /// </summary>
        public Vector2 ZoomedPosition { get; set; } = new();
        /// <summary>
        /// Player's Rotation (direction/pitch) in Local Game World.
        /// 90 degree offset ~already~ applied to account for 2D-Map orientation.
        /// </summary>
        public Vector2 Rotation { get; private set; } = new Vector2(0, 0); // 64 bits will be atomic
        /// <summary>
        /// (PMC ONLY) Player's Gear Loadout.
        /// Key = Slot Name, Value = Item 'Long Name' in Slot
        /// </summary>
        public ReadOnlyDictionary<string, GearItem> Gear
        {
            get => _gearManager?.Gear;
        }
        /// <summary>
        /// If 'true', Player object is no longer in the RegisteredPlayers list.
        /// Will be checked if dead/exfil'd on next loop.
        /// </summary>
        public bool LastUpdate { get; set; } = false;
        /// <summary>
        /// Consecutive number of errors that this Player object has 'errored out' while updating.
        /// </summary>
        public int ErrorCount { get; set; } = 0;
        #endregion

        #region Getters
        /// <summary>
        /// Contains 'Acct UUIDs' of tracked players for the Key, and the 'Reason' for the Value.
        /// </summary>
        private static ReadOnlyDictionary<string, string> Watchlist { get; set; } // init in Static Constructor
        /// <summary>
        /// Contains history of Enemy Players (human-controlled) that are allocated during program runtime.
        /// </summary>
        /// 
        
        public static ListViewItem[] History
        {
            get => _history.Select(x => x.View).ToArray();
        }
        /// <summary>
        /// Player is a Hostile PMC Operator.
        /// </summary>
        public bool IsHostilePmc
        {
            get => IsPmc && (Type is PlayerType.PMC || Type is PlayerType.SpecialPlayer);
        }
        /// <summary>
        /// Player is human-controlled.
        /// </summary>
        public bool IsHuman
        {
            get => (Type is PlayerType.LocalPlayer ||
                Type is PlayerType.Teammate ||
                Type is PlayerType.PMC ||
                Type is PlayerType.SpecialPlayer ||
                Type is PlayerType.PScav||
                Type is PlayerType.BEAR ||
                Type is PlayerType.USEC);
        }
        /// <summary>
        /// Player is human-controlled and Active/Alive.
        /// </summary>
        public bool IsHumanActive
        {
            get => (Type is PlayerType.LocalPlayer ||
                Type is PlayerType.Teammate ||
                Type is PlayerType.PMC ||
                Type is PlayerType.SpecialPlayer ||
                Type is PlayerType.PScav||
                Type is PlayerType.BEAR ||
                Type is PlayerType.USEC) && IsActive && IsAlive;
        }
        /// <summary>
        /// Player is human-controlled & Hostile.
        /// </summary>
        public bool IsHumanHostile
        {
            get => (Type is PlayerType.PMC ||
                Type is PlayerType.SpecialPlayer ||
                Type is PlayerType.PScav ||
                Type is PlayerType.BEAR ||
                Type is PlayerType.USEC);
        }
        /// <summary>
        /// Player is human-controlled, hostile, and Active/Alive.
        /// </summary>
        public bool IsHumanHostileActive
        {
            get => (Type is PlayerType.BEAR || Type is PlayerType.USEC ||
                    Type is PlayerType.SpecialPlayer ||
                    Type is PlayerType.PScav)
                    && IsActive && IsAlive;
        }
        /// <summary>
        /// Player is friendly to LocalPlayer (including LocalPlayer) and Active/Alive.
        /// </summary>
        public bool IsFriendlyActive
        {
            get => ((Type is PlayerType.LocalPlayer ||
                Type is PlayerType.Teammate) && IsActive && IsAlive);
        }
        /// <summary>
        /// Player has exfil'd/left the raid.
        /// </summary>
        public bool HasExfild
        {
            get => !IsActive && IsAlive;
        }
        /// <summary>
        /// EFT.Player Address
        /// </summary>
        public ulong Base { get; }
        /// <summary>
        /// EFT.Profile Address
        /// </summary>
        public ulong Profile { get; }
        /// <summary>
        /// PlayerInfo Address (GClass1044)
        /// </summary>
        /// 
        public ulong NextObservedPlayerView { get; }
        public ulong Info { get; }
        public ulong TransformInternal { get; }
        public ulong VerticesAddr
        {
            get => _transform?.VerticesAddr ?? 0x0;
        }
        public ulong IndicesAddr
        {
            get => _transform?.IndicesAddr ?? 0x0;
        }
        /// <summary>
        /// Health Entries for each Body Part.
        /// </summary>
        public ulong[] HealthEntries { get; }
        public ulong MovementContext { get; }
        public ulong CorpsePtr
        {
            get => Base + Offsets.Player.Corpse;
        }

        public ulong ObservedPlayerViewCorpsePtr
        {
            get => NextObservedPlayerView + 0x80 + 0x128;
        }
        /// <summary>
        /// IndicesAddress -> IndicesSize -> VerticesAddress -> VerticesSize
        /// </summary>
        public Tuple<ulong, int, ulong, int> TransformScatterReadParameters
        {
            get => _transform?.GetScatterReadParameters() ?? new Tuple<ulong, int, ulong, int>(0, 0, 0, 0);
        }
        #endregion

        #region Static_Constructor
        static Player()
        {
            LoadWatchlist();
            _watchlistMonitor = new FileSystemWatcher(".")
            {
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = "playerWatchlist.txt",
                EnableRaisingEvents = true
            };
            _watchlistMonitor.Changed += new FileSystemEventHandler(watchlist_Changed);
        }
        #endregion

        Dictionary<string, string> nameTranslations = new Dictionary<string, string>
        {
            {"Килла", "Killa"},
            {"Решала", "Reshala"},
            {"Глухарь", "Glukhar"},
            {"Штурман", "Shturman"},
            {"Санитар", "Sanitar"},
            {"Тагилла", "Tagilla"},
            {"Рейдеры", "Raider"},
            {"Сектант Жрец", "Cultist Priest"},
            {"Отступники", "Renegade"},
            {"Big Pipe", "Big Pipe"},
            {"Birdeye", "Birdeye"},
            {"Knight", "Knight"},
            {"Зрячий", "Zryachiy"},
            {"Кабан", "Kaban"},
            {"Коллонтай", "Kollontay"}
        };

        private static readonly Dictionary<char, string> CyrillicToLatinMap = new Dictionary<char, string>
        {
                {'А', "A"}, {'Б', "B"}, {'В', "V"}, {'Г', "G"}, {'Д', "D"},
                {'Е', "E"}, {'Ё', "E"}, {'Ж', "Zh"}, {'З', "Z"}, {'И', "I"},
                {'Й', "Y"}, {'К', "K"}, {'Л', "L"}, {'М', "M"}, {'Н', "N"},
                {'О', "O"}, {'П', "P"}, {'Р', "R"}, {'С', "S"}, {'Т', "T"},
                {'У', "U"}, {'Ф', "F"}, {'Х', "Kh"}, {'Ц', "Ts"}, {'Ч', "Ch"},
                {'Ш', "Sh"}, {'Щ', "Shch"}, {'Ъ', ""}, {'Ы', "Y"}, {'Ь', ""},
                {'Э', "E"}, {'Ю', "Yu"}, {'Я', "Ya"},
                {'а', "a"}, {'б', "b"}, {'в', "v"}, {'г', "g"}, {'д', "d"},
                {'е', "e"}, {'ё', "e"}, {'ж', "zh"}, {'з', "z"}, {'и', "i"},
                {'й', "y"}, {'к', "k"}, {'л', "l"}, {'м', "m"}, {'н', "n"},
                {'о', "o"}, {'п', "p"}, {'р', "r"}, {'с', "s"}, {'т', "t"},
                {'у', "u"}, {'ф', "f"}, {'х', "kh"}, {'ц', "ts"}, {'ч', "ch"},
                {'ш', "sh"}, {'щ', "shch"}, {'ъ', ""}, {'ы', "y"}, {'ь', ""},
                {'э', "e"}, {'ю', "yu"}, {'я', "ya"}
        };

        public static string TransliterateCyrillic(string input)
        {
            StringBuilder output = new StringBuilder();

            foreach (char c in input)
            {
                output.Append(CyrillicToLatinMap.TryGetValue(c, out var latinEquivalent) ? latinEquivalent : c.ToString());
            }

            return output.ToString();
        }

        #region Constructor
        /// <summary>
        /// Player Constructor.
        /// </summary>
        public Player(ulong playerBase, ulong playerProfile, Vector3? pos = null, string baseClassName = null)
        {
            Debug.WriteLine("Player Constructor: Initialization started.");
            try
            {
                Base = playerBase;
                Profile = playerProfile;
                //Debug.WriteLine($"Player Base Address: 0x{playerBase:X}, Profile Address: 0x{playerProfile:X}");
                if (pos is not null)
                {
                    this.Position = (Vector3)pos; // Populate provided Position (usually only for a re-alloc)
                }
                if (baseClassName is null)
                {
                    return;
                    
                } else {
                    //Debug.WriteLine($"Base Class Name: {baseClassName}");
                }
                if (baseClassName == "ClientPlayer" || baseClassName == "LocalPlayer" || baseClassName == "HideoutPlayer") {
                    ulong localPlayerInfoOffset = playerProfile + Offsets.Profile.PlayerInfo;
                    Info = Memory.ReadPtr(localPlayerInfoOffset);
                    MovementContext = Memory.ReadPtr(playerBase + Offsets.Player.MovementContext);
                    TransformInternal = Memory.ReadPtrChain(playerBase, Offsets.Player.To_TransformInternal);
                    _transform = new Transform(TransformInternal, true);
                    var namePtr = Memory.ReadPtr(Info + Offsets.PlayerInfo.Nickname);
                    Name = Memory.ReadUnityString(namePtr);
                    try {
                        var gameVersionPtr = Memory.ReadPtr(Info + Offsets.PlayerInfo.GameVersion);
                        var gameVersion = Memory.ReadUnityString(gameVersionPtr);
                        //If empty, then it's a scav
                        if (gameVersion == "")
                        {
                            Type = PlayerType.AIOfflineScav;
                            IsLocalPlayer = false;
                            IsPmc = false;
                            Name = TransliterateCyrillic(Name);
                            var settings = Memory.ReadPtr(Info + Offsets.PlayerInfo.Settings);
                            var roleFlag = Memory.ReadValue<int>(settings + Offsets.PlayerSettings.Role);
                            // roleflag switch
                            //Console.WriteLine($"RoleFlag: {roleFlag} Name: {Name}");
                            Type = roleFlag switch
                            {
                                0 => PlayerType.AISniperScav,
                                1 => PlayerType.AIOfflineScav,
                                2 => PlayerType.AIBossGuard,
                                17 => PlayerType.AIBoss,
                                28 => PlayerType.AIBoss,
                                7 => PlayerType.AIBoss,
                                _ => PlayerType.AIOfflineScav,
                            };

                        }
                        else
                        {
                            Type = PlayerType.LocalPlayer;
                            IsLocalPlayer = true;
                            IsPmc = true;
                            try { _gearManager = new GearManager(playerBase, true, true); } catch { }
                            GroupID = GetGroupID();
                            
                        }
                    }catch {}
                    
                    

                } else if (baseClassName == "ObservedPlayerView") {
                    IsLocalPlayer = false;
                    //Debug.WriteLine("Processing PMC Player.");
                    var ObservedPlayerView = playerBase;
                    MovementContext = Memory.ReadPtrChain(ObservedPlayerView, Offsets.ObservedPlayerView.To_MovementContext);
                    TransformInternal = Memory.ReadPtrChain(ObservedPlayerView, Offsets.ObservedPlayerView.To_TransformInternal);
                    _transform = new Transform(TransformInternal, true);
                    Name = Memory.ReadUnityString(Memory.ReadPtr(ObservedPlayerView + Offsets.ObservedPlayerView.NickName));
                    var nameForBossCheck = Name;
                    Name = TransliterateCyrillic(Name);
                    Info = ObservedPlayerView;
                    var playerSide = GetNextObservedPlayerSide();
                    var playerIsAI = GetNextObservedPlayerIsAI();
                    // Check corpse ptr
                    if (nameTranslations.ContainsKey(Name))
                    {
                        Name = nameTranslations[Name];
                    }
                    if (playerSide == 1 && playerIsAI == false) {
                        Type = PlayerType.USEC;
                        IsPmc = true;
                        try { _gearManager = new GearManager(playerBase, true, false); } catch { }
                        GroupID = GetObservedPlayerGroupID();
                        
                    }
                    else if (playerSide == 2 && playerIsAI == false)
                    {
                        Type = PlayerType.BEAR;
                        IsPmc = true;
                        try { _gearManager = new GearManager(playerBase, true, false); } catch { }
                        GroupID = GetObservedPlayerGroupID();
                    }
                    else if (playerSide == 4 && playerIsAI == false) {
                        Type = PlayerType.PScav;
                        try { _gearManager = new GearManager(playerBase, true, false); } catch { }
                        GroupID = GetObservedPlayerGroupID();
                    } else if (playerSide == 4 && playerIsAI == true) {
                        //Console.WriteLine("AI Scav");
                        if (nameTranslations.ContainsKey(nameForBossCheck) || nameTranslations.ContainsKey(Name) || Name == "Reshala")
                        {
                            Type = PlayerType.AIBoss;
                        }
                        else {
                            Type = PlayerType.AIScav;
                        }
                    }

                } else throw new ArgumentOutOfRangeException("classNameString");

                FinishAlloc(); // Finish allocation (check watchlist, member type,etc.)
            }
            catch (Exception ex)
            {
                throw new DMAException($"ERROR during Player constructor for base addr 0x{playerBase.ToString("X")}", ex);
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Event fires when the Player Watchlist Textfile is updated.
        /// </summary>
        private static void watchlist_Changed(object sender, FileSystemEventArgs e)
        {
            LoadWatchlist();
        }
        #endregion

        #region Setters
        /// <summary>
        /// Set player health.
        /// </summary>
        public bool SetHealth(object[] obj)
        {
            try
            {
                float totalHealth = 0;
                for (uint i = 0; i < HealthEntries.Length; i++)
                {
                    float health = (float)obj[i]; // unbox
                    totalHealth += health;
                }
                this.Health = (int)Math.Round(totalHealth);
                return true;
            }
            catch (Exception ex)
            {
                Program.Log($"ERROR getting Player '{Name}' Health: {ex}");
                return false;
            }
        }

        /// <summary>
        /// Set player rotation (Direction/Pitch)
        /// </summary>
        public bool SetRotation(object obj)
        {
            try
            {
                Vector2 rotation = (Vector2)obj; // unbox
                Vector2 result;
                rotation.X -= 90; // degs offset
                if (rotation.X < 0) rotation.X += 360f; // handle if neg

                if (rotation.X < 0) result.X = 360f + rotation.X;
                else result.X = rotation.X;
                if (rotation.Y < 0) result.Y = 360f + rotation.Y;
                else result.Y = rotation.Y;
                this.Rotation = result;

                return true;
            }
            catch (Exception ex)
            {
                Program.Log($"ERROR getting Player '{Name}' Rotation: {ex}");
                return false;
            }
        }

        /// <summary>
        /// Set player position (Vector3 X,Y,Z)
        /// </summary>
        public bool SetPosition(object[] obj)
        {
            try
            {
                if (obj is null) throw new NullReferenceException();
                //Debug.WriteLine($"Player '{Name}' Position: {obj[0]}, {obj[1]}, {obj[2]}");
                this.Position = _transform.GetPosition(obj);
                return true;
            }
            catch (Exception ex) // Attempt to re-allocate Transform on error
            {
                Program.Log($"ERROR getting Player '{Name}' Position: {ex}");
                if (!_posRefreshSw.IsRunning) _posRefreshSw.Start();
                else if (_posRefreshSw.ElapsedMilliseconds < 250) // Rate limit attempts on getting pos to prevent stutters
                {
                    return false;
                }
                try
                {
                    Program.Log($"Attempting to get new Transform for Player '{Name}'...");
                    var transform = new Transform(TransformInternal, true);
                    _transform = transform;
                    Program.Log($"Player '{Name}' obtained new Position Transform OK.");
                }
                catch (Exception ex2)
                {
                    Program.Log($"ERROR getting new Transform for Player '{Name}': {ex2}");
                }
                finally { _posRefreshSw.Restart(); }
                return false;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Allocation wrap-up.
        /// </summary>
        /// 
        public static class QuestItemRepository
        {
            private static HashSet<string> questItemIds = new HashSet<string>();

            public static void AddQuestItemId(string itemId)
            {
                questItemIds.Add(itemId);
            }

            public static bool IsQuestItem(string itemId)
            {
                return questItemIds.Contains(itemId);
            }
        }
        
        private void FinishAlloc()
        {
            if (IsHumanHostile) // Hostile Human Controlled Players
            {
                // build log message
                string baseMsg = $"{Name} ({Type}),  L:{Lvl}, "; // append name, type, level
                if (GroupID != -1) baseMsg += $"G:{GroupID}, "; // append group (if in group)
                if (Category is not null)
                {
                    Type = PlayerType.SpecialPlayer; // Flag Special Account Types
                    baseMsg += $"Special Acct: {Category}, "; // append acct type (if special)
                }
                baseMsg += $"@{DateTime.Now.ToLongTimeString()}"; // append timestamp
                if (AccountID is not null &&
                    Watchlist is not null &&
                    Watchlist.TryGetValue(AccountID, out var reason)) // player is on watchlist
                {
                    Type = PlayerType.SpecialPlayer; // Flag watchlist player
                    var entry = new PlayerHistoryEntry(AccountID, $"** WATCHLIST ALERT for {baseMsg} ~~ Reason: {reason}");
                    _history.Push(entry);
                }
                else // Not on watchlist
                {
                    var entry = new PlayerHistoryEntry(AccountID, baseMsg);
                    _history.Push(entry);
                }
            }
        }
        /// <summary>
        /// Checks account type of player. Flags special accounts (Sherpa,etc.)
        /// </summary>
        private string GetMemberCategory()
        {
            var member = Memory.ReadValue<int>(Info + Offsets.PlayerInfo.MemberCategory);
            if (member == 0x0 || member == 0x2) return null; // Ignore 0x0 (Standard Acct) and 0x2 (EOD Acct)
            else
            {
                var flags = (MemberCategory)member;
                return flags.ToString("G"); // Returns all flags that are set
            }
        }

        private int GetNextObservedPlayerSide()
        {
            var side = Memory.ReadValue<int>(Base + Offsets.ObservedPlayerView.PlayerSide);
            return side;
        }

        private bool GetNextObservedPlayerIsAI()
        {
            var isAI = Memory.ReadValue<bool>(Base + Offsets.ObservedPlayerView.IsAI);
            return isAI;
        }



        /// <summary>
        /// Get Account ID for Human-Controlled Players.
        /// </summary>
        private string GetAccountID()
        {
            var idPtr = Memory.ReadPtr(Profile + Offsets.Profile.AccountId);
            var id = Memory.ReadUnityString(idPtr);
            return id;
        }

        /// <summary>
        /// Gets player Level based on XP.
        /// </summary>
        private int GetPlayerLevel()
        {
            var exp = Memory.ReadValue<int>(Info + Offsets.PlayerInfo.Experience);
            //Debug.WriteLine($"Player Experience: {exp}");
            return _expTable.Where(x => x.Key > exp).FirstOrDefault().Value - 1;
        }

        /// <summary>
        /// Gets player's Group Number.
        /// </summary>
        private int GetGroupID()
        {
            try
            {
                var grpPtr = Memory.ReadPtr(Info + Offsets.PlayerInfo.GroupId);
                var grp = Memory.ReadUnityString(grpPtr);
                _groups.TryAdd(grp, _groups.Count);
                return _groups[grp];
            }
            catch { return -1; } // will return null if Solo / Don't have a team
        }
        private int GetObservedPlayerGroupID()
        {
            try
            {
                var grpPtr = Memory.ReadPtr(Info + 0x18);
                var grp = Memory.ReadUnityString(grpPtr);
                _groups.TryAdd(grp, _groups.Count);
                return _groups[grp];
            }
            catch { return -1; } // will return null if Solo / Don't have a team
        }
        /// <summary>
        /// Resets/Updates 'static' assets in preparation for a new game/raid instance.
        /// </summary>
        public static void Reset()
        {
            _groups = new(StringComparer.OrdinalIgnoreCase);
            if (_history.TryPeek(out var last) && last.Entry == "---NEW GAME---") { } // Don't spam repeated entries
            else _history.Push(new PlayerHistoryEntry(null, "---NEW GAME---")); // Insert separator in PMC History Log
        }

        /// <summary>
        /// Reloads playerWatchlist.txt into Memory.
        /// </summary>
        public static void LoadWatchlist()
        {
            lock (_watchlistLock) // Sync access to File IO, Resources
            {
                var watchlist = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase); // Allocate new Dictionary (case insensitive keys)
                if (!File.Exists("playerWatchlist.txt"))
                {
                    File.WriteAllText("playerWatchlist.txt",
                        "PlayerAcctID : Watchlist reason/comment here (one entry per line)");
                }
                else
                {
                    var lines = File.ReadAllLines("playerWatchlist.txt");
                    foreach (var line in lines)
                    {
                        var split = line.Split(':'); // remove single delimiting ':' character
                        if (split.Length == 2)
                        {
                            var id = split[0].Trim();
                            var reason = split[1].Trim();
                            watchlist.TryAdd(id, reason);
                        }
                    }
                }
                Watchlist = new(watchlist); // Update ref
            }
        }
        #endregion

        #region XP Table
        private static readonly Dictionary<int, int> _expTable = new Dictionary<int, int>
        {
            {0, 1},
            {1000, 2},
            {4017, 3},
            {8432, 4},
            {14256, 5},
            {21477, 6},
            {30023, 7},
            {39936, 8},
            {51204, 9},
            {63723, 10},
            {77563, 11},
            {92713, 12},
            {111881, 13},
            {134674, 14},
            {161139, 15},
            {191417, 16},
            {225194, 17},
            {262366, 18},
            {302484, 19},
            {345751, 20},
            {391649, 21},
            {440444, 22},
            {492366, 23},
            {547896, 24},
            {609066, 25},
            {675913, 26},
            {748474, 27},
            {826786, 28},
            {910885, 29},
            {1000809, 30},
            {1096593, 31},
            {1198275, 32},
            {1309251, 33},
            {1429580, 34},
            {1559321, 35},
            {1698532, 36},
            {1847272, 37},
            {2005600, 38},
            {2173575, 39},
            {2351255, 40},
            {2538699, 41},
            {2735966, 42},
            {2946585, 43},
            {3170637, 44},
            {3408202, 45},
            {3659361, 46},
            {3924195, 47},
            {4202784, 48},
            {4495210, 49},
            {4801553, 50},
            {5121894, 51},
            {5456314, 52},
            {5809667, 53},
            {6182063, 54},
            {6573613, 55},
            {6984426, 56},
            {7414613, 57},
            {7864284, 58},
            {8333549, 59},
            {8831052, 60},
            {9360623, 61},
            {9928578, 62},
            {10541848, 63},
            {11206300, 64},
            {11946977, 65},
            {12789143, 66},
            {13820522, 67},
            {15229487, 68},
            {17206065, 69},
            {19706065, 70},
            {22706065, 71},
            {26206065, 72},
            {30206065, 73},
            {34706065, 74},
            {39706065, 75},
        };
        #endregion
    }
}
