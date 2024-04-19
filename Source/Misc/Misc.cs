﻿using System.Text.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;
using SkiaSharp;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Runtime.Intrinsics;
using eft_dma_radar.Source.Misc;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace eft_dma_radar
{
    // Small & Miscellaneous Classes/Enums Go here

    #region Program Classes
    /// <summary>
    /// Custom Debug Stopwatch class to measure performance.
    /// </summary>
    public class DebugStopwatch
    {
        private readonly Stopwatch _sw;
        private readonly string _name;

        /// <summary>
        /// Constructor. Starts stopwatch.
        /// </summary>
        /// <param name="name">(Optional) Name of stopwatch.</param>
        public DebugStopwatch(string name = null)
        {
            _name = name;
            _sw = new Stopwatch();
            _sw.Start();
        }

        /// <summary>
        /// End stopwatch and display result to Debug Output.
        /// </summary>
        public void Stop()
        {
            _sw.Stop();
            TimeSpan ts = _sw.Elapsed;
            Debug.WriteLine($"{_name} Stopwatch Runtime: {ts.Ticks} ticks");
        }
    }
    /// <summary>
    /// Global Program Configuration (Config.json)
    /// </summary>
    public class Config
    {
        [JsonIgnore]
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };
        [JsonIgnore]
        private static readonly object _lock = new();
        /// <summary>
        /// Enables Vertical Sync in GUI Render.
        /// </summary>
        [JsonPropertyName("vsyncEnabled")]
        public bool Vsync { get; set; }
        /// <summary>
        /// Player/Teammates Aimline Length (Max: 1000)
        /// </summary>
        [JsonPropertyName("playerAimLine")]
        public int PlayerAimLineLength { get; set; }
        /// <summary>
        /// Last used 'Zoom' level.
        /// </summary>
        [JsonPropertyName("defaultZoom")]
        public int DefaultZoom { get; set; }
        /// <summary>
        /// UI Scale Value (50-200 , default: 100)
        /// </summary>
        [JsonPropertyName("uiScale")]
        public int UIScale { get; set; }
        /// <summary>
        /// Enables loot output on map.
        /// </summary>
        [JsonPropertyName("lootEnabled")]
        public bool LootEnabled { get; set; }
        /// <summary>
        /// Enables Quest Helper output on map.
        /// </summary>
        [JsonPropertyName("questHelperEnabled")]
        public bool QuestHelperEnabled { get; set; }
        /// <summary>
        /// Enables Aimview window in Main Window.
        /// </summary>
        [JsonPropertyName("aimviewEnabled")]
        public bool AimviewEnabled { get; set; }
        /// <summary>
        /// Hides player names & extended player info in Radar GUI.
        /// </summary>
        [JsonPropertyName("hideNames")]
        public bool HideNames { get; set; }
        /// <summary>
        /// Enables/disables showing non-important loot
        /// </summary>
        [JsonPropertyName("importantLootOnly")]
        public bool ImportantLootOnly { get; set; }
        /// <summary>
        /// Enables/disables showing loot value
        /// </summary>
        [JsonPropertyName("hideLootValue")]
        public bool HideLootValue { get; set; }
        /// <summary>
        /// Primary Teammate ACCT ID (for secondary Aimview)
        /// </summary>
        [JsonPropertyName("primaryTeammateAcctId")]
        public string PrimaryTeammateId { get; set; }
        /// <summary>
        /// Enables logging output to 'log.txt'
        /// </summary>
        [JsonPropertyName("loggingEnabled")]
        public bool LoggingEnabled { get; set; }
        /// <summary>
        /// Max game distance to render targets in Aimview, 
        /// and to display dynamic aimlines between two players.
        /// </summary>
        [JsonPropertyName("maxDistance")]
        public float MaxDistance { get; set; }
        /// <summary>
        /// 'Field of View' in degrees to display targets in the Aimview window.
        /// </summary>
        [JsonPropertyName("aimviewFOV")]
        public float AimViewFOV { get; set; }
        /// <summary>
        /// Minimum loot value (rubles) to display 'normal loot' on map.
        /// </summary>
        [JsonPropertyName("minLootValue")]
        public int MinLootValue { get; set; }
        /// <summary>
        /// Minimum loot value (rubles) to display 'important loot' on map.
        /// </summary>
        [JsonPropertyName("minImportantLootValue")]
        public int MinImportantLootValue { get; set; }

        /// <summary>
        /// Enables / disables thermal vision.
        /// </summary>
        [JsonPropertyName("thermalVisionEnabled")]
        public bool ThermalVisionEnabled { get; set; }

        /// <summary>
        /// Enables / disables night vision.
        /// </summary>
        [JsonPropertyName("nightVisionEnabled")]
        public bool NightVisionEnabled { get; set; }

        /// <summary>
        /// Enables / disables thermal optic vision.
        /// </summary>
        [JsonPropertyName("opticThermalVisionEnabled")]
        public bool OpticThermalVisionEnabled { get; set; }

        /// <summary>
        /// Enables / disables no visor.
        /// </summary>
        [JsonPropertyName("noVisorEnabled")]
        public bool NoVisorEnabled { get; set; }

        /// <summary>
        /// Enables / disables no recoil.
        /// </summary>
        [JsonPropertyName("noRecoilSwayEnabled")]
        public bool NoRecoilSwayEnabled { get; set; }

        /// <summary>
        /// Enables / disables max / infinite stamina.
        /// </summary>
        [JsonPropertyName("maxStaminaEnabled")]
        public bool MaxStaminaEnabled { get; set; }

        /// <summary>
        /// Enables / disables double search.
        /// </summary>
        [JsonPropertyName("doubleSearchEnabled")]
        public bool DoubleSearchEnabled { get; set; }

        /// <summary>
        /// Enables / disables jump power modification
        /// </summary>
        [JsonPropertyName("jumpPowerEnabled")]
        public bool JumpPowerEnabled { get; set; }

        /// <summary>
        /// Changes jump power strength
        /// </summary>
        [JsonPropertyName("jumpPowerStrength")]
        public int JumpPowerStrength { get; set; }

        /// <summary>
        /// Enables / disables throw power modification.
        /// </summary>
        [JsonPropertyName("throwPowerEnabled")]
        public bool ThrowPowerEnabled { get; set; }

        /// <summary>
        /// Changes throw power strength.
        /// </summary>
        [JsonPropertyName("throwPowerStrength")]
        public int ThrowPowerStrength { get; set; }

        /// <summary>
        /// Enables / disables faster mag drills.
        /// </summary>
        [JsonPropertyName("magDrillsEnabled")]
        public bool MagDrillsEnabled { get; set; }

        /// <summary>
        /// Changes mag load/unload speed
        /// </summary>
        [JsonPropertyName("magDrillSpeed")]
        public int MagDrillSpeed { get; set; }

        /// <summary>
        /// Enables / disables juggernaut.
        /// </summary>
        [JsonPropertyName("increaseMaxWeightEnabled")]
        public bool IncreaseMaxWeightEnabled { get; set; }

        /// <summary>
        /// Enables / disables juggernaut.
        /// </summary>
        [JsonPropertyName("instantADSEnabled")]
        public bool InstantADSEnabled { get; set; }

        /// <summary>
        /// Enables / disables max / infinite stamina.
        /// </summary>
        [JsonPropertyName("showHoverArmor")]
        public bool ShowHoverArmor { get; set; }

        /// <summary>
        /// Enables / disables exfil names.
        /// </summary>
        [JsonPropertyName("hideExfilNames")]
        public bool HideExfilNames { get; set; }

        /// <summary>
        /// Enables / disables text outlines.
        /// </summary>
        [JsonPropertyName("hideTextOutline")]
        public bool HideTextOutline { get; set; }

        /// <summary>
        /// Enables / disables memory writing master switch.
        /// </summary>
        [JsonPropertyName("masterSwitchEnabled")]
        public bool MasterSwitchEnabled { get; set; }

        /// <summary>
        /// Enables / disables extended reach.
        /// </summary>
        [JsonPropertyName("extendedReachEnabled")]
        public bool ExtendedReachEnabled { get; set; }

        /// <summary>
        /// Enables / disables infinite stamina.
        /// </summary>
        [JsonPropertyName("infiniteStaminaEnabled")]
        public bool InfiniteStaminaEnabled { get; set; }

        /// <summary>
        /// Enables / disables showing corpses.
        /// </summary>
        [JsonPropertyName("showCorpsesEnabled")]
        public bool ShowCorpsesEnabled { get; set; }

        /// <summary>
        /// Enables / disables showing sub items.
        /// </summary>
        [JsonPropertyName("showSubItemsEnabled")]
        public bool ShowSubItemsEnabled { get; set; }

        /// <summary>
        /// Minimum loot value (rubles) to display 'corpses' on map.
        /// </summary>
        [JsonPropertyName("minCorpseValue")]
        public int MinCorpseValue { get; set; }

        /// <summary>
        /// Minimum loot value (rubles) to display 'sub items' on map.
        /// </summary>
        [JsonPropertyName("minSubItemValue")]
        public int MinSubItemValue { get; set; }

        /// <summary>
        /// Enables / disables auto loot refresh.
        /// </summary>
        [JsonPropertyName("autoLootRefreshEnabled")]
        public bool AutoLootRefreshEnabled { get; set; }

        /// <summary>
        /// Allows storage of multiple loot filters.
        /// </summary>
        [JsonPropertyName("LootFilters")]
        public List<LootFilter> Filters { get; set; }

        /// <summary>
        /// Regular Thermal Vision Settings
        /// </summary>
        [JsonPropertyName("MainThermalSetting")]
        public ThermalSettings MainThermalSetting { get; set; }

        /// <summary>
        /// Optical Thermal Vision Settings
        /// </summary>
        [JsonPropertyName("OpticThermalSetting")]
        public ThermalSettings OpticThermalSetting { get; set; }

        /// <summary>
        /// Allows storage of colors for ai scav, pscav etc.
        /// </summary>
        [JsonPropertyName("PaintColors")]
        //public List<PaintColor> PaintColors { get; set; }
        public Dictionary<string, PaintColor.Colors> PaintColors { get; set; }

        [JsonPropertyName("AutoRefreshSettings")]
        public Dictionary<string, int> AutoRefreshSettings { get; set; }

        [JsonIgnore]
        public ParallelOptions ParallelOptions { get; set; }

        public Config()
        {
            Vsync = true;
            PlayerAimLineLength = 1000;
            DefaultZoom = 100;
            UIScale = 100;
            LootEnabled = true;
            QuestHelperEnabled = true;
            AimviewEnabled = false;
            HideNames = false;
            ImportantLootOnly = false;
            HideLootValue = false;
            NoRecoilSwayEnabled = false;
            MaxStaminaEnabled = false;
            LoggingEnabled = false;
            ShowHoverArmor = false;
            MaxDistance = 325;
            AimViewFOV = 30;
            MinLootValue = 90000;
            MinImportantLootValue = 300000;
            PrimaryTeammateId = null;
            Filters = new List<LootFilter>();

            PaintColors = new Dictionary<string, PaintColor.Colors>
            {
                ["AIScav"] = new PaintColor.Colors { A = 255, R = 255, G = 255, B = 0 },
                ["PScav"] = new PaintColor.Colors { A = 255, R = 255, G = 165, B = 0 },
                ["AIRaider"] = new PaintColor.Colors { A = 255, R = 128, G = 0, B = 128 },
                ["Boss"] = new PaintColor.Colors { A = 255, R = 255, G = 0, B = 255 },
                ["USEC"] = new PaintColor.Colors { A = 255, R = 255, G = 0, B = 0 },
                ["BEAR"] = new PaintColor.Colors { A = 255, R = 0, G = 0, B = 255 },
                ["LocalPlayer"] = new PaintColor.Colors { A = 255, R = 255, G = 255, B = 255 },
                ["Teammate"] = new PaintColor.Colors { A = 255, R = 50, G = 205, B = 50 },
                ["TeamHover"] = new PaintColor.Colors { A = 255, R = 125, G = 252, B = 50 },
                ["Special"] = new PaintColor.Colors { A = 255, R = 255, G = 105, B = 180 },
                ["RegularLoot"] = new PaintColor.Colors { A = 255, R = 245, G = 245, B = 245 },
                ["ImportantLoot"] = new PaintColor.Colors { A = 255, R = 64, G = 224, B = 208 },
                ["QuestItem"] = new PaintColor.Colors { A = 255, R = 255, G = 0, B = 128 },
                ["QuestZone"] = new PaintColor.Colors { A = 255, R = 255, G = 0, B = 128 },
                ["ExfilActiveText"] = new PaintColor.Colors { A = 255, R = 255, G = 255, B = 255 },
                ["ExfilActiveIcon"] = new PaintColor.Colors { A = 255, R = 50, G = 205, B = 50 },
                ["ExfilPendingText"] = new PaintColor.Colors { A = 255, R = 255, G = 255, B = 255 },
                ["ExfilPendingIcon"] = new PaintColor.Colors { A = 255, R = 255, G = 255, B = 0 },
                ["ExfilClosedText"] = new PaintColor.Colors { A = 255, R = 255, G = 255, B = 255 },
                ["ExfilClosedIcon"] = new PaintColor.Colors { A = 255, R = 255, G = 0, B = 0 },
                ["TextOutline"] = new PaintColor.Colors { A = 255, R = 0, G = 0, B = 0 },
                ["DeathMarker"] = new PaintColor.Colors { A = 255, R = 0, G = 0, B = 0 }
            };

            NightVisionEnabled = false;
            ThermalVisionEnabled = false;
            NoVisorEnabled = false;
            OpticThermalVisionEnabled = false;
            DoubleSearchEnabled = false;
            JumpPowerEnabled = false;
            JumpPowerStrength = 4;
            ThrowPowerEnabled = false;
            ThrowPowerStrength = 4;
            MagDrillsEnabled = false;
            MagDrillSpeed = 5;
            IncreaseMaxWeightEnabled = false;
            InstantADSEnabled = false;
            HideExfilNames = false;
            HideTextOutline = false;
            MasterSwitchEnabled = false;
            InfiniteStaminaEnabled = false;
            ExtendedReachEnabled = false;
            ShowCorpsesEnabled = false;
            ShowSubItemsEnabled = false;
            MinCorpseValue = 100000;
            MinSubItemValue = 15000;
            AutoLootRefreshEnabled = false;

            MainThermalSetting = new ThermalSettings(0.5f, 0.001f, -0.5f, 0);
            OpticThermalSetting = new ThermalSettings(0.5f, 0.001f, -0.5f, 0);

            AutoRefreshSettings = new Dictionary<string, int>
            {
                ["Customs"] = 30,
                ["Factory"] = 30,
                ["Ground Zero"] = 30,
                ["Interchange"] = 30,
                ["Lighthouse"] = 30,
                ["Reserve"] = 30,
                ["Shoreline"] = 30,
                ["Streets of Tarkov"] = 30,
                ["The Lab"] = 30,
                ["Woods"] = 30
            };

            ParallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 1 };
        }

        /// <summary>
        /// Attempt to load Config.json
        /// </summary>
        /// <param name="config">'Config' instance to populate.</param>
        /// <returns></returns>
        public static bool TryLoadConfig(out Config config)
        {
            lock (_lock)
            {
                try
                {
                    if (!File.Exists("Config.json")) throw new FileNotFoundException("Config.json does not exist!");
                    var json = File.ReadAllText("Config.json");
                    config = JsonSerializer.Deserialize<Config>(json);
                    return true;
                }
                catch (Exception ex)
                {
                    config = null;
                    return false;
                }
            }
        }
        /// <summary>
        /// Save to Config.json
        /// </summary>
        /// <param name="config">'Config' instance</param>
        public static void SaveConfig(Config config)
        {
            lock (_lock)
            {
                var json = JsonSerializer.Serialize<Config>(config, _jsonOptions);
                File.WriteAllText("Config.json", json);
            }
        }
    }
    #endregion

    #region GUI Classes
    /// <summary>
    /// Defines map position for the 2D Map.
    /// </summary>
    public struct MapPosition
    {
        public MapPosition()
        {
        }
        /// <summary>
        /// Contains the Skia Interface (UI) Scaling Value.
        /// </summary>
        public float UIScale = 0;
        /// <summary>
        /// X coordinate on Bitmap.
        /// </summary>
        public float X = 0;
        /// <summary>
        /// Y coordinate on Bitmap.
        /// </summary>
        public float Y = 0;
        /// <summary>
        /// Unit 'height' as determined by Vector3.Z
        /// </summary>
        public float Height = 0;

        private Config _config { get => Program.Config; }

        /// <summary>
        /// Get exact player location (with optional X,Y offsets).
        /// </summary>
        public SKPoint GetPoint(float xOff = 0, float yOff = 0)
        {
            return new SKPoint(X + xOff, Y + yOff);
        }
        /// <summary>
        /// Gets the point where the Aimline 'Line' ends. Applies UI Scaling internally.
        /// </summary>
        private SKPoint GetAimlineEndpoint(double radians, float aimlineLength)
        {
            aimlineLength *= UIScale;
            return new SKPoint((float)(this.X + Math.Cos(radians) * aimlineLength), (float)(this.Y + Math.Sin(radians) * aimlineLength));
        }
        /// <summary>
        /// Gets up arrow where loot is. IDisposable. Applies UI Scaling internally.
        /// </summary>
        private SKPath GetUpArrow(float size = 6)
        {
            size *= UIScale;
            SKPath path = new SKPath();
            path.MoveTo(X, Y);
            path.LineTo(X - size, Y + size);
            path.LineTo(X + size, Y + size);
            path.Close();

            return path;
        }
        /// <summary>
        /// Gets down arrow where loot is. IDisposable. Applies UI Scaling internally.
        /// </summary>
        private SKPath GetDownArrow(float size = 6)
        {
            size *= UIScale;
            SKPath path = new SKPath();
            path.MoveTo(X, Y);
            path.LineTo(X - size, Y - size);
            path.LineTo(X + size, Y - size);
            path.Close();

            return path;
        }
        /// <summary>
        /// Draws an Exfil on this location.
        /// </summary>
        public void DrawExfil(SKCanvas canvas, Exfil exfil, float localPlayerHeight)
        {
            var paint = Extensions.GetEntityPaint(exfil);
            var text = Extensions.GetTextPaint(exfil);
            var heightDiff = this.Height - localPlayerHeight;
            if (heightDiff > 1.85) // exfil is above player
            {
                using var path = this.GetUpArrow(5);
                canvas.DrawPath(path, paint);
            }
            else if (heightDiff < -1.85) // exfil is below player
            {
                using var path = this.GetDownArrow(5);
                canvas.DrawPath(path, paint);
            }
            else // exfil is level with player
            {
                canvas.DrawCircle(this.GetPoint(), 4 * UIScale, paint);
            }

            if (!_config.HideExfilNames)
            {
                var coords = this.GetPoint();
                var textWidth = text.MeasureText(exfil.Name);

                coords.X = (coords.X - textWidth / 2);
                coords.Y = (coords.Y - text.TextSize / 2) - 3;

                if (!_config.HideTextOutline)
                    canvas.DrawText(exfil.Name, coords, Extensions.GetTextOutlinePaint());

                canvas.DrawText(exfil.Name, coords, text);
            }
        }
        /// <summary>
        /// Draws a 'Hot' Grenade on this location.
        /// </summary>
        public void DrawGrenade(SKCanvas canvas)
        {
            canvas.DrawCircle(this.GetPoint(), 5 * UIScale, SKPaints.PaintGrenades);
        }
        /// <summary>
        /// Draws a lootable object on this location.
        /// </summary>
        public void DrawLootableObject(SKCanvas canvas, LootableObject item, float heightDiff) {
            if (item is LootItem lootItem)
            {
                this.DrawLootItem(canvas, lootItem, heightDiff);
            }
            else if (item is LootContainer container)
            {
                this.DrawLootContainer(canvas, container, heightDiff);
            }
            else if (item is LootCorpse corpse)
            {
                this.DrawLootCorpse(canvas, corpse, heightDiff);
            }
        }
        /// <summary>
        /// Draws a loot item on this location.
        /// </summary>
        public void DrawLootItem(SKCanvas canvas, LootItem item, float heightDiff)
        {
            var paint = Extensions.GetEntityPaint(item);
            var text = Extensions.GetTextPaint(item);
            var label = _config.HideLootValue ? item.Item.shortName : item.GetFormattedValueShortName();

            if (heightDiff > 1.45)
            {
                using var path = this.GetUpArrow();
                canvas.DrawPath(path, paint);
            }
            else if (heightDiff < -1.45)
            {
                using var path = this.GetDownArrow();
                canvas.DrawPath(path, paint);
            }
            else
            {
                canvas.DrawCircle(this.GetPoint(), 5 * UIScale, paint);
            }

            var coords = this.GetPoint(7 * UIScale, 3 * UIScale);
            if (!_config.HideTextOutline)
                canvas.DrawText(label, coords, Extensions.GetTextOutlinePaint());
            canvas.DrawText(label, coords, text);
        }
        /// <summary>
        /// Draws a loot container on this location.
        /// </summary>
        public void DrawLootContainer(SKCanvas canvas, LootContainer container, float heightDiff)
        {
            var paint = Extensions.GetEntityPaint(container);
            var text = Extensions.GetTextPaint(container);
            var label = container.Name;

            if (heightDiff > 1.45)
            {
                using var path = this.GetUpArrow();
                canvas.DrawPath(path, paint);
            }
            else if (heightDiff < -1.45)
            {
                using var path = this.GetDownArrow();
                canvas.DrawPath(path, paint);
            }
            else
            {
                canvas.DrawCircle(this.GetPoint(), 5 * UIScale, paint);
            }

            var coords = this.GetPoint(7 * UIScale, 3 * UIScale);
            if (!_config.HideTextOutline)
                canvas.DrawText(label, coords, Extensions.GetTextOutlinePaint());
            canvas.DrawText(label, coords, text);
        }
        /// <summary>
        /// Draws a loot corpse on this location.
        /// </summary>
        public void DrawLootCorpse(SKCanvas canvas, LootCorpse corpse, float heightDiff)
        {
            float length = 6 * UIScale;
            var paint = Extensions.GetDeathMarkerPaint(corpse);
            float offsetX = -15 * UIScale;

            if (heightDiff > 1.45)
            {
                using var path = this.GetUpArrow();
                path.Offset(offsetX, 0);
                canvas.DrawPath(path, paint);
            }
            else if (heightDiff < -1.45)
            {
                using var path = this.GetDownArrow();
                path.Offset(offsetX, 0);
                canvas.DrawPath(path, paint);
            }
            else
            {
                canvas.DrawCircle(this.X + offsetX, this.Y, 5 * UIScale, paint);
            }

            canvas.DrawLine(new SKPoint(this.X - length, this.Y + length), new SKPoint(this.X + length, this.Y - length), paint);
            canvas.DrawLine(new SKPoint(this.X - length, this.Y - length), new SKPoint(this.X + length, this.Y + length), paint);
        }
        /// <summary>
        /// Draws a Quest Item on this location.
        /// </summary>
        public void DrawQuestItem(SKCanvas canvas, QuestItem item, float heightDiff) {
            var label = item.Name;
            SKPaint paint = Extensions.GetEntityPaint(item);
            SKPaint text = Extensions.GetTextPaint(item);

            if (heightDiff > 1.45) // loot is above player
            {
                
                using var path = this.GetUpArrow();
                canvas.DrawPath(path, paint);
            }
            else if (heightDiff < -1.45) // loot is below player
            {
                using var path = this.GetDownArrow();
                canvas.DrawPath(path, paint);
            }
            else // loot is level with player
            {
                canvas.DrawCircle(this.GetPoint(), 5 * UIScale, paint);
            }

            var coords = this.GetPoint(7 * UIScale, 3 * UIScale);
            if (!_config.HideTextOutline)
                canvas.DrawText(label, coords, Extensions.GetTextOutlinePaint());
            canvas.DrawText(label, coords, text);
        }
        /// <summary>
        /// Draws a quest zone on this location.
        /// </summary>
        public void DrawTaskZone(SKCanvas canvas, QuestZone zone, float heightDiff)
        {
            var label = zone.ObjectiveType;
            SKPaint paint = Extensions.GetEntityPaint(zone);
            SKPaint text = Extensions.GetTextPaint(zone);

            if (heightDiff > 1.45) // above player
            {
                using var path = this.GetUpArrow();
                canvas.DrawPath(path, paint);
            }
            else if (heightDiff < -1.45) // below player
            {
                using var path = this.GetDownArrow();
                canvas.DrawPath(path, paint);
            }
            else // level with player
            {
                canvas.DrawCircle(this.GetPoint(), 5 * UIScale, paint);
            }

            var coords = this.GetPoint(7 * UIScale, 3 * UIScale);
            if (!_config.HideTextOutline)
                canvas.DrawText(label, coords, Extensions.GetTextOutlinePaint());
            canvas.DrawText(label, coords, text);
        }
        /// <summary>
        /// Draws a Player Marker on this location.
        /// </summary>
        public void DrawPlayerMarker(SKCanvas canvas, Player player, int aimlineLength, int? mouseoverGrp)
        {
            var radians = player.Rotation.X.ToRadians();
            SKPaint paint;
            
            if (mouseoverGrp is not null && mouseoverGrp == player.GroupID) {
                paint = SKPaints.PaintMouseoverGroup;
                paint.Color = Extensions.SKColorFromPaintColor("TeamHover");
            } else {
                paint = player.GetEntityPaint();
            }

            canvas.DrawCircle(this.GetPoint(), 6 * UIScale, paint); // draw LocalPlayer marker
            canvas.DrawLine(this.GetPoint(),
                this.GetAimlineEndpoint(radians, aimlineLength),
                paint); // draw LocalPlayer aimline
        }
        /// <summary>
        /// Draws Player Text on this location.
        /// </summary>
        public void DrawPlayerText(SKCanvas canvas, Player player, string[] lines, int? mouseoverGrp)
        {
            SKPaint text;
            if (mouseoverGrp is not null && mouseoverGrp == player.GroupID) {
                text = SKPaints.TextMouseoverGroup;
                text.Color = Extensions.SKColorFromPaintColor("TeamHover");
            } else {
                text = player.GetTextPaint();
            }

            float spacing = 3 * UIScale;
            foreach (var line in lines)
            {
                var coords = this.GetPoint(9 * UIScale, spacing);

                if (!_config.HideTextOutline)
                    canvas.DrawText(line, coords, Extensions.GetTextOutlinePaint());
                canvas.DrawText(line, coords, text);
                spacing += 12 * UIScale;
            }
        }
        /// <summary>
        /// Draws Loot information on this location
        /// </summary>
        public void DrawLootableObjectToolTip(SKCanvas canvas, LootableObject item)
        {
            if (item is LootContainer container)
            {
                DrawToolTip(canvas, container);
            }
            else if (item is LootCorpse corpse)
            {
                DrawToolTip(canvas, corpse);
            }
            else if (item is LootItem lootItem)
            {
                DrawToolTip(canvas, lootItem);
            }
        }
        /// <summary>
        /// Draws the tool tip for quest items
        /// </summary>
        public void DrawToolTip(SKCanvas canvas, QuestItem item)
        {
            var tooltipText = new List<string>();
            tooltipText.Insert(0, item.TaskName);

            var lines = string.Join("\n", tooltipText).Split('\n');
            var maxWidth = 0f;

            foreach (var line in lines)
            {
                var width = SKPaints.TextBase.MeasureText(line);
                maxWidth = Math.Max(maxWidth, width);
            }

            var textSpacing = 12 * UIScale;
            var padding = 3 * UIScale;

            var height = lines.Length * textSpacing;

            var left = X + padding;
            var top = Y - padding;
            var right = left + maxWidth + padding * 2;
            var bottom = top + height + padding * 2;

            var backgroundRect = new SKRect(left, top, right, bottom);
            canvas.DrawRect(backgroundRect, SKPaints.PaintTransparentBacker);

            var y = bottom - (padding * 1.5f);
            foreach (var line in lines)
            {
                canvas.DrawText(line, left + padding, y, SKPaints.TextBase);
                y -= textSpacing;
            }
        }
        /// <summary>
        /// Draws the tool tip for quest items
        /// </summary>
        public void DrawToolTip(SKCanvas canvas, QuestZone item)
        {
            var tooltipText = new List<string>();
            tooltipText.Insert(0, item.TaskName);
            tooltipText.Insert(0, item.Description);

            var lines = string.Join("\n", tooltipText).Split('\n');
            var maxWidth = 0f;

            foreach (var line in lines)
            {
                var width = SKPaints.TextBase.MeasureText(line);
                maxWidth = Math.Max(maxWidth, width);
            }

            var textSpacing = 12 * UIScale;
            var padding = 3 * UIScale;

            var height = lines.Length * textSpacing;

            var left = X + padding;
            var top = Y - padding;
            var right = left + maxWidth + padding * 2;
            var bottom = top + height + padding * 2;

            var backgroundRect = new SKRect(left, top, right, bottom);
            canvas.DrawRect(backgroundRect, SKPaints.PaintTransparentBacker);

            var y = bottom - (padding * 1.5f);
            foreach (var line in lines)
            {
                canvas.DrawText(line, left + padding, y, SKPaints.TextBase);
                y -= textSpacing;
            }
        }
        /// <summary>
        /// Draws player tool tip based on if theyre alive or not
        /// </summary>
        public void DrawToolTip(SKCanvas canvas, Player player)
        {
            if (!player.IsAlive)
            {
                //DrawCorpseTooltip(canvas, player);
                return;
            }

            if (!player.IsHostileActive)
            {
                return;
            }

            DrawHostileTooltip(canvas, player);
        }
        /// <summary>
        /// Draws the tool tip for loot items
        /// </summary>
        private void DrawToolTip(SKCanvas canvas, LootItem lootItem)
        {
            var width = SKPaints.TextBase.MeasureText(lootItem.GetFormattedValueName());

            var textSpacing = 15 * UIScale;
            var padding = 3 * UIScale;

            var height = 1 * textSpacing;

            var left = X + padding;
            var top = Y - padding;
            var right = left + width + padding * 2;
            var bottom = top + height + padding * 2;

            var backgroundRect = new SKRect(left, top, right, bottom);
            canvas.DrawRect(backgroundRect, SKPaints.PaintTransparentBacker);

            var y = bottom - (padding * 2.2f);

            canvas.DrawText(lootItem.GetFormattedValueName(), left + padding, y, Extensions.GetTextPaint(lootItem));
        }
        /// <summary>
        /// Draws the tool tip for loot containers
        /// </summary>
        private void DrawToolTip(SKCanvas canvas, LootContainer container)
        {
            var maxWidth = 0f;

            foreach (var item in container.Items)
            {
                var width = SKPaints.TextBase.MeasureText(item.GetFormattedValueName());
                maxWidth = Math.Max(maxWidth, width);
            }

            var textSpacing = 15 * UIScale;
            var padding = 3 * UIScale;

            var height = container.Items.Count * textSpacing;

            var left = X + padding;
            var top = Y - padding;
            var right = left + maxWidth + padding * 2;
            var bottom = top + height + padding * 2;

            var backgroundRect = new SKRect(left, top, right, bottom);
            canvas.DrawRect(backgroundRect, SKPaints.PaintTransparentBacker);

            var y = bottom - (padding * 2.2f);
            foreach (var item in container.Items)
            {
                canvas.DrawText(item.GetFormattedValueName(), left + padding, y, Extensions.GetTextPaint(item));
                y -= textSpacing;
            }
        }
        /// <summary>
        /// Draws the tool tip for loot corpses
        /// </summary>
        private void DrawToolTip(SKCanvas canvas, LootCorpse corpse)
        {
            var maxWidth = 0f;
            var items = corpse.Items;
            var height = items.Count;
            var isEmptyCorpseName = corpse.Name.Contains("Clone");

            if (!isEmptyCorpseName)
            {
                height += 1;
            }

            foreach (var gearItem in items)
            {
                var width = SKPaints.TextBase.MeasureText(gearItem.GetFormattedTotalValueName());
                maxWidth = Math.Max(maxWidth, width);

                if (_config.ShowSubItemsEnabled && gearItem.Loot.Count > 0)
                {
                    foreach (var lootItem in gearItem.Loot)
                    {
                        if (lootItem.AlwaysShow || lootItem.Important || (!_config.ImportantLootOnly && _config.ShowSubItemsEnabled && lootItem.Value > _config.MinSubItemValue))
                        {
                            width = SKPaints.TextBase.MeasureText($"     {lootItem.GetFormattedValueName()}");
                            maxWidth = Math.Max(maxWidth, width);

                            height++;
                        }
                    }
                }
            }

            var textSpacing = 15 * UIScale;
            var padding = 3 * UIScale;

            height = (int)(height * textSpacing);

            var left = X + padding;
            var top = Y - padding;
            var right = left + maxWidth + padding * 2;
            var bottom = top + height + padding * 2;

            var backgroundRect = new SKRect(left, top, right, bottom);
            canvas.DrawRect(backgroundRect, SKPaints.PaintTransparentBacker);

            var y = bottom - (padding * 2.2f);
            foreach (var gearItem in items)
            {
                if (_config.ShowSubItemsEnabled && gearItem.Loot.Count > 0)
                {
                    foreach (var lootItem in gearItem.Loot)
                    {
                        if (lootItem.AlwaysShow || lootItem.Important || (!_config.ImportantLootOnly && _config.ShowSubItemsEnabled && lootItem.Value > _config.MinSubItemValue))
                        {
                            canvas.DrawText("   " + lootItem.GetFormattedValueName(), left + padding, y, Extensions.GetTextPaint(lootItem));
                            y -= textSpacing;
                        }
                    }
                }

                canvas.DrawText(gearItem.GetFormattedTotalValueName(), left + padding, y, Extensions.GetTextPaint(gearItem));
                y -= textSpacing;
            }

            if (!isEmptyCorpseName)
            {
                canvas.DrawText(corpse.Name, left + padding, y, SKPaints.TextBase);
                y -= textSpacing;
            }
        }
        /// <summary>
        /// Draws tool tip of hostile players 
        /// </summary>
        private void DrawHostileTooltip(SKCanvas canvas, Player player)
        {
            var lines = new List<string>();

            lines.Insert(0, player.Name);

            if (player.Gear != null)
            {
                GearItem gearItem;
                var weaponSlots = new Dictionary<string, string>()
                {
                    {"FirstPrimaryWeapon", "Primary"},
                    {"SecondPrimaryWeapon", "Secondary"},
                    {"Holster", "Holster"}
                };

                foreach (var slot in weaponSlots)
                {
                    if (player.Gear.TryGetValue(slot.Key, out gearItem))
                    {
                        lines.Insert(0, $"{slot.Value}: {gearItem.Short}");
                    }
                }

                if (_config.ShowHoverArmor)
                {
                    var gearSlots = new Dictionary<string, string>()
                    {
                        {"Headwear","Head"},
                        {"FaceCover","Face"},
                        {"ArmorVest","Armor"},
                        {"TacticalVest","Vest"},
                        {"Backpack","Backpack"}
                    };

                    foreach (var slot in gearSlots)
                    {
                        if (player.Gear.TryGetValue(slot.Key, out gearItem))
                        {
                            lines.Insert(0, $"{slot.Value}: {gearItem.Short}");
                        }
                    }
                }
            }

            lines.Insert(0, $"Value: {TarkovDevManager.FormatNumber(player.Value)}");

            if (player.KDA != -1)
            {
                lines.Insert(0, $"KD: {player.KDA}");
            }

            DrawToolTip(canvas, string.Join("\n", lines));
        }
        /// <summary>
        /// Draws tooltip for corpses
        /// </summary>
        private void DrawCorpseTooltip(SKCanvas canvas, Player player)
        {
            var lines = new List<string>();
            var corpseName = $"Corpse [{player.Name}]";

            lines.Insert(0, corpseName);

            if (player.Lvl != 0)
                lines.Insert(0, $"L:{player.Lvl}");

            if (player.GroupID != -1)
                lines.Insert(0, $"G:{player.GroupID}");

            DrawToolTip(canvas, string.Join("\n", lines));
        }
        /// <summary>
        /// Draws the tool tip for players/hostiles
        /// </summary>
        private void DrawToolTip(SKCanvas canvas, string tooltipText)
        {
            var lines = tooltipText.Split('\n');
            var maxWidth = 0f;

            foreach (var line in lines)
            {
                var width = SKPaints.TextBase.MeasureText(line);
                maxWidth = Math.Max(maxWidth, width);
            }

            var textSpacing = 12 * UIScale;
            var padding = 3 * UIScale;

            var height = lines.Length * textSpacing;

            var left = X + padding;
            var top = Y - padding;
            var right = left + maxWidth + padding * 2;
            var bottom = top + height + padding * 2;

            var backgroundRect = new SKRect(left, top, right, bottom);
            canvas.DrawRect(backgroundRect, SKPaints.PaintTransparentBacker);

            var y = bottom - (padding * 1.5f);
            foreach (var line in lines)
            {
                canvas.DrawText(line, left + padding, y, SKPaints.TextBase);
                y -= textSpacing;
            }
        }
    }

    /// <summary>
    /// Defines a Map for use in the GUI.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Name of map (Ex: Customs)
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// 'MapConfig' class instance
        /// </summary>
        public readonly MapConfig ConfigFile;
        /// <summary>
        /// File path to Map .JSON Config
        /// </summary>
        public readonly string ConfigFilePath;

        public Map(string name, MapConfig config, string configPath, string mapID)
        {
            Name = name;
            ConfigFile = config;
            ConfigFilePath = configPath;
            MapID = mapID;
        }

        public readonly string MapID;
    }

    /// <summary>
    /// Contains multiple map parameters used by the GUI.
    /// </summary>
    public class MapParameters
    {
        /// <summary>
        /// Contains the Skia Interface (UI) Scaling Value.
        /// </summary>
        public float UIScale;
        /// <summary>
        /// Contains the 'index' of which map layer to display.
        /// For example: Labs has 3 floors, so there is a Bitmap image for 'each' floor.
        /// Index is dependent on LocalPlayer height.
        /// </summary>
        public int MapLayerIndex;
        /// <summary>
        /// Rectangular 'zoomed' bounds of the Bitmap to display.
        /// </summary>
        public SKRect Bounds;
        /// <summary>
        /// Regular -> Zoomed 'X' Scale correction.
        /// </summary>
        public float XScale;
        /// <summary>
        /// Regular -> Zoomed 'Y' Scale correction.
        /// </summary>
        public float YScale;
    }

    /// <summary>
    /// Defines a .JSON Map Config File
    /// </summary>
    public class MapConfig
    {
        [JsonIgnore]
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };

        [JsonPropertyName("mapID")]
        public List<string> MapID { get; set; } // New property for map IDs

        [JsonPropertyName("x")]
        public float X { get; set; }

        [JsonPropertyName("y")]
        public float Y { get; set; }

        [JsonPropertyName("scale")]
        public float Scale { get; set; }

        // Updated to match new JSON format
        [JsonPropertyName("mapLayers")]
        public List<MapLayer> MapLayers { get; set; }

        public static MapConfig LoadFromFile(string file)
        {
            var json = File.ReadAllText(file);
            return JsonSerializer.Deserialize<MapConfig>(json, _jsonOptions);
        }

        public void Save(Map map)
        {
            var json = JsonSerializer.Serialize(this, _jsonOptions);
            File.WriteAllText(map.ConfigFilePath, json);
        }
    }

    public class MapLayer
    {
        [JsonPropertyName("minHeight")]
        public float MinHeight { get; set; }

        [JsonPropertyName("filename")]
        public string Filename { get; set; }
    }
    /// <summary>
    /// Represents a PMC in the PMC History log.
    /// </summary>
    public class PlayerHistoryEntry
    {
        private readonly string _id;
        private readonly ListViewItem _view;
        /// <summary>
        /// Entry text
        /// </summary>
        public string Entry { get; }
        /// <summary>
        /// For insertion into a ListView control.
        /// </summary>
        public ListViewItem View
        {
            get => _view;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Player BSG ID.</param>
        /// <param name="entry">Full History log entry.</param>
        public PlayerHistoryEntry(string id, string entry)
        {
            _id = id;
            Entry = entry;
            var view = new ListViewItem(
            new string[2]
            {
                entry,
                id
            });
            view.Tag = this; // Store ref to this object
            _view = view;
        }

        /// <summary>
        /// Returns player Acct ID.
        /// </summary>
        public override string ToString()
        {
            return _id;
        }
    }
    #endregion

    #region Custom EFT Classes
    public class ThermalSettings
    {
        public float ColorCoefficient { get; set; }
        public float MinTemperature { get; set; }
        public float RampShift { get; set; }
        public int ColorScheme { get; set; }

        public ThermalSettings() { }

        public ThermalSettings(float colorCoefficient, float minTemp, float rampShift, int colorScheme)
        {
            this.ColorCoefficient = colorCoefficient;
            this.MinTemperature = minTemp;
            this.RampShift = rampShift;
            this.ColorScheme = colorScheme;
        }
    }
    /// <summary>
    /// Contains weapon info for Primary Weapons.
    /// </summary>
    public struct PlayerWeaponInfo
    {
        public string ThermalScope;
        public string AmmoType;

        public override string ToString()
        {
            var result = string.Empty;
            if (AmmoType is not null) result += AmmoType;
            if (ThermalScope is not null)
            {
                if (result != string.Empty) result += $", {ThermalScope}";
                else result += ThermalScope;
            }
            if (result == string.Empty) return null;
            return result;
        }
    }
    /// <summary>
    /// Defines Player Unit Type (Player,PMC,Scav,etc.)
    /// </summary>
    public enum PlayerType
    {
        /// <summary>
        /// Default value if a type cannot be established.
        /// </summary>
        Default,
        /// <summary>
        /// The primary player running this application/radar.
        /// </summary>
        LocalPlayer,
        /// <summary>
        /// Teammate of LocalPlayer.
        /// </summary>
        Teammate,
        /// <summary>
        /// Hostile/Enemy PMC.
        /// </summary>
        PMC,
        /// <summary>
        /// Normal AI Bot Scav.
        /// </summary>
        AIScav,
        /// <summary>
        /// Difficult AI Raider.
        /// </summary>
        AIRaider,
        /// <summary>
        /// Difficult AI Rogue.
        /// </summary>
        AIRogue,
        /// <summary>
        /// Difficult AI Boss.
        /// </summary>
        AIBoss,
        /// <summary>
        /// Player controlled Scav.
        /// </summary>
        PScav,
        /// <summary>
        /// 'Special' Human Controlled Hostile PMC/Scav (on the watchlist, or a special account type).
        /// </summary>
        SpecialPlayer,
        /// <summary>
        /// Hostile/Enemy BEAR PMC.
        /// </summary>
        BEAR,
        /// <summary>
        /// Hostile/Enemy USEC PMC.
        /// </summary>
        USEC,
        /// <summary>
        /// Offline LocalPlayer.
        /// </summary>
        AIOfflineScav,
        AISniperScav,
        AIBossGuard,
        AIBossFollower,
        
    }
    /// <summary>
    /// Defines Role for an AI Bot Player.
    /// </summary>
    public struct AIRole
    {
        /// <summary>
        /// Name of Bot Player.
        /// </summary>
        public string Name;
        /// <summary>
        /// Type of Bot Player.
        /// </summary>
        public PlayerType Type;
    }
    #endregion

    #region EFT Enums
    [Flags]
    public enum MemberCategory : int
    {
        Default = 0, // Standard Account
        Developer = 1,
        UniqueId = 2, // EOD Account
        Trader = 4,
        Group = 8,
        System = 16,
        ChatModerator = 32,
        ChatModeratorWithPermamentBan = 64,
        UnitTest = 128,
        Sherpa = 256,
        Emissary = 512
    }
    #endregion

    #region Helpers
    public static class Helpers
    {
        /// <summary>
        /// Returns the 'type' of player based on the 'role' value.
        /// </summary>
        /// 
        public static readonly Dictionary<char, string> CyrillicToLatinMap = new Dictionary<char, string>
        {
                {'А', "A"}, {'Б', "B"}, {'В', "V"}, {'Г', "G"}, {'Д', "D"},
                {'Е', "E"}, {'Ё', "E"}, {'Ж', "Zh"}, {'З', "Z"}, {'И', "I"},
                {'Й', "Y"}, {'К', "K"}, {'Л', "L"}, {'М', "M"}, {'Н', "N"},
                {'О', "O"}, {'П', "P"}, {'Р', "R"}, {'С', "S"}, {'Т', "T"},
                {'У', "U"}, {'Ф', "F"}, {'Х', "Kh"}, {'Ц', "Ts"}, {'Ч', "Ch"},
                {'Ш', "Sh"}, {'Щ', "Shch"}, {'Ъ', ""}, {'Ы', "Y"}, {'Ь', ""},
                {'Э', "E"}, {'Ю', "Yu"}, {'Я', "Ya"},
                {'а', "a"}, {'б', "b"}, {'в', "v"}, {'г', "g"}, {'д', "d"},
                {'е', "e"}, {'ё', "e"}, {'ж', "zh"}, {'з', "z"}, {'и', "i"},
                {'й', "y"}, {'к', "k"}, {'л', "l"}, {'м', "m"}, {'н', "n"},
                {'о', "o"}, {'п', "p"}, {'р', "r"}, {'с', "s"}, {'т', "t"},
                {'у', "u"}, {'ф', "f"}, {'х', "kh"}, {'ц', "ts"}, {'ч', "ch"},
                {'ш', "sh"}, {'щ', "shch"}, {'ъ', ""}, {'ы', "y"}, {'ь', ""},
                {'э', "e"}, {'ю', "yu"}, {'я', "ya"}
        };

        public static Dictionary<string, string> NameTranslations = new Dictionary<string, string>
        {
            {"Килла", "Killa"},
            {"Решала", "Reshala"},
            {"Глухарь", "Glukhar"},
            {"Штурман", "Shturman"},
            {"Санитар", "Sanitar"},
            {"Тагилла", "Tagilla"},
            {"Рейдеры", "Raider"},
            {"Сектант Жрец", "Cultist Priest"},
            {"Отступники", "Renegade"},
            {"Big Pipe", "Big Pipe"},
            {"Birdeye", "Birdeye"},
            {"Knight", "Knight"},
            {"Зрячий", "Zryachiy"},
            {"Кабан", "Kaban"},
            {"Коллонтай", "Kollontay"}
        };

        public static string[] RaiderGuardRogueNames = {
            "Afraid", // Rogues
            "Andresto",
            "Applejuice",
            "Arizona",
            "Auron",
            "Badboy",
            "Baddie",
            "Beard",
            "Beverly",
            "Bison",
            "Blackbird",
            "Blade",
            "Blakemore",
            "Boatswain",
            "Boogerman",
            "Brockley",
            "Browski",
            "Bullet",
            "Bunny",
            "Butcher",
            "Chester",
            "Churchill",
            "Cliffhanger",
            "Condor",
            "Cook",
            "Corsair",
            "Cougar",
            "Coyote",
            "Crooked",
            "Cross",
            "Dakota",
            "Dawg",
            "Deceit",
            "Denver",
            "Diggi",
            "Donutop",
            "Duke",
            "Dustin",
            "Enzo",
            "Esquilo",
            "Father",
            "Firion",
            "Floridaman",
            "Foxy",
            "Frenzy",
            "Garandthumb",
            "Goat",
            "Golden",
            "Grandpa",
            "Greyzone",
            "Grim",
            "Grommet",
            "Gunporn",
            "Handsome",
            "Haunted",
            "Hellshrimp",
            "Honorable",
            "Hypno",
            "Instructor",
            "Iowa",
            "Ironfists",
            "James",
            "Jeff",
            "Jersey",
            "John",
            "Juggernaut",
            "Justkilo",
            "Kanzas",
            "Kentucky",
            "Kry",
            "Lancaster",
            "Lee",
            "Legia",
            "Litton",
            "Lost",
            "Lunar",
            "Madknight",
            "Mamba",
            "Marooner",
            "Marquesses",
            "Meldon",
            "Melo",
            "Michigan",
            "Mike",
            "Momma",
            "Mortal",
            "Mother",
            "Nevada",
            "Nine-hole",
            "Noisy",
            "Nukem",
            "Ocean",
            "Oklahoma",
            "OneEye",
            "Oskar",
            "Panther",
            "Philbo",
            "Quebec",
            "Raccoon",
            "Rage",
            "Rambo",
            "Rassler",
            "Receit",
            "Rib-eye",
            "Riot",
            "Rock",
            "Rocket",
            "Ronflex",
            "Ronny",
            "Rossler",
            "RoughDog",
            "Sektant", // Cultists
            "Scar",
            "Scottsdale",
            "Seafarer",
            "Shadow",
            "SharkBait",
            "Sharkkiller",
            "Sherifu",
            "Sherman",
            "Shifty",
            "Slayer",
            "Sly",
            "Snake",
            "Sneaky",
            "Sniperlife",
            "Solem",
            "Solidus",
            "Spectator-6",
            "Spyke",
            "Stamper",
            "Striker",
            "Texas",
            "Three-Teeth",
            "Trent",
            "Trickster",
            "Triggerhappy",
            "Two-Finger",
            "Vicious",
            "Victor",
            "Voodoo",
            "Voss",
            "Wadley",
            "Walker",
            "Weasel",
            "Whale-Eye",
            "Whisky",
            "Whitemane",
            "Woodrow",
            "Wrath",
            "Zed",
            "Zero-Zero",
            "Aimbotkin",
            "Baklazhan", // kaban guards
            "Bazil",
            "Belyash",
            "Bibop",
            "Cheburek",
            "Dihlofos",
            "Docha",
            "Flamberg",
            "Gladius",
            "Gromila",
            "Gus",
            "Kant",
            "Kaylanshchik",
            "Kapral",
            "Karas",
            "Kartezhnik",
            "Katorzhnik",
            "Khetchkok",
            "Khvost",
            "Kolt",
            "Kompot",
            "Kozyrek",
            "Kudeyar",
            "Mauzer",
            "Medoed",
            "Miposhka",
            "Mosin",
            "Moydodyr",
            "Naperstochnik",
            "Poker",
            "Polzuchiy",
            "Shtempel",
            "Snayler",
            "Sokhatyy",
            "Supermen",
            "Tihiy",
            "Varan",
            "Vasiliy",
            "Verhniy",
            "Zevaka",
            "Afganec",
            "Alfons", // Glukhar guards
            "Assa",
            "Baks",
            "Balu",
            "Banschik",
            "Barguzin",
            "Basmach",
            "Batar",
            "Batya",
            "Belyy",
            "Bob",
            "Borec",
            "Byk",
            "BZT",
            "Calabrissa",
            "Chelovek",
            "Chempion",
            "Chepushila",
            "Dnevalnyy",
            "Drossel",
            "Dum",
            "Fedya",
            "Gepe",
            "Gepard",
            "Gorbatyy",
            "Gotka",
            "Grif",
            "Grustnyy",
            "Kadrovik",
            "Karabin",
            "Karaul",
            "Kastet",
            "Katok",
            "Kocherga",
            "Kosoy",
            "Krot",
            "Kuling",
            "Kumulyativ",
            "Kuzya",
            "Letyoha",
            "Lysyy",
            "Lyutyy",
            "Maga",
            "Matros",
            "Mihalych",
            "Mysh",
            "Nakat",
            "Nemonas",
            "Oficer",
            "Omeh",
            "Oskolochnyy",
            "Otbityy",
            "Patron",
            "Pluton",
            "Radar",
            "Rayan",
            "Rembo",
            "Ryaha",
            "Salobon",
            "Sapog",
            "Seryy",
            "Shapka",
            "Shustryy",
            "Sibiryak",
            "Signal",
            "Sobr",
            "Specnaz",
            "Stvol",
            "Sych",
            "Tankist",
            "Tihohod",
            "Toropyga",
            "Trubochist",
            "Utyug",
            "Valet",
            "Vegan",
            "Veteran",
            "Vityok",
            "Zampolit",
            "Zarya",
            "Zhirnyy",
            "Zh-12",
            "Zimniy",
            "Anton Zavodskoy", // Reshala guards
            "Damirka Zavodskoy",
            "Filya Zavodskoy",
            "Gena Zavodskoy",
            "Grisha Zavodskoy",
            "Kolyan Zavodskoy",
            "Kuling Zavodskoy",
            "Lesha Zavodskoy",
            "Nikita Zavodskoy",
            "Sanya Zavodskoy",
            "Shtopor Zavodskoy",
            "Skif Zavodskoy",
            "Stas Zavodskoy",
            "Toha Zavodskoy",
            "Torpeda Zavodskoy",
            "Vasya Zavodskoy",
            "Vitek Zavodskoy",
            "Zhora Zavodskoy",
            "Dimon Svetloozerskiy", // Shturman guards
            "Enchik Svetloozerskiy",
            "Kachok Svetloozerskiy",
            "Krysa Svetloozerskiy",
            "Malchik Svetloozerskiy",
            "Marat Svetloozerskiy",
            "Mels Svetloozerskiy",
            "Motlya Svetloozerskiy",
            "Motyl Svetloozerskiy",
            "Pashok Svetloozerskiy",
            "Plyazhnik Svetloozerskiy",
            "Robinzon Svetloozerskiy",
            "Sanya Svetloozerskiy",
            "Shmyga Svetloozerskiy",
            "Tokha Svetloozerskiy",
            "Ugryum Svetloozerskiy",
            "Vovan Svetloozerskiy",
            "Akula", // scav raiders
            "Assa",
            "BZT",
            "Balu",
            "Bankir",
            "Barrakuda",
            "Bars",
            "Berkut",
            "Bob",
            "Dikobraz",
            "Gadyuka",
            "Gepard",
            "Grif",
            "Grizzli",
            "Gyurza",
            "Irbis",
            "Jaguar",
            "Kalan",
            "Karakurt",
            "Kayman",
            "Kobra",
            "Kondor",
            "Krachun",
            "Krasnyy volk",
            "Krechet",
            "Kuling",
            "Leopard",
            "Lev",
            "Lis",
            "Loggerhed",
            "Lyutty",
            "Maga",
            "Mangust",
            "Manul",
            "Mantis",
            "Medved",
            "Nosorog",
            "Orel",
            "Orlan",
            "Padalshchik",
            "Pantera",
            "Pchel",
            "Piton",
            "Piranya",
            "Puma",
            "Radar",
            "Rosomaha",
            "Rys",
            "Sapsan",
            "Sekach",
            "Shakal",
            "Signal",
            "Skorpion",
            "Stervyatnik",
            "Tarantul",
            "Taypan",
            "Tigr",
            "Varan",
            "Vegan",
            "Vepr",
            "Veteran",
            "Volk",
            "Voron",
            "Yaguar",
            "Yastreb",
            "Zubr",
            "Akusher", // Sanitar guards
            "Khirurg",
            "Anesteziolog",
            "Dermatolog",
            "Farmacevt",
            "Feldsher",
            "Fiziolog",
            "Glavvrach",
            "Gomeopat",
            "Hirurg",
            "Immunolog",
            "Kardiolog",
            "Laborant",
            "Lasha Ortoped",
            "Lor",
            "Medbrat",
            "Medsestra",
            "Nevrolog",
            "Okulist",
            "Paracetamol",
            "Pilyulya",
            "Proktolog",
            "Propital",
            "Psihiatr",
            "Psikhiatr",
            "Pyotr Planchik",
            "Revmatolog",
            "Rodion Bubesh",
            "Scavvaf",
            "Shpric",
            "Stomatolog",
            "Terapevt",
            "Travmatolog",
            "Trupovoz",
            "Urolog",
            "Vaha Geroy",
            "Venerolog",
            "Zaveduyuschiy",
            "Zaveduyushchiy",
            "Zhgut",
            "Arsenal", // Kollontay guards
            "Basyak",
            "Begemotik",
            "Dezhurka",
            "Furazhka",
            "Glavdur",
            "Kozyrek Desatnik",
            "Mayor",
            "Peps",
            "Serzhant",
            "Slonolyub",
            "Sluzhebka",
            "Starley Desatnik",
            "Starley brat",
            "Starley",
            "Starshiy brat",
            "Strelok brat",
            "Tatyanka Desatnik",
            "Tatyanka",
            "Vasya Desantnik",
            "Visyak",
            "Baba Yaga", // Follower of Morana
            "Buran",
            "Domovoy",
            "Gamayun",
            "Gololed",
            "Gorynych",
            "Hladnik",
            "Hladovit",
            "Holodec",
            "Holodryg",
            "Holodun",
            "Ineevik",
            "Ineyko",
            "Ineynik",
            "Karachun",
            "Kikimora",
            "Koleda",
            "Kupala",
            "Ledorez",
            "Ledovik",
            "Ledyanik",
            "Ledyanoy",
            "Liho",
            "Merzlotnik",
            "Mor",
            "Morozec",
            "Morozina",
            "Morozko",
            "Moroznik",
            "Obmoroz",
            "Poludnik",
            "Serebryak",
            "Severyanin",
            "Sirin",
            "Skvoznyak",
            "Snegobur",
            "Snegoed",
            "Snegohod",
            "Snegovey",
            "Snegovik",
            "Snezhin",
            "Sosulnik",
            "Striga",
            "Studen",
            "Stuzhaylo",
            "Stuzhevik",
            "Sugrobnik",
            "Sugrobus",
            "Talasum",
            "Tryasovica",
            "Tuchevik",
            "Uraganische",
            "Vetrenik",
            "Vetrozloy",
            "Vihrevoy",
            "Viy",
            "Vodyanoy",
            "Vyugar",
            "Vyugovik",
            "Zimar",
            "Zimnik",
            "Zimobor",
            "Zimogor",
            "Zimorod",
            "Zimovey",
            "Zlomraz",
            "Zloveschun"
        };

        public static string TransliterateCyrillic(string input)
        {
            StringBuilder output = new StringBuilder();

            foreach (char c in input)
            {
                output.Append(CyrillicToLatinMap.TryGetValue(c, out var latinEquivalent) ? latinEquivalent : c.ToString());
            }

            return output.ToString();
        }
    }
    #endregion
}
