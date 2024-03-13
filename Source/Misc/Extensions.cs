using SkiaSharp;

namespace eft_dma_radar
{
    /// <summary>
    /// Extension methods go here.
    /// </summary>
    public static class Extensions
    {
        #region Generic Extensions
        /// <summary>
        /// Restarts a timer from 0. (Timer will be started if not already running)
        /// </summary>
        public static void Restart(this System.Timers.Timer t)
        {
            t.Stop();
            t.Start();
        }

        /// <summary>
        /// Converts 'Degrees' to 'Radians'.
        /// </summary>
        public static double ToRadians(this float degrees)
        {
            return (Math.PI / 180) * degrees;
        }
        /// <summary>
        /// Converts 'Radians' to 'Degrees'.
        /// </summary>
        public static double ToDegrees(this float radians)
        {
            return (180 / Math.PI) * radians;
        }
        /// <summary>
        /// Converts 'Degrees' to 'Radians'.
        /// </summary>
        public static double ToRadians(this double degrees)
        {
            return (Math.PI / 180) * degrees;
        }
        /// <summary>
        /// Converts 'Radians' to 'Degrees'.
        /// </summary>
        public static double ToDegrees(this double radians)
        {
            return (180 / Math.PI) * radians;
        }
        #endregion

        #region GUI Extensions
        /// <summary>
        /// Convert game position to 'Bitmap' Map Position coordinates.
        /// </summary>
        public static MapPosition ToMapPos(this System.Numerics.Vector3 vector, Map map)
        {
            return new MapPosition()
            {
                X = map.ConfigFile.X + (vector.X * map.ConfigFile.Scale),
                Y = map.ConfigFile.Y - (vector.Y * map.ConfigFile.Scale), // Invert 'Y' unity 0,0 bottom left, C# top left
                Height = vector.Z // Keep as float, calculation done later
            };
        }

        /// <summary>
        /// Gets 'Zoomed' map position coordinates.
        /// </summary>
        public static MapPosition ToZoomedPos(this MapPosition location, MapParameters mapParams)
        {
            return new MapPosition()
            {
                UIScale = mapParams.UIScale,
                X = (location.X - mapParams.Bounds.Left) * mapParams.XScale,
                Y = (location.Y - mapParams.Bounds.Top) * mapParams.YScale,
                Height = location.Height
            };
        }

        /// <summary>
        /// Ghetto helper method to get the Color from a PaintColor object by Key & return a new SKColor object based on it
        /// </summary>
        public static SKColor SKColorFromPaintColor(string key) {
            var col = Program.Config.PaintColors[key];
            return new SKColor(col.R, col.G, col.B, col.A);
        }

        /// <summary>
        /// Gets drawing paintbrush based on Player Type
        /// </summary>
        public static SKPaint GetPaint(this Player player) {
            SKPaint basePaint = SKPaints.PaintBase.Clone();

            basePaint.Color = player.Type switch {
                PlayerType.LocalPlayer => SKColorFromPaintColor("LocalPlayer"),
                PlayerType.Teammate => SKColorFromPaintColor("Teammate"),
                PlayerType.BEAR => SKColorFromPaintColor("BEAR"),
                PlayerType.USEC => SKColorFromPaintColor("USEC"),
                PlayerType.AIScav => SKColorFromPaintColor("AIScav"),
                PlayerType.AIBoss => SKColorFromPaintColor("Boss"),
                PlayerType.AIOfflineScav => SKColorFromPaintColor("AIScav"),
                PlayerType.AIRaider or PlayerType.AIBossGuard or PlayerType.AIRogue or PlayerType.AIBossFollower => SKColorFromPaintColor("AIRaider"),
                PlayerType.AISniperScav => SKColorFromPaintColor(""),
                PlayerType.PScav => SKColorFromPaintColor("PScav"),

                // default to white
                _ => new SKColor(255, 255, 255, 255),
            };

            return basePaint;
        }

        /// <summary>
        /// Gets text paintbrush based on Player Type
        /// </summary>
        public static SKPaint GetText(this Player player) {
            SKPaint baseText = SKPaints.TextBase.Clone();

            baseText.Color = player.Type switch {
                PlayerType.LocalPlayer => SKColorFromPaintColor("LocalPlayer"),
                PlayerType.Teammate => SKColorFromPaintColor("Teammate"),
                PlayerType.BEAR => SKColorFromPaintColor("BEAR"),
                PlayerType.USEC => SKColorFromPaintColor("USEC"),
                PlayerType.AIScav => SKColorFromPaintColor("AIScav"),
                PlayerType.AIBoss => SKColorFromPaintColor("Boss"),
                PlayerType.AIOfflineScav => SKColorFromPaintColor("AIScav"),
                PlayerType.AIRaider or PlayerType.AIBossGuard or PlayerType.AIRogue or PlayerType.AIBossFollower => SKColorFromPaintColor("AIRaider"),
                PlayerType.AISniperScav => SKColorFromPaintColor(""),
                PlayerType.PScav => SKColorFromPaintColor("PScav"),

                // default to white
                _ => new SKColor(255, 255, 255, 255),
            };

            return baseText;
        }

        /// <summary>
        /// Gets Aimview drawing paintbrush based on Player Type.
        /// </summary>
        public static SKPaint GetAimviewPaint(this Player player)
        {
            SKPaint basePaint = SKPaints.PaintBase.Clone();
            basePaint.StrokeWidth = 1;
            basePaint.Style = SKPaintStyle.Fill;

            basePaint.Color = player.Type switch {
                PlayerType.LocalPlayer => SKColorFromPaintColor("LocalPlayer"),
                PlayerType.Teammate => SKColorFromPaintColor("Teammate"),
                PlayerType.BEAR => SKColorFromPaintColor("BEAR"),
                PlayerType.USEC => SKColorFromPaintColor("USEC"),
                PlayerType.AIScav => SKColorFromPaintColor("AIScav"),
                PlayerType.AIBoss => SKColorFromPaintColor("Boss"),
                PlayerType.AIOfflineScav => SKColorFromPaintColor("AIScav"),
                PlayerType.AIRaider or PlayerType.AIBossGuard or PlayerType.AIRogue or PlayerType.AIBossFollower => SKColorFromPaintColor("AIRaider"),
                PlayerType.AISniperScav => SKColorFromPaintColor(""),
                PlayerType.PScav => SKColorFromPaintColor("PScav"),

                // default to white
                _ => new SKColor(255, 255, 255, 255),
            };

            return basePaint;
        }

        /// <summary>
        /// Get Exfil drawing paintbrush based on status.
        /// </summary>
        public static SKPaint GetPaint(this ExfilStatus status)
        {
            switch (status)
            {
                case ExfilStatus.Open:
                    return SKPaints.PaintExfilOpen;
                case ExfilStatus.Pending:
                    return SKPaints.PaintExfilPending;
                case ExfilStatus.Closed:
                    return SKPaints.PaintExfilClosed;
                default:
                    return SKPaints.PaintExfilClosed;
            }
        }
        #endregion
    }
}
