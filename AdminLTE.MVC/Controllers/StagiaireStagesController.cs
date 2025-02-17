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
using System.Linq.Dynamic.Core;


namespace AdminLTE.MVC.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
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

            var items = await _context.StagiaireStages.Include(s => s.Specialite).Include(s => s.Stagiaire).ToListAsync();

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
                .Include(s => s.Specialite)
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
            ViewData["SpecilaiteId"] = new SelectList(_context.Specialites, "Id", "Name", stagiaireStage.SpecialiteId);
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
            ViewData["SpecilaiteId"] = new SelectList(_context.Specialites, "Id", "Name", stagiaireStage.SpecialiteId);
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
            ViewData["SpecilaiteId"] = new SelectList(_context.Specialites, "Id", "Name", stagiaireStage.SpecialiteId);
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
                .Include(s => s.Specialite)
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


        [HttpPost]
        public IActionResult GetCustomers(StagiaireStage stagiaireStage)
        {

            

            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();

                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][nom]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                //var customerData = (from tempcustomer in _context.StagiaireStages select tempcustomer);
                var customerData = _context.StagiaireStages.Include(s => s.Specialite).Include(s => s.Stagiaire);
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    //customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<StagiaireStage, Stagiaire>)customerData.Where(
                                                m => m.Stagiaire.Nom.Contains(searchValue)
                                                || m.Stagiaire.Prenom.Contains(searchValue)
                                                || m.Stagiaire.Mle.Contains(searchValue)
                                                || m.Specialite.Name.Contains(searchValue));
                }
                recordsTotal = customerData.Count();
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };
                return Ok(jsonData);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult GetList()
        {
            /*
            //Server Side Parameter
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            */
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            var length = Convert.ToInt32(Request.Form["length"].FirstOrDefault());
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][nom]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            //int pageSize = length != null ? Convert.ToInt32(length) : 0;
            //int skip = start != null ? Convert.ToInt32(start) : 0;
            //int recordsTotal = 0;

            //List<StagiaireStage> empList = new List<StagiaireStage>();

            using (_context)
            {
               
                //empList = _context.StagiaireStages.Include(s => s.Stagiaire).Include(s => s.Specialite).ToList<StagiaireStage>();

                 var stagiaires = _context.StagiaireStages
                    .Include(s => s.Stagiaire)
                    .Include(s => s.Specialite)
                    .ToArray();



                var query = stagiaires.AsQueryable().Select(x => new
                {
                    id = x.StagiaireId + "-" + x.StageId + "-" + x.SpecialiteId,
                    grade = x.Stagiaire.Grade,
                    prenom = x.Stagiaire.Prenom,
                    nom = x.Stagiaire.Nom,
                    mle = x.Stagiaire.Mle,
                    specialite = x.Specialite.Name,
                    dateDebut = x.DateDebut,
                    dateFin = x.DateFin
                });


                /*
                 * 
                 * .Select(
                            x => new { 
                                id = x.StagiaireId + "-" + x.StageId + "-" + x.SpecialiteId,
                                x.Stagiaire.Grade,
                                x.Stagiaire.Prenom,
                                x.Stagiaire.Nom,
                                x.Stagiaire.Mle,
                                specialite = x.Specialite.Name,
                                dateDebut = x.DateDebut,
                                dateFin = x.DateFin
                            })
                 * */
                //IQueryable requredDataFields = data.Select(x => new { x.Title, x.NestedObject });
                //{ index, str = fruit.Substring(0, index) })

                //.Select((fruit, index) =>
                //    new { index, str = fruit.Substring(0, index) });

                int totalrows = stagiaires.Count();

                int totalrowsafterfiltering = stagiaires.Count();

                List<StagiaireStage> empList = new List<StagiaireStage>();
               

                if (!string.IsNullOrEmpty(searchValue))//filter
                {
                    empList = (List<StagiaireStage>)query.
                                            Where(x => x.nom.ToLower().Contains(searchValue.ToLower()) ||
                                            x.prenom.ToLower().Contains(searchValue.ToLower()) ||
                                            x.specialite.ToLower().Contains(searchValue.ToLower()) ||
                                            //x.Age.ToString().Contains(searchValue.ToLower()) || 
                                            x.mle.ToLower().Contains(searchValue.ToLower()));
                }
                else
                {
                    //empList = query.ToList<StagiaireStage>();
                }
                
                
                //sorting
                //empList = stagiaires.OrderBy(sortColumn + " " + sortColumnDirection).ToList<StagiaireStage>();

                //paging
                empList = empList.Skip(start).Take(length).ToList<StagiaireStage>();

                var jsonData = new
                {
                    //Name = "Pranaya",
                    //ID = 4,
                    //DateOfBirth = new DateTime(1988, 02, 29)
                    data = empList,
                    draw,
                    recordsTotal = totalrows,
                    recordsFiltered = totalrowsafterfiltering
                };
                // Returning a JsonResult object with the jsonData as the content to be serialized to JSON
                return new JsonResult(jsonData);

                //return Json(new { data = empList, draw = draw, recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}
