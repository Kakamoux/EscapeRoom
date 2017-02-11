using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Xceed.Wpf.Toolkit;

namespace NeoarcadiaescapeRoom
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static Settings SETTINGS = new Settings("default.st");
        private int _timer =60;
        private object o = new object();

        private TimeSpan _timerTimeSpan;
        private DispatcherTimer _dispatcherTimer;
        private string _timerString = "yolo";
        private bool isPaused = false;
        private string _selectedSound = String.Empty;

        private int _hubFontSize = 32;

        private int _timerFontSize = 120;
        private int _clueFontSize = 120;
        
        MusicPlayer _player = new MusicPlayer();
        MusicPlayer _playerBackground = new MusicPlayer();
        MusicPlayer _playerSFX = new MusicPlayer();


        #region propfull
        public int ClueFontSize
        {
            get { return _clueFontSize; }
            set { _clueFontSize = value; if (gameWindow != null) gameWindow.SClue.FontSize = _clueFontSize; }
        }


        


        public int TimerFontSize
        {
            get { return _timerFontSize; }
            set { _timerFontSize = value; if (gameWindow != null) gameWindow.STimer.FontSize = value; }
        }
        

        public int HubFontSize
        {
            get { return _hubFontSize; }
            set { _hubFontSize = value; if (gameWindow != null) { gameWindow.STeamNameLabel.FontSize = _hubFontSize; gameWindow.SCurrentProgressLabel.FontSize = _hubFontSize; gameWindow.SCurrentClueLabel.FontSize = _hubFontSize; gameWindow.SCurrentClue.FontSize = _hubFontSize; gameWindow.STeamName.FontSize = _hubFontSize; } }
        }
        

        public string SelectedSound
        {
            get { return _selectedSound; }
            set { _selectedSound = value; }
        }
        

        public string TimerString
        {
            get { return _timerString; }
            set { _timerString = value; }
        }
        

        public TimeSpan TimerTimeSpan
        {
            get { return _timerTimeSpan; }
            set { _timerTimeSpan = value; }
        }
        

        public int Timer
        {
            get { return _timer; }
            set { _timer = value; }
        }

        private string _lastClue;

        public string LastClue
        {
            get { return _lastClue; }
            set { _lastClue = value; }
        }

        private int _currentClue = 0;

        public int CurrentClue
        {
            get { return _currentClue; }
            set { _currentClue = value; }
        }

        private int _progressMax = 10;

        public int ProgressMax
        {
            get { return _progressMax; }
            set { _progressMax = value; }
        }

        private int _currentProgress = 0;

        public int CurrentProgress
        {
            get { return _currentProgress; }
            set { _currentProgress = value; }
        }

        private string _teamName;

        public string TeamName
        {
            get { return _teamName; }
            set { _teamName = value; }
        }

        #endregion

        private SecondWindow gameWindow = null;
        
        

        public MainWindow()
        {

           

            InitializeComponent();
            this.DataContext = this;

            DirectoryInfo d = new DirectoryInfo(Environment.CurrentDirectory+"/clueSounds");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.wav").Union(d.GetFiles("*.mp3")).ToArray() ; //Getting Text files
           
            foreach (FileInfo file in Files)
            {
                SoundList.Items.Add(file.Name);
            }

        }

        private void StartGame()
        {
           
          
                
                StopGame.IsEnabled = true;
                PauseGame.IsEnabled = true;
                Send.IsEnabled = true;
                Delete.IsEnabled = true;
                Question.IsEnabled = true;
                ProgressAdd.IsEnabled = true;
                ProgressRemove.IsEnabled = true;
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
                browseBgImg.IsEnabled=true;
                checkUseBgImg.IsChecked = SETTINGS.UseBgImage ? true : false;
                checkUseBgImg.IsEnabled = true;
                colorPicker.IsEnabled = true;
                progressFG.IsEnabled = true;
                progressBG.IsEnabled = true;
                colorPicker.setSelectedColor(SETTINGS.BgColor.Name);
                progressFG.setSelectedColor(SETTINGS.ProgressForeground.Name);
                progressBG.setSelectedColor(SETTINGS.ProgressBackground.Name);


                foreach (Button v in sfxPanel.Children)
                {
                    v.IsEnabled = true;
                }
        
                System.Drawing.Rectangle workingArea;
                if (System.Windows.Forms.SystemInformation.MonitorCount > 1)
                {


                      workingArea = System.Windows.Forms.Screen.AllScreens[1].WorkingArea;
             
           
            
                }else
                {
                    workingArea = System.Windows.Forms.Screen.AllScreens[0].WorkingArea;
                }
                gameWindow = new SecondWindow(this, workingArea);


                //init timer

                _timerTimeSpan = TimeSpan.FromMinutes(SETTINGS.Timer);
                gameWindow.STimer.Content = _timerTimeSpan.ToString("c");
                gameWindow.STeamName.Content = SETTINGS.SettingName;
                _dispatcherTimer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                {
                    lock (o)
                    {
                        
                        gameWindow.STimer.Content = _timerTimeSpan.ToString("c");
                        MTimer.Content = gameWindow.STimer.Content;
                        if (_timerTimeSpan == TimeSpan.Zero) { _dispatcherTimer.Stop(); gameWindow.GameOver(); disableControl(); }
                        _timerTimeSpan = _timerTimeSpan.Add(TimeSpan.FromSeconds(-1));
                    }
                }, Application.Current.Dispatcher);


                if(SETTINGS.BackgroungMusic!=String.Empty)
                {
                    _playerBackground.Open(SETTINGS.BackgroungMusic);
                    _playerBackground.Play(SETTINGS.LoopBgdMusic);
                }
                gameWindow.Show();
                _dispatcherTimer.Start();
                
          


         
        }

        private void disableControl()
        {
            BrowseBgdMusic.IsEnabled = false;
            Send.IsEnabled = false;
            Delete.IsEnabled = false;
           
            ProgressAdd.IsEnabled = false;
            ProgressRemove.IsEnabled = false;
            PauseGame.IsEnabled = false;
        
        
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
            progressFG.IsEnabled = false;
            progressBG.IsEnabled = false;
        }

        internal void NewGame_Click(object sender, RoutedEventArgs e)
        {

            Reset();

            SettingSelect n = new SettingSelect();
            n.ShowDialog();
            if(n.DialogResult.HasValue && n.DialogResult.Equals(true))
            {
                n.Close();
                NewGame.IsEnabled = false;
                StartGame();
            }

        }

        private void ProgressAdd_Click(object sender, RoutedEventArgs e)
        {
            CurrentProgress++;
            CurrentProgressLabel.Content = CurrentProgress;

            
            gameWindow.ProgressBar.Value++;
            
            
            
        }

        private void ProgressRemove_Click(object sender, RoutedEventArgs e)
        {
            CurrentProgress--;
            CurrentProgressLabel.Content = CurrentProgress;
            gameWindow.ProgressBar.Value--;
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            LastClue = Question.Text;
            gameWindow.SClue.Text = LastClue;
            CurrentClue++;

            gameWindow.SCurrentClue.Content = CurrentClue;

            CurrentClueLabel.Content = CurrentClue;
            LastClueLabel.Content = LastClue;
            setPlayerImage(true);
            setPlayerImage();

            if (SelectedSound != String.Empty)
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
            
            LastClue = String.Empty;
            gameWindow.SClue.Text = LastClue;
            LastClueLabel.Content = "";
            setPlayerImage(true);
        }

      

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
           var workingArea = System.Windows.Forms.Screen.AllScreens[0].WorkingArea;

          Width = workingArea.Width;
          Height = workingArea.Height;
          WindowState = System.Windows.WindowState.Maximized;
        }

        internal void Reset()
        {
            BackgroundMusicLabel.Content = "";
            BrowseBgdMusic.IsEnabled = false;
            Send.IsEnabled = false;
            Delete.IsEnabled = false;
            Question.IsEnabled = false;
            ProgressAdd.IsEnabled = false;
            ProgressRemove.IsEnabled = false;
            PauseGame.IsEnabled = false;
            StopGame.IsEnabled = false;
            NewGame.IsEnabled = true;
            LastClueLabel.Content = "";
            CurrentProgressLabel.Content = "0";
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
            BgimageLabel.Content="";
            browseBgImg.IsEnabled=false;
            checkUseBgImg.IsEnabled=false;
            colorPicker.IsEnabled = false;
            progressFG.IsEnabled = false;
            progressBG.IsEnabled = false;
            GameOverScreenLabel.Content = "";
            CluePictures.Items.Clear();

            foreach(Button v in sfxPanel.Children)
            {
                v.IsEnabled = false;
            }
            


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
            Question.Text = String.Empty;
            CurrentProgress = 0;
            CurrentClue = 0;
            LastClue = String.Empty;
            this.PauseGame.Content = "PauseGame";

        }

        private void StopGame_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            
        }

        private void PauseGame_Click(object sender, RoutedEventArgs e)
        {
            if(_dispatcherTimer!=null)
            {
                if(isPaused == false)
                {
                    isPaused = true;
                    this.PauseGame.Content = "ResumeGame";
                    _dispatcherTimer.Stop();
                }else
                {
                    isPaused = false;
                    this.PauseGame.Content = "PauseGame";
                    _dispatcherTimer.Start();
                    
                }
            }
        }

        private void addMinute_Click(object sender, RoutedEventArgs e)
        {
            
            lock (o) {_timerTimeSpan= _timerTimeSpan.Add(TimeSpan.FromMinutes((int)minToAdd.Value)); }
            if (File.Exists(SETTINGS.TimerEditSound))
            {
                _playerSFX.Stop();
                _playerSFX.Open(SETTINGS.TimerEditSound);
                _playerSFX.Play();
            }
            if (gameWindow != null && SETTINGS.BlinkOnTimeEdit)
            {
                gameWindow.blinkTime(false);
            }
            
        }

        private void removeMinute_Click(object sender, RoutedEventArgs e)
        {
            lock (o) {_timerTimeSpan= _timerTimeSpan.Add(TimeSpan.FromMinutes(-(int)minToAdd.Value)); }
            if (File.Exists(SETTINGS.TimerEditSound2))
            {
                _playerSFX.Stop();
                _playerSFX.Open(SETTINGS.TimerEditSound2);
                _playerSFX.Play();
            }
            if (gameWindow != null && SETTINGS.BlinkOnTimeEdit2)
            {
                gameWindow.blinkTime(true);
            }
          
        }

        private void BrowseBgdMusic_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".wav";
            dlg.Filter = "WAV Files (*.wav)|*.wav|MP3 Files (*.mp3)|*.mp3";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                BackgroundMusicLabel.Content = filename;
                _playerBackground.Stop();
                _playerBackground.Open(filename);
                _playerBackground.Play(LoopBgdM.IsChecked == null ? false : (bool)LoopBgdM.IsChecked);
            }
        }

        private void StopBgdMusic_Click(object sender, RoutedEventArgs e)
        {
            _playerBackground.Stop();
            BackgroundMusicLabel.Content = "";
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            DirectoryInfo d = new DirectoryInfo(Environment.CurrentDirectory + "/SFX");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.wav").Union(d.GetFiles("*.mp3")).ToArray(); //Getting Text files

            foreach (FileInfo file in Files)
            {
                Button btn = new Button();
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
            progressFG.setSelectedColor(SETTINGS.ProgressForeground.Name);
            progressBG.setSelectedColor(SETTINGS.ProgressBackground.Name);
            

        }

        private void sfxClick(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            _playerSFX.Stop();
            _playerSFX.Open("./SFX/" + b.Content);
            _playerSFX.Play();
        }

        public void setTimeout(Action TheAction, int Timeout)
        {
            Thread t = new Thread(
                () =>
                {
                    Thread.Sleep(Timeout*1000);
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { TheAction.Invoke(); }));
                    
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
             SETTINGS.DeleteTime =  (int)((IntegerUpDown)sender).Value;
        }

        private void AddCluePicture_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();


            dlg.Multiselect = true;
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                  foreach (String file in dlg.FileNames) 
                  {
                      CluePictures.Items.Add(file);
                  }
             
              
            }
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
            if(gameWindow == null) return;
            if (reset)
            {
                gameWindow.Pictures.Children.Clear();
                return;
            }
            
            foreach(String s in CluePictures.Items)
            {
       
                Image i = new Image();
                BitmapImage b = new BitmapImage();
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
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                GameOverScreenLabel.Content = filename;
                SETTINGS.GameOverScreen = filename;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FontConfigurator f = new FontConfigurator(SETTINGS);
            var r = f.ShowDialog();
            if (r == true)
            {
                if (gameWindow!=null)
                gameWindow.setFont();
            }
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
            SETTINGS.BgColor = ((ColorFont.ColorPicker)sender).SelectedColor;
            gameWindow.updateBg();
        }

        private void browseBgImg_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                SETTINGS.BgImage = filename;
                gameWindow.updateBg();
            }
        }

        private void newSettings_Click(object sender, RoutedEventArgs e)
        {
            NewGameSettings n = new NewGameSettings();
            n.ShowDialog();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void editSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingSelect s = new SettingSelect(true);
            s.ShowDialog();
        }

        private void progressBG_ColorChanged(object sender, RoutedEventArgs e)
        {
            SETTINGS.ProgressBackground = ((ColorFont.ColorPicker)sender).SelectedColor;
            gameWindow.updateProgressStyle();
        }

        private void progressFG_ColorChanged(object sender, RoutedEventArgs e)
        {
            SETTINGS.ProgressForeground = ((ColorFont.ColorPicker)sender).SelectedColor;
            gameWindow.updateProgressStyle();
        }

  

    }
}
