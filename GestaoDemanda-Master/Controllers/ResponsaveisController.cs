﻿using System;
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
    public class ResponsaveisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private string _filePath;

        public ResponsaveisController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _filePath = env.WebRootPath;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Path = _filePath;
            var applicationDbContext = _context.Responsavel.Include(r => r.Nivel);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsavel = await _context.Responsavel
                .Include(r => r.Nivel)
                .FirstOrDefaultAsync(m => m.ResponsavelId == id);
            if (responsavel == null)
            {
                return NotFound();
            }

            return View(responsavel);
        }

        public IActionResult Create()
        {
            ViewData["NivelId"] = new SelectList(_context.Set<Nivel>(), "NivelId", "NivelId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResponsavelId,NomeResponsavel,ApresentacaoResponsavel,NivelId,FotoResponsavel")] Responsavel responsavel, IFormFile anexo)
        {
            if (ModelState.IsValid)
            {
                if (!ValidaImagem(anexo))
                    return View(responsavel);

                var nome = SalvarArquivo(anexo);
                responsavel.FotoResponsavel = nome;

                _context.Add(responsavel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NivelId"] = new SelectList(_context.Set<Nivel>(), "NivelId", "NivelId", responsavel.NivelId);
            return View(responsavel);
        }

        public bool ValidaImagem(IFormFile anexo)
        {
            switch (anexo.ContentType)
            {
                case "image/jpeg":
                    return true;
                case "image/bmp":
                    return true;
                case "image/gif":
                    return true;
                case "image/png":
                    return true;
                default:
                    return false;
            }
        }
        public string SalvarArquivo(IFormFile anexo)
        {
            var nome = Guid.NewGuid().ToString() + anexo.FileName;

            var filePath = _filePath + "\\fotos";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            using (var stream = System.IO.File.Create(filePath + "\\" + nome))
            {
                anexo.CopyToAsync(stream);
            }

            return nome;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsavel = await _context.Responsavel.FindAsync(id);
            if (responsavel == null)
            {
                return NotFound();
            }
            ViewData["NivelId"] = new SelectList(_context.Set<Nivel>(), "NivelId", "NivelId", responsavel.NivelId);
            return View(responsavel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResponsavelId,NomeResponsavel,ApresentacaoResponsavel,NivelId,FotoResponsavel")] Responsavel responsavel)
        {
            if (id != responsavel.ResponsavelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(responsavel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResponsavelExists(responsavel.ResponsavelId))
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
            ViewData["NivelId"] = new SelectList(_context.Set<Nivel>(), "NivelId", "NivelId", responsavel.NivelId);
            return View(responsavel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsavel = await _context.Responsavel
                .Include(r => r.Nivel)
                .FirstOrDefaultAsync(m => m.ResponsavelId == id);
            if (responsavel == null)
            {
                return NotFound();
            }

            return View(responsavel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var responsavel = await _context.Responsavel.FindAsync(id);
            string filePathName = _filePath + "\\fotos\\" + responsavel.FotoResponsavel;

            if (System.IO.File.Exists(filePathName))
                System.IO.File.Delete(filePathName);

            _context.Responsavel.Remove(responsavel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResponsavelExists(int id)
        {
            return _context.Responsavel.Any(e => e.ResponsavelId == id);
        }
    }
}
