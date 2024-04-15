using BriefWerkstatt.Models;
using System.Windows.Input;

namespace BriefWerkstatt.ViewModels
{
    public class StandardLetterViewModel : ViewModelBase
    {
        private StandardLetterModel _standardLetter;
        private Repository.Repository _repository;

        #region Absender-Properties
        public string? SenderName
        {
            get => _standardLetter.Sender.Name;
            set
            {
                _standardLetter.Sender.Name = value;
                OnPropertyChanged(nameof(SenderName));
                ChangeSaveButtonBorderColor();
            }
        }

        public string? SenderStreetName
        {
            get => _standardLetter.Sender.StreetName;
            set
            {
                _standardLetter.Sender.StreetName = value;
                OnPropertyChanged(nameof(SenderStreetName));
                ChangeSaveButtonBorderColor();
            }
        }

        public int? SenderStreetNumber
        {
            get => _standardLetter.Sender.StreetNumber;
            set
            {
                _standardLetter.Sender.StreetNumber = value;
                OnPropertyChanged(nameof(SenderStreetNumber));
                ChangeSaveButtonBorderColor();
            }
        }

        public int? SenderZipCode
        {
            get => _standardLetter.Sender.ZipCode;
            set
            {
                _standardLetter.Sender.ZipCode = value;
                OnPropertyChanged(nameof(SenderZipCode));
                ChangeSaveButtonBorderColor();
            }
        }

        public string? SenderCityName
        {
            get => _standardLetter.Sender.CityName;
            set
            {
                _standardLetter.Sender.CityName = value;
                OnPropertyChanged(nameof(SenderCityName));
                ChangeSaveButtonBorderColor();
            }
        }

        public string? SenderCareOfInfo
        {
            get => _standardLetter.Sender.CareOfInfo;
            set
            {
                _standardLetter.Sender.CareOfInfo = value;
                OnPropertyChanged(nameof(SenderCareOfInfo));
            }
        }

        public string? SenderAdditionalAdressInfo
        {
            get => _standardLetter.Sender.AdditionalAdressInfo;
            set
            {
                _standardLetter.Sender.AdditionalAdressInfo = value;
                OnPropertyChanged(nameof(SenderAdditionalAdressInfo));
            }
        }
        #endregion

        #region Empfänger-Properties
        public string? RecipientName
        {
            get => _standardLetter.Recipient.Name;
            set
            {
                _standardLetter.Recipient.Name = value;
                OnPropertyChanged(nameof(RecipientName));
                ChangeSaveButtonBorderColor();
            }
        }

        public string? RecipientStreetName
        {
            get => _standardLetter.Recipient.StreetName;
            set
            {
                _standardLetter.Recipient.StreetName = value;
                OnPropertyChanged(nameof(RecipientStreetName));
                ChangeSaveButtonBorderColor();
            }
        }

        public int? RecipientStreetNumber
        {
            get => _standardLetter.Recipient.StreetNumber;
            set
            {
                _standardLetter.Recipient.StreetNumber = value;
                OnPropertyChanged(nameof(RecipientStreetNumber));
                ChangeSaveButtonBorderColor();
            }
        }

        public int? RecipientZipCode
        {
            get => _standardLetter.Recipient.ZipCode;
            set
            {
                _standardLetter.Recipient.ZipCode = value;
                OnPropertyChanged(nameof(RecipientZipCode));
                ChangeSaveButtonBorderColor();
            }
        }

        public string? RecipientCityName
        {
            get => _standardLetter.Recipient.CityName;
            set
            {
                _standardLetter.Recipient.CityName = value;
                OnPropertyChanged(nameof(RecipientCityName));
                ChangeSaveButtonBorderColor();
            }
        }

        public string? RecipientCareOfInfo
        {
            get => _standardLetter.Recipient.CareOfInfo;
            set
            {
                _standardLetter.Recipient.CareOfInfo = value;
                OnPropertyChanged(nameof(RecipientCareOfInfo));
            }
        }

        public string? RecipientAdditionalAdressInfo
        {
            get => _standardLetter.Recipient.AdditionalAdressInfo;
            set
            {
                _standardLetter.Recipient.AdditionalAdressInfo = value;
                OnPropertyChanged(nameof(RecipientAdditionalAdressInfo));
            }
        }
        #endregion

        #region Briefinhalt-Properties
        public string? TopicLineOne
        {
            get => _standardLetter.LetterContent.TopicLineOne;
            set
            {
                _standardLetter.LetterContent.TopicLineOne = value;
                OnPropertyChanged(nameof(TopicLineOne));
                ChangeSaveButtonBorderColor();
            }
        }

        public string? TopicLineTwo
        {
            get => _standardLetter.LetterContent.TopicLineTwo;
            set
            {
                _standardLetter.LetterContent.TopicLineTwo = value;
                OnPropertyChanged(nameof(TopicLineTwo));
            }
        }

        public string? Intro
        {
            get => _standardLetter.LetterContent?.Intro;
            set
            {
                _standardLetter.LetterContent.Intro = value;
                OnPropertyChanged(nameof(Intro));
                ChangeSaveButtonBorderColor();
            }
        }

        public string? TextBody
        {
            get => _standardLetter.LetterContent.TextBody;
            set
            {
                _standardLetter.LetterContent.TextBody = value;
                OnPropertyChanged(nameof(TextBody));
                ChangeSaveButtonBorderColor();
            }
        }

        public string? Outro
        {
            get => _standardLetter.LetterContent.Outro;
            set
            {
                _standardLetter.LetterContent.Outro = value;
                OnPropertyChanged(nameof(Outro));
            }
        }
        #endregion

        #region Datei-Info-Properties
        public string? CustomerNumber
        {
            get => _standardLetter.FileInfo.CustomerNumber;
            set
            {
                _standardLetter.FileInfo.CustomerNumber = value;
                OnPropertyChanged(nameof(CustomerNumber));
                ChangeSaveButtonBorderColor();
            }
        }

        public string? FileName
        {
            get => _standardLetter.FileInfo?.FileName;
            set
            {
                _standardLetter.FileInfo.FileName = value;
                OnPropertyChanged(nameof(FileName));
                ChangeSaveButtonBorderColor();
            }
        }
        #endregion

        #region Commands
        public void SaveExecute()
        {
            if (ValidateModel())
            {
                var dialog = new FolderBrowserDialog();
                var result = dialog.ShowDialog();
                if (result.ToString() != string.Empty && !result.ToString().Equals("Cancel"))
                {
                    string saveFolderPath = dialog.SelectedPath + @"\";
                    _repository.CreatePdfDocument(_standardLetter, saveFolderPath);
                }
            }
        }

        public ICommand Save
        {
            get
            {
                return new RelayCommand(SaveExecute);
            }
        }

        public void FillWithExampleDataExecute()
        {
            SenderName = "Max Mustermann";
            SenderCareOfInfo = "c/o Musterfrau";
            SenderStreetName = "Musterstraße";
            SenderStreetNumber = 1234;
            SenderAdditionalAdressInfo = "Vorderhaus, links";
            SenderZipCode = 12345;
            SenderCityName = "Musterstadt";

            RecipientName = "Beispielfirma GmbH";
            RecipientCareOfInfo = "z.H. Erika Musterfrau";
            RecipientStreetName = "Musterallee";
            RecipientStreetNumber = 1234;
            RecipientAdditionalAdressInfo = "Hinterhaus, links";
            RecipientZipCode = 12345;
            RecipientCityName = "Musterstadt";

            TopicLineOne = "Bewerbung als Marketingmanager";
            TopicLineTwo = "Ihr Stellenangebot vom 10. April 2024";

            Intro = "Sehr geehrte Frau Musterfrau,";

            TextBody = "ich habe mit großem Interesse Ihr Stellenangebot für die Position des Marketingmanagers vom 10. April 2024 gelesen und möchte mich hiermit bei Ihnen bewerben." +
                "\n\nIn meiner bisherigen beruflichen Laufbahn konnte ich umfangreiche Erfahrungen im Bereich Marketing sammeln. Besonders hervorheben möchte ich meine Expertise in der Planung und Umsetzung von Marketingstrategien sowie in der Analyse von Markt- und Kundendaten. Ich bin überzeugt davon, dass meine Fähigkeiten und mein Engagement eine Bereicherung für Ihr Team darstellen würden." +
                "\n\nAnbei finden Sie meinen Lebenslauf und meine Zeugnisse. Ich würde mich sehr freuen, wenn Sie mir die Gelegenheit geben würden, mich in einem persönlichen Gespräch näher vorzustellen und mehr über die Anforderungen und Ziele Ihrer Abteilung zu erfahren." +
                "\n\nFür Rückfragen stehe ich Ihnen selbstverständlich jederzeit zur Verfügung." +
                "\n\nIch danke Ihnen für die Berücksichtigung meiner Bewerbung und freue mich auf Ihre positive Rückmeldung.";

            CustomerNumber = "1234";
            FileName = "Example";
        }

        public ICommand FillWithExampleData
        {
            get
            {
                return new RelayCommand(FillWithExampleDataExecute);
            }
        }

        public void DeleteDataExecute()
        {
            var dialogResult = MessageBox.Show(
                "Eingetragene Daten werden gelöscht. Sicher?",
                "Daten löschen?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

            if (dialogResult == DialogResult.Yes)
            {
                SenderName = null;
                SenderCareOfInfo = null;
                SenderStreetName = null;
                SenderStreetNumber = null;
                SenderAdditionalAdressInfo = null;
                SenderZipCode = null;
                SenderCityName = null;

                RecipientName = null;
                RecipientCareOfInfo = null;
                RecipientStreetName = null;
                RecipientStreetNumber = null;
                RecipientAdditionalAdressInfo = null;
                RecipientZipCode = null;
                RecipientCityName = null;

                TopicLineOne = null;
                TopicLineTwo = null;
                Intro = "Sehr geehrte Damen und Herren,";
                TextBody = null;

                CustomerNumber = null;
                FileName = null;
            }
        }

        public ICommand DeleteData
        {
            get
            {
                return new RelayCommand(DeleteDataExecute);
            }
        }
        #endregion

        #region Properties XAML Control Styles
        private System.Windows.Media.Brush _saveButtonBorderColor;
        public System.Windows.Media.Brush SaveButtonBorderColor
        {
            get
            {
                return _saveButtonBorderColor;
            }
            set
            {
                _saveButtonBorderColor = value;
                OnPropertyChanged(nameof(SaveButtonBorderColor));
            }
        }
        #endregion

        public StandardLetterViewModel()
        {
            _standardLetter = new StandardLetterModel();
            _repository = new Repository.Repository();
            _saveButtonBorderColor = System.Windows.Media.Brushes.Red;
        }

        #region Model Validation
        private bool ValidateModel()
        {
            // Überprüft bei Betätigen des Speichern-Buttons, ob alle Nutzereingaben gültig sind.
            // Zeigt eine gesammelte Fehlerliste an, wenn nicht.

            var validationResults = _standardLetter.Validate().ToList();
            bool isValid = validationResults.Count == 0;

            if (!isValid)
            {
                string errorMessage = string.Join("\n", validationResults.Select(r => r.ErrorMessage));
                MessageBox.Show(errorMessage, "Fehlende Eingaben", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return isValid;
        }
        #endregion

        private void ChangeSaveButtonBorderColor()
        {
            SaveButtonBorderColor = _standardLetter.IsValid ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
        }
    }
}
