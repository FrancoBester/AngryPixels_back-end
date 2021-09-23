using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace application_programming_interface.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly DataContext _context;

        public RolesController(DataContext context)
        {
            _context = context;
        }

        [Route("~/Roles/GetAll")]
        [HttpGet]
        public IEnumerable<Document_Type> Get()
        {
            return _context.Document_Type.ToList();
        }

        [Route("~/Roles/Create")]
        [HttpPost]
        public JsonResult Post([FromBody] Roles roles)
        {
            try
            {
                _context.Set<Roles>().Add(roles);
                _context.SaveChanges();
                return new JsonResult("Data saved");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }


        [Route("~/Roles/Edit/{id}")]
        [HttpPut("{id}")]
        public JsonResult Put(Roles roles)
        {
            try
            {
                _context.Entry(roles).State = EntityState.Modified;
                _context.SaveChanges();
                return new JsonResult("Data saved");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }


        [Route("~/Roles/Delete/{id}")]
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            try
            {
                _context.Remove(_context.Roles.Single(r => r.Role_Id == id));
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
