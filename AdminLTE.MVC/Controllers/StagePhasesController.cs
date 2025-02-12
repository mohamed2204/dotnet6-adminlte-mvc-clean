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
    public class StagePhasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StagePhasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StagePhases
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StagePhases.Include(s => s.Phase).Include(s => s.Specialile).Include(s => s.Stage);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StagePhases/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.StagePhases == null)
            {
                return NotFound();
            }

            var stagePhase = await _context.StagePhases
                .Include(s => s.Phase)
                .Include(s => s.Specialile)
                .Include(s => s.Stage)
                .FirstOrDefaultAsync(m => m.StageId == id);
            if (stagePhase == null)
            {
                return NotFound();
            }

            return View(stagePhase);
        }

        // GET: StagePhases/Create
        public IActionResult Create()
        {
            ViewData["PhaseId"] = new SelectList(_context.Phases, "Id", "Name");
            ViewData["SpecialileId"] = new SelectList(_context.Specialites, "Id", "Name");
            ViewData["StageId"] = new SelectList(_context.Stages, "Id", "Name");
            return View();
        }

        // POST: StagePhases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StageId,PhaseId,SpecialileId,DateDebut,DateFin,AddedOn")] StagePhase stagePhase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stagePhase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PhaseId"] = new SelectList(_context.Phases, "Id", "Name", stagePhase.PhaseId);
            ViewData["SpecialileId"] = new SelectList(_context.Specialites, "Id", "Name", stagePhase.SpecialileId);
            ViewData["StageId"] = new SelectList(_context.Stages, "Id", "Name", stagePhase.StageId);
            return View(stagePhase);
        }

        // GET: StagePhases/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.StagePhases == null)
            {
                return NotFound();
            }

            var stagePhase = await _context.StagePhases.FindAsync(id);
            if (stagePhase == null)
            {
                return NotFound();
            }
            ViewData["PhaseId"] = new SelectList(_context.Phases, "Id", "Name", stagePhase.PhaseId);
            ViewData["SpecialileId"] = new SelectList(_context.Specialites, "Id", "Name", stagePhase.SpecialileId);
            ViewData["StageId"] = new SelectList(_context.Stages, "Id", "Name", stagePhase.StageId);
            return View(stagePhase);
        }

        // POST: StagePhases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("StageId,PhaseId,SpecialileId,DateDebut,DateFin,AddedOn")] StagePhase stagePhase)
        {
            if (id != stagePhase.StageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stagePhase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StagePhaseExists(stagePhase.StageId))
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
            ViewData["PhaseId"] = new SelectList(_context.Phases, "Id", "Name", stagePhase.PhaseId);
            ViewData["SpecialileId"] = new SelectList(_context.Specialites, "Id", "Name", stagePhase.SpecialileId);
            ViewData["StageId"] = new SelectList(_context.Stages, "Id", "Name", stagePhase.StageId);
            return View(stagePhase);
        }

        // GET: StagePhases/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.StagePhases == null)
            {
                return NotFound();
            }

            var stagePhase = await _context.StagePhases
                .Include(s => s.Phase)
                .Include(s => s.Specialile)
                .Include(s => s.Stage)
                .FirstOrDefaultAsync(m => m.StageId == id);
            if (stagePhase == null)
            {
                return NotFound();
            }

            return View(stagePhase);
        }

        // POST: StagePhases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.StagePhases == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StagePhases'  is null.");
            }
            var stagePhase = await _context.StagePhases.FindAsync(id);
            if (stagePhase != null)
            {
                _context.StagePhases.Remove(stagePhase);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StagePhaseExists(long id)
        {
            return _context.StagePhases.Any(e => e.StageId == id);
        }
    }
}
