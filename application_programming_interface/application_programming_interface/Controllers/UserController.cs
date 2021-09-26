using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.Models;
using Microsoft.EntityFrameworkCore;
using application_programming_interface.DTOs;

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
        //      Get user name, surname, type and policy type ------- (DONE)
        //  Select - expand click
        //      Get all info from user model, policy, role, 
        //  Edit
        //      info  policies, 
        // Search
        //      name,surname, policy type, user type, -------- (DONE)

        [Route("~/Users/GetAdminLoadPageData")]
        [HttpGet]
        public IEnumerable<AdminLoadPageDTO> GetAdminLoadPageData(int? pageNumber)
        {
            //--------------------------------
            //VIR EXAMPLE MOENI DELETE NIE
            //--------------------------------
            //var test2 = (from user in _context.Users
            //             select new UserDTO
            //             {
            //                 FirstName = user.User_Name,
            //                 LastName = user.User_Surname,
            //                 Roles = (from ur in _context.User_Roles
            //                          join r in _context.Roles
            //                             on ur.Role_Id equals r.Role_Id
            //                          where ur.User_Id == user.User_Id
            //                          select r.Role_Name).ToList(),
            //                 Policies = (from up in _context.User_Policy
            //                             join p in _context.Policy
            //                                on up.Policy_Id equals p.Policy_Id
            //                             where up.User_Id == user.User_Id
            //                             select p).ToList()
            //             }).ToList();

            //Pagination
            int curPage = pageNumber ?? 1;
            int curPageSize = 20;

            //Query for needed info
            var userData = (from user in _context.Users
                         select new AdminLoadPageDTO
                         {
                             UserId = user.User_Id,
                             FirstName = user.User_Name,
                             LastName = user.User_Surname,
                             Roles = (from ur in _context.User_Roles
                                      join r in _context.Roles
                                         on ur.Role_Id equals r.Role_Id
                                      where ur.User_Id == user.User_Id
                                      select r).ToList(),
                             Policies = (from up in _context.User_Policy
                                         join p in _context.Policy
                                            on up.Policy_Id equals p.Policy_Id
                                         where up.User_Id == user.User_Id
                                         select p).ToList()
                         }).ToList();

            return userData.Skip((curPage - 1) * curPageSize).Take(curPageSize);
        }

        [Route("~/Users/SearchLoadPageData")]
        [HttpGet]
        public IEnumerable<AdminLoadPageDTO> SearchLoadPageData(int? pageNumber, string search)
        {
            
            //Pagination
            int curPage = pageNumber ?? 1;
            int curPageSize = 20;

            //Query for needed info
            var userData = (from user in _context.Users
                            select new AdminLoadPageDTO
                            {
                                UserId = user.User_Id,
                                FirstName = user.User_Name,
                                LastName = user.User_Surname,
                                Roles = (from ur in _context.User_Roles
                                         join r in _context.Roles
                                            on ur.Role_Id equals r.Role_Id
                                         where ur.User_Id == user.User_Id
                                         select r.Role_Name).ToList(),
                                Policies = (from up in _context.User_Policy
                                            join p in _context.Policy
                                               on up.Policy_Id equals p.Policy_Id
                                            where up.User_Id == user.User_Id
                                            select p.Policy_Type).ToList()
                            }).ToList();

            //Search results
            var searchResults = userData.Where(s => s.FirstName.Contains(search)
                                || s.LastName.Contains(search)
                                || s.Roles.Contains(search)
                                || s.Policies.Contains(search)).ToList();


            return searchResults.Skip((curPage - 1) * curPageSize).Take(curPageSize);
        }

    }
}
