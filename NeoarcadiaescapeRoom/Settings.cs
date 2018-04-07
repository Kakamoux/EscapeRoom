using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;
using ColorFont;


/*************************************************************************************
 * 
 *			Class       : Settings
 *			Date        : 05/05/2015 19:44:21
 *			Author      : Matthieu
 *			Comment		: Manage Settings
 * 
*************************************************************************************/

namespace NeoarcadiaescapeRoom
{
    /// ---------------------------------------------------------------------
    /// <summary>
    ///   This class defined a Settings
    /// </summary>
    /// ---------------------------------------------------------------------
    public class Settings
    {
        public FontInfo CF { get; set; } = new FontInfo();

        public FontInfo SF { get; set; } = new FontInfo();

        public FontInfo TF { get; set; } = new FontInfo();

        public FontInfo ICLF { get; set; } = new FontInfo();

        public FontInfo ICF { get; set; } = new FontInfo();

        public FontInfo TNLF { get; set; } = new FontInfo();

        public FontInfo TNF { get; set; } = new FontInfo();

        public string TeamName { get; set; }

        #region Properties

        public double BlinkDuration { get; set; } = 2;

        public double BlinkInterval { get; set; } = 500;


        public string TimerEditSound { get; set; } = "";

        public FontColor TimerEditColor { get; set; } = new FontColor("Black", new SolidColorBrush(Colors.Black));

        public bool BlinkOnTimeEdit { get; set; } = true;

        /// <summary>
        private string _timerEditSound2 = "";

        public double BlinkDuration2 { get; set; } = 2;

        public double BlinkInterval2 { get; set; } = 500;


        public string TimerEditSound2
        {
            get => _timerEditSound2;
            set => _timerEditSound2 = value;
        }

        public FontColor TimerEditColor2 { get; set; } = new FontColor("Black", new SolidColorBrush(Colors.Black));

        public bool BlinkOnTimeEdit2 { get; set; } = true;


        /// </summary>


        public string SettingName { get; set; } = "N/A";


        public bool LoopBgdMusic { get; set; } = true;


        public int Timer { get; set; } = 60;

        public bool DeleteAuto { get; set; }


        public int DeleteTime { get; set; } = 10;


        public string BackgroungMusic { get; set; } = "";


        public string GameOverScreen { get; set; } = "";

        public FontColor BgColor { get; set; } = new FontColor("Black", new SolidColorBrush(Colors.Black));

        public string BgImage { get; set; } = "";

        public bool UseBgImage { get; set; }

        #endregion

        #region Public Fields

        #endregion

        #region Protected Fields

        #endregion

        #region Private Fields

        #endregion

        #region Constructors

        public Settings(string s)
        {
            Load(s);
        }

        public Settings()
        {
        }

        #endregion

        #region Public Methods

        public void Create(string s)
        {
            var sr = new XmlDocument();
            XmlElement t;
            var e = sr.AppendChild(sr.CreateElement("Settings")) as XmlElement;
            e.AppendChild(sr.CreateElement("Name")).InnerText = SettingName;
            e.AppendChild(sr.CreateElement("Timer")).InnerText = Timer.ToString();
            e.AppendChild(sr.CreateElement("BackgroundMusic")).InnerText = BackgroungMusic;
            e.AppendChild(sr.CreateElement("LoopBackgroundMusic")).InnerText = LoopBgdMusic.ToString();
            e.AppendChild(sr.CreateElement("DeleteAuto")).InnerText = DeleteAuto.ToString();
            e.AppendChild(sr.CreateElement("DeleteTime")).InnerText = DeleteTime.ToString();
            e.AppendChild(sr.CreateElement("GameOverScreen")).InnerText = GameOverScreen;

            e.AppendChild(sr.CreateElement("BackgroundColorName")).InnerText = BgColor.Name;
            e.AppendChild(sr.CreateElement("BackgroundColor")).InnerText =
                BgColor.Brush.Color.A + "/" + BgColor.Brush.Color.R + "/" + BgColor.Brush.Color.G + "/" +
                BgColor.Brush.Color.B;
            e.AppendChild(sr.CreateElement("BackgroundImage")).InnerText = BgImage;
            e.AppendChild(sr.CreateElement("UseBackgroundImage")).InnerText = UseBgImage.ToString();


            t = sr.CreateElement("CF");
            CreateFontInfo(CF, t, sr);
            e.AppendChild(t);

            t = sr.CreateElement("SF");
            CreateFontInfo(SF, t, sr);
            e.AppendChild(t);


            t = sr.CreateElement("TF");
            CreateFontInfo(CF, t, sr);
            e.AppendChild(t);

            t = sr.CreateElement("ICLF");
            CreateFontInfo(CF, t, sr);
            e.AppendChild(t);


            t = sr.CreateElement("ICF");
            CreateFontInfo(CF, t, sr);
            e.AppendChild(t);


            t = sr.CreateElement("TNLF");
            CreateFontInfo(CF, t, sr);
            e.AppendChild(t);


            t = sr.CreateElement("TNF");
            CreateFontInfo(CF, t, sr);
            e.AppendChild(t);

            e.AppendChild(sr.CreateElement("TimerEditColorName")).InnerText = TimerEditColor.Name;
            e.AppendChild(sr.CreateElement("TimerEditColor")).InnerText =
                TimerEditColor.Brush.Color.A + "/" + TimerEditColor.Brush.Color.R + "/" + TimerEditColor.Brush.Color.G +
                "/" + TimerEditColor.Brush.Color.B;

            e.AppendChild(sr.CreateElement("TimerEditSound")).InnerText = TimerEditSound;
            e.AppendChild(sr.CreateElement("BlinkOnTimeEdit")).InnerText = BlinkOnTimeEdit.ToString();

            e.AppendChild(sr.CreateElement("BlinkDuration")).InnerText = BlinkDuration.ToString();
            e.AppendChild(sr.CreateElement("BlinkInterval")).InnerText = BlinkInterval.ToString();


            e.AppendChild(sr.CreateElement("TimerEditColorName2")).InnerText = TimerEditColor2.Name;
            e.AppendChild(sr.CreateElement("TimerEditColor2")).InnerText =
                TimerEditColor2.Brush.Color.A + "/" + TimerEditColor2.Brush.Color.R + "/" +
                TimerEditColor2.Brush.Color.G + "/" + TimerEditColor2.Brush.Color.B;

            e.AppendChild(sr.CreateElement("TimerEditSound2")).InnerText = TimerEditSound2;
            e.AppendChild(sr.CreateElement("BlinkOnTimeEdit2")).InnerText = BlinkOnTimeEdit2.ToString();

            e.AppendChild(sr.CreateElement("BlinkDuration2")).InnerText = BlinkDuration2.ToString();
            e.AppendChild(sr.CreateElement("BlinkInterval2")).InnerText = BlinkInterval2.ToString();


            sr.Save("./settings/" + s);
        }

        private void CreateFontInfo(FontInfo toSerialize, XmlElement t, XmlDocument sr)
        {
            t.AppendChild(sr.CreateElement("FAMILY")).InnerText = toSerialize.Family.Source;
            t.AppendChild(sr.CreateElement("SIZE")).InnerText = toSerialize.Size.ToString();
            t.AppendChild(sr.CreateElement("FONTSTYLE")).InnerText = toSerialize.Style.ToString();
            t.AppendChild(sr.CreateElement("FONTWEIGHT")).InnerText = toSerialize.Weight.ToString();
            t.AppendChild(sr.CreateElement("FONTSTRETCH")).InnerText = toSerialize.Stretch.ToString();
            t.AppendChild(sr.CreateElement("BRUSH")).InnerText =
                toSerialize.BrushColor.Color.A + "/" + toSerialize.BrushColor.Color.R + "/" +
                toSerialize.BrushColor.Color.G + "/" + toSerialize.BrushColor.Color.B;
        }

        private void SaveFontInfo(FontInfo toSerialize, XmlDocument sr, string anchor)
        {
            if (sr.SelectSingleNode("/Settings//" + anchor + "/FAMILY") == null)
            {
                var t = sr.CreateElement(anchor);
                CreateFontInfo(toSerialize, t, sr);
                sr.SelectSingleNode("/Settings").AppendChild(t);
                return;
            }

            sr.SelectSingleNode("/Settings//" + anchor + "/FAMILY").InnerText = toSerialize.Family.Source;
            sr.SelectSingleNode("/Settings//" + anchor + "/SIZE").InnerText = toSerialize.Size.ToString();
            sr.SelectSingleNode("/Settings//" + anchor + "/FONTSTYLE").InnerText = toSerialize.Style.ToString();
            sr.SelectSingleNode("/Settings//" + anchor + "/FONTWEIGHT").InnerText = toSerialize.Weight.ToString();
            sr.SelectSingleNode("/Settings//" + anchor + "/FONTSTRETCH").InnerText = toSerialize.Stretch.ToString();
            sr.SelectSingleNode("/Settings//" + anchor + "/BRUSH").InnerText =
                toSerialize.BrushColor.Color.A + "/" + toSerialize.BrushColor.Color.R + "/" +
                toSerialize.BrushColor.Color.G + "/" + toSerialize.BrushColor.Color.B;
        }

        private void LoadFontInfo(FontInfo toD, XmlDocument sr, string anchor)
        {
            try
            {
                toD.Family = new FontFamily(sr.SelectSingleNode("/Settings//" + anchor + "/FAMILY").InnerText);
            }
            catch (Exception e)
            {
                return;
            }

            toD.Size = double.Parse(sr.SelectSingleNode("/Settings//" + anchor + "/SIZE").InnerText);

            var propertyInfo = typeof(FontStyles).GetProperty(
                sr.SelectSingleNode("/Settings//" + anchor + "/FONTSTYLE").InnerText,
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.IgnoreCase);

            toD.Style = (FontStyle) propertyInfo.GetValue(null, null);
            var a = sr.SelectSingleNode("/Settings//" + anchor + "/FONTWEIGHT").InnerText;
            propertyInfo = typeof(FontWeights).GetProperty(
                sr.SelectSingleNode("/Settings//" + anchor + "/FONTWEIGHT").InnerText,
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.IgnoreCase);

            toD.Weight = (FontWeight) propertyInfo.GetValue(null, null);

            propertyInfo = typeof(FontStretches).GetProperty(
                sr.SelectSingleNode("/Settings//" + anchor + "/FONTSTRETCH").InnerText,
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.IgnoreCase);

            toD.Stretch = (FontStretch) propertyInfo.GetValue(null, null);

            var b = sr.SelectSingleNode("/Settings//" + anchor + "/BRUSH").InnerText.Split('/');

            toD.BrushColor = new SolidColorBrush(Color.FromArgb(
                byte.Parse(b[0]),
                byte.Parse(b[1]),
                byte.Parse(b[2]),
                byte.Parse(b[3])
            ));
        }

        private FontInfo Deserialize(string toSerialize)
        {
            var serializer = new XmlSerializer(typeof(FontInfo));
            object result;
            FontInfo t;

            using (TextReader reader = new StringReader(toSerialize))
            {
                result = serializer.Deserialize(reader);
                t = (FontInfo) result;
            }

            return t;
        }

        public void Save(string s)
        {
            if (!File.Exists("./settings/" + s))
            {
                Create(s);
                return;
            }

            var sr = new XmlDocument();
            sr.Load("./settings/" + s);
            sr.SelectSingleNode("/Settings//Name").InnerText = SettingName;
            sr.SelectSingleNode("/Settings//Timer").InnerText = Timer.ToString();
            sr.SelectSingleNode("/Settings//BackgroundMusic").InnerText = BackgroungMusic;
            sr.SelectSingleNode("/Settings//LoopBackgroundMusic").InnerText = LoopBgdMusic.ToString();
            sr.SelectSingleNode("/Settings//DeleteAuto").InnerText = DeleteAuto.ToString();
            sr.SelectSingleNode("/Settings//DeleteTime").InnerText = DeleteTime.ToString();
            sr.SelectSingleNode("/Settings//GameOverScreen").InnerText = GameOverScreen;
            sr.SelectSingleNode("/Settings//BackgroundColorName").InnerText = BgColor.Name;
            sr.SelectSingleNode("/Settings//BackgroundColor").InnerText =
                BgColor.Brush.Color.A + "/" + BgColor.Brush.Color.R + "/" + BgColor.Brush.Color.G + "/" +
                BgColor.Brush.Color.B;


            sr.SelectSingleNode("/Settings//BackgroundImage").InnerText = BgImage;
            sr.SelectSingleNode("/Settings//UseBackgroundImage").InnerText = UseBgImage.ToString();


            SaveFontInfo(CF, sr, "CF");
            SaveFontInfo(SF, sr, "SF");
            SaveFontInfo(TF, sr, "TF");
            SaveFontInfo(ICLF, sr, "ICLF");
            SaveFontInfo(ICF, sr, "ICF");
            SaveFontInfo(TNLF, sr, "TNLF");
            SaveFontInfo(TNF, sr, "TNF");

            sr.SelectSingleNode("/Settings//TimerEditColorName").InnerText = TimerEditColor.Name;

            sr.SelectSingleNode("/Settings//TimerEditColor").InnerText =
                TimerEditColor.Brush.Color.A + "/" + TimerEditColor.Brush.Color.R + "/" + TimerEditColor.Brush.Color.G +
                "/" + TimerEditColor.Brush.Color.B;
            sr.SelectSingleNode("/Settings//TimerEditSound").InnerText = TimerEditSound;
            sr.SelectSingleNode("/Settings//BlinkOnTimeEdit").InnerText = BlinkOnTimeEdit.ToString();

            sr.SelectSingleNode("/Settings//BlinkDuration").InnerText = BlinkDuration.ToString();
            sr.SelectSingleNode("/Settings//BlinkInterval").InnerText = BlinkInterval.ToString();


            sr.SelectSingleNode("/Settings//TimerEditColorName2").InnerText = TimerEditColor2.Name;

            sr.SelectSingleNode("/Settings//TimerEditColor2").InnerText =
                TimerEditColor2.Brush.Color.A + "/" + TimerEditColor2.Brush.Color.R + "/" +
                TimerEditColor2.Brush.Color.G + "/" + TimerEditColor2.Brush.Color.B;
            sr.SelectSingleNode("/Settings//TimerEditSound2").InnerText = TimerEditSound2;
            sr.SelectSingleNode("/Settings//BlinkOnTimeEdit2").InnerText = BlinkOnTimeEdit2.ToString();

            sr.SelectSingleNode("/Settings//BlinkDuration2").InnerText = BlinkDuration2.ToString();
            sr.SelectSingleNode("/Settings//BlinkInterval2").InnerText = BlinkInterval2.ToString();


            sr.Save("./settings/" + s);
        }

        public void Load(string filename)
        {
            if (!File.Exists("./settings/" + filename)) Create(filename);

            var sr = new XmlDocument();
            sr.Load("./settings/" + filename);
            SettingName = sr.SelectSingleNode("/Settings//Name").InnerText;
            Timer = int.Parse(sr.SelectSingleNode("/Settings//Timer").InnerText);
            BackgroungMusic = sr.SelectSingleNode("/Settings//BackgroundMusic").InnerText;
            LoopBgdMusic = bool.Parse(sr.SelectSingleNode("/Settings//LoopBackgroundMusic").InnerText);
            DeleteAuto = bool.Parse(sr.SelectSingleNode("/Settings//DeleteAuto").InnerText);
            DeleteTime = int.Parse(sr.SelectSingleNode("/Settings//DeleteTime").InnerText);
            GameOverScreen = sr.SelectSingleNode("/Settings//GameOverScreen").InnerText;
            var n = sr.SelectSingleNode("/Settings//BackgroundColorName").InnerText;

            var b = sr.SelectSingleNode("/Settings//BackgroundColor").InnerText.Split('/');

            var s = new SolidColorBrush(Color.FromArgb(
                byte.Parse(b[0]),
                byte.Parse(b[1]),
                byte.Parse(b[2]),
                byte.Parse(b[3])
            ));
            BgColor = new FontColor(n, s);

            BgImage = sr.SelectSingleNode("/Settings//BackgroundImage").InnerText;
            UseBgImage = bool.Parse(sr.SelectSingleNode("/Settings//UseBackgroundImage").InnerText);


            LoadFontInfo(CF, sr, "CF");
            LoadFontInfo(SF, sr, "SF");
            LoadFontInfo(TF, sr, "TF");
            LoadFontInfo(ICLF, sr, "ICLF");
            LoadFontInfo(ICF, sr, "ICF");
            LoadFontInfo(TNLF, sr, "TNLF");
            LoadFontInfo(TNF, sr, "TNF");

            n = sr.SelectSingleNode("/Settings//TimerEditColorName").InnerText;

            b = sr.SelectSingleNode("/Settings//TimerEditColor").InnerText.Split('/');
            var ssss = new SolidColorBrush(Color.FromArgb(
                byte.Parse(b[0]),
                byte.Parse(b[1]),
                byte.Parse(b[2]),
                byte.Parse(b[3])
            ));

            TimerEditColor = new FontColor(n, ssss);

            n = sr.SelectSingleNode("/Settings//TimerEditColorName2").InnerText;

            b = sr.SelectSingleNode("/Settings//TimerEditColor2").InnerText.Split('/');
            var sssss = new SolidColorBrush(Color.FromArgb(
                byte.Parse(b[0]),
                byte.Parse(b[1]),
                byte.Parse(b[2]),
                byte.Parse(b[3])
            ));

            TimerEditColor2 = new FontColor(n, sssss);


            TimerEditSound = sr.SelectSingleNode("/Settings//TimerEditSound").InnerText;
            BlinkOnTimeEdit = bool.Parse(sr.SelectSingleNode("/Settings//BlinkOnTimeEdit").InnerText);

            BlinkDuration = double.Parse(sr.SelectSingleNode("/Settings//BlinkDuration").InnerText);
            BlinkInterval = double.Parse(sr.SelectSingleNode("/Settings//BlinkInterval").InnerText);

            TimerEditSound2 = sr.SelectSingleNode("/Settings//TimerEditSound2").InnerText;
            BlinkOnTimeEdit2 = bool.Parse(sr.SelectSingleNode("/Settings//BlinkOnTimeEdit2").InnerText);

            BlinkDuration2 = double.Parse(sr.SelectSingleNode("/Settings//BlinkDuration2").InnerText);
            BlinkInterval2 = double.Parse(sr.SelectSingleNode("/Settings//BlinkInterval2").InnerText);
        }

        #endregion

        #region Private Methods

        #endregion
    }
}