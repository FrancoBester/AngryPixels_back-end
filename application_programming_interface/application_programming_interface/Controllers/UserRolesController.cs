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
    public class UserRolesController : ControllerBase
    {
        private readonly DataContext _context;

        public UserRolesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<User_Roles> Get()
        {
            return _context.User_Roles.ToList();
        }

        [HttpPost]
        public JsonResult Post([FromBody] User_Roles user_Roles)
        {
            try
            {
                _context.Set<User_Roles>().Add(user_Roles);
                _context.SaveChanges();
                return new JsonResult("Data saved");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }


        [Route("~/{id}")]
        [HttpPut("{id}")]
        public JsonResult Put(User_Roles user_Roles)
        {
            try
            {
                _context.Entry(user_Roles).State = EntityState.Modified;
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
