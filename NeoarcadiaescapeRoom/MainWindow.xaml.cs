using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Xceed.Wpf.Toolkit;
using Application = System.Windows.Application;
using Button = System.Windows.Controls.Button;
using ColorPicker = ColorFont.ColorPicker;
using Image = System.Windows.Controls.Image;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using WindowState = System.Windows.WindowState;

namespace NeoarcadiaescapeRoom
{
    /// <summary>
    ///   Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Settings SETTINGS = new Settings("default.st");

        private readonly MusicPlayer _player = new MusicPlayer();
        private readonly MusicPlayer _playerBackground = new MusicPlayer();
        private readonly MusicPlayer _playerSFX = new MusicPlayer();
        private readonly object o = new object();
        private int _clueFontSize = 120;

        private int _dirtySecondCounter;
        private DispatcherTimer _dispatcherTimer;

        private int _hubFontSize = 32;

        private int _timerFontSize = 120;

        private SecondWindow gameWindow;
        private bool isPaused;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            var d = new DirectoryInfo(Environment.CurrentDirectory + "/clueSounds"); //Assuming Test is your Folder
            var Files = d.GetFiles("*.wav").Union(d.GetFiles("*.mp3")).ToArray(); //Getting Text files

            foreach (var file in Files) SoundList.Items.Add(file.Name);
        }

        private void StartGame()
        {
            StopGame.IsEnabled = true;
            PauseGame.IsEnabled = true;
            Send.IsEnabled = true;
            Delete.IsEnabled = true;
            Question.IsEnabled = true;
            addMinute.IsEnabled = true;
            removeMinute.IsEnabled = true;
            BrowseBgdMusic.IsEnabled = true;
            BackgroundMusicLabel.Content = SETTINGS.BackgroungMusic;
            LoopBgdM.IsEnabled = true;
            LoopBgdM.IsChecked = SETTINGS.LoopBgdMusic;
            StopBgdMusic.IsEnabled = true;
            deleteTime.IsEnabled = true;
            deleteAuto.IsEnabled = true;
            deleteTime.Value = SETTINGS.DeleteTime;
            deleteAuto.IsChecked = SETTINGS.DeleteAuto;
            AddCluePicture.IsEnabled = true;
            RemoveCluePicture.IsEnabled = true;
            ClearCluePicture.IsEnabled = true;
            CluePictures.IsEnabled = true;
            GameOverScreenChanged.IsEnabled = true;
            fontConfig.IsEnabled = true;
            GameOverScreenLabel.Content = SETTINGS.GameOverScreen;
            BgimageLabel.Content = SETTINGS.BgImage;
            browseBgImg.IsEnabled = true;
            checkUseBgImg.IsChecked = SETTINGS.UseBgImage ? true : false;
            checkUseBgImg.IsEnabled = true;
            colorPicker.IsEnabled = true;
            colorPicker.setSelectedColor(SETTINGS.BgColor.Name);
            DefaultScore.IsEnabled = false;
            RemovePointCheckBox.IsEnabled = true;


            foreach (Button v in sfxPanel.Children) v.IsEnabled = true;

            foreach (var v in ScoringPannel.Children)
                if (v is Button)
                    (v as Button).IsEnabled = true;

            Rectangle workingArea;
            if (SystemInformation.MonitorCount > 1)
                workingArea = Screen.AllScreens[1].WorkingArea;
            else
                workingArea = Screen.AllScreens[0].WorkingArea;
            gameWindow = new SecondWindow(this, workingArea);


            //init timer
            _dirtySecondCounter = 0;
            TimerTimeSpan = TimeSpan.FromMinutes(SETTINGS.Timer);
            gameWindow.STimer.Content = TimerTimeSpan.ToString("c");
            gameWindow.SScore.Content = "Score : " + DefaultScore.Value;
            MScore.Content = DefaultScore.Value;
            gameWindow.STeamName.Content = SETTINGS.SettingName;
            _dispatcherTimer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                lock (o)
                {
                    gameWindow.STimer.Content = TimerTimeSpan.ToString("c");
                    MTimer.Content = gameWindow.STimer.Content;

                    if (_dirtySecondCounter % 60 == 0 && _dirtySecondCounter != 0) UpdateScore(-1);

                    if (TimerTimeSpan == TimeSpan.Zero)
                    {
                        _dispatcherTimer.Stop();
                        gameWindow.GameOver();
                        disableControl();
                    }

                    _dirtySecondCounter++;

                    TimerTimeSpan = TimerTimeSpan.Add(TimeSpan.FromSeconds(-1));
                }
            }, Application.Current.Dispatcher);


            if (SETTINGS.BackgroungMusic != string.Empty)
            {
                _playerBackground.Open(SETTINGS.BackgroungMusic);
                _playerBackground.Play(SETTINGS.LoopBgdMusic);
                _playerBackground.Play(SETTINGS.LoopBgdMusic);
            }

            ClueFontSizeControl.Value = (int) SETTINGS.CF.Size;
            ScoreFontSizeControl.Value = (int) SETTINGS.SF.Size;
            TimerFontSizeControl.Value = (int) SETTINGS.TF.Size;
            gameWindow.Show();
            _dispatcherTimer.Start();
        }

        private void disableControl()
        {
            BrowseBgdMusic.IsEnabled = false;
            Send.IsEnabled = false;
            Delete.IsEnabled = false;

            RemovePointCheckBox.IsEnabled = false;
            PauseGame.IsEnabled = false;
            DefaultScore.IsEnabled = true;
            addMinute.IsEnabled = false;
            removeMinute.IsEnabled = false;
            LoopBgdM.IsEnabled = false;
            LoopBgdM.IsChecked = false;
            StopBgdMusic.IsEnabled = false;
            deleteTime.IsEnabled = false;
            deleteAuto.IsEnabled = false;
            AddCluePicture.IsEnabled = false;
            RemoveCluePicture.IsEnabled = false;
            ClearCluePicture.IsEnabled = false;
            CluePictures.IsEnabled = false;
            GameOverScreenChanged.IsEnabled = false;
            fontConfig.IsEnabled = false;
            browseBgImg.IsEnabled = false;
            checkUseBgImg.IsEnabled = false;
            colorPicker.IsEnabled = false;

            foreach (var v in ScoringPannel.Children)
                if (v is Button)
                    (v as Button).IsEnabled = false;
        }

        internal void ScoreBtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            UpdateScore(int.Parse(btn.Tag.ToString()));
        }

        internal void Victory_Click(object sender, RoutedEventArgs e)
        {
            PauseGame_Click(sender, e);

            UpdateScore(20);
        }

        private void UpdateScore(int amount)
        {
            var score = int.Parse(MScore.Content.ToString()) + amount;

            gameWindow.SScore.Content = "Score : " + score;
            MScore.Content = score;
        }

        internal void NewGame_Click(object sender, RoutedEventArgs e)
        {
            Reset();

            var n = new SettingSelect();
            n.ShowDialog();
            if (n.DialogResult.HasValue && n.DialogResult.Equals(true))
            {
                n.Close();
                NewGame.IsEnabled = false;
                StartGame();
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            if ((bool) RemovePointCheckBox.IsChecked) UpdateScore(-2);

            LastClue = Question.Text;
            gameWindow.SClue.Text = LastClue;
            CurrentClue++;

            gameWindow.SCurrentClue.Content = CurrentClue;

            CurrentClueLabel.Content = CurrentClue;
            LastClueLabel.Content = LastClue;
            setPlayerImage(true);
            setPlayerImage();

            if (SelectedSound != string.Empty)
            {
                _player.Open("clueSounds/" + SelectedSound);
                _player.Play();
            }

            if (SETTINGS.DeleteAuto)
                setTimeout(delayedClueDelete, SETTINGS.DeleteTime);
        }

        private void delayedClueDelete()
        {
            Delete_Click(null, null);
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            LastClue = string.Empty;
            gameWindow.SClue.Text = LastClue;
            LastClueLabel.Content = "";
            setPlayerImage(true);
        }


        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            var workingArea = Screen.AllScreens[0].WorkingArea;

            Width = workingArea.Width;
            Height = workingArea.Height;
            WindowState = WindowState.Maximized;
        }

        internal void Reset()
        {
            BackgroundMusicLabel.Content = "";
            BrowseBgdMusic.IsEnabled = false;
            Send.IsEnabled = false;
            Delete.IsEnabled = false;
            Question.IsEnabled = false;
            PauseGame.IsEnabled = false;
            StopGame.IsEnabled = false;
            NewGame.IsEnabled = true;
            LastClueLabel.Content = "";
            CurrentClueLabel.Content = "0";
            addMinute.IsEnabled = false;
            removeMinute.IsEnabled = false;
            LoopBgdM.IsEnabled = false;
            LoopBgdM.IsChecked = false;
            StopBgdMusic.IsEnabled = false;
            deleteTime.IsEnabled = false;
            deleteAuto.IsEnabled = false;
            AddCluePicture.IsEnabled = false;
            RemoveCluePicture.IsEnabled = false;
            ClearCluePicture.IsEnabled = false;
            CluePictures.IsEnabled = false;
            GameOverScreenChanged.IsEnabled = false;
            fontConfig.IsEnabled = false;
            BgimageLabel.Content = "";
            browseBgImg.IsEnabled = false;
            checkUseBgImg.IsEnabled = false;
            colorPicker.IsEnabled = false;
            GameOverScreenLabel.Content = "";
            CluePictures.Items.Clear();

            foreach (Button v in sfxPanel.Children) v.IsEnabled = false;


            _playerBackground.Stop();
            _player.Stop();

            if (gameWindow != null)
            {
                gameWindow.Close();
                gameWindow = null;
            }

            if (_dispatcherTimer != null)
            {
                _dispatcherTimer.Stop();
                _dispatcherTimer = null;
            }

            Question.Text = string.Empty;
            CurrentClue = 0;
            LastClue = string.Empty;
            PauseGame.Content = "PauseGame";
        }

        private void StopGame_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void PauseGame_Click(object sender, RoutedEventArgs e)
        {
            if (_dispatcherTimer != null)
                if (isPaused == false)
                {
                    isPaused = true;
                    PauseGame.Content = "ResumeGame";
                    _dispatcherTimer.Stop();
                }
                else
                {
                    isPaused = false;
                    PauseGame.Content = "PauseGame";
                    _dispatcherTimer.Start();
                }
        }

        private void addMinute_Click(object sender, RoutedEventArgs e)
        {
            lock (o)
            {
                TimerTimeSpan = TimerTimeSpan.Add(TimeSpan.FromMinutes((int) minToAdd.Value));
            }

            if (File.Exists(SETTINGS.TimerEditSound))
            {
                _playerSFX.Stop();
                _playerSFX.Open(SETTINGS.TimerEditSound);
                _playerSFX.Play();
            }

            if (gameWindow != null && SETTINGS.BlinkOnTimeEdit) gameWindow.blinkTime(false);
        }

        private void removeMinute_Click(object sender, RoutedEventArgs e)
        {
            lock (o)
            {
                TimerTimeSpan = TimerTimeSpan.Add(TimeSpan.FromMinutes(-(int) minToAdd.Value));
            }

            if (File.Exists(SETTINGS.TimerEditSound2))
            {
                _playerSFX.Stop();
                _playerSFX.Open(SETTINGS.TimerEditSound2);
                _playerSFX.Play();
            }

            if (gameWindow != null && SETTINGS.BlinkOnTimeEdit2) gameWindow.blinkTime(true);
        }

        private void BrowseBgdMusic_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();


            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".wav";
            dlg.Filter = "WAV Files (*.wav)|*.wav|MP3 Files (*.mp3)|*.mp3";


            // Display OpenFileDialog by calling ShowDialog method 
            var result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                var filename = dlg.FileName;
                BackgroundMusicLabel.Content = filename;
                _playerBackground.Stop();
                _playerBackground.Open(filename);
                _playerBackground.Play(LoopBgdM.IsChecked == null ? false : (bool) LoopBgdM.IsChecked);
            }
        }

        private void StopBgdMusic_Click(object sender, RoutedEventArgs e)
        {
            _playerBackground.Stop();
            BackgroundMusicLabel.Content = "";
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            var d = new DirectoryInfo(Environment.CurrentDirectory + "/SFX"); //Assuming Test is your Folder
            var Files = d.GetFiles("*.wav").Union(d.GetFiles("*.mp3")).ToArray(); //Getting Text files

            foreach (var file in Files)
            {
                var btn = new Button();
                btn.Content = file.Name;
                btn.Click += sfxClick;
                btn.Margin = new Thickness(5d, 5d, 5d, 5d);
                btn.IsEnabled = false;
                sfxPanel.Children.Add(btn);
            }

            deleteAuto.IsChecked = SETTINGS.DeleteAuto;
            deleteTime.Value = SETTINGS.DeleteTime;
            checkUseBgImg.IsChecked = SETTINGS.UseBgImage ? true : false;
            colorPicker.setSelectedColor(SETTINGS.BgColor.Name);
        }

        private void sfxClick(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            _playerSFX.Stop();
            _playerSFX.Open("./SFX/" + b.Content);
            _playerSFX.Play();
        }

        public void setTimeout(Action TheAction, int Timeout)
        {
            var t = new Thread(
                () =>
                {
                    Thread.Sleep(Timeout * 1000);
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal,
                        new Action(() => { TheAction.Invoke(); }));
                }
            );
            t.Start();
        }

        private void deleteAuto_Checked(object sender, RoutedEventArgs e)
        {
            SETTINGS.DeleteAuto = true;
        }

        private void deleteAuto_Unchecked(object sender, RoutedEventArgs e)
        {
            SETTINGS.DeleteAuto = false;
        }

        private void deleteTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SETTINGS.DeleteTime = (int) ((IntegerUpDown) sender).Value;
        }

        private void AddCluePicture_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();


            dlg.Multiselect = true;
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg";


            // Display OpenFileDialog by calling ShowDialog method 
            var result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
                foreach (var file in dlg.FileNames)
                    CluePictures.Items.Add(file);
        }

        private void RemoveCluePicture_Click(object sender, RoutedEventArgs e)
        {
            if (CluePictures.SelectedItem == null) return;
            CluePictures.Items.Remove(CluePictures.SelectedItem);
        }

        private void ClearCluePicture_Click(object sender, RoutedEventArgs e)
        {
            CluePictures.Items.Clear();
        }

        private void setPlayerImage(bool reset = false)
        {
            if (gameWindow == null) return;
            if (reset)
            {
                gameWindow.Pictures.Children.Clear();
                return;
            }

            foreach (string s in CluePictures.Items)
            {
                var i = new Image();
                var b = new BitmapImage();
                b.BeginInit();
                b.UriSource = new Uri(s);
                b.EndInit();
                i.Source = b;
                i.Margin = new Thickness(5, 5, 5, 5);
                gameWindow.Pictures.Children.Add(i);
            }
        }

        private void GameOverScreenChanged_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();


            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg";


            // Display OpenFileDialog by calling ShowDialog method 
            var result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                var filename = dlg.FileName;
                GameOverScreenLabel.Content = filename;
                SETTINGS.GameOverScreen = filename;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var f = new FontConfigurator(SETTINGS);
            var r = f.ShowDialog();
            if (r == true)
                if (gameWindow != null)
                    gameWindow.setFont();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SETTINGS.UseBgImage = true;
            if (gameWindow != null)
                gameWindow.updateBg();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SETTINGS.UseBgImage = false;
            if (gameWindow != null)
                gameWindow.updateBg();
        }

        private void colorPicker_ColorChanged(object sender, RoutedEventArgs e)
        {
            SETTINGS.BgColor = ((ColorPicker) sender).SelectedColor;
            gameWindow.updateBg();
        }

        private void browseBgImg_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();


            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg";


            // Display OpenFileDialog by calling ShowDialog method 
            var result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                var filename = dlg.FileName;
                SETTINGS.BgImage = filename;
                gameWindow.updateBg();
            }
        }

        private void newSettings_Click(object sender, RoutedEventArgs e)
        {
            var n = new NewGameSettings();
            n.ShowDialog();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void editSettings_Click(object sender, RoutedEventArgs e)
        {
            var s = new SettingSelect(true);
            s.ShowDialog();
        }


        #region propfull

        public int ClueFontSize
        {
            get => _clueFontSize;
            set
            {
                _clueFontSize = value;
                if (gameWindow != null) gameWindow.SClue.FontSize = _clueFontSize;
            }
        }

        private int _scoreFontSize = 120;

        public int ScoreFontSize
        {
            get => _scoreFontSize;
            set
            {
                _scoreFontSize = value;
                if (gameWindow != null) gameWindow.SScore.FontSize = _scoreFontSize;
            }
        }


        public int TimerFontSize
        {
            get => _timerFontSize;
            set
            {
                _timerFontSize = value;
                if (gameWindow != null) gameWindow.STimer.FontSize = value;
            }
        }


        public int HubFontSize
        {
            get => _hubFontSize;
            set
            {
                _hubFontSize = value;
                if (gameWindow != null)
                {
                    gameWindow.STeamNameLabel.FontSize = _hubFontSize;
                    gameWindow.SCurrentClueLabel.FontSize = _hubFontSize;
                    gameWindow.SCurrentClue.FontSize = _hubFontSize;
                    gameWindow.STeamName.FontSize = _hubFontSize;
                }
            }
        }


        public string SelectedSound { get; set; } = string.Empty;


        public string TimerString { get; set; } = "yolo";


        public TimeSpan TimerTimeSpan { get; set; }


        public int Timer { get; set; } = 60;

        public string LastClue { get; set; }

        public int CurrentClue { get; set; }


        public string TeamName { get; set; }

        #endregion
    }
}