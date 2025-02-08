using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminLTE.MVC.Data;
using AdminLTE.MVC.Models;
using X.PagedList;

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
        //public async Task<IActionResult> Index()
        //{
        //      return View(await _context.Students.ToListAsync());
        //}

        public async Task<IActionResult> Index(int? page, string sortOrder)
        {
            //return View(await _context.Stagiaires.ToListAsync());

            var items = await _context.Students.ToListAsync();

            var pageNumber = page ?? 1;
            // if no page was specified in the querystring, default to the first page (1)

            int pageSize = 15;

            IPagedList<Stagiaire> onePageOfItems = new PagedList<Stagiaire>(items, pageNumber, pageSize);


            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (onePageOfItems.PageNumber != 1 && page > onePageOfItems.PageCount)
            {
                return NotFound();
            }

            ViewBag.OnePageOfItems = onePageOfItems;
            return View();
        }

        // GET: Stagiaires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var stagiaire = await _context.Students
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
            return View();
        }

        // POST: Stagiaires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Grade,Prenom,Nom,Mle,Cin,NomAr,PrenomAr,Spec,Branche")] Stagiaire stagiaire)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stagiaire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stagiaire);
        }

        // GET: Stagiaires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var stagiaire = await _context.Students.FindAsync(id);
            if (stagiaire == null)
            {
                return NotFound();
            }
            return View(stagiaire);
        }

        // POST: Stagiaires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Grade,Prenom,Nom,Mle,Cin,NomAr,PrenomAr,Spec,Branche")] Stagiaire stagiaire)
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
            return View(stagiaire);
        }

        // GET: Stagiaires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var stagiaire = await _context.Students
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            var stagiaire = await _context.Students.FindAsync(id);
            if (stagiaire != null)
            {
                _context.Students.Remove(stagiaire);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StagiaireExists(int id)
        {
          return _context.Students.Any(e => e.Id == id);
        }
    }
}
