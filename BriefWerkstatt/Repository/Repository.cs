using BriefWerkstatt.Models;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System.Diagnostics;
using System.Text;

namespace BriefWerkstatt.Repository
{
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

    #region Licensing PDFSharp-WPF 6.0.0
    /* ﻿ Copyright (c) 2001-2024 empira Software GmbH, Troisdorf (Cologne Area), Germany

        http://docs.pdfsharp.net

        MIT License

        Permission is hereby granted, free of charge, to any person obtaining a
        copy of this software and associated documentation files (the "Software"),
        to deal in the Software without restriction, including without limitation
        the rights to use, copy, modify, merge, publish, distribute, sublicense,
        and/or sell copies of the Software, and to permit persons to whom the
        Software is furnished to do so, subject to the following conditions:

        The above copyright notice and this permission notice shall be included
        in all copies or substantial portions of the Software.

        THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
        IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
        FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
        THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
        LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
        FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
        DEALINGS IN THE SOFTWARE.
    */
    #endregion

    public class Repository
    {
        #region Constants
        // Seitengröße in Millimeter
        private const double PageWidth = 210.0;
        private const double PageHeight = 297.0;

        // Seitenrandabstände in Millimeter
        private const double LeftMargin = 25.0;
        private const double RightMargin = 20.0;
        private const double TopMargin = 25.0;
        private const double BottomMargin = 20.0;
        private const double HeaderMargin = 45.0;
        #endregion

        #region Fonts
        private readonly XFont _senderNameFont = new XFont("Arial", 12, XFontStyleEx.Bold);
        private readonly XFont _normalFont = new XFont("Arial", 11, XFontStyleEx.Regular);
        private readonly XFont _topicFont = new XFont("Arial", 11, XFontStyleEx.Bold);
        private readonly XFont _windowEnvelopeLineFont = new XFont("Arial", 6, XFontStyleEx.Regular);
        private readonly XFont _pageCountFont = new XFont("Arial", 9, XFontStyleEx.Regular);
        #endregion

        public void CreatePdfDocument(StandardLetterModel standardLetter, string saveFolderPath)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            DrawSenderBlock(gfx, standardLetter);
            DrawWindowEnvelopeAddress(gfx, standardLetter);
            DrawRecipientBlock(gfx, standardLetter);
            DrawLetterBodyText(gfx, document, standardLetter);

            DrawFoldingLines(gfx);
            DrawHolePunchGuide(gfx);

            SaveDocument(document, saveFolderPath, standardLetter);
            OpenDocumentInViewer(saveFolderPath, standardLetter);
        }

        private void OpenDocumentInViewer(string saveFolderPath, StandardLetterModel standardLetterModel)
        {
            // Open PDF with external viewer
            Process PdfViewer = new Process();
            PdfViewer.StartInfo.UseShellExecute = true;
            PdfViewer.StartInfo.WorkingDirectory = saveFolderPath;
            PdfViewer.StartInfo.FileName = standardLetterModel.FullFileName;
            PdfViewer.Start();
        }

        private void SaveDocument(PdfDocument document, string saveFolderPath, StandardLetterModel standardLetterModel)
        {
            document.Save($"{saveFolderPath}{standardLetterModel.FullFileName}");
        }

        private void DrawSenderBlock(XGraphics gfx, StandardLetterModel letter)
        {
            // Der Absender-Bereich befindet sich im 45 mm hohen Briefkopf, linksbündig
            XRect HeaderRect = new XRect(
                CreateXPointFromMillimetres(LeftMargin, TopMargin - 10),
                CreateXPointFromMillimetres(PageWidth - RightMargin, HeaderMargin));

            XTextFormatter tf = new XTextFormatter(gfx);

            StringBuilder senderAdressBlock = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(letter.SenderCareOfInfo))
            {
                senderAdressBlock.Append($"\n{letter.SenderCareOfInfo}");
            }

            senderAdressBlock.Append($"\n{letter.SenderStreetAndNumber}");

            if (!string.IsNullOrWhiteSpace(letter.SenderAdditionalInfo))
            {
                senderAdressBlock.Append($"\n{letter.SenderAdditionalInfo}");
            }

            senderAdressBlock.Append($"\n{letter.SenderZipCodeAndCity}");

            tf.DrawString(letter.SenderName, _senderNameFont, XBrushes.Black, HeaderRect, XStringFormats.TopLeft);
            tf.DrawString(senderAdressBlock.ToString(), _normalFont, XBrushes.Black, HeaderRect, XStringFormats.TopLeft);
        }

        private void DrawWindowEnvelopeAddress(XGraphics gfx, StandardLetterModel letter)
        {
            // Die Fensterkuvertzeile befindet sich 45mm von der oberen Blattkante, ist 85mm breit und 7.5mm hoch
            XRect windowTextLineRect = new XRect(
                CreateXPointFromMillimetres(LeftMargin, HeaderMargin + 10.0),
                CreateXPointFromMillimetres(85.0, HeaderMargin + 2.5)
                );

            XTextFormatter tf = new XTextFormatter(gfx);

            StringBuilder windowTextLine = new StringBuilder();
            windowTextLine.Append($"{letter.SenderName}");

            // Überprüft, ob die Absenderdaten eine bestimmte Länge überschreiten, damit nicht abgeschnitten wird, wenn
            // in die nächste Zeile verschoben wird.
            if (letter.SenderName.Length + letter.SenderStreetAndNumber.Length + letter.SenderZipCodeAndCity.Length >= 52)
            {
                windowTextLine.Append(
                    $", \n{letter.SenderStreetAndNumber}" +
                    $", {letter.SenderZipCodeAndCity}"
                );
            }
            else
            {
                windowTextLine.Append(
                    $", {letter.SenderStreetAndNumber}" +
                    $", {letter.SenderZipCodeAndCity}"
                );
            }

            windowTextLine.Append(string.IsNullOrWhiteSpace(letter.SenderCareOfInfo) ? "" : $"\n{letter.SenderCareOfInfo}");
            windowTextLine.Append(string.IsNullOrWhiteSpace(letter.SenderAdditionalInfo) ? "" : $", {letter.SenderAdditionalInfo}");

            tf.DrawString(
                windowTextLine.ToString(), _windowEnvelopeLineFont, XBrushes.Black, windowTextLineRect, XStringFormats.TopLeft);
        }

        private void DrawRecipientBlock(XGraphics gfx, StandardLetterModel letter)
        {
            // Das Empfänger-Anschriftenfeld hat eine Breite von 85mm und eine Höhe von 40mm
            XRect RecipientAddressRect = new XRect(
                CreateXPointFromMillimetres(LeftMargin, HeaderMargin + 5.0 + 5.0),
                CreateXPointFromMillimetres(85.0, HeaderMargin + 5.0 + 40.0)
                );

            XTextFormatter tf = new XTextFormatter(gfx);

            StringBuilder recipientAddressBlock = new StringBuilder();

            recipientAddressBlock.Append($"\n{letter.RecipientName}");

            if (!string.IsNullOrWhiteSpace(letter.RecipientCareOfInfo))
            {
                recipientAddressBlock.Append($"\n{letter.RecipientCareOfInfo}");
            }

            if (!string.IsNullOrWhiteSpace(letter.RecipientStreetAndNumber))
            {
                recipientAddressBlock.Append($"\n{letter.RecipientStreetAndNumber}");
            }

            if (!string.IsNullOrWhiteSpace(letter.RecipientAdditionalInfo))
            {
                recipientAddressBlock.Append($"\n{letter.RecipientAdditionalInfo}");
            }

            recipientAddressBlock.Append($"\n{letter.RecipientZipCodeAndCity}");

            tf.DrawString(
                recipientAddressBlock.ToString(), _normalFont, XBrushes.Black, RecipientAddressRect, XStringFormats.TopLeft);

        }

        private void DrawLetterBodyText(XGraphics gfx, PdfDocument document, StandardLetterModel letter)
        {
            // Der LetterBodyText beinhaltet das Datum, die Betreffzeilen, die Anrede, den Brieftext, die Grußformel und
            // den Absender-Namen. Es hat einen Abstand von 8.4mm zum Empfänger-Anschriftenfeld und eine maximale Höhe bis
            // zum unteren Seitenrand mit einem unteren Seitenabstand von 20mm.

            string[] parts = letter.SenderZipCodeAndCity.Split(' ');
            string city = "";

            if (parts.Length >= 2) 
            { 
                for (int index = 0; index < parts.Length; index++)
                {
                    if (index !=0)
                    {
                        city += parts[index] + " ";
                    }
                }
            }
            else
            {
                city = parts[0];
            }

            string date = $"{city.TrimEnd()}, den {DateTime.Now.Date.ToString("d. MMMM yyyy")}";

            XRect letterContentFirstPageRect = new XRect(
                CreateXPointFromMillimetres(LeftMargin, HeaderMargin + 5.0 + 40.0 + 8.4),
                CreateXPointFromMillimetres(PageWidth - RightMargin, PageHeight - BottomMargin)
                );

            XRect letterContentNextPageRect = new XRect(
                CreateXPointFromMillimetres(LeftMargin, TopMargin),
                CreateXPointFromMillimetres(PageWidth - RightMargin, PageHeight - BottomMargin)
                );

            StringBuilder letterContentFirstPageOrPreviousBlock = new StringBuilder();
            StringBuilder letterContentNextPageBlock = new StringBuilder();

            XTextFormatterEx2 tf = new XTextFormatterEx2(gfx);

            if (!string.IsNullOrWhiteSpace(letter.TopicLineTwo))
            {
                letterContentFirstPageOrPreviousBlock.Append('\n');
            }

            letterContentFirstPageOrPreviousBlock.Append(
                $"\n\n\n\n\n\n{letter.Intro}" +
                $"\n\n{letter.Content}" +
                $"\n\n{letter.Outro}" +
                $"\n\n\n\n\n{letter.SenderName}"
                );

            gfx.DrawString(
                date, _normalFont, XBrushes.Black, letterContentFirstPageRect, XStringFormats.TopRight);

            tf.DrawString(
                $"\n\n\n{letter.TopicLineOne}", _topicFont, XBrushes.Black, letterContentFirstPageRect, XStringFormats.TopLeft);

            if (!string.IsNullOrWhiteSpace(letter.TopicLineTwo))
            {
                tf.DrawString(
                    $"\n\n\n\n{letter.TopicLineTwo}", _topicFont, XBrushes.Black, letterContentFirstPageRect, XStringFormats.TopLeft);
            }

            tf.Alignment = XParagraphAlignment.Justify;
            tf.DrawString(
                letterContentFirstPageOrPreviousBlock.ToString(), _normalFont, XBrushes.Black, letterContentFirstPageRect, XStringFormats.TopLeft);

            bool hasNextPage = HasNextPage(tf, letterContentFirstPageOrPreviousBlock, letterContentFirstPageRect, out int lastCharIndex);

            while (hasNextPage)
            {
                string nextPageText = letterContentFirstPageOrPreviousBlock.ToString().Substring(lastCharIndex + 1).TrimStart();
                letterContentFirstPageOrPreviousBlock.Clear();
                letterContentFirstPageOrPreviousBlock.Append(nextPageText);
                
                PdfPage nextPage = document.AddPage();
                gfx = XGraphics.FromPdfPage(nextPage);
                tf = new XTextFormatterEx2(gfx);
                tf.Alignment = XParagraphAlignment.Justify;

                tf.DrawString(
                    letterContentFirstPageOrPreviousBlock.ToString(), _normalFont, XBrushes.Black, letterContentNextPageRect, XStringFormats.TopLeft);

                hasNextPage = HasNextPage(tf, letterContentFirstPageOrPreviousBlock, letterContentNextPageRect, out lastCharIndex);

                DrawPageNumber(gfx, document.PageCount);
                DrawFoldingLines(gfx);
                DrawHolePunchGuide(gfx);
            }
        }

        private void DrawPageNumber(XGraphics gfx, int pageCount)
        {
            XRect pageCountRect = new XRect(
                CreateXPointFromMillimetres(LeftMargin, PageHeight - 10),
                CreateXPointFromMillimetres(PageWidth - RightMargin, PageHeight));

            gfx.DrawString($"- {pageCount} -", _pageCountFont, XBrushes.Black, pageCountRect, XStringFormats.Center);
        }

        private bool HasNextPage(XTextFormatterEx2 tf, StringBuilder letterContentBlock, XRect letterContentRect, out int lastFittingCharIndex)
        {
            tf.PrepareDrawString(letterContentBlock.ToString(), _normalFont, letterContentRect, out lastFittingCharIndex, out _);

            return lastFittingCharIndex != -1;
        }

        private XPoint CreateXPointFromMillimetres(double millimetresX, double millimetresY)
        {
            return new XPoint(ConvertMillimetresToPointValue(millimetresX), ConvertMillimetresToPointValue(millimetresY));
        }

        private double ConvertMillimetresToPointValue(double millimetres)
        {
            double conversionRateInchMillimetres = 25.4;
            double InchDenominatorDTP = 72.0;
            return millimetres / conversionRateInchMillimetres * InchDenominatorDTP;
        }

        private void DrawFoldingLines(XGraphics gfx)
        {
            // Falzmarken nach Geschäftsbrief Form B DIN 5008
            gfx.DrawLine(XPens.Black, CreateXPointFromMillimetres(5.0, 105.0), CreateXPointFromMillimetres(10.0, 105.0));
            gfx.DrawLine(XPens.Black, CreateXPointFromMillimetres(5.0, 210.0), CreateXPointFromMillimetres(10.0, 210.0));
        }

        private void DrawHolePunchGuide(XGraphics gfx)
        {
            // Lochmarke nach Geschäftsbrief Form B DIN 5008
            gfx.DrawLine(XPens.Black, CreateXPointFromMillimetres(5.0, 148.5), CreateXPointFromMillimetres(15.0, 148.5));
        }
    }
}
