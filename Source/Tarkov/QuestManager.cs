using System.Collections.ObjectModel;

namespace eft_dma_radar
{
    public class QuestManager
    {
        public static ReadOnlyDictionary<string, Tasks> RequiredTasks { get; private set; }

        static QuestManager()
        {
            RequiredTasks = new ReadOnlyDictionary<string, Tasks>(new Dictionary<string, Tasks>());
        }

        public QuestManager(ulong localGameWorld) {
            var allRequiredTasks = new Dictionary<string, Tasks>(StringComparer.OrdinalIgnoreCase);
            var mainPlayer = Memory.ReadPtr(localGameWorld + Offsets.LocalGameWorld.MainPlayer);
            var profile = Memory.ReadPtr(mainPlayer + Offsets.Player.Profile);
            var questData = Memory.ReadPtr(profile + 0x78); 
            var questDataCount = Memory.ReadValue<int>(questData + 0x18);
            var questDataBaseList = Memory.ReadPtr(questData + 0x10);

            for (int i = 0; i < questDataCount; i++)
            {
                var questEntry = Memory.ReadPtr(questDataBaseList + 0x20 + (uint)(i * 0x8)); //[Class] -.GClass306A : Object
                if (questEntry == 0)
                {
                    continue;
                }
                var questTemplate = Memory.ReadPtr(questEntry + 0x28); //[28] Template : -.GClass306D [Class] -.GClass306D : Object
                if (questTemplate == 0)
                {
                    continue;
                }
                var questIDPtr = Memory.ReadPtr(questTemplate + 0x10);
                var questID = Memory.ReadUnityString(questIDPtr);
                try {
                    var questStatus = Memory.ReadValue<int>(questEntry + 0x34);
                    //2 = started
                    if (questStatus == 2)
                    {
                        TarkovDevAPIManager.AllTasks.TryGetValue(questID, out var task);
                        if (task != null)
                        {
                            //WIP
                            allRequiredTasks.TryAdd(
                                questID,
                                new Tasks()
                                {
                                    Name = task.Name,
                                    ID = task.ID,
                                    KappaRequired = task.KappaRequired,
                                    Objectives = task.Objectives
                                }
                            );
                            var conditions = Memory.ReadPtr(questTemplate + 0x40);
                            var conditionsCount = Memory.ReadValue<int>(conditions + 0x40);
                            var conditionsEntries = Memory.ReadPtr(conditions + 0x18);
                            
                            continue;
                        }
                        continue;

                    }
                } catch {
                    Console.WriteLine($"Quest: {questID} is not in the list");
                }
            }
            RequiredTasks = new (allRequiredTasks);
        }
    }
}
