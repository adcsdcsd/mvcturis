using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Kempery.Data;
using Kempery.Models;
using TuProyecto.Helpers;

public class ContratoService
{
    private readonly BaseContext _context;
    private readonly PdfService _pdfService;
    private readonly IWebHostEnvironment _env;

    public ContratoService(BaseContext context, PdfService pdfService, IWebHostEnvironment env)
    {
        _context = context;
        _pdfService = pdfService;
        _env = env;
    }

    public async Task<string> GenerarContratoAsync(Usuario usuario, int[] RNacionalIds)
    {
        // Convertir imagen a base64
        string imagePath = Path.Combine(_env.WebRootPath, "images", "Empresa.png");
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        string imageSrc = $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";

        // Leer plantilla
        string baseTemplatePath = Path.Combine(_env.WebRootPath, "templates");
        string htmlParte1 = File.ReadAllText(Path.Combine(baseTemplatePath, "contrato.html"));

        // Reemplazo de etiquetas
        htmlParte1 = htmlParte1
            .Replace("{Contrato}", usuario.Contrato)
            .Replace("{Nombre}", string.Join(" ", usuario.Nombre.Split(' ').Select(p => $"<span class='subrayado-negrita'>{p}</span>")) + "&nbsp;")

            .Replace("{Apellido}", string.Join(" ", usuario.Apellido.Split(' ').Select(p => $"<span class='subrayado-negrita'>{p}</span>")))

            .Replace("{Cedula}", usuario.Cedula)
            .Replace("{Correo}", usuario.CorreoElectronico ?? "")
            .Replace("{Celular}", usuario.Celular ?? "")
            .Replace("{Ciudad}", usuario.Ciudad)
            .Replace("{Cotitular}", usuario.Cotitular ?? "")
            .Replace("{EstadoCivil}", usuario.EstadoCivil ?? "")
            .Replace("{FechaNacimiento}", usuario.FechaNacimiento.ToString("dd/MM/yyyy"))
            .Replace("{Noches}", usuario.Noches?.ToString() ?? "0")
            .Replace("{RInternacional}", usuario.RInternacional ?? "")
            .Replace("{Cuotas}", usuario.Cuotas?.ToString() ?? "0")
            .Replace("{Volumen}", usuario.Volumen?.ToString("0.00") ?? "0.00")
            .Replace("{Cash}", usuario.Cash?.ToString("0.00") ?? "0.00")
            .Replace("{Anio}", $"{NumeroHelper.NumeroALetras(usuario.Anio ?? 0)} ({usuario.Anio})")
            .Replace("{FechaActual}", DateTime.Now.ToString("dd/MM/yyyy"))
            .Replace("{imagen_empresa}", imageSrc)
            .Replace("{Clausula1}", EnfasisPorPalabra("CLÁUSULA PRIMERA. - EL OBJETO DEL PRESENTE CONTRATO:"))
            .Replace("{Clausula2}", EnfasisPorPalabra("CLÁUSULA SEGUNDA. - El AFILIADO:"))
            .Replace("{Clausula3}", EnfasisPorPalabra("CLÁUSULA TERCERA. - LA VIGENCIA DEL CONTRATO:"))
            .Replace("{Clausula4}", EnfasisPorPalabra("CLÁUSULA CUARTA. - BENEFICIOS DE LA SUSCRIPCIÓN"))
            .Replace("{Clausula5}", EnfasisPorPalabra("GARANTÍA DE MEJOR PRECIO NACIONAL E INTERNACIONAL"))
            .Replace("{SemanasI}", EnfasisPorPalabra("SEMANAS DE ALOJAMIENTO INTERNACIONAL"));



        string htmlCompleto = htmlParte1;

        // Generar PDF
        string nombreArchivo = $"Contrato_{usuario.Id}_{DateTime.Now.Ticks}.pdf";
        return _pdfService.GenerarContratoPdf(htmlCompleto, nombreArchivo);
    }

    // Método público que puedes llamar desde el controlador
    public string GenerarNumeroContratoPublico(string ciudad, int id)
    {
        return GenerarNumeroContrato(ciudad, id);
    }

    // Lógica privada de generación de contrato
    private string GenerarNumeroContrato(string ciudad, int id)
    {
        string prefijoFijo = "KMPRY";
        string siglasCiudad = ciudad.Length >= 3 ? ciudad.Substring(0, 3).ToUpper() : ciudad.ToUpper();
        string numeroIncremental = id.ToString("D4");
        return $"{prefijoFijo}-{siglasCiudad}-{numeroIncremental}";
    }




public static string EnfasisPorPalabra(string texto, string etiqueta = "strong")
{
    return string.Join(" ", texto.Split(' ').Select(p => $"<{etiqueta}>{p}</{etiqueta}>"));
}



}

/////////////////////////////////////////////////////////////////////////////////////////////////
