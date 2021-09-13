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

        [HttpGet]
        public IEnumerable<Admissions> Get()
        {
            
            //var test = _context.Admissions.Select(x => x.Policy.Policy_id).FirstOrDefault();// example for getting forgein key data
            return _context.Admissions.ToList();
        }

        [HttpPost]
        public JsonResult Post([FromBody] Admissions admissions)
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

        [Route("~/{id}")]
        [HttpPost("{id}")]
        public JsonResult Put(int id, Admissions admissions)
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
    }
}
