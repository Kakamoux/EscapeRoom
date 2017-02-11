using ColorFont;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;


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
    /// This class defined a Settings
    /// </summary>
    /// ---------------------------------------------------------------------

   public class Settings
    {
        #region Properties
        private string _settingName="N/A";

        private string _timerEditSound="";

        private double _blinkDuration = 2;

        public double BlinkDuration
        {
            get { return _blinkDuration; }
            set { _blinkDuration = value; }
        }

        private double _blinkInterval = 500;

        public double BlinkInterval
        {
            get { return _blinkInterval; }
            set { _blinkInterval = value; }
        }
        
        

        public string TimerEditSound
        {
            get { return _timerEditSound; }
            set { _timerEditSound = value; }
        }

        private FontColor _timerEditColor = new FontColor("Black", new SolidColorBrush(Colors.Black));

        public FontColor TimerEditColor
        {
            get { return _timerEditColor; }
            set { _timerEditColor = value; }
        }

        private bool _blinkOnTimeEdit = true;

        public bool BlinkOnTimeEdit
        {
            get { return _blinkOnTimeEdit ; }
            set { _blinkOnTimeEdit = value; }
        }
        
        /// <summary>


        private string _timerEditSound2 = "";

        private double _blinkDuration2 = 2;

        public double BlinkDuration2
        {
            get { return _blinkDuration2; }
            set { _blinkDuration2 = value; }
        }

        private double _blinkInterval2 = 500;

        public double BlinkInterval2
        {
            get { return _blinkInterval2; }
            set { _blinkInterval2 = value; }
        }



        public string TimerEditSound2
        {
            get { return _timerEditSound2; }
            set { _timerEditSound2 = value; }
        }

        private FontColor _timerEditColor2 = new FontColor("Black", new SolidColorBrush(Colors.Black));

        public FontColor TimerEditColor2
        {
            get { return _timerEditColor2; }
            set { _timerEditColor2 = value; }
        }

        private bool _blinkOnTimeEdit2 = true;

        public bool BlinkOnTimeEdit2
        {
            get { return _blinkOnTimeEdit2; }
            set { _blinkOnTimeEdit2 = value; }
        }


        /// </summary>


        public string SettingName
        {
            get { return _settingName; }
            set { _settingName = value; }
        }

        private bool _loopBgdMusic=true;


        public bool LoopBgdMusic
        {
            get { return _loopBgdMusic; }
            set { _loopBgdMusic = value; }
        }
        

        private int _timer=60;

        public int Timer
        {
            get { return _timer; }
            set { _timer = value; }
        }

        private int _progressMax=10;

        public int ProgressMax
        {
            get { return _progressMax; }
            set { _progressMax = value; }
        }

        private bool _deleteAuto = false;

        public bool DeleteAuto
        {
            get { return _deleteAuto; }
            set { _deleteAuto = value; }
        }

        private int _deleteTime = 10;


        public int DeleteTime
        {
            get { return _deleteTime; }
            set { _deleteTime = value; }
        }
        

        private string _backgroungMusic = "";

        public string BackgroungMusic
        {
            get { return _backgroungMusic; }
            set { _backgroungMusic = value; }
        }

        private string _gameOverScreen="";


        public string GameOverScreen
        {
            get { return _gameOverScreen; }
            set { _gameOverScreen = value; }
        }

        private FontColor _bgColor = new FontColor("Black", new SolidColorBrush(Colors.Black));

        public FontColor BgColor
        {
            get { return _bgColor; }
            set { _bgColor = value; }
        }

        private string _bgImage = "";

        public string BgImage
        {
            get { return _bgImage ; }
            set { _bgImage  = value; }
        }

        private string _progressStyleName = "Style-default";

        public string ProgressStyleName
        {
            get { return _progressStyleName; }
            set { _progressStyleName = value; }
        }
        

        private bool _useBgImage;

        public bool UseBgImage
        {
            get { return _useBgImage; }
            set { _useBgImage = value; }
        }


        private FontColor _progressBrushForeground = new FontColor("Black", new SolidColorBrush(Colors.Black));


        public FontColor ProgressForeground
        {
            get { return _progressBrushForeground; }
            set { _progressBrushForeground = value; }
        }


        private FontColor _progressBackground = new FontColor("Black", new SolidColorBrush(Colors.Black));


        public FontColor ProgressBackground
        {
            get { return _progressBackground; }
            set { _progressBackground = value; }
        }
        
        
        
        
        
        
        
        
        
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
            XmlDocument sr = new XmlDocument();
            XmlElement t ;
            XmlElement e = sr.AppendChild(sr.CreateElement("Settings")) as XmlElement;
            e.AppendChild(sr.CreateElement("Name")).InnerText = _settingName;
            e.AppendChild(sr.CreateElement("ProgressMax")).InnerText = _progressMax.ToString();
            e.AppendChild(sr.CreateElement("Timer")).InnerText = _timer.ToString();
            e.AppendChild(sr.CreateElement("BackgroundMusic")).InnerText = _backgroungMusic;
            e.AppendChild(sr.CreateElement("LoopBackgroundMusic")).InnerText = _loopBgdMusic.ToString();
            e.AppendChild(sr.CreateElement("DeleteAuto")).InnerText = _deleteAuto.ToString();
            e.AppendChild(sr.CreateElement("DeleteTime")).InnerText = _deleteTime.ToString();
            e.AppendChild(sr.CreateElement("GameOverScreen")).InnerText = _gameOverScreen;

            e.AppendChild(sr.CreateElement("BackgroundColorName")).InnerText = BgColor.Name;
            e.AppendChild(sr.CreateElement("BackgroundColor")).InnerText = BgColor.Brush.Color.A + "/" + BgColor.Brush.Color.R + "/" + BgColor.Brush.Color.G + "/" + BgColor.Brush.Color.B;
            e.AppendChild(sr.CreateElement("BackgroundImage")).InnerText = BgImage;
            e.AppendChild(sr.CreateElement("UseBackgroundImage")).InnerText = UseBgImage.ToString();
            e.AppendChild(sr.CreateElement("ProgressStyle")).InnerText = ProgressStyleName;

            e.AppendChild(sr.CreateElement("ProgressForegroundName")).InnerText = ProgressForeground.Name;
            e.AppendChild(sr.CreateElement("ProgressForeground")).InnerText = ProgressForeground.Brush.Color.A + "/" + ProgressForeground.Brush.Color.R + "/" + ProgressForeground.Brush.Color.G + "/" + ProgressForeground.Brush.Color.B;

            e.AppendChild(sr.CreateElement("ProgressBackgroundName")).InnerText = ProgressBackground.Name;
            e.AppendChild(sr.CreateElement("ProgressBackground")).InnerText = ProgressBackground.Brush.Color.A + "/" + ProgressBackground.Brush.Color.R + "/" + ProgressBackground.Brush.Color.G + "/" + ProgressBackground.Brush.Color.B;

            

            t = sr.CreateElement("CF");
            CreateFontInfo(CF, t, sr);
            e.AppendChild(t);



            t = sr.CreateElement("TF");
            CreateFontInfo(CF, t, sr);
            e.AppendChild(t);


            t = sr.CreateElement("PLF");
            CreateFontInfo(PLF, t, sr);
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
            e.AppendChild(sr.CreateElement("TimerEditColor")).InnerText = TimerEditColor.Brush.Color.A + "/" + TimerEditColor.Brush.Color.R + "/" + TimerEditColor.Brush.Color.G + "/" + TimerEditColor.Brush.Color.B;
           
            e.AppendChild(sr.CreateElement("TimerEditSound")).InnerText = TimerEditSound;
            e.AppendChild(sr.CreateElement("BlinkOnTimeEdit")).InnerText = BlinkOnTimeEdit.ToString();

            e.AppendChild(sr.CreateElement("BlinkDuration")).InnerText = BlinkDuration.ToString();
            e.AppendChild(sr.CreateElement("BlinkInterval")).InnerText = BlinkInterval.ToString();





            e.AppendChild(sr.CreateElement("TimerEditColorName2")).InnerText = TimerEditColor2.Name;
            e.AppendChild(sr.CreateElement("TimerEditColor2")).InnerText = TimerEditColor2.Brush.Color.A + "/" + TimerEditColor2.Brush.Color.R + "/" + TimerEditColor2.Brush.Color.G + "/" + TimerEditColor2.Brush.Color.B;

            e.AppendChild(sr.CreateElement("TimerEditSound2")).InnerText = TimerEditSound2;
            e.AppendChild(sr.CreateElement("BlinkOnTimeEdit2")).InnerText = BlinkOnTimeEdit2.ToString();

            e.AppendChild(sr.CreateElement("BlinkDuration2")).InnerText = BlinkDuration2.ToString();
            e.AppendChild(sr.CreateElement("BlinkInterval2")).InnerText = BlinkInterval2.ToString();


            sr.Save("./settings/"+s);
            


        }

        private void CreateFontInfo(ColorFont.FontInfo toSerialize, XmlElement t, XmlDocument sr)
        {
            t.AppendChild(sr.CreateElement("FAMILY")).InnerText = toSerialize.Family.Source;
            t.AppendChild(sr.CreateElement("SIZE")).InnerText = toSerialize.Size.ToString();
            t.AppendChild(sr.CreateElement("FONTSTYLE")).InnerText = toSerialize.Style.ToString();
            t.AppendChild(sr.CreateElement("FONTWEIGHT")).InnerText = toSerialize.Weight.ToString();
            t.AppendChild(sr.CreateElement("FONTSTRETCH")).InnerText = toSerialize.Stretch.ToString();
            t.AppendChild(sr.CreateElement("BRUSH")).InnerText = toSerialize.BrushColor.Color.A + "/" + toSerialize.BrushColor.Color.R + "/" + toSerialize.BrushColor.Color.G + "/" + toSerialize.BrushColor.Color.B;


        }

        private void SaveFontInfo(ColorFont.FontInfo toSerialize, XmlDocument sr, string anchor)
        {
            sr.SelectSingleNode("/Settings//" + anchor + "/FAMILY").InnerText = toSerialize.Family.Source;
            sr.SelectSingleNode("/Settings//" + anchor + "/SIZE").InnerText = toSerialize.Size.ToString();
            sr.SelectSingleNode("/Settings//" + anchor + "/FONTSTYLE").InnerText = toSerialize.Style.ToString();
            sr.SelectSingleNode("/Settings//" + anchor + "/FONTWEIGHT").InnerText = toSerialize.Weight.ToString();
            sr.SelectSingleNode("/Settings//" + anchor + "/FONTSTRETCH").InnerText = toSerialize.Stretch.ToString();
            sr.SelectSingleNode("/Settings//" + anchor + "/BRUSH").InnerText = toSerialize.BrushColor.Color.A + "/" + toSerialize.BrushColor.Color.R + "/" + toSerialize.BrushColor.Color.G + "/" + toSerialize.BrushColor.Color.B;
         



        }

        private void LoadFontInfo(ColorFont.FontInfo toD, XmlDocument sr, string anchor)
        {
            
            toD.Family = new System.Windows.Media.FontFamily(sr.SelectSingleNode("/Settings//" + anchor + "/FAMILY").InnerText);
            toD.Size = double.Parse(sr.SelectSingleNode("/Settings//" + anchor + "/SIZE").InnerText);

            var propertyInfo = typeof(FontStyles).GetProperty(sr.SelectSingleNode("/Settings//" + anchor + "/FONTSTYLE").InnerText,
                                                  BindingFlags.Static |
                                                  BindingFlags.Public |
                                                  BindingFlags.IgnoreCase);

            toD.Style = (FontStyle)propertyInfo.GetValue(null, null);
            var a = sr.SelectSingleNode("/Settings//" + anchor + "/FONTWEIGHT").InnerText;
            propertyInfo = typeof(FontWeights).GetProperty(sr.SelectSingleNode("/Settings//" + anchor + "/FONTWEIGHT").InnerText,
                                                 BindingFlags.Static |
                                                 BindingFlags.Public |
                                                 BindingFlags.IgnoreCase);

            toD.Weight = (FontWeight)propertyInfo.GetValue(null, null);

            propertyInfo = typeof(FontStretches).GetProperty(sr.SelectSingleNode("/Settings//" + anchor + "/FONTSTRETCH").InnerText,
                                                BindingFlags.Static |
                                                BindingFlags.Public |
                                                BindingFlags.IgnoreCase);

            toD.Stretch = (FontStretch)propertyInfo.GetValue(null, null);

            string[] b = sr.SelectSingleNode("/Settings//" + anchor + "/BRUSH").InnerText.Split('/');

            toD.BrushColor = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(
                byte.Parse(b[0]),
                byte.Parse(b[1]),
                byte.Parse(b[2]),
                byte.Parse(b[3])
                ));
 


        }

        private ColorFont.FontInfo Deserialize(String toSerialize)
        {
            var serializer = new XmlSerializer(typeof(ColorFont.FontInfo));
            object result;
            ColorFont.FontInfo t;

            using (TextReader reader = new StringReader(toSerialize))
            {
                result = serializer.Deserialize(reader);
                t = (ColorFont.FontInfo)result;
            }

            return t;
        }

        public void Save(string s)
        {
            if(!File.Exists("./settings/"+s))
            {
                Create(s); return;
            }

            XmlDocument sr = new XmlDocument();
            sr.Load("./settings/"+s);
            sr.SelectSingleNode("/Settings//Name").InnerText = _settingName;
            sr.SelectSingleNode("/Settings//ProgressMax").InnerText = _progressMax.ToString();
            sr.SelectSingleNode("/Settings//Timer").InnerText = _timer.ToString();
            sr.SelectSingleNode("/Settings//BackgroundMusic").InnerText = _backgroungMusic;
            sr.SelectSingleNode("/Settings//LoopBackgroundMusic").InnerText = _loopBgdMusic.ToString();
            sr.SelectSingleNode("/Settings//DeleteAuto").InnerText = _deleteAuto.ToString();
            sr.SelectSingleNode("/Settings//DeleteTime").InnerText = _deleteTime.ToString();
            sr.SelectSingleNode("/Settings//GameOverScreen").InnerText = _gameOverScreen;
            sr.SelectSingleNode("/Settings//BackgroundColorName").InnerText = BgColor.Name;
            sr.SelectSingleNode("/Settings//BackgroundColor").InnerText = BgColor.Brush.Color.A + "/" + BgColor.Brush.Color.R + "/" + BgColor.Brush.Color.G + "/" + BgColor.Brush.Color.B;
           
            
            
            
            sr.SelectSingleNode("/Settings//BackgroundImage").InnerText = BgImage;
            sr.SelectSingleNode("/Settings//UseBackgroundImage").InnerText = UseBgImage.ToString();
            sr.SelectSingleNode("/Settings//ProgressStyle").InnerText = ProgressStyleName;


            sr.SelectSingleNode("/Settings//ProgressForegroundName").InnerText = ProgressForeground.Name;
            sr.SelectSingleNode("/Settings//ProgressForeground").InnerText = ProgressForeground.Brush.Color.A + "/" + ProgressForeground.Brush.Color.R + "/" + ProgressForeground.Brush.Color.G + "/" + ProgressForeground.Brush.Color.B;

            sr.SelectSingleNode("/Settings//ProgressBackgroundName").InnerText = ProgressBackground.Name;

            sr.SelectSingleNode("/Settings//ProgressBackground").InnerText = ProgressBackground.Brush.Color.A + "/" + ProgressBackground.Brush.Color.R + "/" + ProgressBackground.Brush.Color.G + "/" + ProgressBackground.Brush.Color.B;


        
           


            SaveFontInfo(CF, sr, "CF");
            SaveFontInfo(TF, sr, "TF");
            SaveFontInfo(PLF, sr, "PLF");
            SaveFontInfo(ICLF, sr, "ICLF");
            SaveFontInfo(ICF, sr, "ICF");
            SaveFontInfo(TNLF, sr, "TNLF");
            SaveFontInfo(TNF, sr, "TNF");

            sr.SelectSingleNode("/Settings//TimerEditColorName").InnerText = TimerEditColor.Name;

            sr.SelectSingleNode("/Settings//TimerEditColor").InnerText = TimerEditColor.Brush.Color.A + "/" + TimerEditColor.Brush.Color.R + "/" + TimerEditColor.Brush.Color.G + "/" + TimerEditColor.Brush.Color.B;
            sr.SelectSingleNode("/Settings//TimerEditSound").InnerText = TimerEditSound;
            sr.SelectSingleNode("/Settings//BlinkOnTimeEdit").InnerText = BlinkOnTimeEdit.ToString() ;

            sr.SelectSingleNode("/Settings//BlinkDuration").InnerText = BlinkDuration.ToString();
            sr.SelectSingleNode("/Settings//BlinkInterval").InnerText = BlinkInterval.ToString();




            sr.SelectSingleNode("/Settings//TimerEditColorName2").InnerText = TimerEditColor2.Name;

            sr.SelectSingleNode("/Settings//TimerEditColor2").InnerText = TimerEditColor2.Brush.Color.A + "/" + TimerEditColor2.Brush.Color.R + "/" + TimerEditColor2.Brush.Color.G + "/" + TimerEditColor2.Brush.Color.B;
            sr.SelectSingleNode("/Settings//TimerEditSound2").InnerText = TimerEditSound2;
            sr.SelectSingleNode("/Settings//BlinkOnTimeEdit2").InnerText = BlinkOnTimeEdit2.ToString();

            sr.SelectSingleNode("/Settings//BlinkDuration2").InnerText = BlinkDuration2.ToString();
            sr.SelectSingleNode("/Settings//BlinkInterval2").InnerText = BlinkInterval2.ToString();
            

            sr.Save("./settings/"+s);
            
        }

        public void Load(string filename)
        {
            if (!File.Exists("./settings/" + filename))
            {
                Create(filename); 
            }

            XmlDocument sr = new XmlDocument();
            sr.Load("./settings/" + filename);
            SettingName =  sr.SelectSingleNode("/Settings//Name").InnerText;
            ProgressMax = Int32.Parse(sr.SelectSingleNode("/Settings//ProgressMax").InnerText);
            Timer = Int32.Parse(sr.SelectSingleNode("/Settings//Timer").InnerText);
            BackgroungMusic = sr.SelectSingleNode("/Settings//BackgroundMusic").InnerText;
            LoopBgdMusic = bool.Parse(sr.SelectSingleNode("/Settings//LoopBackgroundMusic").InnerText);
            DeleteAuto = bool.Parse(sr.SelectSingleNode("/Settings//DeleteAuto").InnerText);
            DeleteTime = Int32.Parse(sr.SelectSingleNode("/Settings//DeleteTime").InnerText);
            GameOverScreen = sr.SelectSingleNode("/Settings//GameOverScreen").InnerText;
            string n = sr.SelectSingleNode("/Settings//BackgroundColorName").InnerText;

            string[] b = sr.SelectSingleNode("/Settings//BackgroundColor").InnerText.Split('/');

            System.Windows.Media.SolidColorBrush s  = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(
                byte.Parse(b[0]),
                byte.Parse(b[1]),
                byte.Parse(b[2]),
                byte.Parse(b[3])
                ));
            BgColor = new FontColor(n, s);

            BgImage = sr.SelectSingleNode("/Settings//BackgroundImage").InnerText;
            UseBgImage = bool.Parse(sr.SelectSingleNode("/Settings//UseBackgroundImage").InnerText);
            ProgressStyleName = sr.SelectSingleNode("/Settings//ProgressStyle").InnerText;

            n = sr.SelectSingleNode("/Settings//ProgressForegroundName").InnerText;
            b = sr.SelectSingleNode("/Settings//ProgressForeground").InnerText.Split('/');
            System.Windows.Media.SolidColorBrush ss = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(
                byte.Parse(b[0]),
                byte.Parse(b[1]),
                byte.Parse(b[2]),
                byte.Parse(b[3])
                ));

            ProgressForeground = new FontColor(n, ss);

            n = sr.SelectSingleNode("/Settings//ProgressBackgroundName").InnerText;
            b = sr.SelectSingleNode("/Settings//ProgressBackground").InnerText.Split('/');
            System.Windows.Media.SolidColorBrush sss = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(
                byte.Parse(b[0]),
                byte.Parse(b[1]),
                byte.Parse(b[2]),
                byte.Parse(b[3])
                ));

            ProgressBackground = new FontColor(n, sss);



            LoadFontInfo(CF, sr, "CF");
            LoadFontInfo(TF, sr, "TF");
            LoadFontInfo(PLF, sr, "PLF");
            LoadFontInfo(ICLF, sr, "ICLF");
            LoadFontInfo(ICF, sr, "ICF");
            LoadFontInfo(TNLF, sr, "TNLF");
            LoadFontInfo(TNF, sr, "TNF");

            n = sr.SelectSingleNode("/Settings//TimerEditColorName").InnerText;

            b = sr.SelectSingleNode("/Settings//TimerEditColor").InnerText.Split('/');
            System.Windows.Media.SolidColorBrush ssss = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(
                byte.Parse(b[0]),
                byte.Parse(b[1]),
                byte.Parse(b[2]),
                byte.Parse(b[3])
                ));

            TimerEditColor = new FontColor(n, ssss);

            n = sr.SelectSingleNode("/Settings//TimerEditColorName2").InnerText;

            b = sr.SelectSingleNode("/Settings//TimerEditColor2").InnerText.Split('/');
            System.Windows.Media.SolidColorBrush sssss = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(
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

        private ColorFont.FontInfo _cf = new ColorFont.FontInfo();
        private ColorFont.FontInfo _tf = new ColorFont.FontInfo();
        private ColorFont.FontInfo _plf = new ColorFont.FontInfo();
        private ColorFont.FontInfo _iclf = new ColorFont.FontInfo();
        private ColorFont.FontInfo _icf = new ColorFont.FontInfo();
        private ColorFont.FontInfo _tnlf = new ColorFont.FontInfo();
        private ColorFont.FontInfo _tnf = new ColorFont.FontInfo();



        public ColorFont.FontInfo CF { get { return _cf; } set { _cf = value; } }

        public ColorFont.FontInfo TF { get { return _tf; } set { _tf = value; } }

        public ColorFont.FontInfo PLF { get { return _plf; } set { _plf = value; } }

        public ColorFont.FontInfo ICLF { get { return _iclf; } set { _iclf = value; } }

        public ColorFont.FontInfo ICF { get { return _icf; } set { _icf = value; } }

        public ColorFont.FontInfo TNLF { get { return _tnlf; } set { _tnlf = value; } }

        public ColorFont.FontInfo TNF { get { return _tnf; } set { _tnf = value; } }

        public string TeamName { get; set; }
    }
}
