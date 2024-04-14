using System.ComponentModel.DataAnnotations;

namespace BriefWerkstatt.Models
{
    /// <summary>
    /// Repräsentiert und enthält Informationen für die interne Verarbeitung.
    /// CustomerNumber: Die intern verwendete Kundennummer.
    /// FileName: Der Dateiname.
    /// FullFileName: Der komplette Dateiname: "Kundennummer_Dateiname.pdf"
    /// </summary>
    public class FileInfoModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Kundennummer darf nicht leer sein.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Kundennummer muss vierstellig sein.")]
        public string? CustomerNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Dateiname darf nicht leer sein.")]
        public string? FileName { get; set; }

        public string? FullFileName
        {
            get
            {
                return $"{CustomerNumber}_{FileName}.pdf";
            } 
        }
    }
}
