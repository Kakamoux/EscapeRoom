using ColorFont;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace NeoarcadiaescapeRoom
{
    /// <summary>
    /// Logique d'interaction pour SecondWindow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        MainWindow m;
      //  ResourceDictionary skin ;
        private System.Drawing.Rectangle workingArea;
        public SecondWindow(MainWindow dataContext)
        {
            
        }


        public SecondWindow(MainWindow mainWindow, System.Drawing.Rectangle workingArea)
        {
            /*Uri uri = new Uri("ProgressBarStyles.xaml", UriKind.Relative);
            StreamResourceInfo info = Application.GetContentStream(uri);
            skin = (ResourceDictionary)XamlReader.Load(info.Stream);*/
            InitializeComponent();
            this.DataContext = mainWindow;

            // TODO: Complete member initialization
            this.m = mainWindow;
            this.workingArea = workingArea;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Topmost = true;
             Left = workingArea.Left;
              Top = workingArea.Top;
              Width = workingArea.Width;
              Height = workingArea.Height;
              WindowState = WindowState.Maximized;
              WindowStyle = WindowStyle.None;

        }

        internal void GameOver()
        {

            container.Children.Clear();
            container.RowDefinitions.Clear();
            if (File.Exists(MainWindow.SETTINGS.GameOverScreen))
            {
                Image i = new Image();
                i.Stretch = Stretch.Fill;
                BitmapImage b = new BitmapImage();
                b.BeginInit();
                b.UriSource = new Uri(MainWindow.SETTINGS.GameOverScreen);
                b.EndInit();
                i.Source = b; 
                container.Children.Add(i);
            }
           
        }



        private void Window_Closed(object sender, EventArgs e)
        {
            m.Reset();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            setFont();
            updateBg();
            ProgressBar.Foreground = MainWindow.SETTINGS.ProgressForeground.Brush;
            ProgressBar.Background = MainWindow.SETTINGS.ProgressBackground.Brush;

            dispatcherTimer.Tick += new EventHandler((a, d) =>
            {
                dispatcherTimer.Dispatcher.BeginInvoke(
                          DispatcherPriority.Normal,
                          new Action(() =>
                          {
                              STimer.Visibility = STimer.Visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
                            
                              DateTime n = DateTime.Now;
                              double dur = removeTime ? MainWindow.SETTINGS.BlinkDuration2 : MainWindow.SETTINGS.BlinkDuration;
                              if (Math.Abs((start - n).TotalSeconds) >dur)
                              {
                                  dispatcherTimer.Stop();
                                  STimer.Visibility = Visibility.Visible;
                                  STimer.Foreground = MainWindow.SETTINGS.TF.BrushColor;
                              }
                          }
                         ));

            });

        }

        internal void setFont()
        {
            FontInfo.ApplyFont(STeamName, MainWindow.SETTINGS.TNF);
            FontInfo.ApplyFont(STeamNameLabel, MainWindow.SETTINGS.TNLF);
            FontInfo.ApplyFont(SCurrentClue, MainWindow.SETTINGS.ICF);
            FontInfo.ApplyFont(SCurrentClueLabel, MainWindow.SETTINGS.ICLF);
            FontInfo.ApplyFont(SCurrentProgressLabel, MainWindow.SETTINGS.PLF);
            FontInfo.ApplyFont(STimer, MainWindow.SETTINGS.TF);
            FontInfo.ApplyFont(SClue, MainWindow.SETTINGS.CF);
        }

        internal void updateBg()
        {
            if (MainWindow.SETTINGS.UseBgImage && File.Exists(MainWindow.SETTINGS.BgImage))
            {
                ImageBrush iss = new ImageBrush(new BitmapImage(new Uri(MainWindow.SETTINGS.BgImage)));
                GameWindow.Background = iss;
            }
            else
            {
                GameWindow.Background = MainWindow.SETTINGS.BgColor.Brush;
            }
        }

        internal void setProgressStyle(string p)
        {
           
           // ProgressBar.Style = skin[p] as Style;

        }

        internal void updateProgressStyle()
        {
            ProgressBar.Foreground = MainWindow.SETTINGS.ProgressForeground.Brush;
            ProgressBar.Background = MainWindow.SETTINGS.ProgressBackground.Brush;
        }

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        DateTime start = DateTime.Now;
        bool removeTime = false;
        internal void blinkTime(bool rm)
        {
            removeTime = rm;
            
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(rm ? MainWindow.SETTINGS.BlinkInterval2 : MainWindow.SETTINGS.BlinkInterval);
            STimer.Foreground = rm ?  MainWindow.SETTINGS.TimerEditColor2.Brush : MainWindow.SETTINGS.TimerEditColor.Brush;
            start = DateTime.Now;
            STimer.Visibility = Visibility.Visible;
            dispatcherTimer.Start();
            
        }
    }
}
