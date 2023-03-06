using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using Yuebon.Chaochi.Core.Models;

namespace Yuebon.Chaochi.Core.Helpers
{
    public class PdfMerger
    {
        private const float _imageLeftMargin = 10f;
        private const float _imageRightMargin = 10f;
        private const float _imageBottomMargin = 30f;
        private const float _imageTopMargin = 30f;

        // https://github.com/VahidN/iTextSharp.LGPLv2.Core
        // https://stackoverflow.com/a/6056801/3013479
        public byte[] Create(List<BLOB> blobs)
        {
            Document document = null;
            PdfCopy copy = null;

            using (var stream = new MemoryStream()) {

                try {
                    document = new Document();
                    copy = new PdfCopy(document, stream);

                    document.Open();

                    foreach (var blob in blobs) {
                        if (blob.ContentType.StartsWith("image/")) {
                            AddImage(copy, blob.Content);
                        } else if (blob.ContentType == "application/pdf") {
                            AddPdf(copy, blob.Content);
                        } else {
                            throw new ArgumentException($"Blob with ContentType {blob.ContentType} is not supported for merging.");
                        }
                    }
                } finally {
                    document?.Close();
                    copy?.Close();
                }

                return stream.ToArray();
            }
        }

        private static void AddPdf(PdfCopy copy, byte[] content)
        {
            PdfReader.AllowOpenWithFullPermissions = true;
            PdfReader reader = null;
            try {
                reader = new PdfReader(content);
                reader.RemoveUsageRights();

                // Grab each page from the PDF and copy it
                for (int i = 1; i <= reader.NumberOfPages; i++) {
                    var page = copy.GetImportedPage(reader, i);
                    copy.AddPage(page);
                }

                // A PDF can have a form; we copy it into the resulting pdf.
                // Example: https://web.archive.org/web/20210322125650/https://www.windjack.com/PDFSamples/ListPrograming_Part1_AcroForm.pdf 
                var form = reader.AcroForm;
                if (form != null) {
                    copy.CopyAcroForm(reader);
                }

            } finally {
                reader?.Close();
            }
        }

        private static void AddImage(PdfCopy copy, byte[] content)
        {
            // We have a little workaround to add images because we use PdfCopy which is only useful for COPYING and doesn't work for adding pages manually.
            // So we create a new PDF in memory containing the image, and then we copy that PDF into the resulting PDF.
            // https://stackoverflow.com/a/26111677/3013479
            Document document = null;
            PdfWriter writer = null;
            PdfReader reader = null;
            using (var stream = new MemoryStream()) {
                try {
                    document = new Document();
                    writer = PdfWriter.GetInstance(document, stream);

                    document.Open();
                    if (!document.NewPage()) {
                        throw new Exception("New page could not be created");
                    }

                    var image = Image.GetInstance(content);

                    // Make the image fit on the page
                    // https://stackoverflow.com/q/4932187/3013479
                    image.Alignment = Element.ALIGN_MIDDLE;
                    var pageWidth = copy.PageSize.Width - (_imageLeftMargin + _imageRightMargin);
                    var pageHeight = copy.PageSize.Height - (_imageBottomMargin + _imageTopMargin);
                    image.ScaleToFit(pageWidth, pageHeight);

                    if (!document.Add(image)) {
                        throw new Exception("Unable to add image to page");
                    }

                    // These are required for the PdfReader instantation to succeed
                    document.Close();
                    writer.Close();

                    reader = new PdfReader(stream.ToArray());

                    copy.AddPage(copy.GetImportedPage(reader, 1));
                } finally {
                    document?.Close();
                    reader?.Close();
                    writer?.Close();
                }
            }
        }

        public void AddPageNumberToPDF(string PDFPath, bool showPageOfPage)
        {
            byte[] Fbytes = File.ReadAllBytes(PDFPath);
            PdfReader.AllowOpenWithFullPermissions = true;
            PdfReader reader = new PdfReader(Fbytes);
            reader.RemoveUsageRights();
            int n = reader.NumberOfPages;

            using (var stream = new MemoryStream()) {

                PdfStamper stamper = new PdfStamper(reader, stream);
                for (int i = 1; i <= n; i++) {
                    ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_CENTER, new Phrase(i + "/" + n), 297f, 15f, 0);
                }

                stamper.Close();
                reader.Close();
                
                System.IO.File.WriteAllBytes(PDFPath, stream.ToArray());
            }
        }
    }
}
