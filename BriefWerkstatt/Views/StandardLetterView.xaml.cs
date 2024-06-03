using BriefWerkstatt.Dialogs;
using System.Windows;
using System.Windows.Input;

namespace BriefWerkstatt.Views
{
    /// <summary>
    /// Interaction logic for StandardLetterView.xaml
    /// </summary>
    public partial class StandardLetterView : Window
    {
        public StandardLetterView()
        {
            InitializeComponent();

        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var dialogResult = System.Windows.Forms.MessageBox.Show(
                "Programm wird beendet. Sicher?",
                "Programm beenden?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dialogResult == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            AboutDialog aboutDialog = new AboutDialog();
            aboutDialog.ShowDialog();

            //System.Windows.MessageBox.Show($"Erstellt von Sven und Patrick\n\n\nDieses Programm nutzt PDFSharp-WPF 6.0.0\n\n﻿Copyright (c) 2001-2024 empira Software GmbH, Troisdorf (Cologne Area), Germany\r\n\r\nhttp://docs.pdfsharp.net\r\n\r\nMIT License\r\n\r\nPermission is hereby granted, free of charge, to any person obtaining a\r\ncopy of this software and associated documentation files (the \"Software\"),\r\nto deal in the Software without restriction, including without limitation\r\nthe rights to use, copy, modify, merge, publish, distribute, sublicense,\r\nand/or sell copies of the Software, and to permit persons to whom the\r\nSoftware is furnished to do so, subject to the following conditions:\r\n\r\nThe above copyright notice and this permission notice shall be included\r\nin all copies or substantial portions of the Software.\r\n\r\nTHE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR\r\nIMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,\r\nFITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL\r\nTHE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER\r\nLIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING\r\nFROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER \r\nDEALINGS IN THE SOFTWARE.",
            //    "Über Briefwerkstatt - Standardbrief");
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SenderNameBox.HasErrors = string.IsNullOrWhiteSpace(SenderNameBox.Text);
            SenderStreetBox.HasErrors = string.IsNullOrWhiteSpace(SenderStreetBox.Text);
            SenderCityBox.HasErrors = string.IsNullOrWhiteSpace(SenderCityBox.Text);

            RecipientNameBox.HasErrors = string.IsNullOrWhiteSpace(RecipientNameBox.Text);
            RecipientCityBox.HasErrors = string.IsNullOrWhiteSpace(RecipientCityBox.Text);

            TopicLineOneBox.HasErrors = string.IsNullOrWhiteSpace(TopicLineOneBox.Text);
            IntroBox.HasErrors = string.IsNullOrWhiteSpace(IntroBox.Text);
            LetterContentBox.HasErrors = string.IsNullOrWhiteSpace(LetterContentBox.Text);

            CustomerNumberBox.HasErrors = string.IsNullOrWhiteSpace(CustomerNumberBox.Text);
            FileNameBox.HasErrors = string.IsNullOrWhiteSpace(FileNameBox.Text);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Dies ist der in den Windows-Anzeige-Einstellungen gesetzte Skalierungswert (z.B. 1,25 für 125%).
            double dpiFactor = System.Windows.PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice.M11;

            // Dies sind die tatsächlichen Größen des genutzten Hauptbildschirms unter Berücksichtung
            // des obigen dpi-Faktors.
            int nWidth = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            int nHeight = (int)System.Windows.SystemParameters.PrimaryScreenHeight;

            // Dieser Wert (Fenstergrößen-Skalierungsfaktor) wird für die Fenstergrößen-Berechnung benötigt 
            // und liegt bei der Standardauflösung (1920x1080) bei 1,0
            float windowScaleFactor = 1f;

            // Setzt den Fenstergrößen-Skalierungsfaktor für verschiedene Auflösungen unter Berücksichtigung des
            // in den Windows-Einstellungen gesetzten dpi-Faktors.
            //
            // Weil die tatsächlichen Größen von nWidth und nHeight bei der obigen Zuweisung durch den
            // dpi-Faktor geteilt werden, muss dies bei der Ermittlung der eingestellten Bildschirm-Auflösung
            // auch gemacht werden, um den Fenstergrößen-Skalierungsfaktor richtig setzen zu können.
            //
            // Beispiel: Auflösung = 1920x1080, Skalierung = 125%
            //           -> nWidth = (int)(1920 / 1,25) = 1584
            //           -> nHeight = (int)(1080 / 1,25) = 864

            // 1920x1080
            if (nWidth == (int)(1920 / dpiFactor) && nHeight == (int)(1080 / dpiFactor))
                windowScaleFactor = 1f;

            // 2560x1440
            else if (nWidth == (int)(2560 / dpiFactor) && nHeight == (int)(1440 / dpiFactor))
                windowScaleFactor = 1.25f;

            // 3840x2160
            else if (nWidth == (int)(3840 / dpiFactor) && nHeight == (int)(2160 / dpiFactor))
                windowScaleFactor = 2f;

            // 4096x2160
            else if (nWidth == (int)(4096 / dpiFactor) && nHeight == (int)(2160 / dpiFactor))
                windowScaleFactor = 2f;

            // 1280x720
            else if (nWidth == (int)(1280 / dpiFactor) && nHeight == (int)(720 / dpiFactor))
                windowScaleFactor = 0.70f;

            // 1366x768
            else if (nWidth == (int)(1366 / dpiFactor) && nHeight == (int)(768 / dpiFactor))
                windowScaleFactor = 0.75f;

            // 1024x1024
            else if (nWidth == (int)(1024 / dpiFactor) && nHeight == (int)(1024 / dpiFactor))
                windowScaleFactor = 1f;

            // Setzt die Fenstergröße unter Berücksichtung des dpi-Faktors sowie des Fenstergrößen-Skalierungsfaktors.
            //
            // 900x940 ist die Standardgröße bei einer Auflösung von 1920x1080.
            this.Width = (int)((900f / dpiFactor) * windowScaleFactor);
            this.Height = (int)((940f / dpiFactor) * windowScaleFactor);

            // Setzt das Fenster in die Mitte des Bildschirms.
            this.Left = (nWidth / 2) - (this.Width / 2);
            this.Top = (nHeight / 2) - (this.Height / 2);
        }
    }
}
