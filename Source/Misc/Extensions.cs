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
        /// Gets drawing paintbrush based on Player Type.
        /// </summary>
        public static SKPaint GetPaint(this Player player)
        {
            switch (player.Type)
            {
                case PlayerType.LocalPlayer:
                    return SKPaints.PaintLocalPlayer;
                case PlayerType.Teammate:
                    return SKPaints.PaintTeammate;
                case PlayerType.PMC:
                    return SKPaints.PaintPMC;
                case PlayerType.BEAR:
                    return SKPaints.PaintBear;
                case PlayerType.USEC:
                    return SKPaints.PaintUsec;
                case PlayerType.AIScav:
                    return SKPaints.PaintScav;
                case PlayerType.AIRaider:
                    return SKPaints.PaintRaider;
                case PlayerType.AIBoss:
                    return SKPaints.PaintBoss;
                case PlayerType.PScav:
                    return SKPaints.PaintPScav;
                case PlayerType.SpecialPlayer:
                    return SKPaints.PaintSpecial;
                case PlayerType.AIOfflineScav:
                    return SKPaints.PaintScav;
                case PlayerType.AIBossGuard:
                    return SKPaints.PaintRaider;
                case PlayerType.AISniperScav:
                    return SKPaints.PaintSpecial;
                default:
                    return SKPaints.PaintPMC;
            }
        }
        /// <summary>
        /// Gets text paintbrush based on Player Type.
        /// </summary>
        public static SKPaint GetText(this Player player)
        {
            switch (player.Type)
            {
                case PlayerType.Teammate:
                    return SKPaints.TextTeammate;
                case PlayerType.PMC:
                    return SKPaints.TextPMC;
                case PlayerType.BEAR:
                    return SKPaints.TextBear;
                case PlayerType.USEC:
                    return SKPaints.TextUsec;
                case PlayerType.AIScav:
                    return SKPaints.TextScav;
                case PlayerType.AIRaider:
                    return SKPaints.TextRaider;
                case PlayerType.AIBoss:
                    return SKPaints.TextBoss;
                case PlayerType.PScav:
                    return SKPaints.TextWhite;
                case PlayerType.SpecialPlayer:
                    return SKPaints.TextSpecial;
                case PlayerType.AIOfflineScav:
                    return SKPaints.TextScav;
                case PlayerType.AIBossGuard:
                    return SKPaints.TextRaider;
                case PlayerType.AISniperScav:
                    return SKPaints.TextSpecial;
                default:
                    return SKPaints.TextPMC;
            }
        }

        /// <summary>
        /// Gets Aimview drawing paintbrush based on Player Type.
        /// </summary>
        public static SKPaint GetAimviewPaint(this Player player)
        {
            switch (player.Type)
            {
                case PlayerType.LocalPlayer:
                    return SKPaints.PaintAimviewLocalPlayer;
                case PlayerType.Teammate:
                    return SKPaints.PaintAimviewTeammate;
                case PlayerType.PMC:
                    return SKPaints.PaintAimviewPMC;
                case PlayerType.AIScav:
                    return SKPaints.PaintAimviewScav;
                case PlayerType.AIRaider:
                    return SKPaints.PaintAimviewRaider;
                case PlayerType.AIBoss:
                    return SKPaints.PaintAimviewBoss;
                case PlayerType.PScav:
                    return SKPaints.PaintAimviewPScav;
                case PlayerType.SpecialPlayer:
                    return SKPaints.PaintAimviewSpecial;
                case PlayerType.AIOfflineScav:
                    return SKPaints.PaintAimviewScav;
                case PlayerType.AIBossGuard:
                    return SKPaints.PaintAimviewRaider;
                case PlayerType.AISniperScav:
                    return SKPaints.PaintAimviewSpecial;
                default:
                    return SKPaints.PaintAimviewPMC;
            }
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

        #region Custom EFT Extensions
        public static AIRole GetRole(this WildSpawnType type)
        {
            switch (type)
            {
                case WildSpawnType.marksman:
                    return new AIRole()
                    {
                        Name = "Sniper",
                        Type = PlayerType.AIScav
                    };
                case WildSpawnType.assault:
                    return new AIRole()
                    {
                        Name = "Scav",
                        Type = PlayerType.AIScav
                    };
                case WildSpawnType.bossTest:
                    return new AIRole()
                    {
                        Name = "bossTest",
                        Type = PlayerType.AIBoss
                    };
                case WildSpawnType.bossBully:
                    return new AIRole()
                    {
                        Name = "Reshala",
                        Type = PlayerType.AIBoss
                    };
                case WildSpawnType.followerTest:
                    return new AIRole()
                    {
                        Name = "followerTest",
                        Type = PlayerType.AIBoss
                    };
                case WildSpawnType.followerBully:
                    return new AIRole()
                    {
                        Name = "Guard",
                        Type = PlayerType.AIRaider
                    };
                case WildSpawnType.bossKilla:
                    return new AIRole()
                    {
                        Name = "Killa",
                        Type = PlayerType.AIBoss
                    };
                case WildSpawnType.bossKojaniy:
                    return new AIRole()
                    {
                        Name = "Shturman",
                        Type = PlayerType.AIBoss
                    };
                case WildSpawnType.followerKojaniy:
                    return new AIRole()
                    {
                        Name = "Guard",
                        Type = PlayerType.AIRaider
                    };
                case WildSpawnType.pmcBot:
                    return new AIRole()
                    {
                        Name = "Raider",
                        Type = PlayerType.AIRaider
                    };
                case WildSpawnType.cursedAssault:
                    return new AIRole()
                    {
                        Name = "Scav",
                        Type = PlayerType.AIScav
                    };
                case WildSpawnType.bossGluhar:
                    return new AIRole()
                    {
                        Name = "Gluhar",
                        Type = PlayerType.AIBoss
                    };
                case WildSpawnType.followerGluharAssault:
                    return new AIRole()
                    {
                        Name = "Assault",
                        Type = PlayerType.AIRaider
                    };
                case WildSpawnType.followerGluharSecurity:
                    return new AIRole()
                    {
                        Name = "Security",
                        Type = PlayerType.AIRaider
                    };
                case WildSpawnType.followerGluharScout:
                    return new AIRole()
                    {
                        Name = "Scout",
                        Type = PlayerType.AIRaider
                    };
                case WildSpawnType.followerGluharSnipe:
                    return new AIRole()
                    {
                        Name = "Sniper",
                        Type = PlayerType.AIRaider
                    };
                case WildSpawnType.followerSanitar:
                    return new AIRole()
                    {
                        Name = "Guard",
                        Type = PlayerType.AIRaider
                    };
                case WildSpawnType.bossSanitar:
                    return new AIRole()
                    {
                        Name = "Sanitar",
                        Type = PlayerType.AIBoss
                    };
                case WildSpawnType.test:
                    return new AIRole()
                    {
                        Name = "test",
                        Type = PlayerType.AIScav
                    };
                case WildSpawnType.assaultGroup:
                    return new AIRole()
                    {
                        Name = "assaultGroup",
                        Type = PlayerType.AIScav
                    };
                case WildSpawnType.sectantWarrior:
                    return new AIRole()
                    {
                        Name = "Cultist",
                        Type = PlayerType.AIRaider
                    };
                case WildSpawnType.sectantPriest:
                    return new AIRole()
                    {
                        Name = "Priest",
                        Type = PlayerType.AIBoss
                    };
                case WildSpawnType.bossTagilla:
                    return new AIRole()
                    {
                        Name = "Tagilla",
                        Type = PlayerType.AIBoss
                    };
                case WildSpawnType.followerTagilla:
                    return new AIRole()
                    {
                        Name = "Guard",
                        Type = PlayerType.AIRaider
                    };
                case WildSpawnType.exUsec:
                    return new AIRole()
                    {
                        Name = "Rogue",
                        Type = PlayerType.AIRaider
                    };
                case WildSpawnType.gifter:
                    return new AIRole()
                    {
                        Name = "SANTA",
                        Type = PlayerType.AIScav
                    };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion
    }
}
