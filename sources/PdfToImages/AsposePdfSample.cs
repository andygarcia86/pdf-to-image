using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Facades;
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

        private static string GetPdfPagePreview(string folderName, Page pdfPage, bool isLandscape) {
            var imgUrl = _dataDir + "images\\" + folderName + "\\" + pdfPage.Number + ".jpeg";

            Directory.CreateDirectory(_dataDir + "images\\" + folderName);

            using (var imageStream = new FileStream(imgUrl, FileMode.Create))
            {
                // Create Resolution object
                var pageWidth = !isLandscape ? (int) pdfPage.PageInfo.Width : (int)pdfPage.PageInfo.Height;
                var pageHeight = !isLandscape ? (int) pdfPage.PageInfo.Height : (int)pdfPage.PageInfo.Width;
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
            var editor = new PdfPageEditor();
            editor.BindPdf(fileName);

            var guid = Guid.NewGuid().ToString();
            var pdfDocument = new Document(fileName);

            if (allPages)
            {
                foreach (var page in pdfDocument.Pages)
                {
                    PageSize size = editor.GetPageSize(page.Number);
                    var isLandscape = size.Width > size.Height;

                    GetPdfPagePreview(guid, page, isLandscape);
                }
            }
            else {
                PageSize size = editor.GetPageSize(pageNumber);
                var isLandscape = size.Width > size.Height;

                GetPdfPagePreview(guid, pdfDocument.Pages[pageNumber], isLandscape);
            }  

            return guid;
        }

        

    }
}
