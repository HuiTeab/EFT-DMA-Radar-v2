using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace eft_dma_radar
{
    public class GearManager
    {
        private static readonly List<string> _skipSlots = new()
        {
            "Scabbard", "SecuredContainer", "Dogtag", "Compass", "Eyewear", "ArmBand"
        };
        /// <summary>
        /// List of equipped items in PMC Inventory Slots.
        /// </summary>
        public ReadOnlyDictionary<string, GearItem> Gear { get; }

        public GearManager(ulong playerBase, bool isPMC, bool isLocal)
        {
            var size = 0;
            var slots = 0UL;
            var slotDict = new Dictionary<string, ulong>(StringComparer.OrdinalIgnoreCase);
            if (isLocal){
                var inventorycontroller = Memory.ReadPtr(playerBase + Offsets.Player.InventoryController);
                var inventory = Memory.ReadPtr(inventorycontroller + Offsets.InventoryController.Inventory);
                var equipment = Memory.ReadPtr(inventory + Offsets.Inventory.Equipment);
                slots = Memory.ReadPtr(equipment + Offsets.Equipment.Slots);
                size = Memory.ReadValue<int>(slots + Offsets.UnityList.Count);

            } else {
                var observedPlayerController = Memory.ReadPtr(playerBase + Offsets.ObservedPlayerView.ObservedPlayerController);
                var inventorycontroller = Memory.ReadPtr(observedPlayerController + Offsets.ObservedPlayerController.InventoryController);
                var inventory = Memory.ReadPtr(inventorycontroller + Offsets.InventoryController.ObservedPlayerInventory);
                var equipment = Memory.ReadPtr(inventory + Offsets.Inventory.Equipment);
                slots = Memory.ReadPtr(equipment + Offsets.Equipment.Slots);
                size = Memory.ReadValue<int>(slots + Offsets.UnityList.Count);
            }
            if (size == 0 || slots == 0) return;

            for (int slotID = 0; slotID < size; slotID++)
            {
                var slotPtr = Memory.ReadPtr(slots + Offsets.UnityListBase.Start + (uint)slotID * 0x8);
                var namePtr = Memory.ReadPtr(slotPtr + Offsets.Slot.Name);
                var name = Memory.ReadUnityString(namePtr);
                if (_skipSlots.Contains(name, StringComparer.OrdinalIgnoreCase)) continue;
                slotDict.TryAdd(name, slotPtr);
            }
            var gearDict = new Dictionary<string, GearItem>(StringComparer.OrdinalIgnoreCase);
            foreach (var slotName in slotDict.Keys)
            {
                if (_skipSlots.Contains(slotName, StringComparer.OrdinalIgnoreCase)) continue;
                try
                {
                    if (slotDict.TryGetValue(slotName, out var slot))
                    {
                        var containedItem = Memory.ReadPtr(slot + Offsets.Slot.ContainedItem);
                        var inventorytemplate = Memory.ReadPtr(containedItem + Offsets.LootItemBase.ItemTemplate);
                        var idPtr = Memory.ReadPtr(inventorytemplate + Offsets.ItemTemplate.BsgId);
                        var id = Memory.ReadUnityString(idPtr);

                        if (TarkovDevAPIManager.AllItems.TryGetValue(id, out var entry))
                        {
                            string longName = entry.Item.name; // Contains 'full' item name
                            string shortName = entry.Item.shortName; // Contains 'full' item name
                            string extraSlotInfo = null; // Contains additional slot information (ammo type,etc.)
                            if (isPMC) // Only recurse further for PMCs (we don't care about P Scavs)
                            {
                                if (slotName == "FirstPrimaryWeapon" || slotName == "SecondPrimaryWeapon") // Only interested in weapons
                                {
                                    try
                                    {
                                        var result = new PlayerWeaponInfo();
                                        RecurseSlotsForThermalsAmmo(containedItem, ref result); // Check weapon ammo type, and if it contains a thermal scope
                                        extraSlotInfo = result.ToString();
                                    }
                                    catch { }
                                }
                            }
                            if (extraSlotInfo is not null)
                            {
                                longName += $" ({extraSlotInfo})";
                                shortName += $" ({extraSlotInfo})";
                            }
                            var gear = new GearItem()
                            {
                                Long = longName,
                                Short = shortName
                            };
                            gearDict.TryAdd(slotName, gear);
                        } else {
                            Debug.WriteLine($"GearManager: ID: {id} not found in TarkovMarketManager.AllItems");
                        }
                    }
                }
                catch { } // Skip over empty slots
            }
            Gear = new(gearDict); // update readonly ref
        }

        /// <summary>
        /// Checks a 'Primary' weapon for Ammo Type, and Thermal Scope.
        /// </summary>
        private void RecurseSlotsForThermalsAmmo(ulong lootItemBase, ref PlayerWeaponInfo result)
        {
            const string reapIR = "5a1eaa87fcdbcb001865f75e";
            const string flir = "5d1b5e94d7ad1a2b865a96b0";

            //609bab8b455afd752b2e6138 ## T12W 30Hz thermal reflex sight
            //6478641c19d732620e045e17 ## ECHO1 1-2x30mm 30Hz thermal reflex
            //63fc44e2429a8a166c7f61e6 ## Armasight 2-8x50 30Hz thermal scope
            
            const string T12W = "63fc44e2429a8a166c7f61e6";
            const string ECHO1 = "6478641c19d732620e045e17";
            const string Armasight = "63fc44e2429a8a166c7f61e6";

            //Debug.WriteLine($"GearManager Scope: Starting...");
            try
            {
                var parentSlots = Memory.ReadPtr(lootItemBase + Offsets.LootItemBase.Slots);
                var size = Memory.ReadValue<int>(parentSlots + Offsets.UnityList.Count);
                var slotDict = new Dictionary<string, ulong>(StringComparer.OrdinalIgnoreCase);

                for (int slotID = 0; slotID < size; slotID++)
                {
                    var slotPtr = Memory.ReadPtr(parentSlots + Offsets.UnityListBase.Start + (uint)slotID * 0x8);
                    var namePtr = Memory.ReadPtr(slotPtr + Offsets.Slot.Name);
                    var name = Memory.ReadUnityString(namePtr);
                    if (_skipSlots.Contains(name, StringComparer.OrdinalIgnoreCase)) continue;
                    slotDict.TryAdd(name, slotPtr);
                }
                foreach (var slotName in slotDict.Keys)
                {
                    try
                    {
                        if (slotDict.TryGetValue(slotName, out var slot))
                        {
                            var containedItem = Memory.ReadPtr(slot + Offsets.Slot.ContainedItem);
                            if (slotName == "mod_magazine") // Magazine slot - Check for ammo!
                            {
                                var cartridge = Memory.ReadPtr(containedItem + Offsets.LootItemBase.Cartridges);
                                var cartridgeStack = Memory.ReadPtr(cartridge + Offsets.StackSlot.Items);
                                var cartridgeStackList = Memory.ReadPtr(cartridgeStack + Offsets.UnityList.Base);
                                var firstRoundItem = Memory.ReadPtr(cartridgeStackList + Offsets.UnityListBase.Start + 0); // Get first round in magazine
                                var firstRoundItemTemplate = Memory.ReadPtr(firstRoundItem + Offsets.LootItemBase.ItemTemplate);
                                var firstRoundIdPtr = Memory.ReadPtr(firstRoundItemTemplate + Offsets.ItemTemplate.BsgId);
                                var firstRoundId = Memory.ReadUnityString(firstRoundIdPtr);
                                if (TarkovDevAPIManager.AllItems.TryGetValue(firstRoundId, out var firstRound)) // Lookup ammo type
                                {
                                    result.AmmoType = firstRound.Item.shortName;
                                }
                            }
                            else // Not a magazine, keep recursing for a scope
                            {
                                var inventorytemplate = Memory.ReadPtr(containedItem + Offsets.LootItemBase.ItemTemplate);
                                var idPtr = Memory.ReadPtr(inventorytemplate + Offsets.ItemTemplate.BsgId);
                                var id = Memory.ReadUnityString(idPtr);
                                if (id.Equals(reapIR, StringComparison.OrdinalIgnoreCase) ||
                                    id.Equals(flir, StringComparison.OrdinalIgnoreCase) ||
                                    id.Equals(T12W, StringComparison.OrdinalIgnoreCase) ||
                                    id.Equals(ECHO1, StringComparison.OrdinalIgnoreCase) ||
                                    id.Equals(Armasight, StringComparison.OrdinalIgnoreCase)
                                    )
                                {
                                    if (TarkovDevAPIManager.AllItems.TryGetValue(id, out var entry))
                                    {
                                        result.ThermalScope = entry.Item.shortName;
                                    }
                                }
                                RecurseSlotsForThermalsAmmo(containedItem, ref result);
                            }
                        }
                    }
                    catch { } // Skip over empty slots
                }
            }
            catch
            {
            }
        }
    }
}
