using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BriefWerkstatt.Models
{
    public class SenderModel
    {

        // Erforderliche Angaben
        public string? Name { get; set; }
        public string? StreetName { get; set; }
        public int StreetNumber { get; set; }
        public int ZipCode { get; set; }
        public string? CityName { get; set; }

        // Optionale Angaben
        public string? CareOfInfo { get; set; }
        public string? AdditionalAdressInfo { get; set; }
    }
}
