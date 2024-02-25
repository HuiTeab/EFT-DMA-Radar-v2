using SkiaSharp;
using System.ComponentModel;

namespace eft_dma_radar
{
    internal static class SKPaints
    {
        #region Radar Paints
        public static readonly SKPaint PaintMouseoverGroup = new SKPaint()
        {
            Color = SKColors.LawnGreen,
            StrokeWidth = 3,
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint TextMouseoverGroup = new SKPaint()
        {
            Color = SKColors.LawnGreen,
            IsStroke = false,
            TextSize = 12,
            TextEncoding = SKTextEncoding.Utf8,
            IsAntialias = true,
            Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold),
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint PaintLocalPlayer = new SKPaint()
        {
            Color = SKColors.Green,
            StrokeWidth = 3,
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint PaintTeammate = new SKPaint()
        {
            Color = SKColors.LimeGreen,
            StrokeWidth = 3,
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint TextTeammate = new SKPaint()
        {
            Color = SKColors.LimeGreen,
            IsStroke = false,
            TextSize = 12,
            TextEncoding = SKTextEncoding.Utf8,
            IsAntialias = true,
            Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold),
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint PaintPMC = new SKPaint()
        {
            Color = SKColors.Red,
            StrokeWidth = 3,
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint TextPMC = new SKPaint()
        {
            Color = SKColors.Red,
            IsStroke = false,
            TextSize = 12,
            TextEncoding = SKTextEncoding.Utf8,
            IsAntialias = true,
            Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold),
            FilterQuality = SKFilterQuality.High
        };
        //BEAR PMC
        public static readonly SKPaint PaintBear = new SKPaint()
        {
            Color = SKColors.Blue,
            StrokeWidth = 3,
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High,
        };
        public static readonly SKPaint TextBear = new SKPaint()
        {
            Color = SKColors.Blue,
            IsStroke = false,
            TextSize = 12,
            TextEncoding = SKTextEncoding.Utf8,
            IsAntialias = true,
            Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold),
            FilterQuality = SKFilterQuality.High,
        };
        //USEC PMC
        public static readonly SKPaint PaintUsec = new SKPaint()
        {
            Color = SKColors.OrangeRed,
            StrokeWidth = 3,
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High,
        };
        public static readonly SKPaint TextUsec = new SKPaint()
        {
            Color = SKColors.OrangeRed,
            IsStroke = false,
            TextSize = 12,
            TextEncoding = SKTextEncoding.Utf8,
            IsAntialias = true,
            Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold),
            FilterQuality = SKFilterQuality.High,
        };
        public static readonly SKPaint PaintSpecial = new SKPaint()
        {
            Color = SKColors.HotPink,
            StrokeWidth = 3,
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint TextSpecial = new SKPaint()
        {
            Color = SKColors.HotPink,
            IsStroke = false,
            TextSize = 12,
            TextEncoding = SKTextEncoding.Utf8,
            IsAntialias = true,
            Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold),
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint PaintScav = new SKPaint()
        {
            Color = SKColors.Yellow,
            StrokeWidth = 3,
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint TextScav = new SKPaint()
        {
            Color = SKColors.Yellow,
            IsStroke = false,
            TextSize = 12,
            TextEncoding = SKTextEncoding.Utf8,
            IsAntialias = true,
            Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold),
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint PaintRaider = new SKPaint()
        {
            Color = SKColor.Parse("ffc70f"),
            StrokeWidth = 3,
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint TextRaider = new SKPaint()
        {
            Color = SKColor.Parse("ffc70f"),
            IsStroke = false,
            TextSize = 12,
            TextEncoding = SKTextEncoding.Utf8,
            IsAntialias = true,
            Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold),
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint PaintBoss = new SKPaint()
        {
            Color = SKColors.Fuchsia,
            StrokeWidth = 3,
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint TextBoss = new SKPaint()
        {
            Color = SKColors.Fuchsia,
            IsStroke = false,
            TextSize = 12,
            TextEncoding = SKTextEncoding.Utf8,
            IsAntialias = true,
            Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold),
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint PaintPScav = new SKPaint()
        {
            Color = SKColors.White,
            StrokeWidth = 3,
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint TextWhite = new SKPaint() // Player Scav Text , Tooltip Text
        {
            Color = SKColors.White,
            IsStroke = false,
            TextSize = 12,
            TextEncoding = SKTextEncoding.Utf8,
            IsAntialias = true,
            Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold),
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint PaintDeathMarker = new SKPaint()
        {
            Color = SKColors.Black,
            StrokeWidth = 3,
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High
        };
        #endregion

        #region Loot Paints
        public static readonly SKPaint PaintLoot = new SKPaint()
        {
            Color = SKColors.WhiteSmoke,
            StrokeWidth = 3,
            Style = SKPaintStyle.Fill,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint PaintImportantLoot = new SKPaint()
        {
            Color = SKColors.Turquoise,
            StrokeWidth = 3,
            Style = SKPaintStyle.Fill,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint TextLoot = new SKPaint()
        {
            Color = SKColors.WhiteSmoke,
            IsStroke = false,
            TextSize = 13,
            TextEncoding = SKTextEncoding.Utf8,
            IsAntialias = true,
            Typeface = SKTypeface.FromFamilyName("Arial"),
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint TextImportantLoot = new SKPaint()
        {
            Color = SKColors.Turquoise,
            IsStroke = false,
            TextSize = 13,
            TextEncoding = SKTextEncoding.Utf8,
            IsAntialias = true,
            Typeface = SKTypeface.FromFamilyName("Arial"),
            FilterQuality = SKFilterQuality.High
        };
        #endregion

        #region Aimview Paints
        public static readonly SKPaint PaintTransparentBacker = new SKPaint()
        {
            Color = SKColors.Black.WithAlpha(0xBE), // Transparent backer
            StrokeWidth = 1,
            Style = SKPaintStyle.Fill,
        };
        public static readonly SKPaint PaintAimviewCrosshair = new SKPaint()
        {
            Color = SKColors.White,
            StrokeWidth = 1,
            Style = SKPaintStyle.Stroke,
            IsAntialias = true
        };
        public static readonly SKPaint PaintAimviewLocalPlayer = new SKPaint()
        {
            Color = SKColors.Green,
            StrokeWidth = 1,
            Style = SKPaintStyle.Fill,
        };
        public static readonly SKPaint PaintAimviewPMC = new SKPaint()
        {
            Color = SKColors.Red,
            StrokeWidth = 1,
            Style = SKPaintStyle.Fill,
        };
        public static readonly SKPaint PaintAimviewSpecial = new SKPaint()
        {
            Color = SKColors.HotPink,
            StrokeWidth = 1,
            Style = SKPaintStyle.Fill,
        };
        public static readonly SKPaint PaintAimviewTeammate = new SKPaint()
        {
            Color = SKColors.LimeGreen,
            StrokeWidth = 1,
            Style = SKPaintStyle.Fill,
        };
        public static readonly SKPaint PaintAimviewBoss = new SKPaint()
        {
            Color = SKColors.Fuchsia,
            StrokeWidth = 1,
            Style = SKPaintStyle.Fill,
        };
        public static readonly SKPaint PaintAimviewScav = new SKPaint()
        {
            Color = SKColors.Yellow,
            StrokeWidth = 1,
            Style = SKPaintStyle.Fill,
        };
        public static readonly SKPaint PaintAimviewRaider = new SKPaint()
        {
            Color = SKColor.Parse("ffc70f"),
            StrokeWidth = 1,
            Style = SKPaintStyle.Fill,
        };
        public static readonly SKPaint PaintAimviewPScav = new SKPaint()
        {
            Color = SKColors.White,
            StrokeWidth = 1,
            Style = SKPaintStyle.Fill,
        };
        #endregion

        #region Render/Misc Paints
        public static readonly SKPaint PaintBitmap = new SKPaint()
        {
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint TextRadarStatus = new SKPaint()
        {
            Color = SKColors.Red,
            IsStroke = false,
            TextSize = 48,
            TextEncoding = SKTextEncoding.Utf8,
            IsAntialias = true,
            Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold),
            TextAlign = SKTextAlign.Center
        };
        public static readonly SKPaint PaintGrenades = new SKPaint()
        {
            Color = SKColors.OrangeRed,
            StrokeWidth = 3,
            Style = SKPaintStyle.Stroke,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint PaintExfilOpen = new SKPaint()
        {
            Color = SKColors.LimeGreen,
            StrokeWidth = 1,
            Style = SKPaintStyle.Fill,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint PaintExfilPending = new SKPaint()
        {
            Color = SKColors.Yellow,
            StrokeWidth = 1,
            Style = SKPaintStyle.Fill,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High
        };
        public static readonly SKPaint PaintExfilClosed = new SKPaint()
        {
            Color = SKColors.Red,
            StrokeWidth = 1,
            Style = SKPaintStyle.Fill,
            IsAntialias = true,
            FilterQuality = SKFilterQuality.High,
        };
        #endregion
    }
}
