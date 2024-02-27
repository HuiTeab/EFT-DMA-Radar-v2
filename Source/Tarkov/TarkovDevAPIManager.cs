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
                                    items{
                                        id
                                        name
                                        shortName
                                        normalizedName
                                        basePrice
                                        avg24hPrice
                                        low24hPrice
                                        high24hPrice
                                        sellFor{
                                            price
                                            vendor{
                                                normalizedName
                                            }
                                        }
                                        category {
                                            id
                                            name
                                            normalizedName
                                        }
                                        weight
                                        categories{
                                            id
                                            name
                                            normalizedName
                                        }
                                    }
                                    tasks {
                                        id
                                        name
                                        kappaRequired
                                        objectives {
                                            optional
                                            description
                                            type
                                            id
                                        }
                                    }
                                    questItems{
                                        id
                                        shortName
                                        name
                                        normalizedName
                                    }
                                    lootContainers{
                                        id
                                        normalizedName
                                        name
                                    }
                                }
                                "
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
                                Label = $"[{FormatNumber(value)}] {tarkovItem.shortName}",
                                Item = tarkovItem
                            }
                        );
                    }
                }
                if (item.data?.tasks != null)
                {
                    foreach (var task in item.data.tasks)
                    {
                        var taskObjectives = new List<Objective>();
                        foreach (var objective in task.objectives)
                        {
                            taskObjectives.Add(new Objective
                            {
                                optional = objective.optional,
                                type = objective.type,
                                id = objective.id
                            });
                        }

                        allTasks.TryAdd(
                            task.id,
                            new Tasks()
                            {
                                Name = task.name,
                                ID = task.id,
                                KappaRequired = task.kappaRequired,
                                Objectives = taskObjectives
                            }
                        );
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
        public double weight { get; set; } // Assuming weight could be in decimals
        public List<Category> categories { get; set; } = new List<Category>();
        public List<VendorPrice> sellFor { get; set; } = new List<VendorPrice>(); // Assuming multiple vendors

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
        public bool kappaRequired { get; set; }
        public List<Objective> objectives { get; set; } = new List<Objective>();

        public class Objective
        {
            public bool optional { get; set; }
            public string description { get; set; }
            public string type { get; set; }
            public string id { get; set; }
        }
    }

    public class TarkovQuestItems
    {
        public string id { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string normalizedName { get; set; }
    }

    public class TarkovDevResponse
    {
        public TarkovDevData data { get; set; }
    }

    public class TarkovDevData
    {
        public List<TarkovItem> items { get; set; }
        public List<TarkovTasks> tasks { get; set; } // Reflects the list of tasks
        public List<TarkovQuestItems> questItems { get; set; }
        public List<LootContainer> lootContainers { get; set; }
    }

    public class LootContainer
    {
        public string id { get; set; }
        public string normalizedName { get; set; }
        public string name { get; set; }
    }

    public class TarkovTask
    {
        public List<Objective> objectives { get; set; } = new List<Objective>();
    }

    public class Objective
    {
        public bool optional { get; set; }
        public string type { get; set; }
        public string id { get; set; }
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
        public bool KappaRequired { get; set; }
        public string ID { get; set; }
        public List<Objective> Objectives { get; set; } = new List<Objective>();
    }
    #endregion
}
