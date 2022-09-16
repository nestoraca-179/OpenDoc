using System;
using System.Web;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
// using PDF = Spire.Pdf;

namespace SLO.Controllers
{
    public class PDFController
    {
        private const string DEMO_TEXT = "Evaluation Warning : The document was created with Spire.PDF for .NET.";

        public bool IsPDF(HttpPostedFile file)
        {
            return System.IO.Path.GetExtension(file.FileName).ToLower() == ".pdf";
        }

        public bool IsValidBL(string path)
        {
            bool is_valid = false;

            try
            {
                using (PdfReader reader = new PdfReader(path))
                {
                    PdfDocument doc = new PdfDocument(reader);
                    string text = PdfTextExtractor.GetTextFromPage(doc.GetFirstPage(), new SimpleTextExtractionStrategy());

                    if (text.Contains("BILL OF LADING"))
                        is_valid = true;
                }
            }
            catch (Exception) { }

            return is_valid;
        }

        public void ProcessPDF(string path)
        {
            /*
            ProcessResult res = new ProcessResult();
            int exitosos = 0, fallidos = 0;

            using (PdfReader reader = new PdfReader(path))
            {
                PdfDocument doc = new PdfDocument(reader);
                int number = doc.GetNumberOfPages();

                for (int i = 1; i <= number; i++)
                {
                    PdfPage page = doc.GetPage(i);

                    if (IsFirstPage(page))
                    {
                        Rectangle size = page.GetMediaBox();
                        float width = size.GetWidth(), height = size.GetHeight();

                        BL bl = new BL();

                        bl.BLNumber = GetText(width - 100, height - 80, 100, 30, page); // BL RECT
                        bl.Voyage = GetText(width - 100, height - 40, 100, 20, page); // VOYAGE RECT
                        bl.Shipper = GetText(0, height - 90, width / 2, 60, page); // SHIPPER RECT
                        bl.Consignee = GetText(0, height - 160, (width / 2) - 100, 60, page); // CONSIGNEE RECT
                        bl.Notify = GetText(0, height - 240, (width / 2) - 100, 70, page); // NOTIFY RECT
                        bl.Export = GetText(width / 2, height - 240, width / 2, 140, page); // EXPORT RECT
                        bl.Precarriage = GetText(0, height - 260, (width / 4) - 100, 10, page); // PRECARRIAGE RECT
                        bl.Place = GetText(width / 4, height - 260, (width / 4) - 100, 10, page); // PLACE RECT
                        bl.Freight = GetText(width / 2, height - 260, (width / 4) - 100, 10, page); // FREIGHT RECT
                        bl.Original = GetText((width * 3) / 4, height - 260, (width / 4) - 100, 10, page); // ORIGINAL RECT
                        bl.Vessel = GetText(0, height - 280, (width / 4) - 100, 10, page); // VESSEL RECT
                        bl.PortL = GetText(width / 4, height - 280, (width / 4) - 100, 10, page); // PORTL RECT
                        bl.PortD = GetText(width / 2, height - 280, (width / 4) - 100, 10, page); // PORTD RECT
                        bl.Final = GetText((width * 3) / 4, height - 280, (width / 4) - 100, 10, page); // FINAL RECT
                        bl.DocumentPage = i;

                        int result = BLController.Add(bl);

                        if (result == 1)
                            exitosos++;
                        else
                            fallidos++;
                    }
                }
            }

            #region DETAILS
            // DETAILS RECT
            // strategy = GetText(0, 50, 50, 50);

            // PDF.PdfDocument pdf = new PDF.PdfDocument(path);
            // PDF.PdfPageBase page1 = pdf.Pages[2];
            // PDF.Texts.PdfTextExtractOptions options = new PDF.Texts.PdfTextExtractOptions();
            // options.ExtractArea = new System.Drawing.RectangleF(0, 300, 100, 250);

            // string text = page1.ExtractText(options).Replace(DEMO_TEXT, "");

            // Lbl_Details.Visible = true;
            // Lbl_Details.Text = "Details: " + text;
            #endregion

            res.Succeded = exitosos;
            res.Failed = fallidos;
            res.Total = exitosos + fallidos;

            return res;
            */
        }

        private string GetText(float x, float y, float width, float height, PdfPage page)
        {
            Rectangle rect = new Rectangle(x, y, width, height);
            TextRegionEventFilter filter = new TextRegionEventFilter(rect);
            ITextExtractionStrategy strategy = new FilteredTextEventListener(new LocationTextExtractionStrategy(), filter);

            return PdfTextExtractor.GetTextFromPage(page, strategy);
        }

        private bool IsFirstPage(PdfPage page)
        {
            bool is_first = false;

            ITextExtractionStrategy strategy = new LocationTextExtractionStrategy();
            string text = PdfTextExtractor.GetTextFromPage(page, strategy);

            if (text.Contains("Sheet 1 of"))
                is_first = true;

            return is_first;
        }
    }
}