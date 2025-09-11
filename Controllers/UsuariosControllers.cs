using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kempery.Models; // Asegúrate de que este namespace coincida con el de tu modelo
using Kempery.Data;
using TuProyecto.Helpers;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;

using System;

namespace Usuarios.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly BaseContext _context;
        private readonly PdfService _pdfService;
        private readonly IWebHostEnvironment _env; // ✅ Agregado
        private readonly ContratoService _contratoService;

        public UsuariosController(BaseContext context, PdfService pdfService, IWebHostEnvironment env, ContratoService contratoService)
        {
            _context = context;
            _pdfService = pdfService;
            _env = env; // ✅ Guardado
            _contratoService = contratoService;
        }

        // ... aquí va tu método Create y demás


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: /Usuarios
        public async Task<IActionResult> Index(string? search)
        {
            var usuarios = from u in _context.Usuarios
                           select u;

            if (!string.IsNullOrWhiteSpace(search))
            {
                usuarios = usuarios.Where(u =>
                    u.Nombre.Contains(search) ||
                    u.Apellido.Contains(search) ||
                    u.Cedula.Contains(search) ||
                    u.CorreoElectronico.Contains(search) ||
                    u.Ciudad.Contains(search));
            }

            return View(await usuarios.ToListAsync());
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: /Usuarios/Details/5
      public async Task<IActionResult> Details(int? id)
{
    if (id == null)
        return NotFound();

    var usuario = await _context.Usuarios
        .FirstOrDefaultAsync(u => u.Id == id);

    if (usuario == null)
        return NotFound();


    return View(usuario);
}

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        // POST: /Usuarios/Create
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(
    [Bind("Cedula,Nombre,Apellido,CorreoElectronico,Celular,Cotitular,FechaNacimiento,Ciudad,EstadoCivil,Foto,Password,Noches,RInternacional,Cuotas,Volumen,Cash,Anio")]
    Usuario usuario,
    int[] RNacionalIds)
{
    if (!ModelState.IsValid)
    {
        var usuarios = await _context.Usuarios.ToListAsync();
        return View("Index", usuarios);
    }

    

    // ✅ Guardar usuario y generar contrato
    _context.Usuarios.Add(usuario);
    await _context.SaveChangesAsync();

    // ✅ Generar número de contrato
    usuario.Contrato = _contratoService.GenerarNumeroContratoPublico(usuario.Ciudad, usuario.Id);
    _context.Usuarios.Update(usuario);
    await _context.SaveChangesAsync();

    // ✅ Generar contrato y PDF desde el servicio
    string contratoUrl = await _contratoService.GenerarContratoAsync(usuario, RNacionalIds);

    // ✅ Enviar PDF a la vista
    TempData["ContratoUrl"] = contratoUrl;
    return RedirectToAction(nameof(Index));
}


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GET: /Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.Usuarios.FindAsync(id);
            if (user == null) return NotFound();

            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        ///////////////////////////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Cedula,Nombre,Apellido,CorreoElectronico,Celular,Cotitular,FechaNacimiento,Ciudad,EstadoCivil,Foto,Password,Noches,RInternacional,Cuotas,Volumen,Cash,Anio")] Usuario usuario, int[] RNacionalIds)
        {
            if (id == null || id != usuario.Id)
                return NotFound();

            var usuarioOriginal = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuarioOriginal == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Actualizar propiedades
                    usuarioOriginal.Cedula = usuario.Cedula;
                    usuarioOriginal.Nombre = usuario.Nombre;
                    usuarioOriginal.Apellido = usuario.Apellido;
                    usuarioOriginal.CorreoElectronico = usuario.CorreoElectronico;
                    usuarioOriginal.Celular = usuario.Celular;
                    usuarioOriginal.Cotitular = usuario.Cotitular;
                    usuarioOriginal.FechaNacimiento = usuario.FechaNacimiento;
                    usuarioOriginal.Ciudad = usuario.Ciudad;
                    usuarioOriginal.EstadoCivil = usuario.EstadoCivil;
                    usuarioOriginal.Foto = usuario.Foto;
                    usuarioOriginal.Password = string.IsNullOrWhiteSpace(usuario.Password)
                        ? usuarioOriginal.Password
                        : usuario.Password;
                    usuarioOriginal.Noches = usuario.Noches;
                    usuarioOriginal.RInternacional = usuario.RInternacional;
                    usuarioOriginal.Cuotas = usuario.Cuotas;
                    usuarioOriginal.Volumen = usuario.Volumen;
                    usuarioOriginal.Cash = usuario.Cash;
                    usuarioOriginal.Anio = usuario.Anio;

                    
                    

                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "✅ Usuario modificado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Usuarios.Any(e => e.Id == usuario.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            var usuarioConRelaciones = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == usuario.Id);

            return View(usuarioConRelaciones);
        }
    }
}