using BriefWerkstatt.Models;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Windows.Input;

namespace BriefWerkstatt.ViewModels
{
    public class StandardLetterViewModel : ViewModelBase, INotifyDataErrorInfo
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
                Validate(nameof(SenderName), value);
                OnPropertyChanged(nameof(SenderName));
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Absender: Straße und Haus-Nr. darf nicht leer sein.")]
        public string? SenderStreetAndNumber
        {
            get => _standardLetter.SenderStreetAndNumber;
            set
            {
                _standardLetter.SenderStreetAndNumber = value;
                Validate(nameof(SenderStreetAndNumber), value);
                OnPropertyChanged(nameof(SenderStreetAndNumber));
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Absender: Postleitzahl und Ort darf nicht leer sein.")]
        public string? SenderZipCodeAndCity
        {
            get => _standardLetter.SenderZipCodeAndCity;
            set
            {
                _standardLetter.SenderZipCodeAndCity = value;
                Validate(nameof(SenderZipCodeAndCity), value);
                OnPropertyChanged(nameof(SenderZipCodeAndCity));
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
                Validate(nameof(RecipientName), value);
                OnPropertyChanged(nameof(RecipientName));
            }
        }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Empfänger: Straße und Haus-Nr. darf nicht leer sein.")]
        public string? RecipientStreetAndNumber
        {
            get => _standardLetter.RecipientStreetAndNumber;
            set
            {
                _standardLetter.RecipientStreetAndNumber = value;
                OnPropertyChanged(nameof(RecipientStreetAndNumber));
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Empfänger: Postleitzahl und Ort darf nicht leer sein.")]
        public string? RecipientZipCodeAndCity
        {
            get => _standardLetter.RecipientZipCodeAndCity;
            set
            {
                _standardLetter.RecipientZipCodeAndCity = value;
                Validate(nameof(RecipientZipCodeAndCity), value);
                OnPropertyChanged(nameof(RecipientZipCodeAndCity));
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
                Validate(nameof(TopicLineOne), value);
                OnPropertyChanged(nameof(TopicLineOne));
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
                Validate(nameof(Intro), value);
                OnPropertyChanged(nameof(Intro));
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Brieftext darf nicht leer sein.")]
        public string? Content
        {
            get => _standardLetter.Content;
            set
            {
                _standardLetter.Content = value;
                Validate(nameof(Content), value);
                OnPropertyChanged(nameof(Content));
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
        //[MaxLength(4, ErrorMessage = "Kundennummer muss vier Ziffern lang sein.")]
        public string? CustomerNumber
        {
            get => _standardLetter.CustomerNumber;
            set
            {
                _standardLetter.CustomerNumber = value;
                Validate(nameof(CustomerNumber), value);
                OnPropertyChanged(nameof(CustomerNumber));
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Dateiname darf nicht leer sein.")]
        public string? FileName
        {
            get => _standardLetter.FileName;
            set
            {
                _standardLetter.FileName = value;
                Validate(nameof(FileName), value);
                OnPropertyChanged(nameof(FileName));
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

        public void SaveExecute()
        {
            if (ValidateModel())
            {
                var dialog = new FolderBrowserDialog();
                var result = dialog.ShowDialog();
                if (result.ToString() != string.Empty && !result.ToString().Equals("Cancel"))
                {
                    string saveFolderPath = dialog.SelectedPath + @"\";

                    if (File.Exists($"{saveFolderPath}{_standardLetter.FullFileName}"))
                    {
                        var dialogResult = MessageBox.Show(
                            $"Eine Datei mit dem Namen\n\n \"{_standardLetter.FullFileName}\"\n\n existiert bereits am gewählten Speicherort.\n\n Soll diese Datei überschrieben werden?",
                            "Datei existiert bereits",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        
                        if (dialogResult == DialogResult.Yes)
                        {
                            _repository.CreatePdfDocument(_standardLetter, saveFolderPath);
                        }
                    }
                    else
                    {
                        _repository.CreatePdfDocument(_standardLetter, saveFolderPath);
                    }

                }
            }
            else
            {
                Validate(nameof(SenderName), SenderName);
                Validate(nameof(SenderStreetAndNumber), SenderStreetAndNumber);
                Validate(nameof(SenderZipCodeAndCity), SenderZipCodeAndCity);

                Validate(nameof(RecipientName), RecipientName);
                Validate(nameof(RecipientZipCodeAndCity), RecipientZipCodeAndCity);

                Validate(nameof(TopicLineOne), TopicLineOne);
                Validate(nameof(Intro), Intro);
                Validate(nameof(Content), Content);

                Validate(nameof(CustomerNumber), CustomerNumber);
                Validate(nameof(FileName), FileName);
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

        public StandardLetterViewModel()
        {
            _standardLetter = new StandardLetterModel();
            _repository = new Repository.Repository();
        }

        #region Model Validation

        private Dictionary<string, List<string?>> _errors = new Dictionary<string, List<string?>>();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public bool HasErrors => _errors.Count > 0;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                return _errors[propertyName];
            }

            return Enumerable.Empty<string>();
        }

        public void Validate(string propertyName, object? propertyValue)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(propertyValue, new ValidationContext(this) { MemberName = propertyName }, results);

            if (results.Any())
            {
                if (_errors.ContainsKey(propertyName))
                {
                    _errors.Remove(propertyName);
                }

                _errors.Add(propertyName, results.Select(r => r.ErrorMessage).ToList());
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
            else
            {
                _errors.Remove(propertyName);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        private bool _isValid;

        public bool IsValid
        {
            get
            {
                return _isValid;
            }

            private set
            {
                _isValid = value;
            }
        }

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

            IsValid = isValid;

            return isValid;
        }
        #endregion
    }
}
