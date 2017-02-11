using System;
using System.Collections.Generic;
using System.IO;
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
    /// Logique d'interaction pour SettingSelect.xaml
    /// </summary>
    public partial class SettingSelect : Window
    {
        bool edit = false;
        int index = 0;
        public SettingSelect(bool editSetting = false)
        {
            edit = editSetting;
            if (!File.Exists("settings/general.ng"))
            {
               FileStream fs = File.Create("settings/general.ng");
               fs.Close();
               StreamWriter sw = new StreamWriter("settings/general.ng");
               sw.AutoFlush = true;
               sw.Write(index.ToString());
               sw.Close();

            }else
            {
                StreamReader sr = new StreamReader("settings/general.ng");
                if (!int.TryParse(sr.ReadLine(), out index))
                     index = 0;
                sr.Close();
            }
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            string selected = settings.SelectedItem as string;
            MainWindow.SETTINGS = new Settings(selected);
            
            if(edit)
            {
                DialogResult = true;
                NewGameSettings n = new NewGameSettings(selected);
                n.ShowDialog();
                this.Close();
            }else
            {
                if (TeamName.Text == "")
                {
                    MessageBox.Show("Please fill team name in order to start game");
                }
                else
                {
                    
                    StreamWriter sw = new StreamWriter("settings/general.ng");
                    sw.Write(settings.SelectedIndex.ToString());
                    sw.AutoFlush = true;
                    sw.Close();
                    MainWindow.SETTINGS.TeamName = TeamName.Text;
                    DialogResult = true;
                }
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            DirectoryInfo d = new DirectoryInfo(Environment.CurrentDirectory + "/settings");//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.st"); //Getting Text files

            bool createDefault = true;

            foreach (FileInfo file in Files)
            {
                if (file.Name == "default.st") createDefault = false;
                settings.Items.Add(file.Name);
                

            }
            if ((settings.Items.Count - 1) > index)
                index = 0;

            settings.SelectedIndex = index;


            if (createDefault)
                new Settings("default.st");

            if(edit)
            {
                OkBtn.Content = "Edit Current";
                MainGrid.Children.Remove(teamGrid);
               
            }
        }
    }
}
