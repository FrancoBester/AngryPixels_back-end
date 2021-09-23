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
    public class PolicyController : ControllerBase
    {
        private readonly DataContext _context;

        public PolicyController(DataContext context)
        {
            //var test = _context.Policy.Select(x => x.Users).First();
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Policy> Get()
        {
            return _context.Policy.ToList();
        }

        [HttpPost]
        public JsonResult Post(Policy policy)
        {
            try
            {
                _context.Add<Policy>(policy);
                _context.SaveChanges();
                return new JsonResult("data saved");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [Route("~/{id}")]
        [HttpPut("{id}")]
        public JsonResult Put(Policy policy)
        {
            try
            {
                _context.Entry(policy).State = EntityState.Modified;
                _context.SaveChanges();
                return new JsonResult("Data saved");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }


        [Route("~/{id}")]
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            try
            {
                _context.Remove(_context.Admissions.Single(a => a.Adms_Id == id));
                _context.SaveChanges();
                return new JsonResult("Record removed");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }
    }
}
