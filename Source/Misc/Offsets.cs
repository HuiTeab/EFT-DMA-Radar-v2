namespace Offsets
{
    public struct UnityList
    {
        public const uint Base = 0x10; // to UnityListBase
        public const uint Count = 0x18; // int32
    }
    public struct UnityListBase
    {
        public const uint Start = 0x20; // start of list +(i * 0x8)
    }
    public struct UnityString
    {
        public const uint Length = 0x10; // int32
        public const uint Value = 0x14; // string,unicode
    }
    public struct ModuleBase
    {
        public const uint GameObjectManager = 0x17FFD28; // to eft_dma_radar.GameObjectManager
    }
    public struct GameObject
    {
        public static readonly uint[] To_TransformInternal = new uint[] { 0x10, 0x30, 0x30, 0x8, 0x28, 0x10 }; // to TransformInternal
        public const uint ObjectClass = 0x30;
        public const uint ObjectName = 0x60; // string,default (null terminated)
    }
    public struct GameWorld
    {
        public static readonly uint[] To_LocalGameWorld = new uint[] { GameObject.ObjectClass, 0x18, 0x28 };
    }
    public struct LocalGameWorld // [Class] -.ClientLocalGameWorld : ClientGameWorld
    {
        public const uint MainPlayer = 0x148; // to EFT.Player
        public const uint ExfilController = 0x18; // to ExfilController
        public const uint LootList = 0xC8; // to UnityList
        public const uint RegisteredPlayers = 0xF0; // to RegisteredPlayers
        public const uint Grenades = 0x1A0; // to Grenades
    }
    public struct ExfilController // -.GClass0B67
    {
        public const uint ExfilCount = 0x18; // int32
        public const uint ExfilList = 0x20; // to UnityListBase
    }
    public struct Exfil
    {
        public const uint Status = 0xA8; // int32
    }

    public struct UnityClass
    {
        public static readonly uint[] Name = new uint[] { 0x0, 0x0, 0x48 }; // to ClassName
    }
    public struct Grenades // -.GClass05FD<Int32, Throwable>
    {
        public const uint List = 0x18; // to UnityList
    }
    public struct Player // EFT.Player : MonoBehaviour, 
    {
        //public static readonly uint[] To_TransformInternal = new uint[] { 0xA8, 0x28, 0x28, 0x10, 0x20, 0x10 }; // to TransformInternal
        public static readonly uint[] To_TransformInternal = new uint[] { 0xA8, 0x28, 0x28, 0x10, 0x20 + (0 * 0x8), 0x10 }; // to TransformInternal
        public const uint MovementContext = 0x40; // to MovementContext
        public const uint Corpse = 0x390; // EFT.Interactive.Corpse
        public const uint Profile = 0x588; // to Profile
        public const uint HealthController = 0x5C8; // to HealthController
        public const uint InventoryController = 0x5E0; // to InventoryController
        public const uint IsLocalPlayer = 0x906; // bool
    }
    public struct Profile // EFT.Profile
    {
        public const uint Id = 0x10; // unity string
        public const uint AccountId = 0x18; // unity string
        public const uint PlayerInfo = 0x28; // to PlayerInfo
        public const uint Stats = 0xE8; // to Stats
    }
    public struct Stats // -.GClass05B9
    {
        public const uint OverallCounters = 0x18; // to OverallCounters
    }
    public struct OverallCounters
    {
        public const uint Counters = 0x10; // to Dictionary<IntPtr, ulong>
    }
    public struct PlayerInfo // -.GClass1044
    {
        public const uint Nickname = 0x10; // unity string
        public const uint MainProfileNickname = 0x18; // unity string
        public const uint GroupId = 0x20; // ptr to UnityString (0/null if solo or bot)
        public const uint GameVersion = 0x38; // unity string
        public const uint Settings = 0x50; // to PlayerSettings
        public const uint PlayerSide = 0x70; // int32
        public const uint RegDate = 0x74; // int32
        public const uint MemberCategory = 0x8C; // int32 enum
        public const uint Experience = 0x90; // int32
    }
    public struct ObservedPlayerView // [Class] EFT.NextObservedPlayer.ObservedPlayerView : MonoBehaviour
    {
        public const uint PlayerSide = 0xF0; // int32
        public const uint IsAI = 0x109; // bool
        public const uint ID = 0x40; // to UnityString
        public const uint NickName = 0x48; // to UnityString
        public const uint ObservedPlayerController = 0x80; // to PlayerController
        public static readonly uint[] To_MovementContext = new uint[] { 0x80, 0xE8, 0x10}; // to MovementContext
        public static readonly uint[] To_TransformInternal = new uint[] { 0x60, 0x28, 0x28, 0x10, 0x20, 0x10 }; // to TransformInternal
    }

    public struct ObservedPlayerController //[Class] -.GClass1E0D : Object, GInterface94D4, IDisposable
    {
        public const uint InfoContainer = 0xE0; // to InfoContainer
        public const uint InventoryController = 0x138; // to InventoryController
        public static readonly uint[] To_MovementContext = new uint[] { 0xC0, 0x10}; // to MovementContext
        
    }

    public struct ObserverdPlayerMovementContext
    {
        public const uint Rotation = 0x78; // to Vector2
    }

    public struct AIData
    {
        public static readonly uint[] To_MovementContext = new uint[] { 0x28, 0x10}; // to MovementContext
    }

    public struct InfoContainer 
    {
        public const uint Id = 0x10; // to Id
        public const uint NickName = 0x18; // to NickName
        public const uint Side = 0x20; // to Side
    }

    public struct PlayerSettings
    {
        public const uint Role = 0x10; // int32 enum
    }
    public struct MovementContext
    {
        public const uint Rotation = 0x3D4; // vector2
    }
    public struct InventoryController // -.GClass1A98
    {
        public const uint Inventory = 0x138; // to Inventory
        public const uint ObservedPlayerInventory = 0x130; // to Inventory
    }
    public struct Inventory
    {
        public const uint Equipment = 0x10; // to Equipment
    }
    public struct Equipment
    {
        public const uint Slots = 0x78; // to UnityList
    }
    public struct Slot
    {
        public const uint Name = 0x18; // string,unity
        public const uint ContainedItem = 0x40; // to LootItemBase
    }
    public struct HealthController // -.GInterface7AEE
    {
        public static readonly uint[] To_HealthEntries = { 0x50 , 0x18}; // to HealthEntries
    }
    public struct HealthEntries
    {
        public const uint HealthEntries_Start = 0x30; // Each body part 0x18 , to HealthEntry
    }
    public struct HealthEntry
    {
        public const uint Value = 0x10; // to HealthValue
    }
    public struct HealthValue
    {
        public const uint Current = 0x0; // float
        public const uint Maximum = 0x4; // float
        public const uint Minimum = 0x8; // float
    }
    public struct LootListItem
    {
        public const uint LootUnknownPtr = 0x10; // to LootUnknownPtr
    }

    public struct LootUnknownPtr
    {
        public const uint LootInteractiveClass = 0x28; // to LootInteractiveClass
    }
    public struct LootInteractiveClass
    {
        public const uint LootBaseObject = 0x10; // to LootBaseObject
        public const uint LootItemBase = 0x50; // to LootItemBase
        public const uint ContainerItemOwner = 0x110; // to ContainerItemOwner
    }
    public struct LootItemBase //EFT.InventoryLogic.Item
    {
        public const uint ItemTemplate = 0x40; // to ItemTemplate
        public const uint Grids = 0x70; // to Grids
        public const uint Slots = 0x78; // to UnityList
        public const uint Cartridges = 0x90; // via -.GClass19FD : GClass19D6, IAmmoContainer , to StackSlot
    }
    public struct StackSlot // EFT.InventoryLogic.StackSlot : Object, IContainer
    {
        public const uint Items = 0x10; // to UnityList , of LootItemBase
    }
    public struct ItemTemplate //EFT.InventoryLogic.ItemTemplate
    {
        public const uint BsgId = 0x50; // string,unity
        public const uint IsQuestItem = 0x9C; // bool
    }
    public struct LootBaseObject
    {
        public const uint GameObject = 0x30; // to GameObject
    }
    public struct LootGameObjectClass
    {
        public static readonly uint[] To_TransformInternal = new uint[] { 0x8, 0x28, 0x10 };
    }
    public struct ContainerItemOwner
    {
        public const uint LootItemBase = 0xA0; // to LootItemBase
    }
    public struct Grids
    {
        public const uint GridsEnumerableClass = 0x40;
    }
    public struct TransformInternal
    {
        public const uint Hierarchy = 0x38; // to TransformHierarchy
        public const uint HierarchyIndex = 0x40; // int32
    }
    public struct TransformHierarchy
    {
        public const uint Vertices = 0x18; // List<Vector128<float>>
        public const uint Indices = 0x20; // List<int>
    }
}