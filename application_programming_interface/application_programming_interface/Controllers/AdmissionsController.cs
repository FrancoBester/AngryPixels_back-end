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

        //[Route("~/{id}")]
        //[HttpPost("{id}")]
        [HttpPost]
        public JsonResult Post([FromBody] Admissions admissions)
        {
            //var new_ad = new Admissions { Adms_id = admissions.Adms_id, Adms_type = admissions.Adms_type, Adms_Hospital = admissions.Adms_Hospital, Adms_Doctor = admissions.Adms_Doctor, Policy_id = admissions.Policy_id };
            try
            {
                _context.Set<Admissions>().Add(admissions);
                //_context.Admissions.Add(admissions);
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
