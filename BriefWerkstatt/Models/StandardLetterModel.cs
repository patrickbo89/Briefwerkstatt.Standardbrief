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

        private bool _isValid;
        public bool IsValid { get { Validate(); return _isValid; } set { _isValid = value; } }

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
            Validator.TryValidateObject(Sender, senderContext, results, true);

            var recipientContext = new ValidationContext(Recipient);
            Validator.TryValidateObject(Recipient, recipientContext, results, true);

            var letterContentContext = new ValidationContext(LetterContent);
            Validator.TryValidateObject(LetterContent, letterContentContext, results, true);

            var fileInfoContext = new ValidationContext(FileInfo);
            Validator.TryValidateObject(FileInfo, fileInfoContext, results, true);

            IsValid = results.Select(r => r.ErrorMessage).ToList().Count == 0;

            return results;
        }
    }
}
