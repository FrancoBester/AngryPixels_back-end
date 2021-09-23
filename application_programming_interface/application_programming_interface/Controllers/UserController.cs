using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.Models;
using Microsoft.EntityFrameworkCore;

namespace application_programming_interface.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Users> Get()
        {
            return _context.Users.ToList();
        }

        [HttpPost]
        public JsonResult Post(Users user)
        {
            try
            {
                _context.Add<Users>(user);
                _context.SaveChanges();
                return new JsonResult("data saved");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        [Route("~/{id}")]
        [HttpPost("{id}")]
        public JsonResult Put(int id, Users user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
                return new JsonResult("data saved");
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
                _context.Remove(_context.Users.Single(u => u.User_id == id));
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
