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
using System.Text.Json.Serialization;
using System.Text.Json;

namespace AdminLTE.MVC.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult GetCustomers()
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

                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = false
                };
                string items = JsonSerializer.Serialize(data, options);

                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, items };
                return Ok(jsonData);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
