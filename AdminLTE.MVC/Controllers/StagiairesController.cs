using System;
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
    public class StagiairesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StagiairesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stagiaires
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Stagiaires.Include(s => s.Specialite);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Stagiaires/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Stagiaires == null)
            {
                return NotFound();
            }

            var stagiaire = await _context.Stagiaires
                .Include(s => s.Specialite)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stagiaire == null)
            {
                return NotFound();
            }

            return View(stagiaire);
        }

        // GET: Stagiaires/Create
        public IActionResult Create()
        {
            ViewData["SpecialiteId"] = new SelectList(_context.Specialites, "Id", "Name");
            return View();
        }

        // POST: Stagiaires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Grade,Prenom,Nom,Mle,Cin,NomAr,PrenomAr,SpecialiteId,Branche,Promotion")] Stagiaire stagiaire)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stagiaire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpecialiteId"] = new SelectList(_context.Specialites, "Id", "Name", stagiaire.SpecialiteId);
            return View(stagiaire);
        }

        // GET: Stagiaires/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Stagiaires == null)
            {
                return NotFound();
            }

            var stagiaire = await _context.Stagiaires.FindAsync(id);
            if (stagiaire == null)
            {
                return NotFound();
            }
            ViewData["SpecialiteId"] = new SelectList(_context.Specialites, "Id", "Name", stagiaire.SpecialiteId);
            return View(stagiaire);
        }

        // POST: Stagiaires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Grade,Prenom,Nom,Mle,Cin,NomAr,PrenomAr,SpecialiteId,Branche,Promotion")] Stagiaire stagiaire)
        {
            if (id != stagiaire.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stagiaire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StagiaireExists(stagiaire.Id))
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
            ViewData["SpecialiteId"] = new SelectList(_context.Specialites, "Id", "Name", stagiaire.SpecialiteId);
            return View(stagiaire);
        }

        // GET: Stagiaires/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Stagiaires == null)
            {
                return NotFound();
            }

            var stagiaire = await _context.Stagiaires
                .Include(s => s.Specialite)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stagiaire == null)
            {
                return NotFound();
            }

            return View(stagiaire);
        }

        // POST: Stagiaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Stagiaires == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Stagiaires'  is null.");
            }
            var stagiaire = await _context.Stagiaires.FindAsync(id);
            if (stagiaire != null)
            {
                _context.Stagiaires.Remove(stagiaire);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StagiaireExists(long id)
        {
            return _context.Stagiaires.Any(e => e.Id == id);
        }
    }
}
