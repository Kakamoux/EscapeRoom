using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ColorFont;
using Image = System.Windows.Controls.Image;

namespace NeoarcadiaescapeRoom
{
    /// <summary>
    ///   Logique d'interaction pour SecondWindow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        private readonly DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private readonly MainWindow m;
        private bool removeTime;

        private DateTime start = DateTime.Now;

        //  ResourceDictionary skin ;
        private Rectangle workingArea;

        public SecondWindow(MainWindow dataContext)
        {
        }


        public SecondWindow(MainWindow mainWindow, Rectangle workingArea)
        {
            InitializeComponent();
            DataContext = mainWindow;

            // TODO: Complete member initialization
            m = mainWindow;
            this.workingArea = workingArea;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*Topmost = true;
             Left = workingArea.Left;
              Top = workingArea.Top;
              Width = workingArea.Width;
              Height = workingArea.Height;
              WindowState = WindowState.Maximized;
              WindowStyle = WindowStyle.None;*/
        }

        internal void GameOver()
        {
            container.Children.Clear();
            container.RowDefinitions.Clear();
            if (File.Exists(MainWindow.SETTINGS.GameOverScreen))
            {
                var i = new Image();
                i.Stretch = Stretch.Fill;
                var b = new BitmapImage();
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

            dispatcherTimer.Tick += (a, d) =>
            {
                dispatcherTimer.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(() =>
                        {
                            STimer.Visibility = STimer.Visibility == Visibility.Hidden
                                ? Visibility.Visible
                                : Visibility.Hidden;

                            var n = DateTime.Now;
                            var dur = removeTime
                                ? MainWindow.SETTINGS.BlinkDuration2
                                : MainWindow.SETTINGS.BlinkDuration;
                            if (Math.Abs((start - n).TotalSeconds) > dur)
                            {
                                dispatcherTimer.Stop();
                                STimer.Visibility = Visibility.Visible;
                                STimer.Foreground = MainWindow.SETTINGS.TF.BrushColor;
                            }
                        }
                    ));
            };
        }

        internal void setFont()
        {
            FontInfo.ApplyFont(STeamName, MainWindow.SETTINGS.TNF);
            FontInfo.ApplyFont(STeamNameLabel, MainWindow.SETTINGS.TNLF);
            FontInfo.ApplyFont(SCurrentClue, MainWindow.SETTINGS.ICF);
            FontInfo.ApplyFont(SCurrentClueLabel, MainWindow.SETTINGS.ICLF);
            FontInfo.ApplyFont(STimer, MainWindow.SETTINGS.TF);
            FontInfo.ApplyFont(SClue, MainWindow.SETTINGS.CF);
            FontInfo.ApplyFont(SScore, MainWindow.SETTINGS.SF);
        }

        internal void updateBg()
        {
            if (MainWindow.SETTINGS.UseBgImage && File.Exists(MainWindow.SETTINGS.BgImage))
            {
                var iss = new ImageBrush(new BitmapImage(new Uri(MainWindow.SETTINGS.BgImage)));
                GameWindow.Background = iss;
            }
            else
            {
                GameWindow.Background = MainWindow.SETTINGS.BgColor.Brush;
            }
        }

        internal void blinkTime(bool rm)
        {
            removeTime = rm;

            dispatcherTimer.Interval =
                TimeSpan.FromMilliseconds(rm ? MainWindow.SETTINGS.BlinkInterval2 : MainWindow.SETTINGS.BlinkInterval);
            STimer.Foreground =
                rm ? MainWindow.SETTINGS.TimerEditColor2.Brush : MainWindow.SETTINGS.TimerEditColor.Brush;
            start = DateTime.Now;
            STimer.Visibility = Visibility.Visible;
            dispatcherTimer.Start();
        }
    }
}