using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorFont
{
    public class FontInfo
    {
        public FontInfo()
        {
            Family = new FontFamily("Segoe UI");
            Size = 12;
            Style = new FontStyle();
            Stretch = new FontStretch();
            Weight = new FontWeight();
            BrushColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
        }

        public FontInfo(FontFamily fam, double sz, FontStyle style,
            FontStretch strc, FontWeight weight, SolidColorBrush c)
        {
            Family = fam;
            Size = sz;
            Style = style;
            Stretch = strc;
            Weight = weight;
            BrushColor = c;
        }

        public FontFamily Family { get; set; }
        public double Size { get; set; }
        public FontStyle Style { get; set; }
        public FontStretch Stretch { get; set; }
        public FontWeight Weight { get; set; }
        public SolidColorBrush BrushColor { get; set; }

        public FontColor Color => AvailableColors.GetFontColor(BrushColor);

        public FamilyTypeface Typeface
        {
            get
            {
                var ftf = new FamilyTypeface();
                ftf.Stretch = Stretch;
                ftf.Weight = Weight;
                ftf.Style = Style;
                return ftf;
            }
        }

        #region Static Utils

        public static string TypefaceToString(FamilyTypeface ttf)
        {
            var sb = new StringBuilder(ttf.Stretch.ToString());
            sb.Append("-");
            sb.Append(ttf.Weight);
            sb.Append("-");
            sb.Append(ttf.Style);
            return sb.ToString();
        }

        public static void ApplyFont(Control control, FontInfo font)
        {
            var exist = !(font.Family == null);

            font.Family = control.FontFamily = exist ? font.Family : control.FontFamily;
            font.Size = control.FontSize = exist ? font.Size : control.FontSize;
            font.Style = control.FontStyle = exist ? font.Style : control.FontStyle;
            font.Stretch = control.FontStretch = exist ? font.Stretch : control.FontStretch;
            font.Weight = control.FontWeight = exist ? font.Weight : control.FontWeight;
            control.Foreground = exist ? font.BrushColor : control.Foreground;
            font.BrushColor = (SolidColorBrush) control.Foreground;
        }

        public static FontInfo GetControlFont(Control control)
        {
            var font = new FontInfo();
            font.Family = control.FontFamily;
            font.Size = control.FontSize;
            font.Style = control.FontStyle;
            font.Stretch = control.FontStretch;
            font.Weight = control.FontWeight;
            font.BrushColor = (SolidColorBrush) control.Foreground;
            return font;
        }

        #endregion
    }
}