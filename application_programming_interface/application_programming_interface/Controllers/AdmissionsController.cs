using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.Models;
using Microsoft.EntityFrameworkCore;

namespace application_programming_interface.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdmissionsController : ControllerBase
    {
        private readonly DataContext _context;

        public AdmissionsController(DataContext context)
        {
            _context = context;
        }

        [Route("~/Admissions/GetAll")]
        [HttpGet]
        public IEnumerable<Admissions> GetAll()
        {
            //var test = _context.Admissions.Select(x => x.Policy.Policy_id).FirstOrDefault();// example for getting forgein key data
            return _context.Admissions.ToList();
        }

        [Route("~/Admissions/Create")]
        [HttpPost]
        public JsonResult Create([FromBody] Admissions admissions)
        {
            try
            {
                _context.Set<Admissions>().Add(admissions);
                _context.SaveChanges();
                return new JsonResult("data saved");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }

        [Route("~/Admissions/Update/{id}")]
        [HttpPut("{id}")]
        public JsonResult Update(int id, Admissions admissions)
        {
            try
            {
                _context.Entry(admissions).State = EntityState.Modified;
                _context.SaveChanges();
                return new JsonResult("data saved");
            }
            catch(Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }

        [Route("~/Admissions/Delete/{id}")]
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            try
            {
                var test = id;
                _context.Remove(_context.Admissions.Single(a => a.Adms_Id == id));
                _context.SaveChanges();
                return new JsonResult("Record removed");
            }
            catch(Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }
    }
}
