#region License
/* 
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
#endregion

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
        public bool HasBeenSaved { get; set; } = false;

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
