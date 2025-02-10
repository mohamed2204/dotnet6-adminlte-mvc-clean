﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminLTE.MVC.Data;
using AdminLTE.MVC.Models;

namespace AdminLTE.MVC.Controllers
{
    public class MatieresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatieresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Matieres
        public async Task<IActionResult> Index()
        {
              return View(await _context.Matieres.ToListAsync());
        }

        // GET: Matieres/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Matieres == null)
            {
                return NotFound();
            }

            var matiere = await _context.Matieres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matiere == null)
            {
                return NotFound();
            }

            return View(matiere);
        }

        // GET: Matieres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Matieres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Matiere matiere)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matiere);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(matiere);
        }

        // GET: Matieres/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Matieres == null)
            {
                return NotFound();
            }

            var matiere = await _context.Matieres.FindAsync(id);
            if (matiere == null)
            {
                return NotFound();
            }
            return View(matiere);
        }

        // POST: Matieres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] Matiere matiere)
        {
            if (id != matiere.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matiere);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatiereExists(matiere.Id))
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
            return View(matiere);
        }

        // GET: Matieres/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Matieres == null)
            {
                return NotFound();
            }

            var matiere = await _context.Matieres
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matiere == null)
            {
                return NotFound();
            }

            return View(matiere);
        }

        // POST: Matieres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Matieres == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Matieres'  is null.");
            }
            var matiere = await _context.Matieres.FindAsync(id);
            if (matiere != null)
            {
                _context.Matieres.Remove(matiere);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatiereExists(long id)
        {
          return _context.Matieres.Any(e => e.Id == id);
        }
    }
}
