using BriefWerkstatt.Models;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace BriefWerkstatt.ViewModels
{
    public class StandardLetterViewModel : ViewModelBase
    {
        private StandardLetterModel _standardLetter;
        private Repository.Repository _repository;

        #region Absender-Properties

        [Required(AllowEmptyStrings = false, ErrorMessage = "Absender: Name darf nicht leer sein.")]
        public string? SenderName
        {
            get => _standardLetter.SenderName;
            set
            {
                _standardLetter.SenderName = value;
                OnPropertyChanged(nameof(SenderName));
                ChangeBorderColors();
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Absender: Straße und Haus-Nr. darf nicht leer sein.")]
        public string? SenderStreetAndNumber
        {
            get => _standardLetter.SenderStreetAndNumber;
            set
            {
                _standardLetter.SenderStreetAndNumber = value;
                OnPropertyChanged(nameof(SenderStreetAndNumber));
                ChangeBorderColors();
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Absender: Postleitzahl und Ort darf nicht leer sein.")]
        public string? SenderZipCodeAndCity
        {
            get => _standardLetter.SenderZipCodeAndCity;
            set
            {
                _standardLetter.SenderZipCodeAndCity = value;
                OnPropertyChanged(nameof(SenderZipCodeAndCity));
                ChangeBorderColors();
            }
        }

        public string? SenderCareOfInfo
        {
            get => _standardLetter.SenderCareOfInfo;
            set
            {
                _standardLetter.SenderCareOfInfo = value;
                OnPropertyChanged(nameof(SenderCareOfInfo));
            }
        }

        public string? SenderAdditionalInfo
        {
            get => _standardLetter.SenderAdditionalInfo;
            set
            {
                _standardLetter.SenderAdditionalInfo = value;
                OnPropertyChanged(nameof(SenderAdditionalInfo));
            }
        }
        #endregion

        #region Empfänger-Properties

        [Required(AllowEmptyStrings = false, ErrorMessage = "Empfänger: Name darf nicht leer sein.")]
        public string? RecipientName
        {
            get => _standardLetter.RecipientName;
            set
            {
                _standardLetter.RecipientName = value;
                OnPropertyChanged(nameof(RecipientName));
                ChangeBorderColors();
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Empfänger: Straße und Haus-Nr. darf nicht leer sein.")]
        public string? RecipientStreetAndNumber
        {
            get => _standardLetter.RecipientStreetAndNumber;
            set
            {
                _standardLetter.RecipientStreetAndNumber = value;
                OnPropertyChanged(nameof(RecipientStreetAndNumber));
                ChangeBorderColors();
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "AbRecipient: Postleitzahl und Ort darf nicht leer sein.")]
        public string? RecipientZipCodeAndCity
        {
            get => _standardLetter.RecipientZipCodeAndCity;
            set
            {
                _standardLetter.RecipientZipCodeAndCity = value;
                OnPropertyChanged(nameof(RecipientZipCodeAndCity));
                ChangeBorderColors();
            }
        }

        public string? RecipientCareOfInfo
        {
            get => _standardLetter.RecipientCareOfInfo;
            set
            {
                _standardLetter.RecipientCareOfInfo = value;
                OnPropertyChanged(nameof(RecipientCareOfInfo));
            }
        }

        public string? RecipientAdditionalInfo
        {
            get => _standardLetter.RecipientAdditionalInfo;
            set
            {
                _standardLetter.RecipientAdditionalInfo = value;
                OnPropertyChanged(nameof(RecipientAdditionalInfo));
            }
        }
        #endregion

        #region Briefinhalt-Properties

        [Required(AllowEmptyStrings = false, ErrorMessage = "Betreffzeile 1 darf nicht leer sein.")]
        public string? TopicLineOne
        {
            get => _standardLetter.TopicLineOne;
            set
            {
                _standardLetter.TopicLineOne = value;
                OnPropertyChanged(nameof(TopicLineOne));
                ChangeBorderColors();
            }
        }

        public string? TopicLineTwo
        {
            get => _standardLetter.TopicLineTwo;
            set
            {
                _standardLetter.TopicLineTwo = value;
                OnPropertyChanged(nameof(TopicLineTwo));
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Anrede darf nicht leer sein.")]
        public string? Intro
        {
            get => _standardLetter.Intro;
            set
            {
                _standardLetter.Intro = value;
                OnPropertyChanged(nameof(Intro));
                ChangeBorderColors();
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Briefinhalt darf nicht leer sein.")]
        public string? Content
        {
            get => _standardLetter.Content;
            set
            {
                _standardLetter.Content = value;
                OnPropertyChanged(nameof(Content));
                ChangeBorderColors();
            }
        }

        public string? Outro
        {
            get => _standardLetter.Outro;
            set
            {
                _standardLetter.Outro = value;
                OnPropertyChanged(nameof(Outro));
            }
        }
        #endregion

        #region Datei-Info-Properties

        [Required(AllowEmptyStrings = false, ErrorMessage = "Kundennummer darf nicht leer sein.")]
        [MaxLength(4, ErrorMessage = "Kundennummer muss vier Ziffern lang sein.")]
        public string? CustomerNumber
        {
            get => _standardLetter.CustomerNumber;
            set
            {
                _standardLetter.CustomerNumber = value;
                OnPropertyChanged(nameof(CustomerNumber));
                ChangeBorderColors();
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Dateiname darf nicht leer sein.")]
        public string? FileName
        {
            get => _standardLetter.FileName;
            set
            {
                _standardLetter.FileName = value;
                OnPropertyChanged(nameof(FileName));
                ChangeBorderColors();
            }
        }
        #endregion

        #region Commands
        public void CloseAppExecute()
        {
            var dialogResult = MessageBox.Show(
                "Programm wird beendet. Sicher?",
                "Programm beenden?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dialogResult == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        public ICommand CloseApp
        {
            get 
            {
                return new RelayCommand(CloseAppExecute);
            }
        }

        public void MinimizeAppExecute()
        {
        }

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
            SenderStreetAndNumber = "Musterstraße 123";
            SenderAdditionalInfo = "Vorderhaus, links";
            SenderZipCodeAndCity = "12345 Musterstadt";

            RecipientName = "Beispielfirma GmbH";
            RecipientCareOfInfo = "z.H. Erika Musterfrau";
            RecipientStreetAndNumber = "Musterallee 123";
            RecipientAdditionalInfo = "Hinterhaus, links";
            RecipientZipCodeAndCity = "12345 Musterstadt";

            TopicLineOne = "Bewerbung als Marketingmanager";
            TopicLineTwo = "Ihr Stellenangebot vom 10. April 2024";

            Intro = "Sehr geehrte Frau Musterfrau,";

            Content = "ich habe mit großem Interesse Ihr Stellenangebot für die Position des Marketingmanagers vom 10. April 2024 gelesen und möchte mich hiermit bei Ihnen bewerben." +
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
                SenderStreetAndNumber = null;
                SenderAdditionalInfo = null;
                SenderZipCodeAndCity = null;

                RecipientName = null;
                RecipientCareOfInfo = null;
                RecipientStreetAndNumber = null;
                RecipientAdditionalInfo = null;
                RecipientZipCodeAndCity = null;

                TopicLineOne = null;
                TopicLineTwo = null;
                Intro = "Sehr geehrte Damen und Herren,";
                Content = null;

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
        private System.Windows.Media.Brush _saveButtonColor;
        public System.Windows.Media.Brush SaveButtonColor
        {
            get
            {
                return _saveButtonColor;
            }
            set
            {
                _saveButtonColor = value;
                OnPropertyChanged(nameof(SaveButtonColor));
            }
        }

        private System.Windows.Media.Brush _saveButtonColorHover;
        public System.Windows.Media.Brush SaveButtonColorHover
        {
            get
            {
                return _saveButtonColorHover;
            }
            set
            {
                _saveButtonColorHover = value;
                OnPropertyChanged(nameof(SaveButtonColorHover));
            }
        }

        private System.Windows.Media.Brush _senderGroupBoxBorderColor;
        public System.Windows.Media.Brush SenderGroupBoxBorderColor
        {
            get
            {
                return _senderGroupBoxBorderColor;
            }
            set
            {
                _senderGroupBoxBorderColor = value;
                OnPropertyChanged(nameof(SenderGroupBoxBorderColor));
            }
        }

        private System.Windows.Media.Brush _recipientGroupBoxBorderColor;
        public System.Windows.Media.Brush RecipientGroupBoxBorderColor
        {
            get
            {
                return _recipientGroupBoxBorderColor;
            }
            set
            {
                _recipientGroupBoxBorderColor = value;
                OnPropertyChanged(nameof(RecipientGroupBoxBorderColor));
            }
        }

        private System.Windows.Media.Brush _letterBodyGroupBoxBorderColor;
        public System.Windows.Media.Brush LetterBodyGroupBoxBorderColor
        {
            get
            {
                return _letterBodyGroupBoxBorderColor;
            }
            set
            {
                _letterBodyGroupBoxBorderColor = value;
                OnPropertyChanged(nameof(LetterBodyGroupBoxBorderColor));
            }
        }

        private System.Windows.Media.Brush _fileInfoGroupBoxBorderColor;
        public System.Windows.Media.Brush FileInfoGroupBoxBorderColor
        {
            get
            {
                return _fileInfoGroupBoxBorderColor;
            }
            set
            {
                _fileInfoGroupBoxBorderColor = value;
                OnPropertyChanged(nameof(FileInfoGroupBoxBorderColor));
            }
        }
        #endregion

        public StandardLetterViewModel()
        {
            _standardLetter = new StandardLetterModel();
            _repository = new Repository.Repository();

            _saveButtonColor = System.Windows.Media.Brushes.Red;
            _saveButtonColorHover = System.Windows.Media.Brushes.DarkRed;
            _senderGroupBoxBorderColor = System.Windows.Media.Brushes.Red;
            _recipientGroupBoxBorderColor = System.Windows.Media.Brushes.Red;
            _letterBodyGroupBoxBorderColor = System.Windows.Media.Brushes.Red;
            _fileInfoGroupBoxBorderColor = System.Windows.Media.Brushes.Red;
        }

        #region Model Validation

        private bool _isValid;

        private bool ValidateModel()
        {
            // Überprüft bei Betätigen des Speichern-Buttons, ob alle Nutzereingaben gültig sind.
            // Zeigt eine gesammelte Fehlerliste an, wenn nicht.

            var validationContext = new ValidationContext(this);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(this, validationContext, validationResults, true);

            if (!isValid)
            {
                string errorMessage = string.Join("\n", validationResults.Select(r => r.ErrorMessage));
                MessageBox.Show(errorMessage, "Fehlende Eingaben", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            _isValid = isValid;

            return isValid;
        }
        #endregion

        private void ChangeBorderColors()
        {
            SaveButtonColor = 
                _isValid ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;

            SaveButtonColorHover =
                _isValid ? System.Windows.Media.Brushes.DarkGreen : System.Windows.Media.Brushes.DarkRed;

            SenderGroupBoxBorderColor = 
                _isValid ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;

            RecipientGroupBoxBorderColor = 
                _isValid ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;

            LetterBodyGroupBoxBorderColor = 
                _isValid ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;

            FileInfoGroupBoxBorderColor = 
                _isValid ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
        }
    }
}
