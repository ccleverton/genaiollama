using GenAI.Api.Application.Interfaces;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System.Text;

namespace GenAI.Api.Infrastructure.Parsers;

public class TextParser 
{
    public string ExtractText(Stream fileStream)
    {
        using var reader = new PdfReader(fileStream);
        using var pdf = new PdfDocument(reader);

        var text = new StringBuilder();

        for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
        {
            var page = pdf.GetPage(i);
            text.Append(PdfTextExtractor.GetTextFromPage(page));
        }

        return text.ToString();
    }
}