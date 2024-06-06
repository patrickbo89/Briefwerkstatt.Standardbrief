using BriefWerkstatt.Dialogs;
using BriefWerkstatt.ViewModels;
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
            this.Width = (int)((880f / dpiFactor) * windowScaleFactor);
            this.Height = (int)((940f / dpiFactor) * windowScaleFactor);

            // Setzt das Fenster in die Mitte des Bildschirms.
            this.Left = (nWidth / 2) - (this.Width / 2);
            this.Top = (nHeight / 2) - (this.Height / 2);
        }

        private void NewLetterButton_Click(object sender, RoutedEventArgs e)
        {
            var standardLetterViewModel = DataContext as StandardLetterViewModel;

            System.Windows.Forms.DialogResult? dialogResult = null;

            if (standardLetterViewModel?.StandardLetter.HasBeenSaved == false)
            {
                dialogResult = System.Windows.Forms.MessageBox.Show
                    ("Der aktuelle Brief wurde noch nicht gespeichert und geht verloren, wenn er nicht zuvor gespeichert wird.\n\nTrotzdem fortfahren?",
                     "Der aktuelle Brief wurde noch nicht gespeichert",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }
            else if (standardLetterViewModel?.StandardLetter.HasBeenChanged == true)
            {
                dialogResult = System.Windows.Forms.MessageBox.Show(
                    "Die Änderungen am aktuellen Brief wurden noch nicht gespeichert und gehen verloren, wenn diese nicht zuvor gespeichert werden.\n\nTrotzdem fortfahren?",
                    "Änderungen wurden noch nicht gespeichert",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }

            if (dialogResult == System.Windows.Forms.DialogResult.Yes || dialogResult == null)
            {
                NewLetterDialog newLetterDialog = new NewLetterDialog();
                newLetterDialog.ShowDialog();

                SenderNameBox.HasErrors = false;
                SenderStreetBox.HasErrors = false;
                SenderCityBox.HasErrors = false;

                RecipientNameBox.HasErrors = false;
                RecipientCityBox.HasErrors = false;

                IntroBox.HasErrors = false;
                TopicLineOneBox.HasErrors = false;
                LetterContentBox.HasErrors = false;

                CustomerNumberBox.HasErrors = false;
                FileNameBox.HasErrors = false;
            }
        }
    }
}
