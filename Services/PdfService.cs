using DinkToPdf;
using DinkToPdf.Contracts;
using System;
using System.IO;

public class PdfService
{
    private readonly IConverter _converter;

    public PdfService(IConverter converter) 
    {
        _converter = converter;
    }
public string GenerarContratoPdf(string html, string nombreArchivo)
{
    string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "contratos");

    if (!Directory.Exists(folderPath))
        Directory.CreateDirectory(folderPath);

    string outputPath = Path.Combine(folderPath, nombreArchivo);

    var doc = new HtmlToPdfDocument()
    {
        GlobalSettings = new GlobalSettings
        {
            PaperSize = PaperKind.A4,
            Orientation = Orientation.Portrait,
            Out = outputPath,
            Margins = new MarginSettings
            {
                Top = 55,    // ✅ Margen superior
                Bottom = 35,
                Left = 15,
                Right = 15
            }
        },

        // ✅ Esta parte es obligatoria: el contenido HTML que va a convertir
        Objects = {
            new ObjectSettings()
            {
                HtmlContent = html,
                WebSettings = { DefaultEncoding = "utf-8" }
            }
        }
    };

    _converter.Convert(doc);

    return "/contratos/" + nombreArchivo;
}
}
