﻿using System;

namespace PdfToImages
{
    class Program
    {
        static void Main(string[] args)
        {
            string _dataDir = "D:/andy/projects/pdf-to-image/";
            string pdfFileName = _dataDir + "pdf-samples/multi-pages-portrait.pdf";

            var pages = AsposePdfSample.GetPdfTotalPages(pdfFileName);

            var pageNumber = 1;
            var allPages = true;

            var imgUrl = AsposePdfSample.ConverstPDFtoImage(pdfFileName, pageNumber, allPages);
        }
    }
}
