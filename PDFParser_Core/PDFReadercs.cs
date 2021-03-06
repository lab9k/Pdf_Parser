﻿using iTextSharp.text.pdf;
using System.IO;
using System.Text;
using iTextSharp.text.pdf.parser;

namespace PDFParser_Core
{
    public class PDFReadercs
    {
        public string GetPdfAsString(string path)
        {
            if (File.Exists(path))
            {
                FileStream stream = File.Open(path, FileMode.Open);
                return GetStreamAsString(stream);
            }
            return null;
        }

        public string GetStreamAsString(Stream stream)
        {
            StringBuilder text = new StringBuilder();

            PdfReader pdfReader = new PdfReader(stream);

            for (int page = 1; page <= pdfReader.NumberOfPages; page++)
            {
                SimpleTextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8,
                    Encoding.Default.GetBytes(currentText)));
                text.Append(currentText);
            }
            pdfReader.Close();

            return text.ToString();
        }
    }
}