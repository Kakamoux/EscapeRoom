using System;
using System.Windows;
using System.Windows.Controls;
using ColorFont;

namespace NeoarcadiaescapeRoom
{
    /// <summary>
    ///   Logique d'interaction pour FontConfigurator.xaml
    /// </summary>
    public partial class FontConfigurator : Window
    {
        private readonly Settings s;

        public FontConfigurator(Settings s)
        {
            this.s = s;
            InitializeComponent();
        }

        private void TNFB_Click(object sender, RoutedEventArgs e)
        {
            var f = fontPicker(TNF);
            if (f == null) return;
            s.TNF = f;
            FontInfo.ApplyFont(TNF, f);
            TNF.FontSize = 12;
        }

        private void TNLFB_Click(object sender, RoutedEventArgs e)
        {
            var f = fontPicker(TNLF);
            if (f == null) return;
            s.TNLF = f;
            FontInfo.ApplyFont(TNLF, f);
            TNLF.FontSize = 12;
        }

        private void ICFB_Click(object sender, RoutedEventArgs e)
        {
            var f = fontPicker(ICF);
            if (f == null) return;
            s.ICF = f;
            FontInfo.ApplyFont(ICF, f);
            ICF.FontSize = 12;
        }

        private void ICLFB_Click(object sender, RoutedEventArgs e)
        {
            var f = fontPicker(ICLF);
            if (f == null) return;
            s.ICLF = f;
            FontInfo.ApplyFont(ICLF, f);
            ICLF.FontSize = 12;
        }


        private void TFB_Click(object sender, RoutedEventArgs e)
        {
            var f = fontPicker(TF);
            if (f == null) return;
            s.TF = f;
            FontInfo.ApplyFont(TF, f);
            TF.FontSize = 12;
        }

        private void SFB_Click(object sender, RoutedEventArgs e)
        {
            var f = fontPicker(SF);
            if (f == null) return;
            s.SF = f;
            FontInfo.ApplyFont(SF, f);
            SF.FontSize = 12;
        }

        private void CFB_Click(object sender, RoutedEventArgs e)
        {
            var f = fontPicker(CF);
            if (f == null) return;
            s.CF = f;
            FontInfo.ApplyFont(CF, f);
            CF.FontSize = 12;
        }

        private FontInfo fontPicker(Control c)
        {
            var fntDialog = new ColorFontDialog();
            fntDialog.Owner = this;
            fntDialog.Font = FontInfo.GetControlFont(c);
            if (fntDialog.ShowDialog() == true)
            {
                var selectedFont = fntDialog.Font;
                if (selectedFont != null) return selectedFont;
            }

            return null;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            FontInfo.ApplyFont(TNF, s.TNF);
            TNF.FontSize = 12;
            FontInfo.ApplyFont(TNLF, s.TNLF);
            TNLF.FontSize = 12;
            FontInfo.ApplyFont(ICF, s.ICF);
            ICF.FontSize = 12;
            FontInfo.ApplyFont(ICLF, s.ICLF);
            ICLF.FontSize = 12;
            FontInfo.ApplyFont(TF, s.TF);
            TF.FontSize = 12;
            FontInfo.ApplyFont(CF, s.CF);
            CF.FontSize = 12;
            FontInfo.ApplyFont(SF, s.SF);
            SF.FontSize = 12;
        }

        private void settingsOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}