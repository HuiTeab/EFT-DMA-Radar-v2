using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using eft_dma_radar.Source.Misc;

namespace eft_dma_radar {
    public class LootManager {
        private readonly Config _config;
        /// <summary>
        /// Filtered loot ready for display by GUI.
        /// </summary>
        public ReadOnlyCollection < DevLootItem > Filter {
            get;
            private set;
        }
        /// <summary>
        /// All tracked loot/corpses in Local Game World.
        /// </summary>
        private ReadOnlyCollection < DevLootItem > Loot {
            get;
        }

        /// <summary>
        /// InHideout from Game.cs
        /// </summary>
        /// 
        private bool IsAtHideout
        {
            get => Memory.InHideout;
        }
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LootManager"/> class.
        /// </summary>
        /// <param name="localGameWorld"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        ///
        public LootManager(ulong localGameWorld) {
            // If in hideout, don't parse loot
            if (IsAtHideout) {
                Program.Log("In Hideout, skipping loot parsing");
                return;
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start(); // Start timing
            Program.Log("Parsing loot...");

            _config = Program.Config;
            var lootlistPtr = Memory.ReadPtr(localGameWorld + Offsets.LocalGameWorld.LootList);
            var lootListEntity = Memory.ReadPtr(lootlistPtr + Offsets.UnityList.Base);
            var countLootListObjects = Memory.ReadValue < int > (lootListEntity + Offsets.UnityList.Count);
            if (countLootListObjects < 0 || countLootListObjects > 4096) throw new ArgumentOutOfRangeException("countLootListObjects"); // Loot list sanity check
            var loot = new List < DevLootItem > (countLootListObjects);
            
            var scatterMap = new ScatterReadMap(countLootListObjects);
            var round1 = scatterMap.AddRound();
            var round2 = scatterMap.AddRound();
            var round3 = scatterMap.AddRound();
            var round4 = scatterMap.AddRound();
            var round5 = scatterMap.AddRound();
            var round6 = scatterMap.AddRound();
            var round7 = scatterMap.AddRound();
            var round8 = scatterMap.AddRound();
            var round9 = scatterMap.AddRound();
            var round10 = scatterMap.AddRound();
            var round11 = scatterMap.AddRound();

            for (int i = 0; i < countLootListObjects; i++) {
                var p1 = round1.AddEntry<MemPointer>(i, 0, lootListEntity + Offsets.UnityListBase.Start + (uint)(i * 0x8));
                var p2 = round2.AddEntry<MemPointer>(i, 1, p1, null, Offsets.LootListItem.LootUnknownPtr);
                var p3 = round3.AddEntry<MemPointer>(i, 2, p2, null, Offsets.LootUnknownPtr.LootInteractiveClass);
                var p4 = round4.AddEntry<MemPointer>(i, 3, p3, null, Offsets.LootInteractiveClass.LootBaseObject);
                var p5 = round5.AddEntry<MemPointer>(i, 4, p4, null, Offsets.LootBaseObject.GameObject);
                var p6 = round6.AddEntry<MemPointer>(i, 5, p5, null, Offsets.GameObject.ObjectName);
                var p7 = round7.AddEntry<string>(i, 6, p6, 64);
                //className
                var p8 = round6.AddEntry<MemPointer>(i, 7, p3, null, 0x0);
                var p9 = round7.AddEntry<MemPointer>(i, 8, p8, null, 0x0);
                var p10 = round8.AddEntry<MemPointer>(i, 9, p9, null, 0x48);
                var p11 = round9.AddEntry<string>(i, 10, p10, 64); // ClassName
            }
            scatterMap.Execute();
            Parallel.For(0, countLootListObjects, i => {
                try {
                    var result1 = scatterMap.Results[i][0].TryGetResult<MemPointer>(out var lootObjectsEntity);
                    var result2 = scatterMap.Results[i][1].TryGetResult<MemPointer>(out var unknownPtr);
                    if (!scatterMap.Results[i][2].TryGetResult<MemPointer>(out var interactiveClass))
                    {
                        return;
                    }
                    var result4 = scatterMap.Results[i][3].TryGetResult<MemPointer>(out var baseObject);
                    if (!scatterMap.Results[i][4].TryGetResult<MemPointer>(out var gameObject))
                    {
                        return;
                    }
                    var result6 = scatterMap.Results[i][10].TryGetResult<string>(out var className);
                    var result7 = scatterMap.Results[i][6].TryGetResult<string>(out var name);

                    if (className == "LootableContainer")
                    {
                        var objectClass = Memory.ReadPtr(gameObject + Offsets.GameObject.ObjectClass);
                        var transformInternal = Memory.ReadPtrChain(objectClass, Offsets.LootGameObjectClass.To_TransformInternal);
                        var pos = new Transform(transformInternal).GetPosition();
                        var containerIDPtr = Memory.ReadPtr(interactiveClass + 0x128); //[118] Template : String
                        var containerID = Memory.ReadUnityString(containerIDPtr);
                        TarkovDevAPIManager.AllLootContainers.TryGetValue(containerID, out var container);
                        if (container != null)
                        {
                            try {
                                var itemOwner = Memory.ReadPtr(interactiveClass + Offsets.LootInteractiveClass.ContainerItemOwner);
                                var itemBase = Memory.ReadPtr(itemOwner + 0xC0); //Offsets.ContainerItemOwner.LootItemBase);
                                var grids = Memory.ReadPtr(itemBase + Offsets.LootItemBase.Grids);
                                GetItemsInGrid(grids, name, pos, loot, true, container.Name, name);
                            }
                            catch{}
                        }
                        else {
                            Program.Log($"Container: {name} {containerID} is not in the list");
                        }
                    }
                    else if (className == "ObservedCorpse")
                    {
                        if (interactiveClass == 0x0) return;
                        var itemOwner = Memory.ReadPtr(interactiveClass + 0x40); //[40] ItemOwner : -.GClass24D0
                        var rootItem = Memory.ReadPtr(itemOwner + 0xC0); //[C0] item_0xC0 : EFT.InventoryLogic.Item
                        var slots = Memory.ReadPtr(rootItem + 0x78);
                        var slotsArray = new MemArray(slots);
                        var objectClass = Memory.ReadPtr(gameObject + Offsets.GameObject.ObjectClass);
                        var transformInternal = Memory.ReadPtrChain(objectClass, Offsets.LootGameObjectClass.To_TransformInternal);
                        var pos = new Transform(transformInternal).GetPosition();
                        foreach(var slot in slotsArray.Data) 
                        {
                            try {
                                var namePtr = Memory.ReadPtr(slot + Offsets.Slot.Name);
                                var slotName = Memory.ReadUnityString(namePtr);
                                var containedItem = Memory.ReadPtr(slot + 0x40);
                                if (containedItem == 0x0){
                                    continue;
                                }
                                if (slotName == "SecuredContainer"){
                                    continue;
                                }
                                var itemTemplate = Memory.ReadPtr(containedItem + Offsets.LootItemBase.ItemTemplate); //EFT.InventoryLogic.ItemTemplate
                                var BSGIdPtr = Memory.ReadPtr(itemTemplate + Offsets.ItemTemplate.BsgId);
                                var id = Memory.ReadUnityString(BSGIdPtr);
                                var corpseItemNamePtr = Memory.ReadPtr(itemTemplate + 0x58);
                                var corpseItemName = Memory.ReadUnityString(corpseItemNamePtr);
                                var grids = Memory.ReadPtr(containedItem + Offsets.LootItemBase.Grids);
                                var containerName = slotName;
                                if (grids == 0x0){
                                    //The loot item we found does not have any grids so it's weapon slot?
                                    if (TarkovDevAPIManager.AllItems.TryGetValue(id, out
                                            var entry)) {
                                        loot.Add(new DevLootItem {
                                            Label = entry.Label,
                                                AlwaysShow = entry.AlwaysShow,
                                                Important = entry.Important,
                                                Position = pos,
                                                Item = entry.Item,
                                                Container = true,
                                                ContainerName = containerName
                                        });
                                    }
                                };
                                GetItemsInGrid(grids, id, pos, loot, true, containerName);
                            } 
                            catch {
                                continue;
                            }

                        }
                    }
                    else if (className == "ObservedLootItem")
                    {
                        //Loose loot
                        var item = Memory.ReadPtr(interactiveClass + 0xB0); //EFT.InventoryLogic.Item
                        var itemTemplate = Memory.ReadPtr(item + Offsets.LootItemBase.ItemTemplate); //EFT.InventoryLogic.ItemTemplate
                        bool questItem = Memory.ReadValue < bool > (itemTemplate + Offsets.ItemTemplate.IsQuestItem);
                        if (!questItem) {
                            var objectClass = Memory.ReadPtr(gameObject + Offsets.GameObject.ObjectClass);
                            var transformInternal = Memory.ReadPtrChain(objectClass, Offsets.LootGameObjectClass.To_TransformInternal);
                            var pos = new Transform(transformInternal).GetPosition();
                            var BSGIdPtr = Memory.ReadPtr(itemTemplate + Offsets.ItemTemplate.BsgId);
                            var id = Memory.ReadUnityString(BSGIdPtr);
                            if (id == null) return;
                            try {
                                var grids = Memory.ReadPtr(item + Offsets.LootItemBase.Grids);
                                var count = new MemArray(grids).Count;
                                GetItemsInGrid(grids, id, pos, loot, false , "Loose Loot");
                                } 
                                catch {
                                    //The loot item we found does not have any grids so it's basically like a keycard or a ledx etc. Therefore add it to our loot dictionary.
                                    if (TarkovDevAPIManager.AllItems.TryGetValue(id, out
                                            var entry)) {
                                        loot.Add(new DevLootItem {
                                            Label = entry.Label,
                                                AlwaysShow = entry.AlwaysShow,
                                                Important = entry.Important,
                                                Position = pos,
                                                Item = entry.Item
                                        });
                                    }
                                }
                        }
                    }

                }catch{
                    Program.Log($"Error reading loot item {i}");
                }
            });

            Loot = new(loot); // update readonly ref
            stopwatch.Stop(); // Stop timing
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("RunTime {0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Program.Log("RunTime " + elapsedTime);
            //Console.WriteLine("Loot RunTime " + elapsedTime);
            Program.Log("Loot parsing completed");
        }
        #endregion

        #region Methods

        /// <summary>
        /// Applies specified loot filter.
        /// </summary>
        /// <param name="filter">Filter by item 'name'. Will use values instead if left null.</param>
        public void ApplyFilter(string filter) {
            var loot = this.Loot; // cache ref
            if (loot is not null) {
                var filtered = new List < DevLootItem > (loot.Count);

                if (filter is null || filter.Trim() == string.Empty) // Use loot values
                {
                    foreach(var item in loot) {
                        if (item.Label == null){
                            Program.Log($"Item label is null {item.BsgId} - {item.Item.shortName}");
                            return;
                        }
                        var value = TarkovDevAPIManager.GetItemValue(item.Item);

                        if (item.AlwaysShow || value >= _config.MinLootValue) {
                            if (!filtered.Contains(item)) {
                                filtered.Add(item);
                            }
                        }

                    }
                }
                else
                {
                    var alwaysShow = loot.Where(x => x.AlwaysShow); // Always show these
                    foreach(var item in alwaysShow) {
                        if (!filtered.Contains(item))
                            filtered.Add(item);
                    }

                    var names = filter.Split(','); // Get multiple items searched
                    foreach(var name in names) {
                        var search = loot.Where(x => x.Item.shortName.Contains(name.Trim(), StringComparison.OrdinalIgnoreCase));
                        foreach(var item in search) {
                            if (!filtered.Contains(item))
                                filtered.Add(item);
                        }
                    }
                }
                this.Filter = new(filtered); // update ref
            }
        }
        ///This method recursively searches grids. Grids work as follows:
        ///Take a Groundcache which holds a Blackrock which holds a pistol.
        ///The Groundcache will have 1 grid array, this method searches for whats inside that grid.
        ///Then it finds a Blackrock. This method then invokes itself recursively for the Blackrock.
        ///The Blackrock has 11 grid arrays (not to be confused with slots!! - a grid array contains slots. Look at the blackrock and you'll see it has 20 slots but 11 grids).
        ///In one of those grid arrays is a pistol. This method would recursively search through each item it finds
        ///To Do: add slot logic, so we can recursively search through the pistols slots...maybe it has a high value scope or something.
        private void GetItemsInGrid(ulong gridsArrayPtr, string id, Vector3 pos, List < DevLootItem > loot, bool isContainer = false, string containerName = "", string realContainerName = "") {
            var gridsArray = new MemArray(gridsArrayPtr);

            //write console which item is in which container
            //Console.WriteLine($"{id} in {containerName} - {realContainerName}");

            if (TarkovDevAPIManager.AllItems.TryGetValue(id, out
                    var entry)) {
                loot.Add(new DevLootItem {
                    Label = entry.Label,
                        AlwaysShow = entry.AlwaysShow,
                        Important = entry.Important,
                        Position = pos,
                        Item = entry.Item,
                        Container = isContainer,
                        ContainerName = containerName,
                });
            }
            // Check all sections of the container
            foreach(var grid in gridsArray.Data) {
                var gridEnumerableClass = Memory.ReadPtr(grid + Offsets.Grids.GridsEnumerableClass); // -.GClass178A->gClass1797_0x40 // Offset: 0x0040 (Type: -.GClass1797)
                var itemListPtr = Memory.ReadPtr(gridEnumerableClass + 0x18); // -.GClass1797->list_0x18 // Offset: 0x0018 (Type: System.Collections.Generic.List<Item>)
                var itemList = new MemList(itemListPtr);

                foreach(var childItem in itemList.Data) {
                    try {
                        var childItemTemplate = Memory.ReadPtr(childItem + Offsets.LootItemBase.ItemTemplate); // EFT.InventoryLogic.Item->_template // Offset: 0x0038 (Type: EFT.InventoryLogic.ItemTemplate)
                        var childItemIdPtr = Memory.ReadPtr(childItemTemplate + Offsets.ItemTemplate.BsgId);
                        var childItemIdStr = Memory.ReadUnityString(childItemIdPtr).Replace("\\0", "");
                        //Set important and always show if quest item using ID
                        // Check to see if the child item has children
                        var childGridsArrayPtr = Memory.ReadPtr(childItem + Offsets.LootItemBase.Grids); // -.GClassXXXX->Grids // Offset: 0x0068 (Type: -.GClass1497[])
                        GetItemsInGrid(childGridsArrayPtr, childItemIdStr, pos, loot, true, containerName, realContainerName); // Recursively add children to the entity
                    } catch  {
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
    public class MemArray {
        public ulong Address {
            get;
        }
        public int Count {
            get;
        }
        public ulong[] Data {
            get;
        }

        public MemArray(ulong address) {
            var type = typeof (ulong);

            Address = address;
            Count = Memory.ReadValue < int > (address + Offsets.UnityList.Count);
            var arrayBase = address + Offsets.UnityListBase.Start;
            var tSize = (uint) Marshal.SizeOf(type);

            // Rudimentary sanity check
            if (Count > 4096 || Count < 0)
                Count = 0;

            var retArray = new ulong[Count];
            var buf = Memory.ReadBuffer(arrayBase, Count * (int) tSize);

            for (uint i = 0; i < Count; i++) {
                var index = i * tSize;
                var t = MemoryMarshal.Read < ulong > (buf.Slice((int) index, (int) tSize));
                if (t == 0x0) throw new NullPtrException();
                retArray[i] = t;
            }

            Data = retArray;
        }
    }

    //Helper class or struct
    public class MemList {
        public ulong Address {
            get;
        }

        public int Count {
            get;
        }

        public List < ulong > Data {
            get;
        }

        public MemList(ulong address) {
            var type = typeof (ulong);

            Address = address;
            Count = Memory.ReadValue < int > (address + Offsets.UnityList.Count);

            if (Count > 4096 || Count < 0)
                Count = 0;

            var arrayBase = Memory.ReadPtr(address + Offsets.UnityList.Base) + Offsets.UnityListBase.Start;
            var tSize = (uint) Marshal.SizeOf(type);
            var retList = new List < ulong > (Count);
            var buf = Memory.ReadBuffer(arrayBase, Count * (int) tSize);

            for (uint i = 0; i < Count; i++) {
                var index = i * tSize;
                var t = MemoryMarshal.Read < ulong > (buf.Slice((int) index, (int) tSize));
                if (t == 0x0) throw new NullPtrException();
                retList.Add(t);
            }

            Data = retList;
        }
    }

    public class DevLootItem {
        public string Label {
            get;
            init;
        }
        public bool Important {
            get;
            init;
        } = false;
        public Vector3 Position {
            get;
            init;
        }
        public bool AlwaysShow {
            get;
            init;
        } = false;
        public string BsgId {
            get;
            init;
        }
        public bool Container {
            get;
            init;
        } = false;
        public string ContainerName {
            get;
            init;
        }
        public TarkovItem Item {
            get;
            init;
        } = new();   
    }

    public class LootContainers {
        public string Name {
            get;
            init;
        }
        public string ID {
            get;
            init;
        }
        public string NormalizedName {
            get;
            init;
        }
    }
    #endregion
}