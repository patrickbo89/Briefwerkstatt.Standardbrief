using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BriefWerkstatt.Models
{
    public class SenderModel
    {

        // Erforderliche Angaben

        [Required(AllowEmptyStrings = false, ErrorMessage = "Absender: Name darf nicht leer sein.")]
        public string? Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Absender: Straße darf nicht leer sein.")]
        public string? StreetName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Absender: Haus-Nr. darf nicht leer sein.")]
        [Range(1, 9999, ErrorMessage = "Absender: Haus-Nr. muss zwischen 1 und 9999 sein.")]
        public int? StreetNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Absender: Postleitzahl darf nicht leer sein.")]
        [Range(10000, 99999, ErrorMessage = "Absender: Postleitzahl muss zwischen 10000 und 99999 sein.")]
        public int? ZipCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Absender: Ort darf nicht leer sein.")]
        public string? CityName { get; set; }

        // Optionale Angaben
        public string? CareOfInfo { get; set; }
        public string? AdditionalAdressInfo { get; set; }
    }
}
