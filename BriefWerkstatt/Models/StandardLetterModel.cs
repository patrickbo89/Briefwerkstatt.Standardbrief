#region License
/* 
MIT License

Copyright (c) 2024 patrickbo89

https://github.com/patrickbo89

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

namespace BriefWerkstatt.Models
{
    public class StandardLetterModel
    {
        #region Sender Properties
        public string? SenderName { get; set; }
        public string? SenderAdditionalInfoOne { get; set; }
        public string? SenderStreetAndNumber { get; set; }
        public string? SenderAdditionalInfoTwo { get; set; }
        public string? SenderZipCodeAndCity { get; set; }
        #endregion

        #region Recipient Properties
        public string? RecipientName { get; set; }
        public string? RecipientAdditionalInfoOne { get; set; }
        public string? RecipientStreetAndNumber { get; set; }
        public string? RecipientAdditionalInfoTwo { get; set; }
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
        public bool HasUnsavedChanges { get; set; } = false;

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
