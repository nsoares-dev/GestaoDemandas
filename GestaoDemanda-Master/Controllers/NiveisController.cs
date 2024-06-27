using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoDemanda_Master.Data;
using GestaoDemanda_Master.Models;

namespace GestaoDemanda_Master.Controllers
{
    public class NiveisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NiveisController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Nivel.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nivel = await _context.Nivel
                .FirstOrDefaultAsync(m => m.NivelId == id);
            if (nivel == null)
            {
                return NotFound();
            }

            return View(nivel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NivelId,NivelNome")] Nivel nivel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nivel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nivel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nivel = await _context.Nivel.FindAsync(id);
            if (nivel == null)
            {
                return NotFound();
            }
            return View(nivel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NivelId,NivelNome")] Nivel nivel)
        {
            if (id != nivel.NivelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nivel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NivelExists(nivel.NivelId))
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
            return View(nivel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nivel = await _context.Nivel
                .FirstOrDefaultAsync(m => m.NivelId == id);
            if (nivel == null)
            {
                return NotFound();
            }

            return View(nivel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nivel = await _context.Nivel.FindAsync(id);
            if (nivel != null)
            {
                _context.Nivel.Remove(nivel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NivelExists(int id)
        {
            return _context.Nivel.Any(e => e.NivelId == id);
        }
    }
}
