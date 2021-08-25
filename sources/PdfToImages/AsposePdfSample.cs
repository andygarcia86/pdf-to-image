using Aspose.Pdf;
using Aspose.Pdf.Devices;
using System;
using System.IO;

namespace PdfToImages
{
    public static class AsposePdfSample
    {
        public static int GetPdfTotalPages(string fileName)
        {
            Document pdfDocument = new Document(fileName);
            return pdfDocument.Pages.Count;
        }

        private static string getPdfPagePreview(String folderName, Page pdfPage) {
            var imgUrl = "C:/pdfs/pdf-to-image/"+ folderName + "/ " + pdfPage.Number + ".jpeg";
            using (var imageStream = new FileStream(imgUrl, FileMode.Create))
            {
                // Create Resolution object
                var pageWidth = (int) pdfPage.PageInfo.Width;
                var pageHeight = (int)pdfPage.PageInfo.Height;
                var resolution = new Resolution(300);

                // Create Jpeg device with specified attributes
                // Width, Height, Resolution
                JpegDevice JpegDevice = new JpegDevice(pageWidth, pageHeight, resolution);

                // Convert a particular page and save the image to stream
                JpegDevice.Process(pdfPage, imageStream);

                //imageStream.CopyTo

                // Close stream
                imageStream.Close();
            }
            return imgUrl;
        }

        public static string ConverstPDFtoImage(string fileName, int pageNumber = 1, bool allPages = false)
        {
            Guid g = Guid.NewGuid();
            // Open document
            var pdfDocument = new Document(fileName);

            if (allPages)
            {
                {
                    for (int pageCount = 1; pageCount <= pdfDocument.Pages.Count; pageCount++)
                    {
                        getPdfPagePreview(g.ToString(), pdfDocument.Pages[pageCount]);
                    }
                }
            }
            else {
                getPdfPagePreview(g.ToString(), pdfDocument.Pages[pageNumber]);
            }  

            return "";
        }

        

    }
}
