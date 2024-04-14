using System.ComponentModel.DataAnnotations;

namespace BriefWerkstatt.Models
{
    public class LetterContentModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Betreffzeile 1 darf nicht leer sein.")]
        public string? TopicLineOne { get; set; }

        public string? TopicLineTwo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Anrede darf nicht leer sein.")]
        public string? Intro { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Briefinhalt darf nicht leer sein.")]
        public string? TextBody { get; set; }
        public string? Outro { get; set; }
    }
}
