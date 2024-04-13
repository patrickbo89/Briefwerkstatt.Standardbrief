using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BriefWerkstatt.ViewModels;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace BriefWerkstatt.Repository
{
    public class Repository
    {
        #region Constants
        // Seitengröße in Millimeter
        private const double PageWidth = 210.0;
        private const double PageHeight = 297.0;

        // Seitenrandabstände in Millimeter
        private const double LeftMargin = 25.0;
        private const double RightMargin = 20.0;
        private const double TopMargin = 45.0;
        private const double BottomMargin = 20.0;
        #endregion

        #region Fonts
        private readonly XFont _boldFont = new XFont("Verdana", 12, XFontStyleEx.Bold);
        private readonly XFont _normalFont = new XFont("Verdana", 12);
        private readonly XFont _windowEnvelopeLineFont = new XFont("Verdana", 8, XFontStyleEx.Underline);
        #endregion

        public void CreatePdfDocument(StandardLetterViewModel standardLetter, string saveFolderPath)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            DrawSenderBlock(gfx);
            DrawWindowEnvelopeAddress(gfx);
            DrawRecipientBlock(gfx);
            DrawCurrentDate(gfx);
            DrawTopicText(gfx);
            DrawIntroText(gfx);
            DrawLetterBodyText(gfx);
            DrawOutroText(gfx);

            DrawFoldingLines(gfx);
            DrawHolePunchGuide(gfx);

            SaveDocument(document, saveFolderPath, standardLetter);
            OpenDocumentInViewer(saveFolderPath, standardLetter);
        }

        private void OpenDocumentInViewer(string saveFolderPath, StandardLetterViewModel standardLetterModel)
        {
            // Open PDF with external viewer
            Process PdfViewer = new Process();
            PdfViewer.StartInfo.UseShellExecute = true;
            PdfViewer.StartInfo.WorkingDirectory = saveFolderPath;
            PdfViewer.StartInfo.FileName = standardLetterModel.FullFileName;
            PdfViewer.Start();
        }

        private void SaveDocument(PdfDocument document, string saveFolderPath, StandardLetterViewModel standardLetterModel)
        {
            document.Save($"{saveFolderPath}{standardLetterModel.FullFileName}");
        }

        private void DrawSenderBlock(XGraphics gfx)
        {

        }

        private void DrawWindowEnvelopeAddress(XGraphics gfx)
        {

        }

        private void DrawRecipientBlock(XGraphics gfx)
        {

        }

        private void DrawCurrentDate(XGraphics gfx)
        {

        }

        private void DrawTopicText(XGraphics gfx)
        {

        }

        private void DrawIntroText(XGraphics gfx)
        {

        }

        private void DrawLetterBodyText(XGraphics gfx)
        {

        }

        private void DrawOutroText(XGraphics gfx)
        {

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
