﻿#region License
/* 
MIT License

Copyright (c) 2024 patrickbo89

https://github.com/patrickbo89

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
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

                if (HasBeenSaved)
                    HasUnsavedChanges = true;

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

                if (HasBeenSaved)
                    HasUnsavedChanges = true;

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

                if (HasBeenSaved)
                    HasUnsavedChanges = true;

                OnPropertyChanged(nameof(SenderZipCodeAndCity));
            }
        }

        public string? SenderAdditionalInfoOne
        {
            get => _standardLetter.SenderAdditionalInfoOne;
            set
            {
                _standardLetter.SenderAdditionalInfoOne = value;

                if (HasBeenSaved)
                    HasUnsavedChanges = true;

                OnPropertyChanged(nameof(SenderAdditionalInfoOne));
            }
        }

        public string? SenderAdditionalInfoTwo
        {
            get => _standardLetter.SenderAdditionalInfoTwo;
            set
            {
                _standardLetter.SenderAdditionalInfoTwo = value;

                if (HasBeenSaved)
                    HasUnsavedChanges = true;

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

                if (HasBeenSaved)
                    HasUnsavedChanges = true;

                OnPropertyChanged(nameof(RecipientName));
            }
        }

        public string? RecipientStreetAndNumber
        {
            get => _standardLetter.RecipientStreetAndNumber;
            set
            {
                _standardLetter.RecipientStreetAndNumber = value;

                if (HasBeenSaved)
                    HasUnsavedChanges = true;

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

                if (HasBeenSaved)
                    HasUnsavedChanges = true;

                OnPropertyChanged(nameof(RecipientZipCodeAndCity));
            }
        }

        public string? RecipientAdditionalInfoOne
        {
            get => _standardLetter.RecipientAdditionalInfoOne;
            set
            {
                _standardLetter.RecipientAdditionalInfoOne = value;

                if (HasBeenSaved)
                    HasUnsavedChanges = true;

                OnPropertyChanged(nameof(RecipientAdditionalInfoOne));
            }
        }

        public string? RecipientAdditionalInfoTwo
        {
            get => _standardLetter.RecipientAdditionalInfoTwo;
            set
            {
                _standardLetter.RecipientAdditionalInfoTwo = value;

                if (HasBeenSaved)
                    HasUnsavedChanges = true;

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

                if (HasBeenSaved)
                    HasUnsavedChanges = true;

                OnPropertyChanged(nameof(TopicLineOne));
            }
        }

        public string? TopicLineTwo
        {
            get => _standardLetter.TopicLineTwo;
            set
            {
                _standardLetter.TopicLineTwo = value;

                if (HasBeenSaved)
                    HasUnsavedChanges = true;

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

                if (HasBeenSaved)
                    HasUnsavedChanges = true;

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

                if (HasBeenSaved)
                    HasUnsavedChanges = true;

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

        public bool HasBeenSaved
        {
            get => _standardLetter.HasBeenSaved;
            set
            {
                _standardLetter.HasBeenSaved = value;
                ChangeInfoText();
                OnPropertyChanged(nameof(HasBeenSaved));
            }
        }

        public bool HasUnsavedChanges
        {
            get => _standardLetter.HasUnsavedChanges;
            set
            {
                _standardLetter.HasUnsavedChanges = value;
                ChangeInfoText();
                OnPropertyChanged(nameof(HasUnsavedChanges));
            }
        }

        public bool HaveChangesBeenSaved
        {
            get => _standardLetter.HaveChangesBeenSaved;
            set
            {
                _standardLetter.HaveChangesBeenSaved = value;
                ChangeInfoText();
                OnPropertyChanged(nameof(HaveChangesBeenSaved));
            }
        }

        private string _infoText;
        public string InfoText
        {
            get
            {
                return _infoText;
            }
            set
            {
                _infoText = value;
                OnPropertyChanged(nameof(InfoText));
            }
        }
        #endregion

        #region Commands
        public void CloseAppExecute()
        {
            DialogResult dialogResult;

            if (HasBeenSaved == false)
            {
                dialogResult = MessageBox.Show(
                    "Der aktuelle Brief wurde noch nicht gespeichert und geht verloren, wenn das Programm beendet wird.\n\nTrotzdem beenden?",
                    "Der Brief wurde noch nicht gespeichert",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }
            else if (HasUnsavedChanges == true)
            {
                dialogResult = MessageBox.Show(
                    "Die Änderungen am aktuellen Brief wurden noch nicht gespeichert und gehen verloren, wenn das Programm beendet wird.\n\nTrotzdem beenden?",
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
                dialog.FileName = $"{_standardLetter.FullFileName}";
                dialog.Filter = "PDF-Datei|*.pdf";
                var result = dialog.ShowDialog();
                dialog.Dispose();
                if (result == DialogResult.OK)
                {
                    string filePath = dialog.FileName;
                    FileInfo fileInfo = new FileInfo(filePath);
                    string fileNameWithoutExtension = fileInfo.Name.Remove(fileInfo.Name.Length - fileInfo.Extension.Length);
                    
                    int underScoreCharCount = 0;
                    foreach (char c in fileNameWithoutExtension)
                    {
                        if (char.Equals(c, '_'))
                        {
                            underScoreCharCount++;
                        }
                    }

                    if (underScoreCharCount > 1 || (underScoreCharCount == 1 && fileNameWithoutExtension.LastIndexOf('_') != fileNameWithoutExtension.Length - 1))
                    {
                        string[] parts = fileInfo.Name.Split('_', 2);
                        CustomerNumber = parts[0];
                        FileName = parts[1].Contains(".pdf") ? parts[1].Remove(parts[1].Length - ".pdf".Length) : parts[1];
                    }
                    else
                    {
                        CustomerNumber = "LEER";
                        FileName = fileInfo.Name.Contains(".pdf") ? fileInfo.Name.Remove(fileInfo.Name.Length - ".pdf".Length) : fileInfo.Name;
                    }

                    bool PDFCreationSuccess = _repository.CreatePdfDocument(_standardLetter, filePath);

                    if (PDFCreationSuccess)
                    {
                        HasBeenSaved = true;

                        if (HasUnsavedChanges)
                            HaveChangesBeenSaved = true;

                        HasUnsavedChanges = false;
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

            HasBeenSaved = false;
            HasUnsavedChanges = false;
            HaveChangesBeenSaved = false;
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

            HasBeenSaved = false;
            HasUnsavedChanges = false;
            HaveChangesBeenSaved= false;
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

            HasBeenSaved = false;
            HasUnsavedChanges = false;
            HaveChangesBeenSaved = false;
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
            _infoText = "Brief wurde noch nicht gespeichert";
        }

        private void ChangeInfoText()
        {
            if (HasBeenSaved && !HasUnsavedChanges && !HaveChangesBeenSaved)
                InfoText = "Brief wurde erfolgreich gespeichert";
            else if (!HasBeenSaved && !HasUnsavedChanges)
                InfoText = "Brief wurde noch nicht gespeichert";
            else if (HasBeenSaved && HasUnsavedChanges)
                InfoText = "Änderungen wurden noch nicht gespeichert";
            else
                InfoText = "Änderungen wurden erfolgreich gespeichert";
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
