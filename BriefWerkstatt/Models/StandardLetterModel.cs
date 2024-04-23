using System.ComponentModel.DataAnnotations;

namespace BriefWerkstatt.Models
{
    public class StandardLetterModel
    {
        #region Sender Properties
        public string? SenderName { get; set; }
        public string? SenderCareOfInfo { get; set; }
        public string? SenderStreetAndNumber { get; set; }
        public string? SenderAdditionalInfo { get; set; }
        public string? SenderZipCodeAndCity { get; set; }
        #endregion

        #region Recipient Properties
        public string? RecipientName { get; set; }
        public string? RecipientCareOfInfo { get; set; }
        public string? RecipientStreetAndNumber { get; set; }
        public string? RecipientAdditionalInfo { get; set; }
        public string? RecipientZipCodeAndCity { get; set; }
        #endregion

        #region Letter Content Properties
        public string? TopicLineOne { get; set; }
        public string? TopicLineTwo { get; set; }
        public string? Intro { get; set; } = "Sehr geehrte Damen und Herren,";
        public string? Content { get; set; }
        public string? Outro { get; set; } = "Mit freundlichen Grüßen";
        #endregion

        #region File Properties
        public string? CustomerNumber { get; set; }
        public string? FileName { get; set; }

        public string? FullFileName
        {
            get
            {
                return $"{CustomerNumber}_{FileName}.pdf";
            }
        }
        #endregion
    }
}
