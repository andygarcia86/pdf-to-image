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

        public static string ConverstPDFtoImage(string fileName, int pageNumber = 1, bool allPages = false)
        {
            var imgUrl = "D:/andy/projects/pdf-to-image/images/image_out.jpeg";

            // Open document
            var pdfDocument = new Document(fileName);

            if (allPages)
            {
                double imgWidth = 0;
                double imgHeight = 0;

                foreach (var page in pdfDocument.Pages)
                {
                    imgWidth = page.PageInfo.Width > imgWidth ? page.PageInfo.Width : imgWidth;
                    imgHeight += page.PageInfo.Height;
                }

                Console.WriteLine("imgWidth: " + imgWidth);
                Console.WriteLine("imgHeight: " + imgHeight);

                using (var imageStream = File.Create(imgUrl))
                {
                    // Create Resolution object
                    var resolution = new Resolution(300);

                    for (int pageCount = 1; pageCount <= pdfDocument.Pages.Count; pageCount++)
                    {
                        using (var imagePageStream = new MemoryStream())
                        {
                            var pWidth = (int)pdfDocument.Pages[pageCount].PageInfo.Width;
                            var pHeight = (int)pdfDocument.Pages[pageCount].PageInfo.Height;

                            JpegDevice JpegDevice = new JpegDevice(pWidth, pHeight, resolution);
                            JpegDevice.Process(pdfDocument.Pages[pageCount], imagePageStream);
                            imagePageStream.CopyTo(imageStream);
                        }
                    }

                    // Close stream
                    imageStream.Close();
                }
            }
            else
            {
                using (var imageStream = new FileStream(imgUrl, FileMode.Create))
                {
                    // Create Resolution object
                    var resolution = new Resolution(300);

                    // Create Jpeg device with specified attributes
                    // Width, Height, Resolution
                    JpegDevice JpegDevice = new JpegDevice(500, 700, resolution);



                    // Convert a particular page and save the image to stream
                    JpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);

                    //imageStream.CopyTo

                    // Close stream
                    imageStream.Close();
                }
            }            

            return imgUrl;
        }

        

    }
}
