using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;

namespace eft_dma_radar
{
    public class LootManager
    {
        private ulong lootlistPtr;
        private ulong lootListEntity;
        private int countLootListObjects;
        private ulong localGameWorld;
        private readonly Config _config;
        private List<(bool Valid, int Index, ulong Pointer)> validLootEntities = new List<(bool Valid, int Index, ulong Pointer)>();
        private List<(bool Valid, int Index, ulong Pointer)> invalidLootEntities = new List<(bool Valid, int Index, ulong Pointer)>();
        private ConcurrentBag<LootContainerInfo> savedLootContainersInfo = new ConcurrentBag<LootContainerInfo>();
        private ConcurrentBag<LootCorpseInfo> savedLootCorpsesInfo = new ConcurrentBag<LootCorpseInfo>();
        private ConcurrentBag<LootItemInfo> savedLootItemsInfo = new ConcurrentBag<LootItemInfo>();
        private List<DevLootItem> lootList = new List<DevLootItem>();
        /// <summary>
        /// Filtered loot ready for display by GUI.
        /// </summary>
        public ReadOnlyCollection<DevLootItem> Filter
        {
            get;
            private set;
        }
        /// <summary>
        /// All tracked loot/corpses in Local Game World.
        /// </summary>
        public ReadOnlyCollection<DevLootItem> Loot
        {
            get; set;
        }

        private Collection<QuestItem> QuestItems
        {
            get => Memory.QuestManager.QuestItems;
        }
        /// <summary>
        /// key,value pair of filtered item ids (key) and their filtered color (value)
        /// </summary>
        public Dictionary<string, LootFilter.Colors> LootFilterColors { get; private set; }
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LootManager"/> class.
        /// </summary>
        /// <param name="localGameWorld"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        ///
        
        public LootManager(ulong localGameWorld)
        {
            _config = Program.Config;
            this.localGameWorld = localGameWorld;
            RefreshLootListAddresses();
            new Thread((ThreadStart)delegate
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                //Console.WriteLine("[LootManager] Refresh thread started.");
                while (Memory.InGame)
                {
                    stopwatch.Restart();
                    Console.WriteLine("[LootManager] Refreshing loot...");
                    GetLootList();
                    GetLoot();
                    FillLoot();
                    ApplyFilter();
                    Console.WriteLine($"[LootManager] Refreshed loot in {stopwatch.ElapsedMilliseconds}ms.");
                    Thread.Sleep(2500);
                    //Thread.Sleep(10000);

                }
                //Console.WriteLine("[LootManager] Refresh thread stopped.");
            })
            {
                Priority = ThreadPriority.BelowNormal,
                IsBackground = true
            }.Start();
            
        }
        #endregion

        #region Methods
        private void GetLootList()
        {
            RefreshLootListAddresses();
            if (countLootListObjects < 0 || countLootListObjects > 4096) throw new ArgumentOutOfRangeException("countLootListObjects"); // Loot list sanity check
            var scatterMap = new ScatterReadMap(countLootListObjects);
            var round1 = scatterMap.AddRound();

            var lootEntitiesWithIndex = new List<(bool Valid, int Index, ulong Pointer)>();

            for (int i = 0; i < countLootListObjects; i++)
            {
                var p1 = round1.AddEntry<ulong>(i, 0, lootListEntity + Offsets.UnityListBase.Start + (uint)(i * 0x8));
            }
            scatterMap.Execute();
            for (int i = 0; i < countLootListObjects; i++)
            {
                try
                {
                    var result1 = scatterMap.Results[i][0].TryGetResult<ulong>(out var lootObjectsEntity);
                    if (lootObjectsEntity == 0x0)
                    {
                        lootEntitiesWithIndex.Add((false, i, lootListEntity + Offsets.UnityListBase.Start + (uint)(i * 0x8)));
                    }
                    else {
                        lootEntitiesWithIndex.Add((true, i, lootObjectsEntity));
                    }
                    
                }catch
                {
                    Program.Log($"Error reading loot item {i}");
                }
            }
            
            lootEntitiesWithIndex = lootEntitiesWithIndex.OrderBy(x => x.Index).ToList();
            validLootEntities = lootEntitiesWithIndex.Where(x => x.Valid).ToList();
            invalidLootEntities = lootEntitiesWithIndex.Where(x => !x.Valid).ToList();
        }

        private void GetLoot()
        {
            //test first 50 failed loot entities
            var scatterMap = new ScatterReadMap(invalidLootEntities.Count);
            var round1 = scatterMap.AddRound();
            for (int i = 0; i < invalidLootEntities.Count; i++)
            {
                var p1 = round1.AddEntry<ulong>(i, 0, invalidLootEntities[i].Pointer);
            }
            scatterMap.Execute();
            for (int i = 0; i < invalidLootEntities.Count; i++)
            {
                try
                {
                    var result1 = scatterMap.Results[i][0].TryGetResult<ulong>(out var lootObjectsEntity);
                    if (lootObjectsEntity != 0x0)
                    {
                        //Remove from invalid list
                        validLootEntities.Add((true, invalidLootEntities[i].Index, lootObjectsEntity));
                        invalidLootEntities.RemoveAt(i);
                    }
                }
                catch
                {
                    Program.Log($"Error reading loot item {i}");
                }
            }
            //check all valid loot entities
            var validScatterCheckMap = new ScatterReadMap(validLootEntities.Count);
            var validCheckRound1 = validScatterCheckMap.AddRound();
            for (int i = 0; i < validLootEntities.Count; i++)
            {
                var lootUnknownPtr = validCheckRound1.AddEntry<ulong>(i, 0, validLootEntities[i].Pointer, null);
            }
            validScatterCheckMap.Execute();
            for (int i = 0; i < validLootEntities.Count; i++)
            {
                try
                {
                    var result1 = validScatterCheckMap.Results[i][0].TryGetResult<ulong>(out var lootUnknownPtr);
                    if (lootUnknownPtr == 0x0)
                    {
                        invalidLootEntities.Add((true, validLootEntities[i].Index, validLootEntities[i].Pointer));
                        //Console.WriteLine($"Invalid loot entity {validLootEntities[i].Index}");
                        validLootEntities.RemoveAt(i);
                    }
                }
                catch
                {
                    Program.Log($"Error reading loot item {i}");
                }
            }
            var validScatterMap = new ScatterReadMap(validLootEntities.Count);
            var validRound1 = validScatterMap.AddRound();
            var validRound2 = validScatterMap.AddRound();
            var validRound3 = validScatterMap.AddRound();
            var validRound4 = validScatterMap.AddRound();
            var validRound5 = validScatterMap.AddRound();
            var validRound6 = validScatterMap.AddRound();
            var validRound7 = validScatterMap.AddRound();
            var validRound8 = validScatterMap.AddRound();
            var validRound9 = validScatterMap.AddRound();
            var validRound10 = validScatterMap.AddRound();
            var validRound11 = validScatterMap.AddRound();
            var validRound12 = validScatterMap.AddRound();
            var validRound13 = validScatterMap.AddRound();

            for (int i = 0; i < validLootEntities.Count; i++)
            {
                var lootUnknownPtr = validRound1.AddEntry<ulong>(i, 0, validLootEntities[i].Pointer, null, Offsets.LootListItem.LootUnknownPtr);
                var interactiveClass = validRound2.AddEntry<ulong>(i, 1, lootUnknownPtr, null, Offsets.LootUnknownPtr.LootInteractiveClass);
                var lootBaseObject = validRound3.AddEntry<ulong>(i, 2, interactiveClass, null, Offsets.LootInteractiveClass.LootBaseObject);
                var gameObject = validRound4.AddEntry<ulong>(i, 3, lootBaseObject, null, Offsets.LootBaseObject.GameObject);

                var objectName = validRound5.AddEntry<ulong>(i, 4, gameObject, null, Offsets.GameObject.ObjectName);
                var entry7 = validRound5.AddEntry<ulong>(i, 7, interactiveClass, null, 0x0);
                var objectClass = validRound5.AddEntry<ulong>(i, 11, gameObject, null, Offsets.GameObject.ObjectClass);

                var transformOne = validRound6.AddEntry<ulong>(i, 12, objectClass, null, Offsets.LootGameObjectClass.To_TransformInternal[0]);
                var containerName = validRound6.AddEntry<string>(i, 6, objectName, 64);
                var entry9 = validRound6.AddEntry<ulong>(i, 8, entry7, null, 0x0);

                var transformTwo = validRound7.AddEntry<ulong>(i, 13, transformOne, null, Offsets.LootGameObjectClass.To_TransformInternal[1]);
                var entry10 = validRound7.AddEntry<ulong>(i, 9, entry9, null, 0x48);

                var position = validRound8.AddEntry<ulong>(i, 14, transformTwo, null, Offsets.LootGameObjectClass.To_TransformInternal[2]);
                var className = validRound8.AddEntry<string>(i, 10, entry10, 64);

                //LootableContainer / ObservedLootItem / ObservedCorpse
                var item = validRound3.AddEntry<ulong>(i, 15, interactiveClass, null, 0xB0);
                var itemTemplate = validRound9.AddEntry<ulong>(i, 16, item, null, Offsets.LootItemBase.ItemTemplate);
                var isQuestItem = validRound10.AddEntry<bool>(i, 17, itemTemplate, null, Offsets.ItemTemplate.IsQuestItem);
                var BSGIdPtr = validRound11.AddEntry<ulong>(i, 18, itemTemplate, null, Offsets.ItemTemplate.BsgId);
                var containerIDPtr = validRound3.AddEntry<ulong>(i, 19, interactiveClass, null, 0x128);
                var itemOwner = validRound3.AddEntry<ulong>(i, 20, item, null, 0x40);
                var rootItem = validRound3.AddEntry<ulong>(i, 21, itemOwner, null, 0xC0);
                var slots = validRound3.AddEntry<ulong>(i, 22, rootItem, null, 0x78);
                var containerItemOwner = validRound3.AddEntry<ulong>(i, 23, interactiveClass, null, Offsets.LootInteractiveClass.ContainerItemOwner);
                var containerItemBase = validRound12.AddEntry<ulong>(i, 24, containerItemOwner, null, 0xC0);
                var containerGrids = validRound13.AddEntry<ulong>(i, 25, containerItemBase, null, Offsets.LootItemBase.Grids);

            }

            validScatterMap.Execute();
            Parallel.For(0, validLootEntities.Count, i =>
            {
                try
                {
                    var result2 = validScatterMap.Results[i][1].TryGetResult<ulong>(out var lootInteractiveClass);
                    if (!result2)
                        return;
                    var result3 = validScatterMap.Results[i][2].TryGetResult<ulong>(out var lootBaseObject);
                    if (!result3)
                        return;
                    var result4 = validScatterMap.Results[i][3].TryGetResult<ulong>(out var gameObject);
                    var result5 = validScatterMap.Results[i][4].TryGetResult<ulong>(out var objectName);
                    var result6 = validScatterMap.Results[i][6].TryGetResult<string>(out var name);
                    var result7 = validScatterMap.Results[i][10].TryGetResult<string>(out var className);
                    var result8 = validScatterMap.Results[i][14].TryGetResult<ulong>(out var transformInternal);
                    var result9 = validScatterMap.Results[i][7].TryGetResult<ulong>(out var objectClass);
                    if (className == "ObservedLootItem" || className == "Corpse")
                    {
                        if (!savedLootItemsInfo.Any(x => x.LootInteractiveClass == lootInteractiveClass))
                        {
                            var result10 = validScatterMap.Results[i][15].TryGetResult<ulong>(out var item);
                            if (!result10)
                                return;
                            var result11 = validScatterMap.Results[i][16].TryGetResult<ulong>(out var itemTemplate);
                            if (!result11)
                                return;
                            var result12 = validScatterMap.Results[i][17].TryGetResult<bool>(out var questItem);
                            var pos = new Transform(transformInternal).GetPosition();
                            var result13 = validScatterMap.Results[i][18].TryGetResult<ulong>(out var BSGIdPtr);
                            if (!result13)
                                return;
                            var id = Memory.ReadUnityString(BSGIdPtr);
                            if (id == null)
                                return;
                            savedLootItemsInfo.Add(new LootItemInfo(lootInteractiveClass, item, itemTemplate, questItem, pos, id));
                        }
                    }
                    if (className == "LootableContainer")
                    {
                        if (!savedLootContainersInfo.Any(x => x.LootInteractiveClass == lootInteractiveClass))
                        {
                            var pos = new Transform(transformInternal).GetPosition();
                            var result14 = validScatterMap.Results[i][19].TryGetResult<ulong>(out var containerIDPtr);
                            if (!result14)
                                return;
                            var containerID = Memory.ReadUnityString(containerIDPtr);
                            var result15 = validScatterMap.Results[i][25].TryGetResult<ulong>(out var containerGrids);
                            TarkovDevAPIManager.AllLootContainers.TryGetValue(containerID, out var container);
                            savedLootContainersInfo.Add(new LootContainerInfo(lootInteractiveClass, objectClass, name, pos, containerID, container?.Name ?? name, containerGrids));
                        }

                    }
                    else if (className == "ObservedCorpse")
                    {
                        if (!savedLootCorpsesInfo.Any(x => x.LootInteractiveClass == lootInteractiveClass))
                        {
                            var result15 = validScatterMap.Results[i][20].TryGetResult<ulong>(out var itemOwner);
                            if (!result15)
                                return;
                            var result16 = validScatterMap.Results[i][21].TryGetResult<ulong>(out var rootItem);
                            if (!result16)
                                return;
                            var result17 = validScatterMap.Results[i][22].TryGetResult<ulong>(out var slots);
                            if (!result17)
                                return;
                            var pos = new Transform(transformInternal).GetPosition();
                            savedLootCorpsesInfo.Add(new LootCorpseInfo(lootInteractiveClass, itemOwner, rootItem, slots, pos));
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                catch
                {
                    Program.Log($"Error reading loot item {i}");
                }
            });
        }

        private void FillLoot()
        {
            var loot = new List<DevLootItem>();
            foreach (var item in savedLootItemsInfo)
            {
                if (validLootEntities.Any(x => x.Pointer == item.LootInteractiveClass))
                {
                    if (!item.QuestItem)
                    {
                        if (TarkovDevAPIManager.AllItems.TryGetValue(item.ItemID, out var entry))
                        {
                            loot.Add(new DevLootItem
                            {
                                Label = entry.Label,
                                AlwaysShow = entry.AlwaysShow,
                                Important = entry.Important,
                                Position = item.Pos,
                                Item = entry.Item,
                            });
                        }
                        else {
                            Console.WriteLine($"[LootManager] Item {item.ItemID} not found in API.");
                            //55d7217a4bdc2d86028b456d = corpse
                        }
                    }
                    else {
                        var questItemTest = QuestItems.Where(x => x.Id == item.ItemID).FirstOrDefault();
                        if (questItemTest != null)
                        {
                            questItemTest.Position = item.Pos;
                        }
                    }
                }
                else {
                    //remove from saved itemlist
                    savedLootItemsInfo = new ConcurrentBag<LootItemInfo>(savedLootItemsInfo.Where(x => x.LootInteractiveClass != item.LootInteractiveClass));
                }
            }
            foreach (var container in savedLootContainersInfo)
            {
                TarkovDevAPIManager.AllLootContainers.TryGetValue(container.ContainerID, out var MarketContainer);
                if (MarketContainer != null)
                {
                    try
                    {
                        GetItemsInGrid(container.ContainerGrids, container.Name, container.Pos, loot, true, container.ContainerName);
                    }
                    catch { }
                }
                else
                {
                    Console.WriteLine($"[LootManager] Container {container.Name} not found in API.");
                }
            }
            Loot = new ReadOnlyCollection<DevLootItem>(loot);
        }


        private void RefreshLootListAddresses()
        {
            lootlistPtr = Memory.ReadPtr(localGameWorld + Offsets.LocalGameWorld.LootList);
            lootListEntity = Memory.ReadPtr(lootlistPtr + Offsets.UnityList.Base);
            countLootListObjects = Memory.ReadValue<int>(lootListEntity + Offsets.UnityList.Count);
        }
        /// <summary>
        /// Applies loot filter
        /// </summary>
        public void ApplyFilter()
        {
            var loot = this.Loot;
            if (loot == null)
            {
                return;
            }
            var activeFilters = _config.Filters.Where(f => f.IsActive).ToList();
            var minValueLootItems = loot?.Where(x => x.AlwaysShow || TarkovDevAPIManager.GetItemValue(x.Item) > _config.MinLootValue).ToList();

            var itemsWithData = activeFilters.SelectMany(f => f.Items)
                .Distinct()
                .Select(item => new {
                    ItemId = item,
                    Filter = activeFilters
                        .Where(f => f.Items.Contains(item))
                        .OrderBy(f => f.Order)
                        .First()
                });

            var orderedItems = itemsWithData
               .OrderBy(x => x.Filter.Order)
               .Select(x => new {
                   x.ItemId,
                   x.Filter.Color
               })
               .ToList();

            var orderedIds = orderedItems.Select(x => x.ItemId).ToList();

            //ghetto way to prevent overriding DevLootItems in the original loot list
            var lootCopy = loot?.Select(l => new DevLootItem
            {
                Label = l.Label,
                Important = l.Important,
                Position = l.Position,
                AlwaysShow = l.AlwaysShow,
                BsgId = l.BsgId,
                ContainerName = l.ContainerName,
                Container = l.Container,
                Item = l.Item
            }).ToList();

            var filteredLoot = from l in lootCopy
                               join id in orderedItems on l.Item.id equals id.ItemId
                               select l;

            // ghetto quickfix lmao
            filteredLoot = filteredLoot.ToList();

            foreach (var lootItem in filteredLoot)
            {
                lootItem.Important = true;
            }

            foreach (var lootItem in minValueLootItems)
            {
                if (TarkovDevAPIManager.GetItemValue(lootItem.Item) >= _config.MinImportantLootValue)
                {
                    lootItem.Important = true;
                }
            }

            filteredLoot = filteredLoot.Union(minValueLootItems)
                .GroupBy(x => x.Position)
                .Select(g => g.OrderBy(x => {
                    var match = orderedItems.FirstOrDefault(oi => oi.ItemId == x.Item.id);
                    return match == null ? int.MaxValue : orderedItems.IndexOf(match);
                })
                .First())
                .OrderBy(x => {
                    var match = orderedItems.FirstOrDefault(oi => oi.ItemId == x.Item.id);
                    return match == null ? int.MaxValue : orderedItems.IndexOf(match);
                });

            this.LootFilterColors = orderedItems.ToDictionary(item => item.ItemId, item => item.Color);
            this.Filter = new ReadOnlyCollection<DevLootItem>(filteredLoot.ToList());
        }

        /// <summary>
        /// Removes an item from the loot filter list
        /// </summary>
        /// <param name="itemToRemove">The item to remove</param>
        public void RemoveFilterItem(DevLootItem itemToRemove)
        {
            var filter = this.Filter.ToList();
            filter.Remove(itemToRemove);

            this.Filter = new ReadOnlyCollection<DevLootItem>(new List<DevLootItem>(filter));
            this.ApplyFilter();
        }

        internal class GridState
        {
            public int ItemCount { get; set; }
            public ulong GridPointer { get; set; }
            public HashSet<string> ItemIdentifiers { get; set; } = new HashSet<string>();
        }

        private ConcurrentDictionary<ulong, GridState> gridCache = new ConcurrentDictionary<ulong, GridState>();

        private void GetItemsInGrid(ulong gridsArrayPtr, string id, Vector3 pos, List<DevLootItem> loot, bool isContainer = false, string containerName = "")
        {
            if (TarkovDevAPIManager.AllItems.TryGetValue(id, out var entry))
            {
                loot.Add(new DevLootItem
                {
                    Label = entry.Label,
                    AlwaysShow = entry.AlwaysShow,
                    Important = entry.Important,
                    Position = pos,
                    Item = entry.Item,
                    Container = isContainer,
                    ContainerName = containerName,
                });
            }

            if (gridsArrayPtr == 0x0)
            {
                return;
            }

            var gridsArray = new MemArray(gridsArrayPtr);
            if (gridsArray.Count == 0)
            {
                return;
            }

            HashSet<string> currentItemsIdentifiers = new HashSet<string>();

            bool shouldProcessGrid = true;
            if (gridCache.TryGetValue(gridsArrayPtr, out var cachedState))
            {
                foreach (var grid in gridsArray.Data)
                {
                    var gridEnumerableClass = Memory.ReadPtr(grid + Offsets.Grids.GridsEnumerableClass);
                    var itemListPtr = Memory.ReadPtr(gridEnumerableClass + 0x18);
                    var itemList = new MemList(itemListPtr);
                    
                    foreach (var childItem in itemList.Data)
                    {
                        var childItemTemplate = Memory.ReadPtr(childItem + Offsets.LootItemBase.ItemTemplate);
                        var childItemIdPtr = Memory.ReadPtr(childItemTemplate + Offsets.ItemTemplate.BsgId);
                        var childItemIdStr = Memory.ReadUnityString(childItemIdPtr).Replace("\\0", "");
                        currentItemsIdentifiers.Add(childItemIdStr);
                    }
                }
                shouldProcessGrid = !currentItemsIdentifiers.SetEquals(cachedState.ItemIdentifiers);
            }

            if (!shouldProcessGrid)
            {
                return;
            }
            if (shouldProcessGrid)
            {
                foreach (var grid in gridsArray.Data)
                {
                    var gridEnumerableClass = Memory.ReadPtr(grid + Offsets.Grids.GridsEnumerableClass); 
                    var itemListPtr = Memory.ReadPtr(gridEnumerableClass + 0x18);
                    var itemList = new MemList(itemListPtr);
                    
                    foreach (var childItem in itemList.Data)
                    {
                        try
                        {
                            var childItemTemplate = Memory.ReadPtr(childItem + Offsets.LootItemBase.ItemTemplate);
                            var childItemIdPtr = Memory.ReadPtr(childItemTemplate + Offsets.ItemTemplate.BsgId); 
                            var childItemIdStr = Memory.ReadUnityString(childItemIdPtr).Replace("\\0", "");
                            var childGridsArrayPtr = Memory.ReadPtrNullable(childItem + Offsets.LootItemBase.Grids); 
                            GetItemsInGrid(childGridsArrayPtr, childItemIdStr, pos, loot, true, containerName);
                        }
                        catch
                        {
                        }
                    }
                }
            }

            gridCache[gridsArrayPtr] = new GridState
            {
                ItemCount = gridsArray.Count,
                GridPointer = gridsArrayPtr,
                ItemIdentifiers = currentItemsIdentifiers
            };
        }
        #endregion
    }

    #region Classes
    //Helper class or struct

    public class MemArray
    {
        public ulong Address
        {
            get;
        }
        public int Count
        {
            get;
        }
        public ulong[] Data
        {
            get;
        }

        public MemArray(ulong address)
        {
            var type = typeof(ulong);

            Address = address;
            Count = Memory.ReadValue<int>(address + Offsets.UnityList.Count);
            var arrayBase = address + Offsets.UnityListBase.Start;
            var tSize = (uint)Marshal.SizeOf(type);

            // Rudimentary sanity check
            if (Count > 4096 || Count < 0)
                Count = 0;

            var retArray = new ulong[Count];
            var buf = Memory.ReadBuffer(arrayBase, Count * (int)tSize);

            for (uint i = 0; i < Count; i++)
            {
                var index = i * tSize;
                var t = MemoryMarshal.Read<ulong>(buf.Slice((int)index, (int)tSize));
                if (t == 0x0) throw new NullPtrException();
                retArray[i] = t;
            }

            Data = retArray;
        }
    }

    //Helper class or struct
    public class MemList
    {
        public ulong Address
        {
            get;
        }

        public int Count
        {
            get;
        }

        public List<ulong> Data
        {
            get;
        }

        public MemList(ulong address)
        {
            var type = typeof(ulong);

            Address = address;
            Count = Memory.ReadValue<int>(address + Offsets.UnityList.Count);

            if (Count > 4096 || Count < 0)
                Count = 0;

            var arrayBase = Memory.ReadPtr(address + Offsets.UnityList.Base) + Offsets.UnityListBase.Start;
            var tSize = (uint)Marshal.SizeOf(type);
            var retList = new List<ulong>(Count);
            var buf = Memory.ReadBuffer(arrayBase, Count * (int)tSize);

            for (uint i = 0; i < Count; i++)
            {
                var index = i * tSize;
                var t = MemoryMarshal.Read<ulong>(buf.Slice((int)index, (int)tSize));
                if (t == 0x0) throw new NullPtrException();
                retList.Add(t);
            }

            Data = retList;
        }
    }

    public class DevLootItem
    {
        public string Label
        {
            get;
            init;
        }
        public bool Important
        {
            get;
            set;
        } = false;
        public Vector3 Position
        {
            get;
            init;
        }
        public bool AlwaysShow
        {
            get;
            init;
        } = false;
        public string BsgId
        {
            get;
            init;
        }
        public bool Container
        {
            get;
            init;
        } = false;
        public string ContainerName
        {
            get;
            init;
        }
        public TarkovItem Item
        {
            get;
            init;
        } = new();

        /// <summary>
        /// Cached 'Zoomed Position' on the Radar GUI. Used for mouseover events.
        /// </summary>
        public Vector2 ZoomedPosition { get; set; } = new();

        /// <summary>
        /// Gets the formatted the items value
        /// </summary>
        public string GetFormattedValue()
        {
            return TarkovDevAPIManager.FormatNumber(TarkovDevAPIManager.GetItemValue(this.Item));
        }

        /// <summary>
        /// Gets the formatted item value + name
        /// </summary>
        public string GetFormattedValueName()
        {
            return (this.AlwaysShow || this.Item.shortName is not null) ? $"[{this.GetFormattedValue()}] {this.Item.name}" : "null";
        }

        /// <summary>
        /// Gets the formatted item value + name
        /// </summary>
        public string GetFormattedValueShortName()
        {
            return (this.AlwaysShow || this.Item.shortName is not null) ? $"[{this.GetFormattedValue()}] {this.Item.shortName}" : "null";
        }
    }

    public class LootContainers
    {
        public string Name
        {
            get;
            init;
        }
        public string ID
        {
            get;
            init;
        }
        public string NormalizedName
        {
            get;
            init;
        }
    }

    /// <summary>
    /// Class to help handle filter lists/profiles for the loot filter
    /// </summary>
    public class LootFilter
    {
        public List<string>? Items { get; set; }
        public Colors Color { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }

        public struct Colors
        {
            public byte A { get; set; }
            public byte R { get; set; }
            public byte G { get; set; }
            public byte B { get; set; }
        }
    }

    struct LootItemInfo
    {
        public ulong LootInteractiveClass;
        public ulong Item;
        public ulong ItemTemplate;
        public bool QuestItem;
        public Vector3 Pos;
        public string ItemID;

        public LootItemInfo(ulong lootInteractiveClass, ulong item, ulong itemTemplate, bool questItem, Vector3 pos, string itemID)
        {
            LootInteractiveClass = lootInteractiveClass;
            Item = item;
            ItemTemplate = itemTemplate;
            QuestItem = questItem;
            Pos = pos;
            ItemID = itemID;
        }
    }

    struct LootContainerInfo
        {
            public ulong LootInteractiveClass;
            public ulong ObjectClass;
            public string Name;
            public Vector3 Pos;
            public string ContainerID;
            public string ContainerName;
            public ulong ContainerGrids;
            public LootContainerInfo(ulong lootInteractiveClass, ulong objectClass, string name, Vector3 pos, string containerID, string containerName, ulong containerGrids)
            {
                LootInteractiveClass = lootInteractiveClass;
                ObjectClass = objectClass;
                Name = name;
                Pos = pos;
                ContainerID = containerID;
                ContainerName = containerName;
                ContainerGrids = containerGrids;
            }
        }

    struct LootCorpseInfo
        {
            public ulong LootInteractiveClass;
            public ulong RootItem;
            public ulong ItemOwner;
            public ulong Slots;
            public Vector3 Pos;

            public LootCorpseInfo(ulong lootInteractiveClass, ulong rootItem, ulong itemOwner, ulong slots, Vector3 pos)
            {
                LootInteractiveClass = lootInteractiveClass;
                RootItem = rootItem;
                ItemOwner = itemOwner;
                Slots = slots;
                Pos = pos;
            }
        }
        
    #endregion
}