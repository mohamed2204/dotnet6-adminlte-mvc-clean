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
    public class SpecialitesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpecialitesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Specialites
        public async Task<IActionResult> Index()
        {
            return View(await _context.Specialites.ToListAsync());
        }

        // GET: Specialites/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Specialites == null)
            {
                return NotFound();
            }

            var specialite = await _context.Specialites
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialite == null)
            {
                return NotFound();
            }

            return View(specialite);
        }

        // GET: Specialites/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Specialites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Specialite specialite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specialite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialite);
        }

        // GET: Specialites/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Specialites == null)
            {
                return NotFound();
            }

            var specialite = await _context.Specialites.FindAsync(id);
            if (specialite == null)
            {
                return NotFound();
            }
            return View(specialite);
        }

        // POST: Specialites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] Specialite specialite)
        {
            if (id != specialite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specialite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialiteExists(specialite.Id))
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
            return View(specialite);
        }

        // GET: Specialites/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Specialites == null)
            {
                return NotFound();
            }

            var specialite = await _context.Specialites
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialite == null)
            {
                return NotFound();
            }

            return View(specialite);
        }

        // POST: Specialites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Specialites == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Specialites'  is null.");
            }
            var specialite = await _context.Specialites.FindAsync(id);
            if (specialite != null)
            {
                _context.Specialites.Remove(specialite);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialiteExists(long id)
        {
            return _context.Specialites.Any(e => e.Id == id);
        }
    }
}
