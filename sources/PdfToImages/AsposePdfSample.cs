using Aspose.Pdf;
using Aspose.Pdf.Devices;
using System;
using System.IO;

namespace PdfToImages
{
    public static class AsposePdfSample
    {
        private static string _dataDir = System.IO.Path.GetFullPath(@"..\..\..\");

        public static int GetPdfTotalPages(string fileName)
        {
            Document pdfDocument = new Document(fileName);
            return pdfDocument.Pages.Count;
        }

        private static string GetPdfPagePreview(string folderName, Page pdfPage) {
            var imgUrl = _dataDir + "images\\" + folderName + "\\" + pdfPage.Number + ".jpeg";

            Directory.CreateDirectory(_dataDir + "images\\" + folderName);

            using (var imageStream = new FileStream(imgUrl, FileMode.Create))
            {
                // Create Resolution object
                var pageWidth = (int) pdfPage.PageInfo.Width;
                var pageHeight = (int) pdfPage.PageInfo.Height;
                var resolution = new Resolution(300);

                // Create Jpeg device with specified attributes (Width, Height, Resolution)
                JpegDevice JpegDevice = new JpegDevice(pageWidth, pageHeight, resolution);

                // Convert a particular page and save the image to stream
                JpegDevice.Process(pdfPage, imageStream);

                imageStream.Close();
            }
            return imgUrl;
        }

        public static string ConvertPDFtoImage(string fileName, int pageNumber = 1, bool allPages = false)
        {
            var guid = Guid.NewGuid().ToString();
            var pdfDocument = new Document(fileName);

            if (allPages)
            {
                foreach (var page in pdfDocument.Pages)
                {
                    GetPdfPagePreview(guid, page);
                }
            }
            else {
                GetPdfPagePreview(guid, pdfDocument.Pages[pageNumber]);
            }  

            return guid;
        }

        

    }
}
