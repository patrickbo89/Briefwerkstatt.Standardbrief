﻿using System.ComponentModel.DataAnnotations;

namespace BriefWerkstatt.Models
{
    public class RecipientModel
    {
        // Erforderliche Angaben

        [Required(AllowEmptyStrings = false, ErrorMessage = "Empfänger: Name darf nicht leer sein.")]
        public string? Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Empfänger: Straße darf nicht leer sein.")]
        public string? StreetName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Empfänger: Haus-Nr. darf nicht leer sein.")]
        [Range(1, 9999, ErrorMessage = "Empfänger: Haus-Nr. muss zwischen 1 und 9999 sein.")]
        public int? StreetNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Empfänger: Postleitzahl darf nicht leer sein.")]
        [Range(10000, 99999, ErrorMessage = "Empfänger: Postleitzahl muss zwischen 10000 und 99999 sein.")]
        public int? ZipCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Empfänger: Ort darf nicht leer sein.")]
        public string? CityName { get; set; }

        // Optionale Angaben
        public string? CareOfInfo { get; set; }
        public string? AdditionalAdressInfo { get; set; }
    }
}
