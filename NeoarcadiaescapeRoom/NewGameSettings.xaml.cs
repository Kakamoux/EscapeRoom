using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NeoarcadiaescapeRoom
{
    /// <summary>
    /// Logique d'interaction pour NewGameSettings.xaml
    /// </summary>
    public partial class NewGameSettings : Window
    {

        Settings s = new Settings();
        bool edit = false;

        public NewGameSettings(string name ="")
        {
            if(name!=""){
                s = new Settings(name);
                edit = true;

            }
            InitializeComponent();
           
            
        }

      

        private void settingsOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            s.ProgressMax = (int)this.ProgressMaxControl.Value;
            s.SettingName = SettingNameControl.Text;
            s.Timer = (int)this.TimerControl.Value;
            s.LoopBgdMusic = bgdMLoop.IsChecked == null ? false : (bool)bgdMLoop.IsChecked;
            s.BackgroungMusic = BgdMusic.Text;
            s.DeleteAuto = deleteAuto.IsChecked == null ? false : (bool)deleteAuto.IsChecked;
            s.DeleteTime = (int)this.deleteTime.Value;
            s.GameOverScreen = this.GameOverScreen.Text;
            s.UseBgImage = checkUseBgImg.IsChecked == true ? true : false;
            s.BgImage = bgImage.Text;
            s.BgColor = colorPicker.SelectedColor;
            s.ProgressBackground = progressBG.SelectedColor;
            s.ProgressForeground = progressFG.SelectedColor;

            s.TimerEditSound = TimeEditSelectedSound.Text;
            s.TimerEditColor = editBlinkColor.SelectedColor;
            s.BlinkOnTimeEdit = timeEditBlinkCheck.IsChecked == true ? true : false;

            s.TimerEditSound2 = TimeEditSelectedSound2.Text;
            s.TimerEditColor2 = editBlinkColor2.SelectedColor;
            s.BlinkOnTimeEdit2 = timeEditBlinkCheck2.IsChecked == true ? true : false;

            s.BlinkDuration = (double)BlinkDurationText.Value;
            s.BlinkInterval = (double)BlinkIntervalText.Value;

            s.BlinkDuration2 = (double)BlinkDurationText2.Value;
            s.BlinkInterval2 = (double)BlinkIntervalText2.Value;

            string sn = SettingNameControl.Text;
            if (!sn.EndsWith(".st")) sn += ".st";
            s.Save(sn);

        }



        private void Window_Initialized(object sender, EventArgs e)
        {
            
            this.ProgressMaxControl.Value = MainWindow.SETTINGS.ProgressMax;
            SettingNameControl.Text = MainWindow.SETTINGS.SettingName;
            this.TimerControl.Value = MainWindow.SETTINGS.Timer;
            BgdMusic.Text = MainWindow.SETTINGS.BackgroungMusic;
            bgdMLoop.IsChecked = MainWindow.SETTINGS.LoopBgdMusic;
            deleteAuto.IsChecked = MainWindow.SETTINGS.DeleteAuto;
            deleteTime.Value = MainWindow.SETTINGS.DeleteTime;
            this.GameOverScreen.Text = MainWindow.SETTINGS.GameOverScreen;
            checkUseBgImg.IsChecked = MainWindow.SETTINGS.UseBgImage;
            bgImage.Text = MainWindow.SETTINGS.BgImage;
            progressBG.setSelectedColor(MainWindow.SETTINGS.ProgressBackground.Name);
            progressFG.setSelectedColor(MainWindow.SETTINGS.ProgressForeground.Name);

            colorPicker.setSelectedColor(MainWindow.SETTINGS.BgColor.Name);

            TimeEditSelectedSound.Text = MainWindow.SETTINGS.TimerEditSound;
            timeEditBlinkCheck.IsChecked = MainWindow.SETTINGS.BlinkOnTimeEdit;
            editBlinkColor.setSelectedColor(MainWindow.SETTINGS.TimerEditColor.Name);

            TimeEditSelectedSound2.Text = MainWindow.SETTINGS.TimerEditSound2;
            timeEditBlinkCheck2.IsChecked = MainWindow.SETTINGS.BlinkOnTimeEdit2;
            editBlinkColor2.setSelectedColor(MainWindow.SETTINGS.TimerEditColor2.Name);

            BlinkDurationText.Value = (int)MainWindow.SETTINGS.BlinkDuration;
            BlinkIntervalText.Value = (int)MainWindow.SETTINGS.BlinkInterval;

            BlinkDurationText2.Value = (int)MainWindow.SETTINGS.BlinkDuration2;
            BlinkIntervalText2.Value = (int)MainWindow.SETTINGS.BlinkInterval2;


            if(edit)
            {

            }

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
                BgdMusic.Text = filename;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
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
                GameOverScreen.Text = filename;
            }
        }

        private void fontCfg_Click(object sender, RoutedEventArgs e)
        {
            FontConfigurator f = new FontConfigurator(s);
            var r = f.ShowDialog();
            if(r==true)
            {

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
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
                bgImage.Text = filename;
            }
        }

        private void colorPicker_ColorChanged(object sender, RoutedEventArgs e)
        {

        }

        private void checkUseBgImg_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void checkUseBgImg_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void progressCfg_Click(object sender, RoutedEventArgs e)
        {
            ProgressStyleSelection p = new ProgressStyleSelection();
            p.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
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
                TimeEditSelectedSound.Text = filename;
           
            }
            
        }

        private void editBlinkColor_ColorChanged(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
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
                TimeEditSelectedSound2.Text = filename;

            }
            
        }
    }
}
