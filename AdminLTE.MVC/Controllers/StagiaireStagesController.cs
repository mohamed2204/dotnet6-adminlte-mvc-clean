using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminLTE.MVC.Data;
using AdminLTE.MVC.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;

namespace AdminLTE.MVC.Controllers
{
    public class StagiaireStagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StagiaireStagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StagiaireStages
        public async Task<IActionResult> Index(int? page, string sortOrder)
        {
            //var applicationDbContext = _context.StagiaireStages.Include(s => s.Specilaite).Include(s => s.Stagiaire);
            //return View(await applicationDbContext.ToListAsync());

            var items = await _context.StagiaireStages.Include(s => s.Specilaite).Include(s => s.Stagiaire).ToListAsync();

            var pageNumber = page ?? 1;
            // if no page was specified in the querystring, default to the first page (1)

            int pageSize = 15;

            IPagedList<StagiaireStage> onePageOfItems = new PagedList<StagiaireStage>(items, pageNumber, pageSize);


            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (onePageOfItems.PageNumber != 1 && page > onePageOfItems.PageCount)
            {
                return NotFound();
            }

            ViewBag.OnePageOfItems = onePageOfItems;
            return View();
        }

        // GET: StagiaireStages/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.StagiaireStages == null)
            {
                return NotFound();
            }

            var stagiaireStage = await _context.StagiaireStages
                .Include(s => s.Specilaite)
                .Include(s => s.Stagiaire)
                .FirstOrDefaultAsync(m => m.StagiaireId == id);
            if (stagiaireStage == null)
            {
                return NotFound();
            }

            return View(stagiaireStage);
        }

        // GET: StagiaireStages/Create
        public IActionResult Create()
        {
            ViewData["SpecilaiteId"] = new SelectList(_context.Specialites, "Id", "Name");
            ViewData["StagiaireId"] = new SelectList(_context.Stagiaires, "Id", "Id");
            return View();
        }

        // POST: StagiaireStages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StagiaireId,StageId,SpecilaiteId,DateDebut,DateFin")] StagiaireStage stagiaireStage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stagiaireStage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpecilaiteId"] = new SelectList(_context.Specialites, "Id", "Name", stagiaireStage.SpecilaiteId);
            ViewData["StagiaireId"] = new SelectList(_context.Stagiaires, "Id", "Id", stagiaireStage.StagiaireId);
            return View(stagiaireStage);
        }

        // GET: StagiaireStages/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.StagiaireStages == null)
            {
                return NotFound();
            }

            var stagiaireStage = await _context.StagiaireStages.FindAsync(id);
            if (stagiaireStage == null)
            {
                return NotFound();
            }
            ViewData["SpecilaiteId"] = new SelectList(_context.Specialites, "Id", "Name", stagiaireStage.SpecilaiteId);
            ViewData["StagiaireId"] = new SelectList(_context.Stagiaires, "Id", "Id", stagiaireStage.StagiaireId);
            return View(stagiaireStage);
        }

        // POST: StagiaireStages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("StagiaireId,StageId,SpecilaiteId,DateDebut,DateFin")] StagiaireStage stagiaireStage)
        {
            if (id != stagiaireStage.StagiaireId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stagiaireStage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StagiaireStageExists(stagiaireStage.StagiaireId))
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
            ViewData["SpecilaiteId"] = new SelectList(_context.Specialites, "Id", "Name", stagiaireStage.SpecilaiteId);
            ViewData["StagiaireId"] = new SelectList(_context.Stagiaires, "Id", "Id", stagiaireStage.StagiaireId);
            return View(stagiaireStage);
        }

        // GET: StagiaireStages/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.StagiaireStages == null)
            {
                return NotFound();
            }

            var stagiaireStage = await _context.StagiaireStages
                .Include(s => s.Specilaite)
                .Include(s => s.Stagiaire)
                .FirstOrDefaultAsync(m => m.StagiaireId == id);
            if (stagiaireStage == null)
            {
                return NotFound();
            }

            return View(stagiaireStage);
        }

        // POST: StagiaireStages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.StagiaireStages == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StagiaireStages'  is null.");
            }
            var stagiaireStage = await _context.StagiaireStages.FindAsync(id);
            if (stagiaireStage != null)
            {
                _context.StagiaireStages.Remove(stagiaireStage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StagiaireStageExists(long id)
        {
            return _context.StagiaireStages.Any(e => e.StagiaireId == id);
        }
    }
}
