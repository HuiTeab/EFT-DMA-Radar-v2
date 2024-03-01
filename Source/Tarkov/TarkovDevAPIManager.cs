using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;

namespace eft_dma_radar
{
    internal static class TarkovDevAPIManager
    {
        /// <summary>
        /// Contains all Tarkov Loot, Quests Items and Tasks mapped via BSGID String.
        /// </summary>
        ///
        public static ReadOnlyDictionary<string, DevLootItem> AllItems { get; }
        public static ReadOnlyDictionary<string, QuestItems> AllQuestItems { get; }
        public static ReadOnlyDictionary<string, Tasks> AllTasks { get; }
        public static ReadOnlyDictionary<string, LootContainers> AllLootContainers { get;}

        #region Static_Constructor

        static TarkovDevAPIManager()
        {
            TarkovDevResponse jsonResponse;

            var jsonItems = new List<TarkovDevResponse>();
            var allItems = new Dictionary<string, DevLootItem>(StringComparer.OrdinalIgnoreCase);
            var allQuestItems = new Dictionary<string, QuestItems>(
                StringComparer.OrdinalIgnoreCase
            );
            var allTasks = new Dictionary<string, Tasks>(StringComparer.OrdinalIgnoreCase);
            var allLootContainers = new Dictionary<string, LootContainers>(
                StringComparer.OrdinalIgnoreCase
            );
            if (
                !File.Exists("api_tarkov_dev_items.json")
                || File.GetLastWriteTime("api_tarkov_dev_items.json").AddHours(480) < DateTime.Now
            ) // only update every 480h
            {
                using (var client = new HttpClient())
                {
                    //Create body and content-type
                    var body = new
                    {
                        query = @"query {
                                    items {
                                        id
                                        name
                                        shortName
                                        normalizedName
                                        basePrice
                                        avg24hPrice
                                        low24hPrice
                                        high24hPrice
                                        sellFor {
                                        price
                                        vendor {
                                            normalizedName
                                        }
                                        }
                                        category {
                                        id
                                        name
                                        normalizedName
                                        }
                                        weight
                                        categories {
                                        id
                                        name
                                        normalizedName
                                        }
                                    }
                                    tasks {
                                        id
                                        name
                                        objectives {
                                            id
                                            type
                                            description
                                            maps {
                                                id
                                                name
                                                normalizedName
                                            }
                                            ... on TaskObjectiveItem {
                                                item {
                                                id
                                                name
                                                shortName
                                                }
                                                zones {
                                                id
                                                map {
                                                    id
                                                    normalizedName
                                                    name
                                                }
                                                position {
                                                    y
                                                    x
                                                    z
                                                }
                                                }
                                                requiredKeys {
                                                id
                                                name
                                                shortName
                                                }
                                                count
                                                foundInRaid
                                            }
                                            ... on TaskObjectiveMark {
                                                id
                                                description
                                                markerItem {
                                                id
                                                name
                                                shortName
                                                }
                                                maps {
                                                id
                                                normalizedName
                                                name
                                                }
                                                zones {
                                                id
                                                map {
                                                    id
                                                    normalizedName
                                                    name
                                                }
                                                position {
                                                    y
                                                    x
                                                    z
                                                }
                                                }
                                                requiredKeys {
                                                id
                                                name
                                                shortName
                                                }
                                            }
                                            ... on TaskObjectiveQuestItem {
                                                id
                                                description
                                                requiredKeys {
                                                id
                                                name
                                                shortName
                                                }
                                                maps {
                                                id
                                                normalizedName
                                                name
                                                }
                                                zones {
                                                id
                                                map {
                                                    id
                                                    normalizedName
                                                    name
                                                }
                                                position {
                                                    y
                                                    x
                                                    z
                                                }
                                                }
                                                requiredKeys {
                                                id
                                                name
                                                shortName
                                                }
                                                count
                                            }
                                            ... on TaskObjectiveBasic {
                                                id
                                                description
                                                requiredKeys {
                                                id
                                                name
                                                shortName
                                                }
                                                maps {
                                                id
                                                normalizedName
                                                name
                                                }
                                                zones {
                                                id
                                                map {
                                                    id
                                                    normalizedName
                                                    name
                                                }
                                                position {
                                                    y
                                                    x
                                                    z
                                                }
                                                }
                                                requiredKeys {
                                                id
                                                name
                                                shortName
                                                }
                                            }
                                        }
                                    }
                                    questItems {
                                        id
                                        shortName
                                        name
                                        normalizedName
                                    }
                                    lootContainers {
                                        id
                                        normalizedName
                                        name
                                    }
                                }"
                    };
                    var jsonBody = JsonSerializer.Serialize(body);
                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                    var response = client
                        .PostAsync("https://api.tarkov.dev/graphql", content)
                        .Result;
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    jsonResponse = JsonSerializer.Deserialize<TarkovDevResponse>(responseString);
                    jsonItems.Add(jsonResponse);
                    File.WriteAllText("api_tarkov_dev_items.json", responseString);
                }
            }
            else
            {
                var responseString = File.ReadAllText("api_tarkov_dev_items.json");
                jsonResponse = JsonSerializer.Deserialize<TarkovDevResponse>(responseString);
                jsonItems.Add(jsonResponse);
            }

            foreach (var item in jsonItems)
            {
                if (item.data?.items != null)
                {
                    foreach (var tarkovItem in item.data.items)
                    {
                        var value = GetItemValue(tarkovItem);
                        
                        allItems.TryAdd(
                            tarkovItem.id,
                            new DevLootItem()
                            {
                                Label = tarkovItem.name,
                                Item = tarkovItem
                            }
                        );
                    }
                }
                if (item.data?.tasks != null)
                {
                    // WIP //
                    foreach (var task in item.data.tasks)
                    {
                        var objectiveCount = task.objectives.Count;
                        for (var i = 0; i < objectiveCount; i++)
                        {
                            var objective = task.objectives[i];
                            //visit
                            //extract
                            //mark
                            //shoot
                            //findQuestItem
                            //giveQuestItem
                            //plantItem
                            //giveItem
                            //experience
                            if (objective.type == "findQuestItem"){

                            }
                            if (objective.type == "visit"){
                                if (objective.maps != null && objective.maps.Any()){
                                    foreach (var map in objective.maps)
                                    {
                                        
                                    }
                                }
                                if (objective.zones != null && objective.zones.Any()){
                                    foreach (var zone in objective.zones)
                                    {

                                    }
                                }

                            }
                            if (objective.type == "mark"){

                            }
                        }
                        
                    }
                }
                if (item.data?.questItems != null)
                {
                    foreach (var questItem in item.data.questItems)
                    {
                        allQuestItems.TryAdd(
                            questItem.id,
                            new QuestItems()
                            {
                                Name = questItem.name,
                                ID = questItem.id,
                                ShortName = questItem.shortName
                            }
                        );
                    }
                }
                if (item.data?.lootContainers != null)
                {
                    foreach (var lootContainer in item.data.lootContainers)
                    {
                        allLootContainers.TryAdd(
                            lootContainer.id,
                            new LootContainers()
                            {
                                Name = lootContainer.name,
                                ID = lootContainer.id,
                                NormalizedName = lootContainer.normalizedName
                            }
                        );
                    }
                }
            }
            AllItems = new(allItems);
            AllTasks = new(allTasks);
            AllQuestItems = new(allQuestItems);
            AllLootContainers = new(allLootContainers);
        }
        #endregion

        #region Private_Methods
        public static string FormatNumber(int num)
        {
            if (num >= 1000000)
                return (num / 1000000D).ToString("0.##") + "M";
            else if (num >= 1000)
                return (num / 1000D).ToString("0") + "K";
            else
                return num.ToString();
        }

        public static int GetItemValue(TarkovItem tarkovItem)
        {
            //find the best price to sell
            int bestPrice;

            if (tarkovItem.avg24hPrice > tarkovItem.basePrice)
            {
                bestPrice = (int)tarkovItem.avg24hPrice;
                foreach (var vendor in tarkovItem.sellFor)
                {
                    if (vendor.price > bestPrice)
                    {
                        bestPrice = vendor.price;
                    }
                }
            }
            else
            {
                bestPrice = tarkovItem.basePrice;
            }

            return bestPrice;
        }
        #endregion
    }

    #region Classes
    /// <summary>
    /// New Class to hold Tarkov Items Data.
    /// </summary>
    public class TarkovItem
    {
        public string id { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string normalizedName { get; set; }
        public int basePrice { get; set; }
        public int? avg24hPrice { get; set; }
        public int? low24hPrice { get; set; }
        public int? high24hPrice { get; set; }
        public double weight { get; set; } 
        public List<Category> categories { get; set; } = new List<Category>();
        public List<VendorPrice> sellFor { get; set; } = new List<VendorPrice>(); 

        public class VendorPrice
        {
            public string vendorName { get; set; }
            public int price { get; set; }
        }

        public class Category
        {
            public string id { get; set; }
            public string name { get; set; }
            public string normalizedName { get; set; }
        }
    }

    public class TarkovTasks
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<Objective> objectives { get; set; } = new List<Objective>();

        public class Objective
        {
            public string id { get; set; }
            public string description { get; set; }
            public string type { get; set; }
            public List<ObjectiveMaps>? maps { get; set; } 
            public List<ObjectiveZones>? zones { get; set; } 
            public int? count { get; set; }
            public bool? foundInRaid { get; set; }

        }
    }

    public class ObjectiveZones
    {
        public string id { get; set; }
        public Map map { get; set; } 
        public Position position { get; set; } 

        public class Map
        {
            public string id { get; set; }
            public string normalizedName { get; set; }
            public string name { get; set; }
        }

        public class Position
        {
            public double? y { get; set; }
            public double? x { get; set; }
            public double? z { get; set; }
        }
    }


    public class ObjectiveMaps
    {
        public string id { get; set; }
        public string name { get; set; }
        public string normalizedName { get; set; }
    }


    public class TarkovQuestItems
    {
        public string id { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string normalizedName { get; set; }
    }

    public class LootContainer
    {
        public string id { get; set; }
        public string normalizedName { get; set; }
        public string name { get; set; }
    }

    public class QuestItems
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string ShortName { get; set; }

    }

    public class Tasks
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public List<Objective> Objectives { get; set; } = new List<Objective>();

        public class Objective
        {
            public string Description { get; set; }
            public string Type { get; set; }
            public string ID { get; set; }
            public List<ObjectiveMaps> Maps { get; set; } = new List<ObjectiveMaps>();
            public List<ObjectiveZones> ObjectiveZones { get; set; } = new List<ObjectiveZones>();
        }

    }

    public class TarkovDevResponse
    {
        public TarkovDevData data { get; set; }
    }

    public class TarkovDevData
    {
        public List<TarkovItem> items { get; set; }
        public List<TarkovTasks> tasks { get; set; } 
        public List<TarkovQuestItems> questItems { get; set; }
        public List<LootContainer> lootContainers { get; set; }
    }
    #endregion
}
