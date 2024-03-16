using System.Collections.ObjectModel;
using System.Numerics;

namespace eft_dma_radar
{
    public class QuestManager
    {

        public Collection<QuestItem> QuestItem {
            get;
            private set;
        }

        public Collection<QuestZone> QuestZone {
            get;
            private set;
        }

        public QuestManager(ulong localGameWorld) {
            var mainPlayer = Memory.ReadPtr(localGameWorld + Offsets.LocalGameWorld.MainPlayer);
            var profile = Memory.ReadPtr(mainPlayer + Offsets.Player.Profile);
            var questData = Memory.ReadPtr(profile + 0x78); 
            var questDataCount = Memory.ReadValue<int>(questData + 0x18);
            var questDataBaseList = Memory.ReadPtr(questData + 0x10);

            var questItem = new List < QuestItem > (questDataCount);
            var questZone = new List < QuestZone > (questDataCount*10);

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
                            //Console.WriteLine($"Quest: {task.Name} is started ID: {questID}");
                            var objectives = task.Objectives;
                            foreach (var objective in objectives)
                            {
                                if (objective == null)
                                {
                                    continue;
                                }
                                if (objective.Type != "visit" && objective.Type != "mark" && objective.Type != "findQuestItem" && objective.Type != "plantItem" && objective.Type != "plantQuestItem")
                                {
                                    continue;
                                }
                                var zones = objective.Zones;
                                if (zones != null)
                                {
                                    foreach (var zone in zones)
                                    {
                                        questZone.Add(new QuestZone
                                        {
                                            ID = zone.id,
                                            MapName = zone.map.name,
                                            Position = new Vector3((float)zone.position.x, (float)zone.position.y, (float)zone.position.z),
                                            ObjectiveType = objective.Type,
                                            Description = objective.Description,
                                            TaskName = task.Name
                                        });
                                    }   
                                }
                                if (objective.Type == "findQuestItem"){
                                    //Add to list
                                    questItem.Add(new QuestItem {
                                        Id = objective.QuestItem.Id,
                                        Name = objective.QuestItem.Name,
                                        ShortName = objective.QuestItem.ShortName,
                                        NormalizedName = objective.QuestItem.NormalizedName,
                                        TaskName = task.Name,
                                        Description = objective.QuestItem.Description
                                    });
                                }
                            }
                        }
                        continue;
                    }
                } catch {
                     Console.WriteLine($"Quest: {questID} is not in the list");
                }
            }
            this.QuestZone = new(questZone); // update readonly ref
            this.QuestItem = new(questItem); // update readonly ref
        }
    }

    public class QuestItem {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string NormalizedName { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public Vector3 Position {
            get;
            set;
        }
        public Vector2 ZoomedPosition { get; set; } = new();
    }

    public class QuestZone {
        public string ID { get; set; }
        public string MapName { get; set; }
        public string Description { get; set; }
        public string TaskName { get; set; }
        public Vector3 Position {
            get;
            set;
        }

        public Vector2 ZoomedPosition { get; set; } = new();
        public string ObjectiveType { get; internal set; }

    }

}
