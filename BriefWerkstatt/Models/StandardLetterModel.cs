using System.ComponentModel.DataAnnotations;

namespace BriefWerkstatt.Models
{
    public class StandardLetterModel
    {
        [Required]
        public SenderModel Sender { get; set; }

        [Required]
        public RecipientModel Recipient { get; set; }

        [Required]
        public LetterContentModel LetterContent { get; set; }

        [Required]
        public FileInfoModel FileInfo { get; set; }

        

        public StandardLetterModel()
        {
            Sender = new SenderModel();
            Recipient = new RecipientModel();

            LetterContent = new LetterContentModel()
            {
                Intro = "Sehr geehrte Damen und Herren,",
                Outro = "Mit freundlichen Grüßen"
            };

            FileInfo = new FileInfoModel();
        }

        public IEnumerable<ValidationResult> Validate()
        {
            var results = new List<ValidationResult>();

            // Gültigkeit der Nutzereingaben in den jeweiligen TextBoxen überprüfen,
            // indem alle Gültigkeitsattribute der Sub Models überprüft und die Ergebnisse
            // in einer Liste gespeichert werden.

            var senderContext = new ValidationContext(Sender);
            IsValidSender = Validator.TryValidateObject(Sender, senderContext, results, true);

            var recipientContext = new ValidationContext(Recipient);
            IsValidRecipient = Validator.TryValidateObject(Recipient, recipientContext, results, true);

            var letterContentContext = new ValidationContext(LetterContent);
            IsValidLetterContent = Validator.TryValidateObject(LetterContent, letterContentContext, results, true);

            var fileInfoContext = new ValidationContext(FileInfo);
            IsValidFileInfo = Validator.TryValidateObject(FileInfo, fileInfoContext, results, true);

            IsValid = results.Select(r => r.ErrorMessage).ToList().Count == 0;

            return results;
        }


        #region Validation Properties für Border Color Change
        // Hässliche Lösung, muss dringend ausgelagert oder komplett anders umgesetzt werden!

        private bool _isValid;
        public bool IsValid
        {
            get
            {
                Validate();
                return _isValid;
            }
            set
            {
                _isValid = value;
            }
        }

        private bool _isValidSender;
        public bool IsValidSender
        {
            get
            {
                Validate();
                return _isValidSender;
            }
            set
            {
                _isValidSender = value;
            }
        }

        private bool _isValidRecipient;
        public bool IsValidRecipient
        {
            get
            {
                Validate();
                return _isValidRecipient;
            }
            set
            {
                _isValidRecipient = value;
            }
        }

        private bool _isValidLetterContent;
        public bool IsValidLetterContent
        {
            get
            {
                Validate();
                return _isValidLetterContent;
            }
            set
            {
                _isValidLetterContent = value;
            }
        }

        private bool _isValidFileInfo;
        public bool IsValidFileInfo
        {
            get
            {
                Validate();
                return _isValidFileInfo;
            }
            set
            {
                _isValidFileInfo = value;
            }
        }
        #endregion
    }
}
