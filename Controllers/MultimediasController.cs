using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kempery.Models;
using Kempery.Data;

namespace Multimedias.Controllers
{
    public class MultimediasController : Controller
    {
        private readonly BaseContext _context;

        public MultimediasController(BaseContext context)
        {
            _context = context;
        }

        // GET: Multimedia
        public async Task<IActionResult> Index()
        {
            return View(await _context.Multimedias.ToListAsync());
        }

        // GET: Multimedia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var multimedia = await _context.Multimedias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (multimedia == null)
            {
                return NotFound();
            }

            return View(multimedia);
        }

        // GET: Multimedia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Multimedia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Ubicacion,Link,LinkDetallado")] Multimedia multimedia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(multimedia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(multimedia);
        }

        // GET: Multimedia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var multimedia = await _context.Multimedias.FindAsync(id);
            if (multimedia == null)
            {
                return NotFound();
            }
            return View(multimedia);
        }

        // POST: Multimedia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Ubicacion,Link,LinkDetallado")] Multimedia multimedia)
        {
            if (id != multimedia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(multimedia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MultimediaExists(multimedia.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(multimedia);
        }

        // GET: Multimedia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var multimedia = await _context.Multimedias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (multimedia == null)
            {
                return NotFound();
            }

            return View(multimedia);
        }

        // POST: Multimedia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var multimedia = await _context.Multimedias.FindAsync(id);
            if (multimedia != null)
            {
                _context.Multimedias.Remove(multimedia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MultimediaExists(int id)
        {
            return _context.Multimedias.Any(e => e.Id == id);
        }
    }
}
