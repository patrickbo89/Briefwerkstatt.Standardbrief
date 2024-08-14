using BriefWerkstatt.Models;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Shapes;

namespace BriefWerkstatt.Repository
{
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
        private const double TopMarginFirstPage = 15.0;
        private const double TopMarginAdditionalPage = 25.0;
        private const double BottomMargin = 20.0;
        private const double BottomMarginPageNumber = 10.0;
        private const double HeaderMargin = 45.0;

        // Größen einzelner Bereiche in Millimeter
        private const double WindowSenderLineWidth = 85.0;
        private const double WindowSenderLineHeight = 7.5;

        private const double RecipientBlockWidth = 85.0;
        private const double RecipientBlockHeight = 40.0;

        // Abstände zwischen einzelnen Bereichen in Millimeter
        private const double MarginToWindowSenderLine = 5.0;
        private const double MarginToRecipientAddressBlock = 8.4;
        #endregion

        #region Fonts
        private readonly XFont _senderNameFont = new XFont("Arial", 12, XFontStyleEx.Bold);
        private readonly XFont _normalFont = new XFont("Arial", 11, XFontStyleEx.Regular);
        private readonly XFont _topicFont = new XFont("Arial", 11, XFontStyleEx.Bold);
        private readonly XFont _windowEnvelopeLineFont = new XFont("Arial", 6, XFontStyleEx.Regular);
        private readonly XFont _pageCountFont = new XFont("Arial", 9, XFontStyleEx.Regular);
        #endregion

        public bool CreatePdfDocument(StandardLetterModel letterModel, string filePath)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            DrawSenderBlock(gfx, letterModel);
            DrawWindowEnvelopeAddress(gfx, letterModel);
            DrawRecipientBlock(gfx, letterModel);
            DrawLetterBodyText(gfx, document, letterModel);

            DrawFoldingLines(gfx);
            DrawHolePunchGuide(gfx);

            try
            {
                SaveDocument(document, filePath);
            }
            catch (IOException e)
            {
                Debug.WriteLine(e.ToString());
                MessageBox.Show("Der angegebene Speicherpfad konnte nicht gefunden werden. Das Dokument wurde NICHT gespeichert!\n\n" +
                    "Wenn sich der Speicherpfad auf einem externen Server befindet, könnte die Verbindung zum Server abgebrochen sein.\n\n" +
                    "Bitte überprüfen Sie in diesem Falle die Verbindung und versuchen Sie es erneut.",
                    "Speichern fehlgeschlagen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (PdfSharpException e)
            {
                Debug.WriteLine(e.ToString());
                MessageBox.Show("Der angegebene Speicherpfad konnte nicht gefunden werden. Das Dokument wurde NICHT gespeichert!\n\n" +
                    "Wenn sich der Speicherpfad auf einem externen Server befindet, könnte die Verbindung zum Server abgebrochen sein.\n\n" +
                    "Bitte überprüfen Sie in diesem Falle die Verbindung und versuchen Sie es erneut.",
                    "Speichern fehlgeschlagen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }



            try
            {
                OpenDocumentInViewer(filePath, letterModel);
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                Debug.WriteLine(e.ToString());
                MessageBox.Show("Das Dokument wurde erfolgreich gespeichert, aber beim Öffnen der Datei konnte der Dateipfad nicht gefunden werden.\n\n" +
                    "Wenn sich der Dateipfad auf einem externen Server befindet, könnte die Verbindung zum Server abgebrochen sein.\n\n" +
                    "Bitte überprüfen Sie in diesem Falle die Verbindung und öffnen Sie die Datei manuell.",
                    "Gespeichertes Dokument konnte nicht geöffnet werden", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            // Saving the document was successful.
            return true;
        }

        private void OpenDocumentInViewer(string filePath, StandardLetterModel letterModel)
        {
            // Get the file directory path without the file name
            FileInfo fileInfo = new FileInfo(filePath);
            string? directory = fileInfo.DirectoryName;

            // Open PDF with external viewer
            Process PdfViewer = new Process();
            try
            {
                PdfViewer.StartInfo.UseShellExecute = true;
                PdfViewer.StartInfo.WorkingDirectory = directory;
                PdfViewer.StartInfo.FileName = fileInfo.Name;
                PdfViewer.Start();
            }
            finally
            {
                PdfViewer.Close();
            }
        }

        private void SaveDocument(PdfDocument document, string filePath)
        {
            {
                FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
                Debug.WriteLine(stream.CanWrite);

                try
                {
                    document.Save(stream, closeStream: true);
                }
                finally
                {
                    Debug.WriteLine(stream.CanWrite);
                    stream.Close();
                    Debug.WriteLine(stream.CanWrite);
                }
            }
        }

        private void DrawSenderBlock(XGraphics gfx, StandardLetterModel letterModel)
        {
            // Der Absender-Bereich befindet sich im 45 mm hohen Briefkopf, linksbündig mit einem oberen Seitenabstand von 15 mm
            XRect HeaderRect = new XRect(
                CreateXPointFromMillimetres(LeftMargin, TopMarginFirstPage),
                CreateXPointFromMillimetres(PageWidth - RightMargin, HeaderMargin));

            XTextFormatter tf = new XTextFormatter(gfx);

            StringBuilder senderAddressBlock = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(letterModel.SenderAdditionalInfoOne))
            {
                senderAddressBlock.Append($"\n{letterModel.SenderAdditionalInfoOne}");
            }

            if (!string.IsNullOrWhiteSpace(letterModel.SenderAdditionalInfoTwo))
            {
                senderAddressBlock.Append($"\n{letterModel.SenderAdditionalInfoTwo}");
            }

            senderAddressBlock.Append($"\n{letterModel.SenderStreetAndNumber}");

            senderAddressBlock.Append($"\n{letterModel.SenderZipCodeAndCity}");

            tf.DrawString(letterModel.SenderName, _senderNameFont, XBrushes.Black, HeaderRect, XStringFormats.TopLeft);
            tf.DrawString(senderAddressBlock.ToString(), _normalFont, XBrushes.Black, HeaderRect, XStringFormats.TopLeft);
        }

        private void DrawWindowEnvelopeAddress(XGraphics gfx, StandardLetterModel letterModel)
        {
            // Die Fensterkuvertzeile befindet sich 45mm von der oberen Blattkante, ist 85mm breit und 7.5mm hoch
            XRect windowTextLineRect = new XRect(
                CreateXPointFromMillimetres(LeftMargin, HeaderMargin),
                CreateXPointFromMillimetres(LeftMargin + WindowSenderLineWidth, HeaderMargin + WindowSenderLineHeight)
                );

            XTextFormatter tf = new XTextFormatter(gfx);
            StringBuilder windowTextLine = new StringBuilder();

            bool hasSenderCareOfInfo = !string.IsNullOrWhiteSpace(letterModel.SenderAdditionalInfoOne);
            bool hasSenderAdditionalInfo = !string.IsNullOrWhiteSpace(letterModel.SenderAdditionalInfoTwo);

            if (!hasSenderCareOfInfo && !hasSenderAdditionalInfo)
            {
                windowTextLine.Append($"\n\n{letterModel.SenderName}");
            }
            else
            {
                windowTextLine.Append($"\n{letterModel.SenderName}");
            }

            windowTextLine.Append(
                $", {letterModel.SenderStreetAndNumber}" +
                $", {letterModel.SenderZipCodeAndCity}"
            );

            if (hasSenderCareOfInfo && !hasSenderAdditionalInfo)
            {
                windowTextLine.Append($"\n{letterModel.SenderAdditionalInfoOne}");
            }
            else if (!hasSenderCareOfInfo && hasSenderAdditionalInfo)
            {
                windowTextLine.Append($"\n{letterModel.SenderAdditionalInfoTwo}");
            }
            else if (hasSenderCareOfInfo && hasSenderAdditionalInfo)
            {
                windowTextLine.Append($"\n{letterModel.SenderAdditionalInfoOne}, {letterModel.SenderAdditionalInfoTwo}");
            }

            tf.DrawString(
                windowTextLine.ToString(), _windowEnvelopeLineFont, XBrushes.Black, windowTextLineRect, XStringFormats.TopLeft);
        }

        private void DrawRecipientBlock(XGraphics gfx, StandardLetterModel letterModel)
        {
            // Das Empfänger-Anschriftenfeld hat eine Breite von 85mm und eine Höhe von 40mm
            XRect RecipientAddressRect = new XRect(
                CreateXPointFromMillimetres(LeftMargin, HeaderMargin + MarginToWindowSenderLine),
                CreateXPointFromMillimetres(LeftMargin + RecipientBlockWidth, HeaderMargin + MarginToWindowSenderLine + RecipientBlockHeight)
                );

            XTextFormatter tf = new XTextFormatter(gfx);

            StringBuilder recipientAddressBlock = new StringBuilder();

            recipientAddressBlock.Append($"\n{letterModel.RecipientName}");

            if (!string.IsNullOrWhiteSpace(letterModel.RecipientAdditionalInfoOne))
            {
                recipientAddressBlock.Append($"\n{letterModel.RecipientAdditionalInfoOne}");
            }

            if (!string.IsNullOrWhiteSpace(letterModel.RecipientAdditionalInfoTwo))
            {
                recipientAddressBlock.Append($"\n{letterModel.RecipientAdditionalInfoTwo}");
            }

            if (!string.IsNullOrWhiteSpace(letterModel.RecipientStreetAndNumber))
            {
                recipientAddressBlock.Append($"\n{letterModel.RecipientStreetAndNumber}");
            }

            recipientAddressBlock.Append($"\n{letterModel.RecipientZipCodeAndCity}");

            tf.DrawString(
                recipientAddressBlock.ToString(), _normalFont, XBrushes.Black, RecipientAddressRect, XStringFormats.TopLeft);

        }

        private void DrawLetterBodyText(XGraphics gfx, PdfDocument document, StandardLetterModel letterModel)
        {
            // Der LetterBodyText beinhaltet das Datum, die Betreffzeilen, die Anrede, den Brieftext, die Grußformel und
            // den Absender-Namen. Es hat einen Abstand von 8.4mm zum Empfänger-Anschriftenfeld und eine maximale Höhe bis
            // zum unteren Seitenrand mit einem unteren Seitenabstand von 20mm.

            string cityName = ExtractCityName(letterModel);

            string date = $"{cityName.TrimEnd()}, {DateTime.Now.Date.ToString("dd.MM.yyyy")}";

            XRect letterContentFirstPageRect = new XRect(
                CreateXPointFromMillimetres(LeftMargin, HeaderMargin + MarginToWindowSenderLine + RecipientBlockHeight + MarginToRecipientAddressBlock),
                CreateXPointFromMillimetres(PageWidth - RightMargin, PageHeight - BottomMargin)
                );

            XRect letterContentNextPageRect = new XRect(
                CreateXPointFromMillimetres(LeftMargin, TopMarginAdditionalPage),
                CreateXPointFromMillimetres(PageWidth - RightMargin, PageHeight - BottomMargin)
                );

            StringBuilder letterContentCurrentPageBlock = new StringBuilder();

            XTextFormatterEx2 tf = new XTextFormatterEx2(gfx);

            if (!string.IsNullOrWhiteSpace(letterModel.TopicLineTwo))
            {
                letterContentCurrentPageBlock.Append('\n');
            }

            letterContentCurrentPageBlock.Append(
                $"\n\n\n\n\n\n{letterModel.Intro}" +
                $"\n\n{letterModel.Content}" +
                $"\n\n{letterModel.Outro}" +
                $"\n\n\n\n\n{letterModel.SenderName}"
                );

            gfx.DrawString(
                date, _normalFont, XBrushes.Black, letterContentFirstPageRect, XStringFormats.TopRight);

            tf.DrawString(
                $"\n\n\n{letterModel.TopicLineOne}", _topicFont, XBrushes.Black, letterContentFirstPageRect, XStringFormats.TopLeft);

            if (!string.IsNullOrWhiteSpace(letterModel.TopicLineTwo))
            {
                tf.DrawString(
                    $"\n\n\n\n{letterModel.TopicLineTwo}", _topicFont, XBrushes.Black, letterContentFirstPageRect, XStringFormats.TopLeft);
            }

            tf.Alignment = XParagraphAlignment.Justify;
            tf.DrawString(
                letterContentCurrentPageBlock.ToString(), _normalFont, XBrushes.Black, letterContentFirstPageRect, XStringFormats.TopLeft);

            bool hasNextPage = HasNextPage(tf, letterContentCurrentPageBlock, letterContentFirstPageRect, out int lastCharIndex);

            while (hasNextPage)
            {
                string nextPageText = letterContentCurrentPageBlock.ToString().Substring(lastCharIndex + 1).TrimStart();
                letterContentCurrentPageBlock.Clear();
                letterContentCurrentPageBlock.Append(nextPageText);

                PdfPage nextPage = document.AddPage();
                gfx = XGraphics.FromPdfPage(nextPage);
                tf = new XTextFormatterEx2(gfx);
                tf.Alignment = XParagraphAlignment.Justify;

                tf.DrawString(
                    letterContentCurrentPageBlock.ToString(), _normalFont, XBrushes.Black, letterContentNextPageRect, XStringFormats.TopLeft);

                hasNextPage = HasNextPage(tf, letterContentCurrentPageBlock, letterContentNextPageRect, out lastCharIndex);

                DrawPageNumber(gfx, document.PageCount);
                DrawFoldingLines(gfx);
                DrawHolePunchGuide(gfx);
            }
        }

        private string ExtractCityName(StandardLetterModel letterModel)
        {
            string cityName = string.Empty;
            string[] parts = letterModel.SenderZipCodeAndCity.Split(' ');

            if (parts.Length >= 2)
            {
                for (int index = 0; index < parts.Length; index++)
                {
                    if (index != 0)
                    {
                        cityName += parts[index] + " ";
                    }
                }
            }
            else
            {
                cityName = parts[0];
            }

            return cityName;
        }

        private void DrawPageNumber(XGraphics gfx, int pageCount)
        {
            XRect pageCountRect = new XRect(
                CreateXPointFromMillimetres(LeftMargin, PageHeight - BottomMarginPageNumber),
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
