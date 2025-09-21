using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kempery.Models;
using Kempery.Data;


namespace Kempery.Controllers
{
    public class ReservasCopropiedadesController : Controller
    {
        private readonly BaseContext _context;

        public ReservasCopropiedadesController(BaseContext context)
        {
            _context = context;
        }

        // GET: ReservasCopropiedades
        public async Task<IActionResult> Index()
        {
            // Incluimos las tablas relacionadas para mostrar datos de usuario y copropiedad
            var reservas = _context.ReservasCopropiedades
                                   .Include(r => r.Usuario)
                                   .Include(r => r.Copropiedad);
            return View(await reservas.ToListAsync());
        }

        // GET: ReservasCopropiedades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var reserva = await _context.ReservasCopropiedades
                                        .Include(r => r.Usuario)
                                        .Include(r => r.Copropiedad)
                                        .FirstOrDefaultAsync(m => m.Id == id);

            if (reserva == null) return NotFound();

            return View(reserva);
        }

        // GET: ReservasCopropiedades/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            ViewData["CopropiedadId"] = new SelectList(_context.Copropiedades, "Id", "Ubicacion");
            return View();
        }

        // POST: ReservasCopropiedades/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,CopropiedadId,FechaEntrada,FechaSalida,HabSencillas,HabDobles,HabTriples,CostoTotal")] ReservaCopropiedad reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Si hay error, volvemos a llenar los SelectList
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", reserva.UsuarioId);
            ViewData["CopropiedadId"] = new SelectList(_context.Copropiedades, "Id", "Ubicacion", reserva.CopropiedadId);
            return View(reserva);
        }

        // GET: ReservasCopropiedades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var reserva = await _context.ReservasCopropiedades.FindAsync(id);
            if (reserva == null) return NotFound();

            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", reserva.UsuarioId);
            ViewData["CopropiedadId"] = new SelectList(_context.Copropiedades, "Id", "Ubicacion", reserva.CopropiedadId);
            return View(reserva);
        }

        // POST: ReservasCopropiedades/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,CopropiedadId,FechaEntrada,FechaSalida,HabSencillas,HabDobles,HabTriples,CostoTotal")] ReservaCopropiedad reserva)
        {
            if (id != reserva.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", reserva.UsuarioId);
            ViewData["CopropiedadId"] = new SelectList(_context.Copropiedades, "Id", "Ubicacion", reserva.CopropiedadId);
            return View(reserva);
        }

        // GET: ReservasCopropiedades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var reserva = await _context.ReservasCopropiedades
                                        .Include(r => r.Usuario)
                                        .Include(r => r.Copropiedad)
                                        .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null) return NotFound();

            return View(reserva);
        }

        // POST: ReservasCopropiedades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.ReservasCopropiedades.FindAsync(id);
            _context.ReservasCopropiedades.Remove(reserva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.ReservasCopropiedades.Any(e => e.Id == id);
        }
    }
}
