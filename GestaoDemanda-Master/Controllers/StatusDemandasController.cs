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
    public class StatusDemandasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatusDemandasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.StatusDemanda.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusDemanda = await _context.StatusDemanda
                .FirstOrDefaultAsync(m => m.StatusDemandaId == id);
            if (statusDemanda == null)
            {
                return NotFound();
            }

            return View(statusDemanda);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusDemandaId,StatusNome,StatusDescricao")] StatusDemanda statusDemanda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statusDemanda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusDemanda);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusDemanda = await _context.StatusDemanda.FindAsync(id);
            if (statusDemanda == null)
            {
                return NotFound();
            }
            return View(statusDemanda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatusDemandaId,StatusNome,StatusDescricao")] StatusDemanda statusDemanda)
        {
            if (id != statusDemanda.StatusDemandaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusDemanda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusDemandaExists(statusDemanda.StatusDemandaId))
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
            return View(statusDemanda);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusDemanda = await _context.StatusDemanda
                .FirstOrDefaultAsync(m => m.StatusDemandaId == id);
            if (statusDemanda == null)
            {
                return NotFound();
            }

            return View(statusDemanda);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statusDemanda = await _context.StatusDemanda.FindAsync(id);
            if (statusDemanda != null)
            {
                _context.StatusDemanda.Remove(statusDemanda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusDemandaExists(int id)
        {
            return _context.StatusDemanda.Any(e => e.StatusDemandaId == id);
        }
    }
}
