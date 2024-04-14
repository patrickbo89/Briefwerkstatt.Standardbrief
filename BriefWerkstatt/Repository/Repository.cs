using BriefWerkstatt.Models;
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
        private readonly XFont _boldFont = new XFont("Arial", 12, XFontStyleEx.Bold);
        private readonly XFont _normalFont = new XFont("Arial", 11, XFontStyleEx.Regular);
        private readonly XFont _windowEnvelopeLineFont = new XFont("Arial", 6, XFontStyleEx.Regular);
        #endregion

        public void CreatePdfDocument(StandardLetterModel standardLetter, string saveFolderPath)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            DrawSenderBlock(gfx, standardLetter.Sender);
            DrawWindowEnvelopeAddress(gfx, standardLetter.Sender);
            DrawRecipientBlock(gfx, standardLetter.Recipient);
            DrawLetterBodyText(gfx, standardLetter.Sender, standardLetter.LetterContent);

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

            if (sender.CareOfInfo != null)
            {
                senderAdressBlock.Append($"\n{sender.CareOfInfo}");
            }

            senderAdressBlock.Append($"\n{sender.StreetName} {sender.StreetNumber}");

            if (sender.AdditionalAdressInfo != null)
            {
                senderAdressBlock.Append($"\n{sender.AdditionalAdressInfo}");
            }

            senderAdressBlock.Append($"\n{sender.ZipCode} {sender.CityName}");

            //gfx.DrawRectangle(XBrushes.Blue, HeaderRect); // Test
            tf.DrawString(sender.Name, _boldFont, XBrushes.Black, HeaderRect, XStringFormats.TopLeft);
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

            windowTextLine.Append(sender.CareOfInfo == null ? "" : $"\n{sender.CareOfInfo}");
            windowTextLine.Append(sender.AdditionalAdressInfo == null ? "" : $", {sender.AdditionalAdressInfo}");

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

            if (recipient.CareOfInfo != null)
            {
                recipientAddressBlock.Append($"\n{recipient.CareOfInfo}");
            }

            recipientAddressBlock.Append($"\n{recipient.StreetName} {recipient.StreetNumber}");

            if (recipient.AdditionalAdressInfo != null)
            {
                recipientAddressBlock.Append($"\n{recipient.AdditionalAdressInfo}");
            }

            recipientAddressBlock.Append($"\n{recipient.ZipCode} {recipient.CityName}");

            //gfx.DrawRectangle(XBrushes.Orange, RecipientAddressRect);

            tf.DrawString(
                recipientAddressBlock.ToString(), _normalFont, XBrushes.Black, RecipientAddressRect, XStringFormats.TopLeft);

        }

        private void DrawLetterBodyText(XGraphics gfx, SenderModel sender, LetterContentModel letterContent)
        {
            // Der LetterBodyText beinhaltet das Datum, die Betreffzeilen, die Anrede, den Brieftext, die Grußformel und
            // den Absender-Namen. Es hat einen Abstand von 8.4mm zum Empfänger-Anschriftenfeld und eine maximale Höhe bis
            // zum unteren Seitenrand mit einem unteren Seitenabstand von 20mm.

            string date = $"{sender.CityName}, den {DateTime.Now.Date.ToString("d. MMMM yyyy")}";

            XRect letterContentRect = new XRect(
                CreateXPointFromMillimetres(LeftMargin, HeaderMargin + 5.0 + 40.0 + 8.4),
                CreateXPointFromMillimetres(PageWidth - RightMargin, PageHeight - BottomMargin)
                );

            StringBuilder letterContentBlock = new StringBuilder();

            if (!string.IsNullOrEmpty(letterContent.TopicLineTwo))
            {
                letterContentBlock.Append('\n');
            }

            letterContentBlock.Append(
                $"\n\n\n\n\n\n{letterContent.Intro}" +
                $"\n\n{letterContent.TextBody}" +
                $"\n\n{letterContent.Outro}" +
                $"\n\n\n\n\n{sender.Name}"
                );

            XTextFormatter tf = new XTextFormatter(gfx);

            //gfx.DrawRectangle(XBrushes.Beige, letterContentRect);
            gfx.DrawString(
                date, _normalFont, XBrushes.Black, letterContentRect, XStringFormats.TopRight);

            tf.DrawString(
                $"\n\n\n{letterContent.TopicLineOne}", _boldFont, XBrushes.Black, letterContentRect, XStringFormats.TopLeft);

            if (!string.IsNullOrEmpty(letterContent.TopicLineTwo))
            {
                tf.DrawString(
                    $"\n\n\n\n{letterContent.TopicLineTwo}", _boldFont, XBrushes.Black, letterContentRect, XStringFormats.TopLeft);
            }

            tf.Alignment = XParagraphAlignment.Justify;
            tf.DrawString(
                letterContentBlock.ToString(), _normalFont, XBrushes.Black, letterContentRect, XStringFormats.TopLeft);
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
