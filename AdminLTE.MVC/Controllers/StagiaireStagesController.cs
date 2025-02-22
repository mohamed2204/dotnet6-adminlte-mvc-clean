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
    public class StagiaireStagesController : Microsoft.AspNetCore.Mvc.Controller
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
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
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



        //[Microsoft.AspNetCore.Mvc.HttpPost]
        //public ActionResult GetList()
        //{
        //    /*
        //    //Server Side Parameter
        //    int start = Convert.ToInt32(Request["start"]);
        //    int length = Convert.ToInt32(Request["length"]);
        //    string searchValue = Request["search[value]"];
        //    string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
        //    string sortDirection = Request["order[0][dir]"];
        //    */
        //    var draw = Request.Form["draw"].FirstOrDefault();
        //    var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
        //    var length = Convert.ToInt32(Request.Form["length"].FirstOrDefault());
        //    var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][nom]"].FirstOrDefault();
        //    var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        //    var searchValue = Request.Form["search[value]"].FirstOrDefault();
        //    //int pageSize = length != null ? Convert.ToInt32(length) : 0;
        //    //int skip = start != null ? Convert.ToInt32(start) : 0;
        //    //int recordsTotal = 0;

        //    List<StagiaireStage> empList = new List<StagiaireStage>();

        //    using (_context)
        //    {

        //        empList = _context.StagiaireStages.ToList<StagiaireStage>();

        //        IQueryable<StagiaireStage> stagiaires = empList.AsQueryable();

        //        int totalrows = stagiaires.Count();
        //        if (!string.IsNullOrEmpty(searchValue))//filter
        //        {
        //            empList = stagiaires.
        //                Where(x => x.Stagiaire.Nom.ToLower().Contains(searchValue.ToLower()) ||
        //                x.Stagiaire.Prenom.ToLower().Contains(searchValue.ToLower()) ||
        //                x.Specialite.Name.ToLower().Contains(searchValue.ToLower()) ||
        //                //x.Age.ToString().Contains(searchValue.ToLower()) || 
        //                x.Stagiaire.Mle.ToString().Contains(searchValue.ToLower())).ToList<StagiaireStage>();
        //        }
        //        int totalrowsafterfiltering = empList.Count;
        //        //sorting
        //        //empList = stagiaires.OrderBy(sortColumn + " " + sortColumnDirection).ToList<StagiaireStage>();

        //        //paging
        //        empList = empList.Skip(start).Take(length).ToList<StagiaireStage>();

        //        var jsonData = new
        //        {
        //            //Name = "Pranaya",
        //            //ID = 4,
        //            //DateOfBirth = new DateTime(1988, 02, 29)
        //            data = empList,
        //            draw,
        //            recordsTotal = totalrows,
        //            recordsFiltered = totalrowsafterfiltering
        //        };
        //        // Returning a JsonResult object with the jsonData as the content to be serialized to JSON
        //        return new JsonResult(jsonData);

        //        //return Json(new { data = empList, draw = draw, recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
        //    }


        //}

        public IActionResult GetList2()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var pageSize = int.Parse(Request.Form["length"]);
            var skip = int.Parse(Request.Form["start"]);

            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
            var sortColumnDirection = Request.Form["order[0][dir]"];

            IQueryable<StagiaireStage> customers = _context.StagiaireStages.Where(m => string.IsNullOrEmpty(searchValue)
                ? true
                : (m.Stagiaire.Nom.ToLower().Contains(searchValue.ToLower()) ||
                m.Stagiaire.Prenom.ToLower().Contains(searchValue.ToLower()) ||
                m.Stagiaire.Mle.ToLower().Contains(searchValue.ToLower()) ||
                m.Stagiaire.Specialite.Name.ToLower().Contains(searchValue.ToLower())));

            //sorting
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            //    customers = customers.OrderBy(string.Concat(sortColumn, " ", sortColumnDirection));

            //paging
            var data = customers.Skip(skip).Take(pageSize);

            var data2 = data.Select(d => new
            {
                id = string.Concat(d.StagiaireId, "-", d.StageId, "-", d.SpecialiteId),
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

        [HttpGet]
        public async Task<IActionResult> AddOrEditAsync(string id = "")
        {
            if (id == "")
                return View(new StagiaireStage());
            else
            {
                //using (DBModel db = new DBModel())
                //{

                string[] Ids = id.Split('-');

                //foreach (var sub in subs)
                //{
                //    Console.WriteLine($"Substring: {sub}");
                //}

                var stagiaireStage = await _context.StagiaireStages
               .Include(s => s.Specialite)
               .Include(s => s.Stagiaire)
               .Include(s => s.Stage)
               .FirstOrDefaultAsync(x => x.StagiaireId == long.Parse(Ids[0]) && x.StageId == long.Parse(Ids[1]) && x.SpecialiteId == long.Parse(Ids[2]));

                //var stagiaireStage = _context.StagiaireStages.Where(
                //    x => x.StagiaireId == long.Parse(Ids[0]) && x.StageId == long.Parse(Ids[1]) && x.SpecialiteId == long.Parse(Ids[2]))
                //    .FirstOrDefault<StagiaireStage>();

                ViewData["SpecilaiteId"] = new SelectList(_context.Specialites, "Id", "Name", stagiaireStage.SpecialiteId);
                ViewData["StagiaireId"] = new SelectList(_context.Stagiaires, "Id", "Id", stagiaireStage.StagiaireId);
                ViewData["Id"] = id;
                return View(stagiaireStage);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddOrEditAsync(StagiaireStage ss)
        {
            //using (DBModel db = new DBModel())
            //{
            //    if (ss. == 0)
            //    {
            //        db.Employees.Add(emp);
            //        db.SaveChanges();
            //        return Json(new { success = true, message = "Saved Successfully" }, System.Web.Mvc.JsonRequestBehavior.AllowGet);
            //    }
            //    else
            //    {
            //        db.Entry(StagiaireStage).State = EntityState.Modified;
            //        db.SaveChanges();
            //        return Json(new { success = true, message = "Updated Successfully" });
            //    }
            //}

            var d = ss;

            if (ss.StageId == 9999999)
            {
                await _context.StagiaireStages.AddAsync(ss);
                _context.SaveChanges();
                return Json(new { success = true, message = "Saved Successfully" });
            }
            else
            {
                _context.StagiaireStages.Update(ss);
                _context.SaveChanges();
                return Json(new { success = true, message = "Updated Successfully" });
            }
        }
        

           
        }


        //[Microsoft.AspNetCore.Mvc.HttpPost]
        //public IActionResult Delete(int id)
        //{
        //    using (DBModel db = new DBModel())
        //    {
        //        Employee emp = db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>();
        //        db.Employees.Remove(emp);
        //        db.SaveChanges();
        //        return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }
}


