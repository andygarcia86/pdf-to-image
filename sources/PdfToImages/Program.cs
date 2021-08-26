using System;

namespace PdfToImages
{
    class Program
    {
        static void Main(string[] args)
        {
            string _dataDir = System.IO.Path.GetFullPath(@"..\..\..\");
            string pdfFileName = _dataDir + "pdf-samples/multi-pages-portrait.pdf";

            var pages = AsposePdfSample.GetPdfTotalPages(pdfFileName);

            var pageNumber = 1;
            var allPages = true;

            var imgUrl = AsposePdfSample.ConvertPDFtoImage(pdfFileName, pageNumber, allPages);

        }
    }
}
