using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BriefWerkstatt.Models
{
    public class LetterContentModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Betreff darf nicht leer sein.")]
        public string? Topic { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Anrede darf nicht leer sein.")]
        public string? Intro { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Briefinhalt darf nicht leer sein.")]
        public string? TextBody { get; set; }
        public string? Outro { get; set; }
    }
}
