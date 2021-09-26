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
    public class UserController : Controller
    {
        private readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }

        [Route("~/Users/GetAll")]
        [HttpGet]
        public IEnumerable<Users> Get()
        {
            return _context.Users.ToList();
        }

        [Route("~/Users/Create")]
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

        [Route("~/Users/Edit/{id}")]
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

        [Route("~/Users/Delete/{id}")]
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            try
            {
                _context.Remove(_context.Users.Single(u => u.User_Id == id));
                _context.SaveChanges();
                return new JsonResult("Record removed");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }



        //user CRUD func - profile page
        //  Select - load page
        //      all user queries(queire detail, title)
        //  
        //  Edit - click pfp image button
        //      info Name,Surname, Email, Cell, Gender, address, documents
        //  Insert into schema request table - change/update policy
        //  
        //  Delete - delete entire profile
        //  


        //Admin gets all CRUD

        //Admin CRUD func
        //  Select - load page
        //      Get user name, surname, type and policy type
        //  Select - expand click
        //      Get all info from user model, policy, role, 
        //  Edit
        //      info  policies, 
        // Search
        //      name,surname, policy type, user type, 

        [Route("~/Users/Test")]
        [HttpGet]
        public IEnumerable<dynamic> Test()
        {

            //var a = (from user in _context.Users
            //select  user.User_Name );

            //var e = _context.Users.Include("User_Roles.Role").Select(x => new { x.User_Name, x.User_Surname }).ToList();


            var test = (from user in _context.Users
                        join uRoles in _context.User_Roles on user.User_Id equals uRoles.User_Id
                        join aRoles in _context.Roles on uRoles.Role_Id equals aRoles.Role_Id
                        join uPolicy in _context.User_Policy on user.User_Id equals uPolicy.User_Id
                        join aPolicy in _context.Policy on uPolicy.Policy_Id equals aPolicy.Policy_Id
                        select new { user.User_Name, user.User_Surname, aRoles.Role_Name, aPolicy.Policy_Type }).AsEnumerable().Cast<dynamic>().ToList<dynamic>();

            return test;
        }

    }
}
