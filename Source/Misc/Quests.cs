using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public static class Quests
{
    public static Dictionary<string, string> QuestIdToName =
        new()
        {
            { "5936d90786f7742b1420ba5b", "Debut" },
            { "5936da9e86f7742d65037edf", "Checking" },
            { "59674cd986f7744ab26e32f2", "Shootout picnic" },
            { "59674eb386f774539f14813a", "Delivery from the past" },
            { "5967530a86f77462ba22226b", "Bad rep evidence" },
            { "59675d6c86f7740a842fc482", "Ice cream cones" },
            { "59675ea386f77414b32bded2", "Postman Pat Part 1" },
            { "596760e186f7741e11214d58", "Postman Pat Part 2" },
            { "5967725e86f774601a446662", "Shaking up teller" },
            { "5967733e86f774602332fc84", "Shortage" },
            { "59689ee586f7740d1570bbd5", "Sanitary Standards Part 1" },
            { "59689fbd86f7740d137ebfc4", "Operation Aquarius Part 1" },
            { "5968eb3186f7741dde183a4d", "Operation Aquarius Part 2" },
            { "5969f90786f77420d2328015", "Painkiller" },
            { "5969f9e986f7741dde183a50", "Pharmacist" },
            { "596a0e1686f7741ddf17dbee", "Supply plans" },
            { "596a101f86f7741ddb481582", "Kind of sabotage" },
            { "596a1e6c86f7741ddc2d3206", "General wares" },
            { "596a204686f774576d4c95de", "Sanitary Standards Part 2" },
            { "596a218586f77420d232807c", "Car repair" },
            { "596b36c586f77450d6045ad2", "Supplier" },
            { "596b43fb86f77457ca186186", "The Extortionist" },
            { "596b455186f77457cb50eccb", "Stirrup" },
            { "5979ed3886f77431307dc512", "Whats on the flash drive" },
            { "5979eee086f774311955e614", "Golden swag" },
            { "5979f8bb86f7743ec214c7a6", "Polikhim hobo" },
            { "5979f9ba86f7740f6c3fe9f2", "Chemical Part 1" },
            { "597a0b2986f77426d66c0633", "Chemical Part 2" },
            { "597a0e5786f77426d66c0636", "Chemical Part 3" },
            { "597a0f5686f774273b74f676", "Chemical Part 4" },
            { "597a160786f77477531d39d2", "Out of curiosity" },
            { "597a171586f77405ba6887d3", "Big customer" },
            { "59c124d686f774189b3c843f", "BP depot" },
            { "59c50a9e86f7745fef66f4ff", "The Punisher Part 1" },
            { "59c50c8886f7745fed3193bf", "The Punisher Part 2" },
            { "59c512ad86f7741f0d09de9b", "The Punisher Part 3" },
            { "59c9392986f7742f6923add2", "Trust regain" },
            { "59c93e8e86f7742a406989c4", "Loyalty buyout" },
            { "59ca1a6286f774509a270942", "No offence" },
            { "59ca264786f77445a80ed044", "The Punisher Part 4" },
            { "59ca29fb86f77445ab465c87", "The Punisher Part 5" },
            { "59ca2eb686f77445a80ed049", "The Punisher Part 6" },
            { "5a03153686f77442d90e2171", "Spa Tour Part 1" },
            { "5a03173786f77451cb427172", "Spa Tour Part 2" },
            { "5a0327ba86f77456b9154236", "Spa Tour Part 3" },
            { "5a03296886f774569778596a", "Spa Tour Part 4" },
            { "5a0449d586f77474e66227b7", "Spa Tour Part 5" },
            { "5a27b75b86f7742e97191958", "Fishing Gear" },
            { "5a27b7a786f774579c3eb376", "Tigr Safari" },
            { "5a27b7d686f77460d847e6a6", "Scrap Metal" },
            { "5a27b80086f774429a5d7e20", "Eagle Eye" },
            { "5a27b87686f77460de0252a8", "Humanitarian Supplies" },
            { "5a27b9de86f77464e5044585", "The Cult Part 1" },
            { "5a27ba1c86f77461ea5a3c56", "The Cult Part 2" },
            { "5a27ba9586f7741b543d8e85", "Spa Tour Part 6" },
            { "5a27bafb86f7741c73584017", "Spa Tour Part 7" },
            { "5a27bb1e86f7741f27621b7e", "Cargo X Part 1" },
            { "5a27bb3d86f77411ea361a21", "Cargo X Part 2" },
            { "5a27bb5986f7741dfb660900", "Cargo X Part 3" },
            { "5a27bb8386f7741c770d2d0a", "Wet Job Part 1" },
            { "5a27bbf886f774333a418eeb", "Wet Job Part 2" },
            { "5a27bc1586f7741f6d40fa2f", "Wet Job Part 3" },
            { "5a27bc3686f7741c73584026", "Wet Job Part 4" },
            { "5a27bc6986f7741c7358402b", "Wet Job Part 5" },
            { "5a27bc8586f7741b543d8ea4", "Wet Job Part 6" },
            { "5a27c99a86f7747d2c6bdd8e", "Friend from the West Part 1" },
            { "5a27d2af86f7744e1115b323", "Friend from the West Part 2" },
            { "5a5642ce86f77445c63c3419", "Hippocratic Vow" },
            { "5a68661a86f774500f48afb0", "Health Care Privacy Part 1" },
            { "5a68663e86f774501078f78a", "Health Care Privacy Part 2" },
            { "5a68665c86f774255929b4c7", "Health Care Privacy Part 3" },
            { "5a68667486f7742607157d28", "Health Care Privacy Part 4" },
            { "5a68669a86f774255929b4d4", "Health Care Privacy Part 5" },
            { "5ac23c6186f7741247042bad", "Gunsmith Part 1" },
            { "5ac2426c86f774138762edfe", "Gunsmith Part 2" },
            { "5ac2428686f77412450b42bf", "Gunsmith Part 3" },
            { "5ac242ab86f77412464f68b4", "Gunsmith Part 5" },
            { "5ac244c486f77413e12cf945", "Gunsmith Part 6" },
            { "5ac244eb86f7741356335af1", "Gunsmith Part 4" },
            { "5ac345dc86f774288030817f", "Farming Part 1" },
            { "5ac3460c86f7742880308185", "Farming Part 2" },
            { "5ac3462b86f7741d6118b983", "Farming Part 3" },
            { "5ac3464c86f7741d651d6877", "Farming Part 4" },
            { "5ac3467986f7741d6224abc2", "Signal Part 1" },
            { "5ac346a886f7744e1b083d67", "Signal Part 2" },
            { "5ac346cf86f7741d63233a02", "Signal Part 3" },
            { "5ac346e886f7741d6118b99b", "Signal Part 4" },
            { "5ac3475486f7741d6224abd3", "Bad habit" },
            { "5ac3477486f7741d651d6885", "Scout" },
            { "5ac3479086f7742880308199", "Insider" },
            { "5ae3267986f7742a413592fe", "Gunsmith Part 7" },
            { "5ae3270f86f77445ba41d4dd", "Gunsmith Part 8" },
            { "5ae3277186f7745973054106", "Gunsmith Part 9" },
            { "5ae327c886f7745c7b3f2f3f", "Gunsmith Part 10" },
            { "5ae3280386f7742a41359364", "Gunsmith Part 11" },
            { "5ae448a386f7744d3730fff0", "Only business" },
            { "5ae448bf86f7744d733e55ee", "Make ULTRA Great Again" },
            { "5ae448e586f7744dcf0c2a67", "Big sale" },
            { "5ae448f286f77448d73c0131", "The Blood of War" },
            { "5ae4490786f7744ca822adcc", "Dressed to kill" },
            { "5ae4493486f7744efa289417", "Database Part 1" },
            { "5ae4493d86f7744b8e15aa8f", "Database Part 2" },
            { "5ae4495086f77443c122bc40", "Sew it good Part 1" },
            { "5ae4495c86f7744e87761355", "Sew it good Part 2" },
            { "5ae4496986f774459e77beb6", "Sew it good Part 3" },
            { "5ae4497b86f7744cf402ed00", "Sew it good Part 4" },
            { "5ae4498786f7744bde357695", "The key to success" },
            { "5ae4499a86f77449783815db", "Charisma brings success" },
            { "5ae449a586f7744bde357696", "No fuss needed" },
            { "5ae449b386f77446d8741719", "Gratitude" },
            { "5ae449c386f7744bde357697", "Sales Night" },
            { "5ae449d986f774453a54a7e1", "Supervisor" },
            { "5b47749f86f7746c5d6a5fd4", "Gunsmith Part 12" },
            { "5b47799d86f7746c5d6a5fd8", "Gunsmith Part 13" },
            { "5b477b6f86f7747290681823", "Gunsmith Part 14" },
            { "5b477f7686f7744d1b23c4d2", "Gunsmith Part 15" },
            { "5b47825886f77468074618d3", "Gunsmith Part 16" },
            { "5b47876e86f7744d1c353205", "The Blood of War Part 2" },
            { "5b47891f86f7744d1b23c571", "Living high is not a crime" },
            { "5b478b1886f7744d1b23c57d", "Hot delivery" },
            { "5b478d0f86f7744d190d91b5", "Minibus" },
            { "5b478eca86f7744642012254", "Vitamins Part 1" },
            { "5b478ff486f7744d184ecbbf", "Vitamins Part 2" },
            { "5b47926a86f7747ccc057c15", "Informed means armed" },
            { "5b4794cb86f774598100d5d4", "Lend lease pt 1" },
            { "5b4795fb86f7745876267770", "Chumming" },
            { "5bc4776586f774512d07cf05", "The Tarkov shooter Part 1" },
            { "5bc479e586f7747f376c7da3", "The Tarkov shooter Part 2" },
            { "5bc47dbf86f7741ee74e93b9", "The Tarkov shooter Part 3" },
            { "5bc480a686f7741af0342e29", "The Tarkov shooter Part 4" },
            { "5bc4826c86f774106d22d88b", "The Tarkov shooter Part 5" },
            { "5bc4836986f7740c0152911c", "The Tarkov shooter Part 6" },
            { "5bc4856986f77454c317bea7", "The Tarkov shooter Part 7" },
            { "5bc4893c86f774626f5ebf3e", "The Tarkov shooter Part 8" },
            { "5c0bbaa886f7746941031d82", "Bullshit" },
            { "5c0bc91486f7746ab41857a2", "Silent caliber" },
            { "5c0bd01e86f7747cdd799e56", "Insomnia" },
            { "5c0bd94186f7747a727f09b2", "Test drive Part 1" },
            { "5c0bdb5286f774166e38eed4", "Flint" },
            { "5c0bde0986f77479cf22c2f8", "A Shooter Born in Heaven" },
            { "5c0be13186f7746f016734aa", "Psycho Sniper" },
            { "5c0be5fc86f774467a116593", "Private clinic" },
            { "5c0d0d5086f774363760aef2", "Athlete" },
            { "5c0d0f1886f77457b8210226", "Lend lease Part 2" },
            { "5c0d190cd09282029f5390d8", "Grenadier" },
            { "5c0d1c4cd0928202a02a6f5c", "Decontamination service" },
            { "5c0d4c12d09282029f539173", "Peacekeeping mission" },
            { "5c0d4e61d09282029f53920e", "The guide" },
            { "5c10f94386f774227172c572", "The Blood of War Part 3" },
            { "5c1128e386f7746565181106", "Fertilizers" },
            { "5c112d7e86f7740d6f647486", "Scavenger" },
            { "5c1141f386f77430ff393792", "Living high is not a crime Part 2" },
            { "5c1234c286f77406fa13baeb", "Setup" },
            { "5c12452c86f7744b83469073", "Perfect mediator" },
            { "5c139eb686f7747878361a6f", "Import" },
            { "5c51aac186f77432ea65c552", "Collector" },
            { "5d2495a886f77425cd51e403", "Introduction" },
            { "5d24b81486f77439c92d6ba8", "Acquaintance" },
            { "5d25aed386f77442734d25d2", "The survivalist path Unprotected but dangerous" },
            { "5d25b6be86f77444001e1b89", "The survivalist path Thrifty" },
            { "5d25bfd086f77442734d3007", "The survivalist path Zhivchik" },
            { "5d25c81b86f77443e625dd71", "The survivalist path Wounded beast" },
            { "5d25cf2686f77443e75488d4", "The survivalist path Tough guy" },
            { "5d25d2c186f77443e35162e5", "The survivalist path Cold blooded" },
            { "5d25e29d86f7740a22516326", "The survivalist path Eagle-owl" },
            { "5d25e2a986f77409dd5cdf2a", "The survivalist path Combat medic" },
            { "5d25e2b486f77409de05bba0", "Huntsman path Secured perimeter" },
            { "5d25e2c386f77443e7549029", "Huntsman path The trophy" },
            { "5d25e2cc86f77443e47ae019", "Huntsman path Woods cleaning" },
            { "5d25e2d886f77442734d335e", "Huntsman path Controller" },
            { "5d25e2e286f77444001e2e48", "Huntsman path Sell-out" },
            { "5d25e2ee86f77443e35162ea", "Huntsman path Woods keeper" },
            { "5d25e43786f7740a212217fa", "Huntsman path Justice" },
            { "5d25e44386f77409453bce7b", "Huntsman path Evil watchman" },
            { "5d25e44f86f77443e625e385", "Huntsman path Eraser Part 1" },
            { "5d25e45e86f77408251c4bfa", "Huntsman path Eraser Part 2" },
            { "5d25e46e86f77409453bce7c", "Ambulance" },
            { "5d25e48186f77443e625e386", "Courtesy visit" },
            { "5d25e48d86f77408251c4bfb", "Shady business" },
            { "5d25e4ad86f77443e625e387", "Nostalgia" },
            { "5d25e4b786f77408251c4bfc", "Fishing place" },
            { "5d25e4ca86f77409dd5cdf2c", "Hunting trip" },
            { "5d25e4d586f77443e625e388", "Reserv" },
            { "5d4bec3486f7743cac246665", "Regulated materials" },
            { "5d6fb2c086f77449da599c24", "An apple a day - keeps the doctor away" },
            { "5d6fbc2886f77449d825f9d3", "Mentor" },
            { "5dc53acb86f77469c740c893", "The stylish one" },
            { "5e381b0286f77420e3417a74", "Textile Part 1" },
            { "5e4d4ac186f774264f758336", "Textile - Part 2" },
            { "5eaaaa7c93afa0558f3b5a1c", "The survivalist path Junkie" },
            { "5eda19f0edce541157209cee", "Anesthesia" },
            { "5edab4b1218d181e29451435", "Huntsman path Sadist" },
            { "5edab736cc183c769d778bc2", "Colleagues Part 1" },
            { "5edaba7c0c502106f869bc02", "Colleagues Part 2" },
            { "5edabd13218d181e29451442", "Rigged game" },
            { "5edac020218d181e29451446", "Samples" },
            { "5edac34d0bb72a50635c2bfa", "Colleagues Part 3" },
            { "5edac63b930f5454f51e128b", "TerraGroup employee" },
            { "5ede55112c95834b583f052a", "Bunker Part 1" },
            { "5ede567cfa6dc072ce15d6e3", "Bunker Part 2" },
            { "5f04886a3937dc337a6b8238", "The chemistry closet" },
            { "5fd9fad9c1ce6b1a3b486d00", "Search Mission" },
            { "600302d73b897b11364cd161", "Hunter" },
            { "6086c852c945025d41566124", "Revision" },
            { "60896888e4a85c72ef3fa300", "Experience exchange" },
            { "60896b7bfa70fc097863b8f5", "Documents" },
            { "60896bca6ee58f38c417d4f2", "No place for renegades" },
            { "60896e28e4a85c72ef3fa301", "Disease history" },
            { "6089732b59b92115597ad789", "Surplus goods" },
            { "6089736efa70fc097863b8f6", "Back door" },
            { "6089743983426423753cd58a", "Safe corridor" },
            { "608974af4b05530f55550c21", "Inventory check" },
            { "608974d01a66564e74191fc0", "Fuel matter" },
            { "608a768d82e40b3c727fd17d", "Pest control" },
            { "60c0c018f7afb4354815096a", "Huntsman Path- Factory Chief" },
            { "60e71b62a0beca400d69efc4", "Escort" },
            { "60e71b9bbd90872cb85440f3", "Capturing Outposts" },
            { "60e71bb4e456d449cd47ca75", "Intimidator" },
            { "60e71c11d54b755a3b53eb65", "Night Sweep" },
            { "60e71c48c1bfa3050473b8e5", "Crisis" },
            { "60e71c9ad54b755a3b53eb66", "Mutual Interest" },
            { "60e71d23c1bfa3050473b8e6", "Calibration" },
            { "60e71dc0a94be721b065bbfc", "Long Line" },
            { "60e71dc67fcf9c556f325056", "Booze" },
            { "60e71e8ed54b755a3b53eb67", "Huntsman Path - Relentless" },
            { "60e729cf5698ee7b05057439", "Swift One" },
            { "60effd818b669d08a35bfad5", "The Choice" },
        };

    public static Dictionary<string, string> QuestMarkerNameToSimpleNameMap = new Dictionary<
        string,
        string
    >()
    {
        { "place_SALE_03_AVOKADO", "Avokado" },
        { "place_SALE_03_KOSTIN", "Kostin" },
        { "place_SALE_03_TREND", "Trend" },
        { "place_SALE_03_DINO", "Dino" },
        { "place_SALE_03_TOPBRAND", "Top Brand" },
        { "place_merch_21_1", "Minibus 1" },
        { "place_merch_21_2", "Minibus 2" },
        { "place_merch_21_3", "Minibus 3" },
        { "place_WARBLOOD_04_1", "Tank 1" },
        { "place_WARBLOOD_04_2", "Tank 2" },
        { "place_WARBLOOD_04_3", "Tank 3" },
        { "huntsman_024_1", "Chairman House" },
        { "huntsman_024_2", "Fisherman House" },
        { "huntsman_024_3", "Priest House" },
        { "huntsman_026", "Bay View Room" },
        { "FTank_1", "Fuel Tank 1" },
        { "FTank_2", "Fuel Tank 2" },
        { "place_meh_sanitar_room", "Sanitar's Office" },
        { "place_SIGNAL_01_1", "1st Signal" },
        { "place_SIGNAL_01_2", "2nd Signal" },
        { "place_SIGNAL_03_1", "Jammer 1" },
        { "place_SIGNAL_03_2", "Jammer 2" },
        { "place_SIGNAL_03_3", "Jammer 3" },
        { "place_peacemaker_010_3", "Artyom's Car" },
        { "place_peacemaker_010_2", "Fisher's Dwelling" },
        { "place_peacemaker_009_3_N1", "Terragroup Cargo" },
        { "place_peacemaker_009_2", "Resevoir Room" },
        { "place_peacemaker_007_1_N1", "Missing Informant" },
        { "place_peacemaker_007_2_N3", "Ritual Spot" },
        { "q_ny_find_christmas_tree_shorl", "Christmas Tree" },
        { "place_peacemaker_005_N2", "2nd Truck" },
        { "place_peacemaker_005_N1", "1st Truck" },
        { "place_peacemaker_004_N2", "2nd UAV" },
        { "place_peacemaker_004_N1", "1st UAV" },
        { "place_peacemaker_003_N3", "Tank 3" },
        { "place_peacemaker_003_N2", "Tank 2" },
        { "place_peacemaker_003_N1", "Tank 1" },
        { "place_peacemaker_001", "Secret Spot" },
        { "place_peacemaker_008_4_N2", "Left Wing Generator" },
        { "place_peacemaker_008_4_N1", "Right Wing Generator" },
        { "place_peacemaker_008_2_N2", "Safe Road" },
        { "place_peacemaker_008_2_N1", "Helicopter" },
        { "skier_022_area_1", "Medical 1" },
        { "skier_022_area_2", "Medical 2" },
        { "skier_022_area_3", "Medical 3" },
        { "ter_013_area_3", "Ambulance 3" },
        { "ter_013_area_2", "Ambulance 2" },
        { "ter_013_area_1", "Ambulance 1" },
        { "ter_023_area_3_1", "Pier Group" },
        { "ter_023_area_2_1", "Cottage Group" },
        { "ter_023_area_1_1", "HR Group" },
        { "prapor_022_area_3", "Trading 3" },
        { "prapor_022_area_2", "Trading 2" },
        { "prapor_022_area_1", "Trading 1" },
        { "place_SADOVOD_03", "Confiscate Warehouse" },
        { "place_peacemaker_002_N3", "Tigr 3" },
        { "place_peacemaker_002_N2", "Tigr 2" },
        { "place_peacemaker_002_N1", "Tigr 1" },
        { "vremyan_case", "Messenger Item" },
        { "room114", "Room 114" },
        { "room206_water", "Room 206" },
        { "fuel1", "Fuel 1" },
        { "fuel2", "Fuel 2" },
        { "fuel3", "Fuel 3" },
        { "fuel4", "Fuel 4" },
        { "gazel", "Transport" },
        { "room214", "Room 214" },
        { "bar_fuel3_1", "Fuel Stash 1" },
        { "bar_fuel3_2", "Fuel Stash 2" },
        { "bar_fuel3_3", "Fuel Stash 3" },
        { "pr_scout_col", "Convoy" },
        { "pr_scout_base", "USEC Camp" },
        { "ter_015_area_1", "Med Car" },
        { "bunker2", "Bunker" },
        { "huntsman_029", "Food Storage" },
        { "prapor_025_area_5", "Hermetic Door" },
        { "prapor_025_area_4", "White Pawn" },
        { "prapor_025_area_3", "Black Pawn" },
        { "prapor_025_area_2", "Black Bishop" },
        { "prapor_025_area_1", "White Bishop" },
        { "prapor_024_area_1", "Control Room" },
        { "prapor_024_area_2", "Underground Bunker" },
        { "baraholshik_fuel_area_2", "Fuel 1" },
        { "baraholshik_fuel_area_3", "Fuel 2" },
        { "baraholshik_arsenal_area_1", "W First Arsenal" },
        { "baraholshik_arsenal_area_3", "W Second Arsenal" },
        { "baraholshik_arsenal_area_4", "N First Arensal" },
        { "baraholshik_arsenal_area_5", "N Second Arsenal" },
        { "baraholshik_dejurniy_area_2", "Duty Room" },
        { "place_pacemaker_SCOUT_03", "Exit 3" },
        { "place_pacemaker_SCOUT_02", "Exit 2" },
        { "place_pacemaker_SCOUT_01", "Exit 1" },
        { "mechanik_exit_area_1", "Secret Exit" },
        { "tadeush_bmp2_area_check_13", "1st BMP" },
        { "tadeush_bmp2_area_mark_13", "1st BMP" },
        { "tadeush_bmp2_area_check_12", "2nd BMP" },
        { "tadeush_bmp2_area_mark_12", "2nd BMP" },
        { "tadeush_bmp2_area_check_11", "3rd BMP" },
        { "tadeush_bmp2_area_mark_11", "3rd BMP" },
        { "tadeush_bmp2_area_check_2", "4th BMP" },
        { "tadeush_bmp2_area_mark_2", "4th BMP" },
        { "tadeush_stryker_area_check_3", "1st LAV" },
        { "tadeush_stryker_area_mark_3", "1st LAV" },
        { "tadeush_stryker_area_check_4", "2nd LAV" },
        { "tadeush_t90_area_check_1", "T-90 Tank" },
        { "qlight_pr1_heli1_find", "Find Heli 1" },
        { "qlight_pr1_heli1_mark", "Mark Heli 1" },
        { "qlight_find_light_merchant", "Merchant Scout Location" },
        { "qlight_mark_vech1", "BDRM 1" },
        { "qlight_mark_vech2", "BDRM 2" },
        { "qlight_mark_vech3", "Stryker 1" },
        { "qlight_mark_vech4", "Stryker 2" },
        { "qlight_hunt_fr_find", "Jaegers Friend" },
        { "vaz", "Vaz" },
        { "dead_posylny", "Dead Posylny" },
        { "bomj", "Sleeping place" },
        { "extraction_zone_zibbo", "Extraction Zone Zibbo" },
        { "place_peacemaker_007_N1", "Ritual Spot" },
        { "place_skier_12_2", "Microwave" },
        { "place_skier_11_2", "WIFI Camera" },
        { "Q019_3", "SV-98 Trash Stash" },
        { "huntsman_020", "Huntsman Zone" },
        { "mech_41_1", "Mech 41" },
        { "mech_41_2", "Mech 41 2" },
        { "prapor_27_1", "Prapor Zone" },
        { "q_ny_find_christmas_tree_cust", "Christmas Tree" },
        { "q_ny_kill_christmas_guys_cust", "Christmas Zone" },
        { "q_ny_hide_christmas_tree_cust", "Christmas Tree" },
        { "q_ny_kill_christmas_guys_shorl", "Christmas Zone" },
        { "q_ny_hide_christmas_tree_shorl", "Christmas Tree" },
        { "prapor_27_3", "Prapor Zone 1" },
        { "prapor_27_4", "Prapor Zone 2" },
        { "q_ny_kill_christmas_guys_int", "Christmas Zone" },
        { "q_ny_find_christmas_tree_int", "Christmas Tree" },
        { "q_ny_hide_christmas_tree_int", "Christmas Tree Hide" },
        { "place_skier_11_3", "WIFI Camera" },
        { "place_skier_12_1", "BTR-80A Mattress" },
        { "place_merch_020_1", "ComTac Stash" },
        { "place_merch_020_2", "Gzhel-K Stash" },
        { "place_merch_022_1", "Place Ragman 1" },
        { "place_merch_022_2", "Place Ragman 2" },
        { "place_merch_022_3", "Place Ragman 3" },
        { "place_merch_022_4", "Place Ragman 4" },
        { "place_merch_022_5", "Place Ragman 5" },
        { "place_merch_022_6", "Place Ragman 6" },
        { "place_merch_022_7", "Place Ragman 7" },
        { "place_SADOVOD_01_1", "Control Board 1" },
        { "place_SADOVOD_01_2", "Control Board 2" },
        { "huntsman_013", "Huntsman Zone" },
        { "peace_027_area", "Sanitars Workplace" },
        { "q_ny_kill_christmas_guys_rez", "Christmas Zone" },
        { "q_ny_find_christmas_tree_rez", "Christmas Tree" },
        { "q_ny_hide_christmas_tree_rez", "Christmas Tree Hide" },
        { "tadeush_stryker_area_mark_4", "Stryker 4" },
        { "tadeush_tunguska_area_check_5", "Tunguska Check 5" },
        { "tadeush_tunguska_area_mark_5", "Tunguska Mark 5" },
        { "tadeush_tunguska_area_check_6", "Tunguska Check 6" },
        { "tadeush_tunguska_area_mark_6", "Tunguska Mark 6" },
        { "tadeush_tunguska_area_check_7", "Tunguska Check 7" },
        { "tadeush_tunguska_area_mark_7", "Tunguska Mark 7" },
        { "tadeush_tunguska_area_check_8", "Tunguska Check 8" },
        { "tadeush_tunguska_area_mark_8", "Tunguska Mark 8" },
        { "tadeush_tunguska_area_check_9", "Tunguska Check 9" },
        { "tadeush_tunguska_area_mark_9", "Tunguska Mark 9" },
        { "tadeush_tunguska_area_check_10", "Tunguska Check 10" },
        { "tadeush_tunguska_area_mark_10", "Tunguska Mark 10" },
        { "eger_barracks_area_1", "Barracks Area 1" },
        { "eger_barracks_area_2", "Barracks Area 2" },
        { "lijnik_storage_area_1", "Underground Warehouse" },
        { "baraholshik_fuel_area_1", "Fuel area 1" },
        { "baraholshik_fuel_area_4", "Fuel area 4" },
        { "prapor_HQ_area_check_1", "Command Bunker" },
        { "q_ny_kill_christmas_guys_wood", "Christmas Zone" },
        { "q_ny_find_christmas_tree_wood", "Christmas Tree" },
        { "q_ny_hide_christmas_tree_wood", "Christmas Tree Hide" },
        { "place_peacemaker_007_2_N2", "Ritual Spot 1" },
        { "place_THX_15", "Stash Ghost Balaclava" },
        { "place_skier_12_3", "Stash Wood Cabin" },
        { "place_skier_11_1", "Sawmill Dock WIFI" },
        { "huntsman_001", "Jaegers Camp" },
        { "huntsman_005_1", "ZB-016 Stash" },
        { "huntsman_005_2", "ZB-014 Stash" },
        { "place_peacemaker_007_2_N2_1", "Ritual Spot 2" },
        { "prapor_27_2", "Scav Base" },
        { "q_ny_kill_christmas_guys_light", "Christmas Zone" },
        { "q_ny_find_christmas_tree_light", "Christmas Tree" },
        { "q_ny_hide_christmas_tree_light", "Christmas Tree Hide" },
        { "qlight_fuel_blood", "Fuel" },
        { "qlight_pr1_heli2_kill", "Heli2 Kill" },
        { "qlight_pc1_ucot_kill", "UCOT Kill" },
        { "qlight_find_scav_group1", "Scav Group 1" },
        { "qlight_fuel_blood_bezovoz1", "Fuel 1" },
        { "qlight_fuel_blood_bezovoz2", "Fuel 2" },
        { "qlight_fuel_blood_bezovoz3", "Fuel 3" },
        { "qlight_find_crushed_heli", "Crushed Heli" },
        { "qlight_br_secure_road", "Secure Road" },
        { "qlight16_peace_terra", "Peace Terra" },
    };

    public static Dictionary<string, List<string>> QuestMap = new Dictionary<string, List<string>>()
    {
        {
            "60896888e4a85c72ef3fa300",
            new List<string>() { "609267a2bb3f46069c3e6c7d", "609267a2bb3f46069c3e6c7d" }
        },
        {
            "60896b7bfa70fc097863b8f5",
            new List<string>()
            {
                "60915994c49cf53e4772cc38",
                "60915994c49cf53e4772cc38",
                "60a3b6359c427533db36cf84",
                "60a3b6359c427533db36cf84",
                "60a3b65c27adf161da7b6e14",
                "60a3b65c27adf161da7b6e14"
            }
        },
        {
            "6089732b59b92115597ad789",
            new List<string>() { "60c080eb991ac167ad1c3ad4", "60c080eb991ac167ad1c3ad4" }
        },
        {
            "60896e28e4a85c72ef3fa301",
            new List<string>() { "608c22a003292f4ba43f8a1a", "60a3b5b05f84d429b732e934" }
        },
        {
            "5936d90786f7742b1420ba5b",
            new List<string>() { "54491c4f4bdc2db1078b4568" }
        },
        {
            "5936da9e86f7742d65037edf",
            new List<string>()
            {
                "5937fd0086f7742bf33fc198",
                "5937fd0086f7742bf33fc198",
                "5937ee6486f77408994ba448"
            }
        },
        {
            "59674eb386f774539f14813a",
            new List<string>() { "591092ef86f7747bb8703422" }
        },
        {
            "5967530a86f77462ba22226b",
            new List<string>()
            {
                "5938144586f77473c2087145",
                "5938188786f77474f723e87f",
                "5938188786f77474f723e87f"
            }
        },
        {
            "59675d6c86f7740a842fc482",
            new List<string>()
            {
                "55d482194bdc2d1d4e8b456b",
                "55d482194bdc2d1d4e8b456b",
                "591afe0186f77431bd616a11"
            }
        },
        {
            "59675ea386f77414b32bded2",
            new List<string>() { "591093bb86f7747caa7bb2ee" }
        },
        {
            "5967725e86f774601a446662",
            new List<string>() { "590dde5786f77405e71908b2", "590dde5786f77405e71908b2" }
        },
        {
            "59c50c8886f7745fed3193bf",
            new List<string>() { "572b7fa524597762b747ce82", "572b7fa524597762b747ce82" }
        },
        {
            "59ca1a6286f774509a270942",
            new List<string>() { "58d3db5386f77426186285a0", "58d3db5386f77426186285a0" }
        },
        {
            "59ca264786f77445a80ed044",
            new List<string>() { "57e26fc7245977162a14b800", "57e26fc7245977162a14b800" }
        },
        {
            "59ca29fb86f77445ab465c87",
            new List<string>()
            {
                "5644bd2b4bdc2d3b4c8b4572",
                "5644bd2b4bdc2d3b4c8b4572",
                "5447a9cd4bdc2dbd208b4567",
                "5447a9cd4bdc2dbd208b4567",
                "5448bd6b4bdc2dfc2f8b4569",
                "5448bd6b4bdc2dfc2f8b4569"
            }
        },
        {
            "59ca2eb686f77445a80ed049",
            new List<string>()
            {
                "59f32bb586f774757e1e8442",
                "59f32bb586f774757e1e8442",
                "59f32c3b86f77472a31742f0",
                "59f32c3b86f77472a31742f0"
            }
        },
        {
            "5d4bec3486f7743cac246665",
            new List<string>()
            {
                "5d03794386f77420415576f5",
                "5d03794386f77420415576f5",
                "5d0379a886f77420407aa271",
                "5d0379a886f77420407aa271"
            }
        },
        {
            "596760e186f7741e11214d58",
            new List<string>() { "591093bb86f7747caa7bb2ee" }
        },
        {
            "5967733e86f774602332fc84",
            new List<string>() { "544fb45d4bdc2dee738b4568", "544fb45d4bdc2dee738b4568" }
        },
        {
            "59689ee586f7740d1570bbd5",
            new List<string>() { "590a3efd86f77437d351a25b", "590a3efd86f77437d351a25b" }
        },
        {
            "5969f90786f77420d2328015",
            new List<string>() { "544fb3f34bdc2d03748b456a", "544fb3f34bdc2d03748b456a" }
        },
        {
            "5969f9e986f7741dde183a50",
            new List<string>() { "5910922b86f7747d96753483", "5910922b86f7747d96753483" }
        },
        {
            "596a0e1686f7741ddf17dbee",
            new List<string>() { "5938878586f7741b797c562f", "5938878586f7741b797c562f" }
        },
        {
            "596a1e6c86f7741ddc2d3206",
            new List<string>() { "57347d7224597744596b4e72", "57347d7224597744596b4e72" }
        },
        {
            "596a204686f774576d4c95de",
            new List<string>() { "590a3efd86f77437d351a25b", "590a3efd86f77437d351a25b" }
        },
        {
            "596a218586f77420d232807c",
            new List<string>()
            {
                "5733279d245977289b77ec24",
                "590a3c0a86f774385a33c450",
                "5733279d245977289b77ec24",
                "590a3c0a86f774385a33c450"
            }
        },
        {
            "59c9392986f7742f6923add2",
            new List<string>()
            {
                "593aa4be86f77457f56379f8",
                "591afe0186f77431bd616a11",
                "5913915886f774123603c392",
                "5913877a86f774432f15d444",
                "593aa4be86f77457f56379f8",
                "591afe0186f77431bd616a11",
                "5913915886f774123603c392",
                "5913877a86f774432f15d444"
            }
        },
        {
            "5a5642ce86f77445c63c3419",
            new List<string>() { "5696686a4bdc2da3298b456a" }
        },
        {
            "5a68663e86f774501078f78a",
            new List<string>() { "5a6860d886f77411cd3a9e47", "5a6860d886f77411cd3a9e47" }
        },
        {
            "5a68665c86f774255929b4c7",
            new List<string>() { "5a687e7886f7740c4a5133fb", "5a687e7886f7740c4a5133fb" }
        },
        {
            "5c0be5fc86f774467a116593",
            new List<string>() { "5af0534a86f7743b6f354284", "5c0530ee86f774697952d952" }
        },
        {
            "5d6fb2c086f77449da599c24",
            new List<string>() { "5449016a4bdc2d6f028b456f" }
        },
        {
            "5edaba7c0c502106f869bc02",
            new List<string>()
            {
                "5efdafc1e70b5e33f86de058",
                "5efdafc1e70b5e33f86de058",
                "5efdaf6de6a30218ed211a48",
                "5efdaf6de6a30218ed211a48"
            }
        },
        {
            "5edac34d0bb72a50635c2bfa",
            new List<string>()
            {
                "5c1d0c5f86f7744bb2683cf0",
                "5c1d0c5f86f7744bb2683cf0",
                "5c1d0dc586f7744baf2e7b79",
                "5c1d0dc586f7744baf2e7b79",
                "5ed515f6915ec335206e4152",
                "5ed515f6915ec335206e4152",
                "5ed515c8d380ab312177c0fa",
                "5ed515c8d380ab312177c0fa"
            }
        },
        {
            "5edac63b930f5454f51e128b",
            new List<string>() { "5eff135be0d3331e9d282b7b", "5eff135be0d3331e9d282b7b" }
        },
        {
            "5c51aac186f77432ea65c552",
            new List<string>()
            {
                "5bc9c377d4351e3bac12251b",
                "5bc9c377d4351e3bac12251b",
                "5bc9c1e2d4351e00367fbcf0",
                "5bc9c1e2d4351e00367fbcf0",
                "5bc9c049d4351e44f824d360",
                "5bc9c049d4351e44f824d360",
                "5bc9b355d4351e6d1509862a",
                "5bc9b355d4351e6d1509862a",
                "5bc9bc53d4351e00367fbcee",
                "5bc9bc53d4351e00367fbcee",
                "5bc9bdb8d4351e003562b8a1",
                "5bc9bdb8d4351e003562b8a1",
                "5bc9b9ecd4351e3bac122519",
                "5bc9b9ecd4351e3bac122519",
                "5bc9b720d4351e450201234b",
                "5bc9b720d4351e450201234b",
                "5bc9b156d4351e00367fbce9",
                "5bc9b156d4351e00367fbce9",
                "5bc9c29cd4351e003562b8a3",
                "5bc9c29cd4351e003562b8a3",
                "5bd073a586f7747e6f135799",
                "5bd073a586f7747e6f135799",
                "5bd073c986f7747f627e796c",
                "5bd073c986f7747f627e796c",
                "5e54f62086f774219b0f1937",
                "5e54f62086f774219b0f1937",
                "5e54f79686f7744022011103",
                "5e54f79686f7744022011103",
                "5e54f76986f7740366043752",
                "5e54f76986f7740366043752",
                "5e54f6af86f7742199090bf3",
                "5e54f6af86f7742199090bf3",
                "5bc9be8fd4351e00334cae6e",
                "5bc9be8fd4351e00334cae6e",
                "5f745ee30acaeb0d490d8c5b",
                "5f745ee30acaeb0d490d8c5b"
            }
        },
        {
            "596a101f86f7741ddb481582",
            new List<string>() { "5938878586f7741b797c562f" }
        },
        {
            "596b36c586f77450d6045ad2",
            new List<string>() { "59e7635f86f7742cbf2c1095", "5a38e6bac4a2826c6e06d79b" }
        },
        {
            "596b43fb86f77457ca186186",
            new List<string>() { "593965cf86f774087a77e1b6", "593965cf86f774087a77e1b6" }
        },
        {
            "5979ed3886f77431307dc512",
            new List<string>() { "590c621186f774138d11ea29", "590c621186f774138d11ea29" }
        },
        {
            "5979eee086f774311955e614",
            new List<string>() { "5939a00786f7742fe8132936" }
        },
        {
            "5979f9ba86f7740f6c3fe9f2",
            new List<string>()
            {
                "5939e5a786f77461f11c0098",
                "5939e5a786f77461f11c0098",
                "5780cfa52459777dfb276eb1",
                "5780cfa52459777dfb276eb1"
            }
        },
        {
            "597a0b2986f77426d66c0633",
            new List<string>()
            {
                "5939e9b286f77462a709572c",
                "5939e9b286f77462a709572c",
                "590c62a386f77412b0130255",
                "590c62a386f77412b0130255"
            }
        },
        {
            "597a0e5786f77426d66c0636",
            new List<string>() { "593a87af86f774122f54a951", "593a87af86f774122f54a951" }
        },
        {
            "59c93e8e86f7742a406989c4",
            new List<string>() { "5449016a4bdc2d6f028b456f" }
        },
        {
            "5a27c99a86f7747d2c6bdd8e",
            new List<string>() { "59f32c3b86f77472a31742f0", "59f32c3b86f77472a31742f0" }
        },
        {
            "5a27d2af86f7744e1115b323",
            new List<string>() { "5696686a4bdc2da3298b456a" }
        },
        {
            "5b478eca86f7744642012254",
            new List<string>()
            {
                "5b43237186f7742f3a4ab252",
                "5b43237186f7742f3a4ab252",
                "5b4c81a086f77417d26be63f",
                "5b4c81a086f77417d26be63f",
                "5b4c81bd86f77418a75ae159",
                "5b4c81bd86f77418a75ae159"
            }
        },
        {
            "5b478ff486f7744d184ecbbf",
            new List<string>()
            {
                "59e7715586f7742ee5789605",
                "59e7715586f7742ee5789605",
                "5b4335ba86f7744d2837a264",
                "5b4335ba86f7744d2837a264"
            }
        },
        {
            "5b4794cb86f774598100d5d4",
            new List<string>()
            {
                "5af04c0b86f774138708f78e",
                "5af04c0b86f774138708f78e",
                "5b4c72b386f7745b453af9c0",
                "5b4c72b386f7745b453af9c0",
                "5b4c72c686f77462ac37e907",
                "5b4c72c686f77462ac37e907",
                "5af04e0a86f7743a532b79e2",
                "5af04e0a86f7743a532b79e2",
                "5b4c72fb86f7745cef1cffc5",
                "5b4c72fb86f7745cef1cffc5"
            }
        },
        {
            "5c0bbaa886f7746941031d82",
            new List<string>() { "5c12301c86f77419522ba7e4" }
        },
        {
            "5a0327ba86f77456b9154236",
            new List<string>()
            {
                "590c5bbd86f774785762df04",
                "590c5bbd86f774785762df04",
                "59e358a886f7741776641ac3",
                "59e358a886f7741776641ac3",
                "59e35cbb86f7741778269d83",
                "59e35cbb86f7741778269d83",
                "59e3556c86f7741776641ac2",
                "59e3556c86f7741776641ac2"
            }
        },
        {
            "5a0449d586f77474e66227b7",
            new List<string>() { "5a0448bc86f774736f14efa8", "5a0448bc86f774736f14efa8" }
        },
        {
            "5a27b80086f774429a5d7e20",
            new List<string>()
            {
                "5a294d7c86f7740651337cf9",
                "5a294d7c86f7740651337cf9",
                "5a294d8486f774068638cd93",
                "5a294d8486f774068638cd93"
            }
        },
        {
            "5a27b87686f77460de0252a8",
            new List<string>() { "590c5f0d86f77413997acfab", "590c5f0d86f77413997acfab" }
        },
        {
            "5a27ba9586f7741b543d8e85",
            new List<string>() { "5696686a4bdc2da3298b456a" }
        },
        {
            "5a27bafb86f7741c73584017",
            new List<string>()
            {
                "544fb3f34bdc2d03748b456a",
                "544fb3f34bdc2d03748b456a",
                "59faf98186f774067b6be103",
                "59faf98186f774067b6be103",
                "59e35cbb86f7741778269d83",
                "59e35cbb86f7741778269d83",
                "59fafb5d86f774067a6f2084",
                "59fafb5d86f774067a6f2084"
            }
        },
        {
            "5a27bb1e86f7741f27621b7e",
            new List<string>() { "5a29284f86f77463ef3db363", "5a29284f86f77463ef3db363" }
        },
        {
            "5a27bb3d86f77411ea361a21",
            new List<string>() { "5939e5a786f77461f11c0098", "5939e5a786f77461f11c0098" }
        },
        {
            "5a27bc3686f7741c73584026",
            new List<string>() { "5a29357286f77409c705e025", "5a29357286f77409c705e025" }
        },
        {
            "5a27bc6986f7741c7358402b",
            new List<string>() { "5a29276886f77435ed1b117c", "5a29276886f77435ed1b117c" }
        },
        {
            "5c0d0f1886f77457b8210226",
            new List<string>()
            {
                "5c05308086f7746b2101e90b",
                "5c05308086f7746b2101e90b",
                "5c052f6886f7746b1e3db148",
                "5c052f6886f7746b1e3db148"
            }
        },
        {
            "5d6fbc2886f77449d825f9d3",
            new List<string>() { "569668774bdc2da2298b4568" }
        },
        {
            "5edac020218d181e29451446",
            new List<string>()
            {
                "5ed51652f6c34d2cc26336a1",
                "5ed51652f6c34d2cc26336a1",
                "5ed5166ad380ab312177c100",
                "5ed5166ad380ab312177c100",
                "5ed5160a87bb8443d10680b5",
                "5ed5160a87bb8443d10680b5",
                "5ed515f6915ec335206e4152",
                "5ed515f6915ec335206e4152",
                "5ed515ece452db0eb56fc028",
                "5ed515ece452db0eb56fc028",
                "5ed515e03a40a50460332579",
                "5ed515e03a40a50460332579",
                "5ed515c8d380ab312177c0fa",
                "5ed515c8d380ab312177c0fa"
            }
        },
        {
            "5ac3460c86f7742880308185",
            new List<string>()
            {
                "59e36c6f86f774176c10a2a7",
                "59e36c6f86f774176c10a2a7",
                "57347cd0245977445a2d6ff1",
                "57347cd0245977445a2d6ff1",
                "590a3b0486f7743954552bdb",
                "590a3b0486f7743954552bdb"
            }
        },
        {
            "5ac3462b86f7741d6118b983",
            new List<string>() { "5ac620eb86f7743a8e6e0da0", "5ac620eb86f7743a8e6e0da0" }
        },
        {
            "5ac3464c86f7741d651d6877",
            new List<string>()
            {
                "57347ca924597744596b4e71",
                "57347ca924597744596b4e71",
                "5734779624597737e04bf329",
                "5734779624597737e04bf329"
            }
        },
        {
            "5ac346a886f7744e1b083d67",
            new List<string>()
            {
                "573477e124597737dd42e191",
                "573477e124597737dd42e191",
                "590a358486f77429692b2790",
                "590a358486f77429692b2790",
                "590a3b0486f7743954552bdb",
                "590a3b0486f7743954552bdb",
                "56742c324bdc2d150f8b456d",
                "56742c324bdc2d150f8b456d"
            }
        },
        {
            "5ac3475486f7741d6224abd3",
            new List<string>()
            {
                "573476d324597737da2adc13",
                "573476d324597737da2adc13",
                "5734770f24597738025ee254",
                "5734770f24597738025ee254",
                "573476f124597737e04bf328",
                "573476f124597737e04bf328"
            }
        },
        {
            "5c1128e386f7746565181106",
            new List<string>()
            {
                "5c06779c86f77426e00dd782",
                "5c06782b86f77426df5407d2",
                "5c06779c86f77426e00dd782",
                "5c06782b86f77426df5407d2"
            }
        },
        {
            "5c139eb686f7747878361a6f",
            new List<string>()
            {
                "5c052fb986f7746b2101e909",
                "5c052fb986f7746b2101e909",
                "5c05300686f7746dce784e5d",
                "5c05300686f7746dce784e5d"
            }
        },
        {
            "5d2495a886f77425cd51e403",
            new List<string>() { "5d3ec50586f774183a607442", "5d3ec50586f774183a607442" }
        },
        {
            "5ae4490786f7744ca822adcc",
            new List<string>() { "59e7708286f7742cbd762753", "5aa2b9ede5b5b000137b758b" }
        },
        {
            "5ae4493486f7744efa289417",
            new List<string>()
            {
                "5ae9a0dd86f7742e5f454a05",
                "5ae9a0dd86f7742e5f454a05",
                "5ae9a18586f7746e381e16a3",
                "5ae9a18586f7746e381e16a3",
                "5ae9a1b886f77404c8537c62",
                "5ae9a1b886f77404c8537c62"
            }
        },
        {
            "5ae4493d86f7744b8e15aa8f",
            new List<string>() { "5ae9a25386f7746dd946e6d9", "5ae9a25386f7746dd946e6d9" }
        },
        {
            "5ae4495086f77443c122bc40",
            new List<string>()
            {
                "5ab8f20c86f7745cdb629fb2",
                "5ab8f20c86f7745cdb629fb2",
                "59e763f286f7742ee57895da",
                "59e763f286f7742ee57895da"
            }
        },
        {
            "5ae4495c86f7744e87761355",
            new List<string>()
            {
                "5ab8e79e86f7742d8b372e78",
                "5ab8e79e86f7742d8b372e78",
                "5ab8e79e86f7742d8b372e78",
                "5ab8e79e86f7742d8b372e78"
            }
        },
        {
            "5ae4496986f774459e77beb6",
            new List<string>()
            {
                "545cdb794bdc2d3a198b456a",
                "545cdb794bdc2d3a198b456a",
                "545cdb794bdc2d3a198b456a",
                "545cdb794bdc2d3a198b456a"
            }
        },
        {
            "5ae4497b86f7744cf402ed00",
            new List<string>()
            {
                "59e7643b86f7742cbf2c109a",
                "59e7643b86f7742cbf2c109a",
                "5648a69d4bdc2ded0b8b457b",
                "5648a69d4bdc2ded0b8b457b"
            }
        },
        {
            "5ae4498786f7744bde357695",
            new List<string>()
            {
                "5ae9a3f586f7740aab00e4e6",
                "5ae9a3f586f7740aab00e4e6",
                "5ae9a4fc86f7746e381e1753",
                "5ae9a4fc86f7746e381e1753"
            }
        },
        {
            "5ae449d986f774453a54a7e1",
            new List<string>() { "5ad7247386f7747487619dc3", "5ad7247386f7747487619dc3" }
        },
        {
            "5b47876e86f7744d1c353205",
            new List<string>() { "5b43575a86f77424f443fe62", "5b43575a86f77424f443fe62" }
        },
        {
            "5b47891f86f7744d1b23c571",
            new List<string>()
            {
                "59e3639286f7741777737013",
                "59e3639286f7741777737013",
                "573478bc24597738002c6175",
                "573478bc24597738002c6175",
                "59e3658a86f7741776641ac4",
                "59e3658a86f7741776641ac4",
                "59faf7ca86f7740dbe19f6c2",
                "59faf7ca86f7740dbe19f6c2"
            }
        },
        {
            "5c1141f386f77430ff393792",
            new List<string>()
            {
                "590de71386f774347051a052",
                "590de7e986f7741b096e5f32",
                "590de71386f774347051a052",
                "590de7e986f7741b096e5f32"
            }
        },
        {
            "5e381b0286f77420e3417a74",
            new List<string>()
            {
                "5e2af4d286f7746d4159f07a",
                "5e2af4a786f7746d3f3c3400",
                "5c12688486f77426843c7d32",
                "5e2af4d286f7746d4159f07a",
                "5e2af4a786f7746d3f3c3400",
                "5c12688486f77426843c7d32"
            }
        },
        {
            "5e383a6386f77465910ce1f3",
            new List<string>()
            {
                "5e2af4d286f7746d4159f07a",
                "5e2af4d286f7746d4159f07a",
                "5e2af4a786f7746d3f3c3400",
                "5e2af4a786f7746d3f3c3400",
                "5c12688486f77426843c7d32",
                "5c12688486f77426843c7d32"
            }
        },
        {
            "5e4d515e86f77438b2195244",
            new List<string>()
            {
                "5e2af47786f7746d404f3aaa",
                "5e2af47786f7746d404f3aaa",
                "5e2af41e86f774755a234b67",
                "5e2af41e86f774755a234b67",
                "5e2af29386f7746d4159f077",
                "5e2af29386f7746d4159f077"
            }
        },
        {
            "5e4d4ac186f774264f758336",
            new List<string>()
            {
                "5e2af47786f7746d404f3aaa",
                "5e2af47786f7746d404f3aaa",
                "5e2af41e86f774755a234b67",
                "5e2af41e86f774755a234b67",
                "5e2af29386f7746d4159f077",
                "5e2af29386f7746d4159f077"
            }
        },
        {
            "5d24b81486f77439c92d6ba8",
            new List<string>()
            {
                "590c5d4b86f774784e1b9c45",
                "656df4fec921ad01000481a2",
                "57347da92459774491567cf5"
            }
        },
        {
            "5d25e2c386f77443e7549029",
            new List<string>() { "5b3b713c5acfc4330140bd8d", "5b3b713c5acfc4330140bd8d" }
        },
        {
            "5d25e2e286f77444001e2e48",
            new List<string>() { "5c0e874186f7745dc7616606", "5c0e874186f7745dc7616606" }
        },
        {
            "5d25e2ee86f77443e35162ea",
            new List<string>() { "5d08d21286f774736e7c94c3", "5d08d21286f774736e7c94c3" }
        },
        {
            "5d25e46e86f77409453bce7c",
            new List<string>()
            {
                "5c052e6986f7746b207bc3c9",
                "5c052e6986f7746b207bc3c9",
                "5d02778e86f774203e7dedbe",
                "5d02778e86f774203e7dedbe"
            }
        },
        {
            "5d25e48d86f77408251c4bfb",
            new List<string>() { "590c621186f774138d11ea29", "590c621186f774138d11ea29" }
        },
        {
            "5d25e4ad86f77443e625e387",
            new List<string>() { "5d357d6b86f7745b606e3508", "5d357d6b86f7745b606e3508" }
        },
        {
            "5d25e4b786f77408251c4bfc",
            new List<string>() { "5c94bbff86f7747ee735c08f", "5c94bbff86f7747ee735c08f" }
        },
        {
            "60c0c018f7afb4354815096a",
            new List<string>() { "60a7acf20c5cb24b01346648", }
        },
        {
            "60e71c11d54b755a3b53eb65",
            new List<string>() { "5fc64ea372b0dd78d51159dc", }
        },
        {
            "60e71c48c1bfa3050473b8e5",
            new List<string>()
            {
                "5c052e6986f7746b207bc3c9",
                "5af0534a86f7743b6f354284",
                "5c0530ee86f774697952d952",
                "5d1b3a5d86f774252167ba22"
            }
        },
        {
            "60e71dc67fcf9c556f325056",
            new List<string>()
            {
                "5d40407c86f774318526545a",
                "5d403f9186f7743cac3f229b",
                "5d1b33a686f7742523398398",
            }
        },
    };
}
