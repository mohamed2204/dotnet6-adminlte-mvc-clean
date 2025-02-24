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

        public IActionResult GetList()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var pageSize = int.Parse(Request.Form["length"]);
            var skip = int.Parse(Request.Form["start"]);

            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
            var sortColumnDirection = Request.Form["order[0][dir]"];

            var stageId = int.Parse(Request.Form["stage[stageId]"].FirstOrDefault());

            IQueryable<StagiaireStage> customers = _context.StagiaireStages.Where(m => string.IsNullOrEmpty(searchValue)
                ? true
                : (m.Stagiaire.Nom.ToLower().Contains(searchValue.ToLower()) ||
                m.Stagiaire.Prenom.ToLower().Contains(searchValue.ToLower()) ||
                m.Stagiaire.Mle.ToLower().Contains(searchValue.ToLower()) ||
                m.Stagiaire.Specialite.Name.ToLower().Contains(searchValue.ToLower())));


            customers = customers.Where(s => s.StageId == stageId);
            //sorting
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            //    customers = customers.OrderBy(string.Concat(sortColumn, " ", sortColumnDirection));

            //paging
            var data = customers.Skip(skip).Take(pageSize);

            var data2 = data.Select(d => new
            {
                //id = string.Concat(d.StagiaireId, "-", d.StageId, "-", d.SpecialiteId),
                id = d.StagiaireId,
                grade = d.Stagiaire.Grade,
                prenom = d.Stagiaire.Prenom,
                nom = d.Stagiaire.Nom,
                specialite = d.Specialite.Name,
                dateDebut = d.DateDebut,
                dateFin = d.DateFin
            }).ToList();

            var recordsTotal = customers.Count();

            var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data = data2 };

            return Ok(jsonData);
        }
        // GET: Stagiaires
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = _context.Stagiaires.Include(s => s.Specialite);
            //return View(await applicationDbContext.ToListAsync());
            var stages = _context.Stages;
            ViewBag.stages = new SelectList(stages, "Id", "Name");
            return View();
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
