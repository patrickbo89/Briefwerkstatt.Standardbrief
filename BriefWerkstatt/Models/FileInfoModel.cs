using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BriefWerkstatt.Models
{
    /// <summary>
    /// Repräsentiert und enthält Informationen für die interne Verarbeitung.
    /// CustomerNumber: Die intern verwendete Kundennummer.
    /// FileName: Der Dateiname.
    /// FullFileName: Der komplette Dateiname, setzt sich zusammen aus Kundennummer und Dateinamen.
    /// </summary>
    public class FileInfoModel
    {
        public int CustomerNumber { get; set; }
        public string? FileName { get; set; }

        public string? FullFileName 
        {
            get
            {
                return $"{CustomerNumber}_{FileName}";
            } 
        }
    }
}
