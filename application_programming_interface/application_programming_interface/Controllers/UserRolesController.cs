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

        [Route("~/UserRoles/GetAll")]
        [HttpGet]
        public IEnumerable<User_Roles> Get()
        {
            return _context.User_Roles.ToList();
        }

        [Route("~/UserRoles/Create")]
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


        [Route("~/UserRoles/Edit/{id}")]
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

        [Route("~/UserRoles/Delete/{id}")]
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            try
            {
                _context.Remove(_context.User_Roles.Single(ur => ur.User_Roles_Id == id));
                _context.SaveChanges();
                return new JsonResult("Record removed");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }

        //Get user name, surname, type and policy type

        //Get all info from user model, policy, role, documents

        //Admin gets all crud

        //user CRUd func
        //  Edit
        //      info Name,Surname, Email, Cell, Gender, address, documents

    }
}
