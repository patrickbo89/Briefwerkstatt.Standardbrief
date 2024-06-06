using BriefWerkstatt.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BriefWerkstatt.Dialogs
{
    /// <summary>
    /// Interaction logic for NewLetterDialog.xaml
    /// </summary>
    public partial class NewLetterDialog : Window
    {
        private StandardLetterViewModel _standardLetterViewModel;
        public NewLetterDialog()
        {
            InitializeComponent();

            Owner = System.Windows.Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            Width = Owner.Width / 1.5;
            Height = Owner.Height / 1.5;

            _standardLetterViewModel = Owner.DataContext as StandardLetterViewModel;
            DataContext = _standardLetterViewModel;
        }

        private void EraseAllButton_Click(object sender, RoutedEventArgs e)
        {
            var dialogResult = System.Windows.Forms.MessageBox.Show("Diese Option löscht alle Eingabedaten um einen neuen Brief für einen neuen Kunden zu erstellen. Sicher?",
            "Gewählte Option bestätigen", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                _standardLetterViewModel.NewLetterEraseAllDataExecute();
                Close();
            }
        }

        private void KeepSenderDataButton_Click(object sender, RoutedEventArgs e)
        {
            var dialogResult = System.Windows.Forms.MessageBox.Show("Diese Option behält nur die Absender-Daten sowie die Kundennummer bei. Sicher?",
            "Gewählte Option bestätigen", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                _standardLetterViewModel.NewLetterKeepSenderDataExecute();
                Close();
            }
        }

        private void KeepSenderAndRecipientDataButton_Click(object sender, RoutedEventArgs e)
        {
            var dialogResult = System.Windows.Forms.MessageBox.Show("Diese Option behält nur die Absender- und Empfänger-Daten sowie die Kundennummer bei. Sicher?",
            "Gewählte Option bestätigen", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                _standardLetterViewModel.NewLetterKeepSenderAndRecipientDataExecute();
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
