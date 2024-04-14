using BriefWerkstatt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;


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
            }
        }

        public string? SenderStreetName
        {
            get => _standardLetter.Sender.StreetName;
            set
            {
                _standardLetter.Sender.StreetName = value;
                OnPropertyChanged(nameof(SenderStreetName));
            }
        }

        public int? SenderStreetNumber
        {
            get => _standardLetter.Sender.StreetNumber;
            set
            {
                _standardLetter.Sender.StreetNumber = value;
                OnPropertyChanged(nameof(SenderStreetNumber));
            }
        }

        public int? SenderZipCode
        {
            get => _standardLetter.Sender.ZipCode;
            set
            {
                _standardLetter.Sender.ZipCode = value;
                OnPropertyChanged(nameof(SenderZipCode));
            }
        }

        public string? SenderCityName
        {
            get => _standardLetter.Sender.CityName;
            set
            {
                _standardLetter.Sender.CityName = value;
                OnPropertyChanged(nameof(SenderCityName));
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
            }
        }

        public string? RecipientStreetName
        {
            get => _standardLetter.Recipient.StreetName;
            set
            {
                _standardLetter.Recipient.StreetName = value;
                OnPropertyChanged(nameof(RecipientStreetName));
            }
        }

        public int? RecipientStreetNumber
        {
            get => _standardLetter.Recipient.StreetNumber;
            set
            {
                _standardLetter.Recipient.StreetNumber = value;
                OnPropertyChanged(nameof(RecipientStreetNumber));
            }
        }

        public int? RecipientZipCode
        {
            get => _standardLetter.Recipient.ZipCode;
            set
            {
                _standardLetter.Recipient.ZipCode = value;
                OnPropertyChanged(nameof(RecipientZipCode));
            }
        }

        public string? RecipientCityName
        {
            get => _standardLetter.Recipient.CityName;
            set
            {
                _standardLetter.Recipient.CityName = value;
                OnPropertyChanged(nameof(RecipientCityName));
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
        public string? Topic
        {
            get => _standardLetter.LetterContent.Topic;
            set
            {
                _standardLetter.LetterContent.Topic = value;
                OnPropertyChanged(nameof(Topic));
            }
        }

        public string? Intro
        {
            get => _standardLetter.LetterContent?.Intro;
            set
            {
                _standardLetter.LetterContent.Intro = value;
                OnPropertyChanged(nameof(Intro));
            }
        }

        public string? TextBody
        {
            get => _standardLetter.LetterContent.TextBody;
            set
            {
                _standardLetter.LetterContent.TextBody = value;
                OnPropertyChanged(nameof(TextBody));
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
            }
        }

        public string? FileName
        {
            get => _standardLetter.FileInfo?.FileName;
            set
            {
                _standardLetter.FileInfo.FileName = value;
                OnPropertyChanged(nameof(FileName));
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

            RecipientName = "Maxine Musterfrau";
            RecipientCareOfInfo = "c/o Mustermann";
            RecipientStreetName = "Musterallee";
            RecipientStreetNumber = 1234;
            RecipientAdditionalAdressInfo = "Hinterhaus, links";
            RecipientZipCode = 12345;
            RecipientCityName = "Musterstadt";

            Topic = "Betreff";
            TextBody = "hiermit teile ich Ihnen mit, dass ich meinen Vertrag kündigen möchte.\n\nBitte senden Sie mir eine Kündigungsbestätigung zu.\n\nVielen Dank.";

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

            Topic = null;
            TextBody = null;

            CustomerNumber = null;
            FileName = null;
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
    }
}
