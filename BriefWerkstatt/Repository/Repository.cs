﻿using BriefWerkstatt.Models;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System.Diagnostics;
using System.Text;

namespace BriefWerkstatt.Repository
{
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
        private const double TopMargin = 15.0;
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

            DrawSenderBlock(gfx, standardLetter.Sender);
            DrawWindowEnvelopeAddress(gfx, standardLetter.Sender);
            DrawRecipientBlock(gfx, standardLetter.Recipient);
            DrawLetterBodyText(gfx, document, standardLetter.Sender, standardLetter.LetterContent);

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
            PdfViewer.StartInfo.FileName = standardLetterModel.FileInfo.FullFileName;
            PdfViewer.Start();
        }

        private void SaveDocument(PdfDocument document, string saveFolderPath, StandardLetterModel standardLetterModel)
        {
            document.Save($"{saveFolderPath}{standardLetterModel.FileInfo.FullFileName}");
        }

        private void DrawSenderBlock(XGraphics gfx, SenderModel sender)
        {
            // Der Absender-Bereich befindet sich im 45 mm hohen Briefkopf, linksbündig
            XRect HeaderRect = new XRect(
                CreateXPointFromMillimetres(LeftMargin, TopMargin),
                CreateXPointFromMillimetres(PageWidth - RightMargin, HeaderMargin));

            XTextFormatter tf = new XTextFormatter(gfx);

            StringBuilder senderAdressBlock = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(sender.CareOfInfo))
            {
                senderAdressBlock.Append($"\n{sender.CareOfInfo}");
            }

            senderAdressBlock.Append($"\n{sender.StreetName} {sender.StreetNumber}");

            if (!string.IsNullOrWhiteSpace(sender.AdditionalAdressInfo))
            {
                senderAdressBlock.Append($"\n{sender.AdditionalAdressInfo}");
            }

            senderAdressBlock.Append($"\n{sender.ZipCode} {sender.CityName}");

            //gfx.DrawRectangle(XBrushes.Blue, HeaderRect); // Test
            tf.DrawString(sender.Name, _senderNameFont, XBrushes.Black, HeaderRect, XStringFormats.TopLeft);
            tf.DrawString(senderAdressBlock.ToString(), _normalFont, XBrushes.Black, HeaderRect, XStringFormats.TopLeft);
        }

        private void DrawWindowEnvelopeAddress(XGraphics gfx, SenderModel sender)
        {
            // Die Fensterkuvertzeile befindet sich 45mm von der oberen Blattkante, ist 85mm breit und 5mm hoch
            XRect windowTextLineRect = new XRect(
                CreateXPointFromMillimetres(LeftMargin, HeaderMargin),
                CreateXPointFromMillimetres(85.0, HeaderMargin + 5.0)
                );

            XTextFormatter tf = new XTextFormatter(gfx);

            StringBuilder windowTextLine = new StringBuilder();
            windowTextLine.Append($"{sender.Name}" +
                $", {sender.StreetName}" +
                $" {sender.StreetNumber}" +
                $", {sender.ZipCode}" +
                $" {sender.CityName}"
                );

            windowTextLine.Append(string.IsNullOrWhiteSpace(sender.CareOfInfo) ? "" : $"\n{sender.CareOfInfo}");
            windowTextLine.Append(string.IsNullOrWhiteSpace(sender.AdditionalAdressInfo) ? "" : $", {sender.AdditionalAdressInfo}");

            //gfx.DrawRectangle(XBrushes.Purple, windowTextLineRect);

            tf.DrawString(
                windowTextLine.ToString(), _windowEnvelopeLineFont, XBrushes.Black, windowTextLineRect, XStringFormats.TopLeft);
        }

        private void DrawRecipientBlock(XGraphics gfx, RecipientModel recipient)
        {
            // Das Empfänger-Anschriftenfeld hat eine Breite von 85mm und eine Höhe von 40mm
            XRect RecipientAddressRect = new XRect(
                CreateXPointFromMillimetres(LeftMargin, HeaderMargin + 5.0),
                CreateXPointFromMillimetres(85.0, HeaderMargin + 5.0 + 40.0)
                );

            XTextFormatter tf = new XTextFormatter(gfx);

            StringBuilder recipientAddressBlock = new StringBuilder();

            recipientAddressBlock.Append($"\n{recipient.Name}");

            if (!string.IsNullOrWhiteSpace(recipient.CareOfInfo))
            {
                recipientAddressBlock.Append($"\n{recipient.CareOfInfo}");
            }

            recipientAddressBlock.Append($"\n{recipient.StreetName} {recipient.StreetNumber}");

            if (!string.IsNullOrWhiteSpace(recipient.AdditionalAdressInfo))
            {
                recipientAddressBlock.Append($"\n{recipient.AdditionalAdressInfo}");
            }

            recipientAddressBlock.Append($"\n{recipient.ZipCode} {recipient.CityName}");

            //gfx.DrawRectangle(XBrushes.Orange, RecipientAddressRect);

            tf.DrawString(
                recipientAddressBlock.ToString(), _normalFont, XBrushes.Black, RecipientAddressRect, XStringFormats.TopLeft);

        }

        private void DrawLetterBodyText(XGraphics gfx, PdfDocument document, SenderModel sender, LetterContentModel letterContent)
        {
            // Der LetterBodyText beinhaltet das Datum, die Betreffzeilen, die Anrede, den Brieftext, die Grußformel und
            // den Absender-Namen. Es hat einen Abstand von 8.4mm zum Empfänger-Anschriftenfeld und eine maximale Höhe bis
            // zum unteren Seitenrand mit einem unteren Seitenabstand von 20mm.

            string date = $"{sender.CityName}, den {DateTime.Now.Date.ToString("d. MMMM yyyy")}";

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

            if (!string.IsNullOrWhiteSpace(letterContent.TopicLineTwo))
            {
                letterContentFirstPageOrPreviousBlock.Append('\n');
            }

            letterContentFirstPageOrPreviousBlock.Append(
                $"\n\n\n\n\n\n{letterContent.Intro}" +
                $"\n\n{letterContent.TextBody}" +
                $"\n\n{letterContent.Outro}" +
                $"\n\n\n\n\n{sender.Name}"
                );



            gfx.DrawString(
                date, _normalFont, XBrushes.Black, letterContentFirstPageRect, XStringFormats.TopRight);

            tf.DrawString(
                $"\n\n\n{letterContent.TopicLineOne}", _topicFont, XBrushes.Black, letterContentFirstPageRect, XStringFormats.TopLeft);

            if (!string.IsNullOrWhiteSpace(letterContent.TopicLineTwo))
            {
                tf.DrawString(
                    $"\n\n\n\n{letterContent.TopicLineTwo}", _topicFont, XBrushes.Black, letterContentFirstPageRect, XStringFormats.TopLeft);
            }

            tf.Alignment = XParagraphAlignment.Justify;
            tf.DrawString(
                letterContentFirstPageOrPreviousBlock.ToString(), _normalFont, XBrushes.Black, letterContentFirstPageRect, XStringFormats.TopLeft);

            bool hasNextPage = HasNextPage(tf, letterContentFirstPageOrPreviousBlock, letterContentFirstPageRect, out int lastCharIndex);
            bool wasOutroCutOff = false;

            while (hasNextPage)
            {
                string nextPageText = letterContentFirstPageOrPreviousBlock.ToString().Substring(lastCharIndex + 1);
                letterContentFirstPageOrPreviousBlock.Clear();
                letterContentFirstPageOrPreviousBlock.Append(nextPageText);

                //if (wasOutroCutOff)
                //{
                //    letterContentFirstPageOrPreviousBlock.Clear();
                //    letterContentFirstPageOrPreviousBlock.Append(
                //        $"\n\n{letterContent.Outro}" +
                //        $"\n\n\n\n\n{sender.Name}"
                //        );
                //}

                //if (!wasOutroCutOff && nextPageText.EndsWith("Mit freundlichen Grüßen") 
                //    || nextPageText.EndsWith("Mit freundlichen Grüßen\n") 
                //    || nextPageText.EndsWith("Mit freundlichen Grüßen\n\n")
                //    || nextPageText.EndsWith("Mit freundlichen Grüßej\n\n\n")
                //    || nextPageText.EndsWith("Mit freundlichen Grüßen\n\n\n\n")
                //    || nextPageText.EndsWith("Mit freundlichen Grüßen\n\n\n\n\n"))
                //{
                //    wasOutroCutOff = true;
                //    letterContentFirstPageOrPreviousBlock.Replace("Mit freundlichen Grüßen", "");
                //}
                
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

        private bool HasNextPage(XTextFormatterEx2 tf, string text, XRect letterContentRect, out int lastFittingCharIndex)
        {
            tf.PrepareDrawString(text, _normalFont, letterContentRect, out lastFittingCharIndex, out _);

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
