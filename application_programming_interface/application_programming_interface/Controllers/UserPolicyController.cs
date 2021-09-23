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

    public class UserPolicyController : ControllerBase
    {
        private readonly DataContext _context;

        public UserPolicyController(DataContext context)
        {
            _context = context;
        }

        [Route("~/UserPolicy/GetAll")]
        [HttpGet]
        public IEnumerable<User_Policy> Get()
        {
            return _context.User_Policies.ToList();
        }

        [Route("~/UserPolicy/Create")]
        [HttpPost]
        public JsonResult Post([FromBody] User_Policy user_Policy)
        {
            try
            {
                _context.Set<User_Policy>().Add(user_Policy);
                _context.SaveChanges();
                return new JsonResult("Data saved");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }


        [Route("~/UserPolicy/Edit/{id}")]
        [HttpPut("{id}")]
        public JsonResult Put(User_Policy user_Policy)
        {
            try
            {
                _context.Entry(user_Policy).State = EntityState.Modified;
                _context.SaveChanges();
                return new JsonResult("Data saved");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }


        [Route("~/UserPolicy/Delete/{id}")]
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            try
            {
                _context.Remove(_context.User_Policies.Single(up => up.User_Policy_Id == id));
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
