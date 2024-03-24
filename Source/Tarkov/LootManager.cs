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
        private ConcurrentBag<ulong> savedLootItems = new ConcurrentBag<ulong>();
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
            //GetLootList(); //works for offline
            new Thread((ThreadStart)delegate
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                //Console.WriteLine("[LootManager] Refresh thread started.");
                while (Memory.InGame)
                {
                    stopwatch.Restart();
                    //Console.WriteLine("[LootManager] Refreshing loot...");
                    GetLootList(); //fix!!!
                    GetLoot();
                    ApplyFilter();
                    //Console.WriteLine($"[LootManager] Refreshed loot in {stopwatch.ElapsedMilliseconds}ms.");
                    Thread.Sleep(10000);

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
            var loot = new List<DevLootItem>();
            var validScatterMap = new ScatterReadMap(validLootEntities.Count);
            var validRound1 = validScatterMap.AddRound();
            var validRound2 = validScatterMap.AddRound();
            var validRound3 = validScatterMap.AddRound();
            var validRound4 = validScatterMap.AddRound();
            var validRound5 = validScatterMap.AddRound();
            var validRound6 = validScatterMap.AddRound();
            var validRound7 = validScatterMap.AddRound();
            var validRound8 = validScatterMap.AddRound();
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
            }

            validScatterMap.Execute();
            Parallel.For(0, validLootEntities.Count, i =>
            {
                try
                {
                    var result2 = validScatterMap.Results[i][1].TryGetResult<ulong>(out var lootInteractiveClass);
                    var result3 = validScatterMap.Results[i][2].TryGetResult<ulong>(out var lootBaseObject);
                    var result4 = validScatterMap.Results[i][3].TryGetResult<ulong>(out var gameObject);
                    var result5 = validScatterMap.Results[i][4].TryGetResult<ulong>(out var objectName);
                    var result6 = validScatterMap.Results[i][6].TryGetResult<string>(out var name);
                    var result7 = validScatterMap.Results[i][10].TryGetResult<string>(out var className);
                    var result8 = validScatterMap.Results[i][14].TryGetResult<ulong>(out var transformInternal);
                    var result9 = validScatterMap.Results[i][7].TryGetResult<ulong>(out var objectClass);
                    if (className == "LootableContainer")
                    {
                        if (!savedLootContainersInfo.Any(x => x.LootInteractiveClass == lootInteractiveClass))
                        {
                            var pos = new Transform(transformInternal).GetPosition();
                            var containerIDPtr = Memory.ReadPtr(lootInteractiveClass + 0x128); //[118] Template : String
                            var containerID = Memory.ReadUnityString(containerIDPtr);
                            TarkovDevAPIManager.AllLootContainers.TryGetValue(containerID, out var container);
                            savedLootContainersInfo.Add(new LootContainerInfo(lootInteractiveClass, objectClass, name, pos, containerID, container?.Name ?? name));
                        }

                    }
                    else if (className == "ObservedLootItem")
                    {
                        //This will add items at first run but deletes them after first run so need to fix that
                        //if (!savedLootItems.Contains(lootInteractiveClass))
                        //{
                            var item = Memory.ReadPtr(lootInteractiveClass + 0xB0); //EFT.InventoryLogic.Item
                            var itemTemplate = Memory.ReadPtr(item + Offsets.LootItemBase.ItemTemplate); //EFT.InventoryLogic.ItemTemplate
                            bool questItem = Memory.ReadValue<bool>(itemTemplate + Offsets.ItemTemplate.IsQuestItem);
                            var pos = new Transform(transformInternal).GetPosition();
                            var BSGIdPtr = Memory.ReadPtr(itemTemplate + Offsets.ItemTemplate.BsgId);
                            var id = Memory.ReadUnityString(BSGIdPtr);
                            if (id == null) return;
                            if (!questItem)
                            {
                                if (TarkovDevAPIManager.AllItems.TryGetValue(id, out
                                        var entry))
                                {
                                    loot.Add(new DevLootItem
                                    {
                                        Label = entry.Label,
                                        AlwaysShow = entry.AlwaysShow,
                                        Important = entry.Important,
                                        Position = pos,
                                        Item = entry.Item
                                    });
                                }
                                savedLootItems.Add(lootInteractiveClass);
                            }
                            else
                            {
                                var questItemTest = this.QuestItems.Where(x => x.Id == id).FirstOrDefault();
                                if (questItemTest != null)
                                {
                                    //update position
                                    questItemTest.Position = pos;
                                }
                            }
                        //}
                        
                    }
                    else if (className == "ObservedCorpse")
                    {
                        if (!savedLootCorpsesInfo.Any(x => x.LootInteractiveClass == lootInteractiveClass))
                        {
                            var itemOwner = Memory.ReadPtr(lootInteractiveClass + 0x40); //[40] ItemOwner : -.GClass24D0
                            var rootItem = Memory.ReadPtr(itemOwner + 0xC0); //[C0] item_0xC0 : EFT.InventoryLogic.Item
                            var slots = Memory.ReadPtr(rootItem + 0x78);
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

            //@Keeegi - Not best method but it works for now
            //Fix this later!!!
            foreach (var container in savedLootContainersInfo)
            {
                var itemOwner = Memory.ReadPtr(container.LootInteractiveClass + Offsets.LootInteractiveClass.ContainerItemOwner);
                var itemBase = Memory.ReadPtr(itemOwner + 0xC0); //Offsets.ContainerItemOwner.LootItemBase);
                var grids = Memory.ReadPtr(itemBase + Offsets.LootItemBase.Grids);
                GetItemsInGrid(grids, container.Name, container.Pos, loot, true, container.ContainerName, container.Name);
            }
            //This adds about 10-30s to the refresh time so need to fix this but it probably includes grid function rework
            foreach (var corpse in savedLootCorpsesInfo)
            {
                var slotsArray = new MemArray(corpse.Slots);
                foreach (var slot in slotsArray.Data)
                {
                    try
                    {
                        var namePtr = Memory.ReadPtr(slot + Offsets.Slot.Name);
                        var slotName = Memory.ReadUnityString(namePtr);
                        var containedItem = Memory.ReadPtr(slot + 0x40);
                        if (containedItem == 0x0)
                        {
                            continue;
                        }
                        if (slotName == "SecuredContainer")
                        {
                            continue;
                        }
                        var itemTemplate = Memory.ReadPtr(containedItem + Offsets.LootItemBase.ItemTemplate); //EFT.InventoryLogic.ItemTemplate
                        var BSGIdPtr = Memory.ReadPtr(itemTemplate + Offsets.ItemTemplate.BsgId);
                        var id = Memory.ReadUnityString(BSGIdPtr);
                        var corpseItemNamePtr = Memory.ReadPtr(itemTemplate + 0x58);
                        var corpseItemName = Memory.ReadUnityString(corpseItemNamePtr);
                        var grids = Memory.ReadPtr(containedItem + Offsets.LootItemBase.Grids);
                        //var containerName = slotName;
                        var containerName = "Corpse";
                        if (grids == 0x0)
                        {
                            //The loot item we found does not have any grids so it's weapon slot?
                            if (TarkovDevAPIManager.AllItems.TryGetValue(id, out var entry))
                            {
                                loot.Add(
                                    new DevLootItem
                                    {
                                        Label = entry.Label,
                                        AlwaysShow = entry.AlwaysShow,
                                        Important = entry.Important,
                                        Position = corpse.Pos,
                                        Item = entry.Item,
                                        Container = true,
                                        ContainerName = containerName
                                    }
                                );
                            }
                        }
                        ;
                        GetItemsInGrid(grids, id, corpse.Pos, loot, true, containerName);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            this.Loot = new ReadOnlyCollection<DevLootItem>(loot);
        }

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

        ///This method recursively searches grids. Grids work as follows:
        ///Take a Groundcache which holds a Blackrock which holds a pistol.
        ///The Groundcache will have 1 grid array, this method searches for whats inside that grid.
        ///Then it finds a Blackrock. This method then invokes itself recursively for the Blackrock.
        ///The Blackrock has 11 grid arrays (not to be confused with slots!! - a grid array contains slots. Look at the blackrock and you'll see it has 20 slots but 11 grids).
        ///In one of those grid arrays is a pistol. This method would recursively search through each item it finds
        ///To Do: add slot logic, so we can recursively search through the pistols slots...maybe it has a high value scope or something.
        private void GetItemsInGrid(ulong gridsArrayPtr, string id, Vector3 pos, List<DevLootItem> loot, bool isContainer = false, string containerName = "", string realContainerName = "")
        {
            //write console which item is in which container
            //Console.WriteLine($"{id} in {containerName} - {realContainerName}");

            if (TarkovDevAPIManager.AllItems.TryGetValue(id, out
                    var entry))
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

            // Check all sections of the container
            foreach (var grid in gridsArray.Data)
            {
                var gridEnumerableClass = Memory.ReadPtr(grid + Offsets.Grids.GridsEnumerableClass); // -.GClass178A->gClass1797_0x40 // Offset: 0x0040 (Type: -.GClass1797)
                var itemListPtr = Memory.ReadPtr(gridEnumerableClass + 0x18); // -.GClass1797->list_0x18 // Offset: 0x0018 (Type: System.Collections.Generic.List<Item>)
                var itemList = new MemList(itemListPtr);

                foreach (var childItem in itemList.Data)
                {
                    try
                    {
                        var childItemTemplate = Memory.ReadPtr(childItem + Offsets.LootItemBase.ItemTemplate); // EFT.InventoryLogic.Item->_template // Offset: 0x0038 (Type: EFT.InventoryLogic.ItemTemplate)
                        var childItemIdPtr = Memory.ReadPtr(childItemTemplate + Offsets.ItemTemplate.BsgId);
                        var childItemIdStr = Memory.ReadUnityString(childItemIdPtr).Replace("\\0", "");
                        //Set important and always show if quest item using ID
                        // Check to see if the child item has children
                        var childGridsArrayPtr = Memory.ReadPtrNullable(childItem + Offsets.LootItemBase.Grids); // -.GClassXXXX->Grids // Offset: 0x0068 (Type: -.GClass1497[])
                        GetItemsInGrid(childGridsArrayPtr, childItemIdStr, pos, loot, true, containerName, realContainerName); // Recursively add children to the entity
                    }
                    catch
                    {
                        //Program.Log("Error reading child item");
                        //Program.Log($"Child item: {childItem} in {id}");
                    }
                }
            }
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

    struct LootContainerInfo
        {
            public ulong LootInteractiveClass;
            public ulong ObjectClass;
            public string Name;
            public Vector3 Pos;
            public string ContainerID;
            public string ContainerName;

            public LootContainerInfo(ulong lootInteractiveClass, ulong objectClass, string name, Vector3 pos, string containerID, string containerName)
            {
                LootInteractiveClass = lootInteractiveClass;
                ObjectClass = objectClass;
                Name = name;
                Pos = pos;
                ContainerID = containerID;
                ContainerName = containerName;
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