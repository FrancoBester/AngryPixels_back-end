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
    public class UserTypeController : ControllerBase
    {
        private readonly DataContext _context;

        public UserTypeController(DataContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IEnumerable<User_Roles> Get()
        {
            return _context.User_Type.ToList();
        }

        [HttpPost]
        public JsonResult Post(User_Roles user_type)
        {
            try
            {
                _context.Set<User_Roles>().Add(user_type);
                _context.SaveChanges();
                return new JsonResult("data saved");
            }
            catch(Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }

        [Route("~/{id}")]
        [HttpPost("{id}")]
        public JsonResult Put(int id, User_Roles user_Type) 
        {
            try 
            {
                _context.Entry(user_Type).State = EntityState.Modified;
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
