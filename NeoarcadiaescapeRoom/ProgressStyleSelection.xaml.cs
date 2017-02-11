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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;

namespace NeoarcadiaescapeRoom
{
    /// <summary>
    /// Logique d'interaction pour ProgressStyleSelection.xaml
    /// </summary>
    public partial class ProgressStyleSelection : Window
    {
        ResourceDictionary skin;

        public ProgressStyleSelection()
        {


            Stream info = (new StreamReader(System.Environment.CurrentDirectory + "\\" + "ProgressBarStyles.xaml")).BaseStream;
            //Stream xmlReader = System.Xml.XmlReader.Create(System.Environment.CurrentDirectory + "\\" + "ProgressBarStyles.xaml");
            ParserContext pc = new ParserContext();
            pc.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            pc.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            pc.XmlnsDictionary.Add("wintheme", "clr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero");
            skin = (System.Windows.ResourceDictionary)System.Windows.Markup.XamlReader.Load(info, pc);
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            MainWindow.SETTINGS.ProgressStyleName = styleCombo.SelectedItem as string;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            foreach(string s in skin.Keys)
            {
                if(s.Contains("Style"))
                {
                    styleCombo.Items.Add(s);
                    ProgressBar p = new ProgressBar();
                    p.Width = 100;
                    p.Height = 25;
                    p.Style = skin[s] as Style;
                    p.Minimum = 0;
                    p.Maximum = 100;
                    p.Value = 50;
                    progressPanel.Children.Add(p);
                    Label l = new Label();
                    l.Height = 25;
                    l.Content = s;
                    progressPanelLabel.Children.Add(l);
                }
            }

            styleCombo.SelectedItem = MainWindow.SETTINGS.ProgressStyleName;
        }
    }
}
