#region License
/* 
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion

using BriefWerkstatt.Models;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;

namespace BriefWerkstatt.ViewModels
{
    public class StandardLetterViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private Repository.Repository _repository;
        private bool _createdNewLetter = false;

        private StandardLetterModel _standardLetter;
        public StandardLetterModel StandardLetter => _standardLetter;

        #region Absender-Properties

        [Required(AllowEmptyStrings = false, ErrorMessage = "Absender: Name darf nicht leer sein.")]
        public string? SenderName
        {
            get => _standardLetter.SenderName;
            set
            {
                _standardLetter.SenderName = value;

                if (!_createdNewLetter)
                    Validate(nameof(SenderName), value);

                if (_standardLetter.HasBeenSaved)
                    _standardLetter.HasUnsavedChanges = true;

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

                if (!_createdNewLetter)
                    Validate(nameof(SenderStreetAndNumber), value);

                if (_standardLetter.HasBeenSaved)
                    _standardLetter.HasUnsavedChanges = true;

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

                if (!_createdNewLetter)
                    Validate(nameof(SenderZipCodeAndCity), value);

                if (_standardLetter.HasBeenSaved)
                    _standardLetter.HasUnsavedChanges = true;

                OnPropertyChanged(nameof(SenderZipCodeAndCity));
            }
        }

        public string? SenderAdditionalInfoOne
        {
            get => _standardLetter.SenderAdditionalInfoOne;
            set
            {
                _standardLetter.SenderAdditionalInfoOne = value;

                if (_standardLetter.HasBeenSaved)
                    _standardLetter.HasUnsavedChanges = true;

                OnPropertyChanged(nameof(SenderAdditionalInfoOne));
            }
        }

        public string? SenderAdditionalInfoTwo
        {
            get => _standardLetter.SenderAdditionalInfoTwo;
            set
            {
                _standardLetter.SenderAdditionalInfoTwo = value;

                if (_standardLetter.HasBeenSaved)
                    _standardLetter.HasUnsavedChanges = true;

                OnPropertyChanged(nameof(SenderAdditionalInfoTwo));
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

                if (!_createdNewLetter)
                    Validate(nameof(RecipientName), value);

                if (_standardLetter.HasBeenSaved)
                    _standardLetter.HasUnsavedChanges = true;

                OnPropertyChanged(nameof(RecipientName));
            }
        }

        public string? RecipientStreetAndNumber
        {
            get => _standardLetter.RecipientStreetAndNumber;
            set
            {
                _standardLetter.RecipientStreetAndNumber = value;

                if (_standardLetter.HasBeenSaved)
                    _standardLetter.HasUnsavedChanges = true;

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

                if (!_createdNewLetter)
                    Validate(nameof(RecipientZipCodeAndCity), value);

                if (_standardLetter.HasBeenSaved)
                    _standardLetter.HasUnsavedChanges = true;

                OnPropertyChanged(nameof(RecipientZipCodeAndCity));
            }
        }

        public string? RecipientAdditionalInfoOne
        {
            get => _standardLetter.RecipientAdditionalInfoOne;
            set
            {
                _standardLetter.RecipientAdditionalInfoOne = value;

                if (_standardLetter.HasBeenSaved)
                    _standardLetter.HasUnsavedChanges = true;

                OnPropertyChanged(nameof(RecipientAdditionalInfoOne));
            }
        }

        public string? RecipientAdditionalInfoTwo
        {
            get => _standardLetter.RecipientAdditionalInfoTwo;
            set
            {
                _standardLetter.RecipientAdditionalInfoTwo = value;

                if (_standardLetter.HasBeenSaved)
                    _standardLetter.HasUnsavedChanges = true;

                OnPropertyChanged(nameof(RecipientAdditionalInfoTwo));
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

                if (!_createdNewLetter)
                    Validate(nameof(TopicLineOne), value);

                if (_standardLetter.HasBeenSaved)
                    _standardLetter.HasUnsavedChanges = true;

                OnPropertyChanged(nameof(TopicLineOne));
            }
        }

        public string? TopicLineTwo
        {
            get => _standardLetter.TopicLineTwo;
            set
            {
                _standardLetter.TopicLineTwo = value;

                if (_standardLetter.HasBeenSaved)
                    _standardLetter.HasUnsavedChanges = true;

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

                if (!_createdNewLetter)
                    Validate(nameof(Intro), value);

                if (_standardLetter.HasBeenSaved)
                    _standardLetter.HasUnsavedChanges = true;

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

                if (!_createdNewLetter)
                    Validate(nameof(Content), value);

                if (_standardLetter.HasBeenSaved)
                    _standardLetter.HasUnsavedChanges = true;

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

                if (!_createdNewLetter)
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

                if (!_createdNewLetter)
                    Validate(nameof(FileName), value);

                OnPropertyChanged(nameof(FileName));
            }
        }
        #endregion

        #region Commands
        public void CloseAppExecute()
        {
            DialogResult dialogResult;

            if (_standardLetter.HasBeenSaved == false)
            {
                dialogResult = MessageBox.Show(
                    "Der aktuelle Brief wurde noch nicht gespeichert und geht verloren, wenn das Programm beendet wird.\n\nTrotzdem beenden?",
                    "Der Brief wurde noch nicht gespeichert",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }
            else if (_standardLetter.HasUnsavedChanges == true)
            {
                dialogResult = MessageBox.Show(
                    "Änderungen am aktuellen Brief wurden noch nicht gespeichert und gehen verloren, wenn das Programm beendet wird.\n\nTrotzdem beenden?",
                    "Änderungen wurden noch nicht gespeichert",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }
            else
            {
                dialogResult = MessageBox.Show(
                    "Programm wird beendet. Sicher?",
                    "Programm beenden?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            }

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
                var dialog = new SaveFileDialog();
                dialog.CheckPathExists = true;
                dialog.FileName = $"{_standardLetter.CustomerNumber}_{_standardLetter.FileName}";
                dialog.Filter = "PDF-Datei|*.pdf";
                var result = dialog.ShowDialog();
                dialog.Dispose();
                if (result == DialogResult.OK)
                {
                    string filePath = dialog.FileName;
                    bool PDFCreationSuccess = _repository.CreatePdfDocument(_standardLetter, filePath);

                    if (PDFCreationSuccess)
                    {
                        _standardLetter.HasBeenSaved = true;
                        _standardLetter.HasUnsavedChanges = false;
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

        public void NewLetterEraseAllDataExecute()
        {
            _createdNewLetter = true;

            SenderName = null;
            SenderStreetAndNumber = null;
            SenderZipCodeAndCity = null;
            SenderAdditionalInfoOne = null;
            SenderAdditionalInfoTwo = null;

            RecipientName = null;
            RecipientStreetAndNumber = null;
            RecipientZipCodeAndCity = null;
            RecipientAdditionalInfoOne = null;
            RecipientAdditionalInfoTwo = null;

            TopicLineOne = null;
            TopicLineTwo = null;
            Intro = "Sehr geehrte Damen und Herren,";
            Content = null;

            CustomerNumber = null;
            FileName = null;

            _standardLetter.HasBeenSaved = false;
            _standardLetter.HasUnsavedChanges = false;
            _createdNewLetter = false;
        }

        public ICommand NewLetterEraseAllData
        {
            get
            {
                return new RelayCommand(NewLetterEraseAllDataExecute);
            }
        }

        public void NewLetterKeepSenderDataExecute()
        {
            _createdNewLetter = true;

            RecipientName = null;
            RecipientStreetAndNumber = null;
            RecipientZipCodeAndCity = null;
            RecipientAdditionalInfoOne = null;
            RecipientAdditionalInfoTwo = null;

            TopicLineOne = null;
            TopicLineTwo = null;
            Intro = "Sehr geehrte Damen und Herren,";
            Content = null;

            FileName = null;

            _standardLetter.HasBeenSaved = false;
            _standardLetter.HasUnsavedChanges = false;
            _createdNewLetter = false;
        }

        public ICommand NewLetterKeepSenderData
        {
            get
            {
                return new RelayCommand(NewLetterKeepSenderDataExecute);
            }
        }

        public void NewLetterKeepSenderAndRecipientDataExecute()
        {
            _createdNewLetter = true;

            TopicLineOne = null;
            TopicLineTwo = null;
            Intro = "Sehr geehrte Damen und Herren,";
            Content = null;

            FileName = null;

            _standardLetter.HasBeenSaved = false;
            _standardLetter.HasUnsavedChanges = false;
            _createdNewLetter = false;
        }

        public ICommand NewLetterKeepSenderAndRecipientData
        {
            get
            {
                return new RelayCommand(NewLetterKeepSenderAndRecipientDataExecute);
            }
        }

        public void FillWithExampleDataExecute()
        {
            SenderName = "Max Mustermann";
            SenderAdditionalInfoOne = "c/o Musterfrau";
            SenderStreetAndNumber = "Musterstraße 123";
            SenderAdditionalInfoTwo = "Vorderhaus, links";
            SenderZipCodeAndCity = "12345 Musterstadt";

            RecipientName = "Beispielfirma GmbH";
            RecipientAdditionalInfoOne = "z.H. Erika Musterfrau";
            RecipientStreetAndNumber = "Musterallee 123";
            RecipientAdditionalInfoTwo = "Hinterhaus, links";
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
                SenderAdditionalInfoOne = null;
                SenderStreetAndNumber = null;
                SenderAdditionalInfoTwo = null;
                SenderZipCodeAndCity = null;

                RecipientName = null;
                RecipientAdditionalInfoOne = null;
                RecipientStreetAndNumber = null;
                RecipientAdditionalInfoTwo = null;
                RecipientZipCodeAndCity = null;

                TopicLineOne = null;
                TopicLineTwo = null;
                Intro = "Sehr geehrte Damen und Herren,";
                Content = null;

                CustomerNumber = null;
                FileName = null;
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
