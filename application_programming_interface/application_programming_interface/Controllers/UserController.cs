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

        #region Client User Functionalities 
        #endregion

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


        #region Admin Functionalities

        //Retreives user information data are displayed on the admin loading page regaring all users with their policies and roles.
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

            //Query for needed info
            //var userData = (from user in _context.Users
            //             select new AdminLoadPageDTO
            //             {
            //                 UserId = user.User_Id,
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
            //                             select p.Policy_Type).ToList()
            //             }).ToList();


            //Pagination
            int curPage = pageNumber ?? 1;
            int curPageSize = 20;

            //Query for needed info
            var userData = (from u in _context.Users
                            join ur in _context.User_Roles on u.User_Id equals ur.User_Id
                            join r in _context.Roles on ur.Role_Id equals r.Role_Id
                            join up in _context.User_Policy on u.User_Id equals up.User_Id
                            join p in _context.Policy on up.Policy_Id equals p.Policy_Id
                            select new AdminLoadPageDTO
                            {
                                UserId = u.User_Id,
                                FirstName = u.User_Name,
                                LastName = u.User_Surname,
                                Roles = r.Role_Name,
                                PolicyId = p.Policy_Id,
                                Policies = p.Policy_Type
                            }).ToList();

            return userData.Skip((curPage - 1) * curPageSize).Take(curPageSize);
        }

        //Allows Admin users to search for any field values on the admin loading page table
        [Route("~/Users/SearchLoadPageData")]
        [HttpGet]
        public IEnumerable<AdminLoadPageDTO> SearchLoadPageData(int? pageNumber, string search)
        {

            //Pagination
            int curPage = pageNumber ?? 1;
            int curPageSize = 20;

            //Query for needed info
            var userData = (from u in _context.Users
                            join ur in _context.User_Roles on u.User_Id equals ur.User_Id
                            join r in _context.Roles on ur.Role_Id equals r.Role_Id
                            join up in _context.User_Policy on u.User_Id equals up.User_Id
                            join p in _context.Policy on up.Policy_Id equals p.Policy_Id
                            where u.User_Name.ToUpper().Contains(search.ToUpper()) ||
                                  u.User_Surname.ToUpper().Contains(search.ToUpper()) ||
                                  r.Role_Name.ToUpper().Contains(search.ToUpper()) ||
                                  p.Policy_Type.ToUpper().Contains(search.ToUpper())
                            select new AdminLoadPageDTO
                            {
                                UserId = u.User_Id,
                                FirstName = u.User_Name,
                                LastName = u.User_Surname,
                                Roles = r.Role_Name,
                                PolicyId = p.Policy_Id,
                                Policies = p.Policy_Type
                            }).ToList();


            return userData.Skip((curPage - 1) * curPageSize).Take(curPageSize);
        }

        //Retreives a specific Client Users information 
            //Use when admin clicks on User_Name in GetAdminLoadPageData(User Controller) and GetAllUserQueries(Queries Controller)
                // Policy_Id ==> Allow Admins to click on Policy_Type to view specific policy info
                // DocType_Id ==> Allow admins to click on Med_Cet, Passport_Doc, Birth_Certificate to download/view it
        [Route("~/Users/GetUserDetails/{userId}")]
        [HttpGet("{userId}")]
        public IEnumerable<UserInfoDTO> GetUserDetails(int userId)
        {
            //Query for needed info
            var userData = (from u in _context.Users
                            join a in _context.Address on u.Address_Id equals a.Address_Id
                            join ur in _context.User_Roles on u.User_Id equals ur.User_Id
                            join r in _context.Roles on ur.Role_Id equals r.Role_Id
                            join up in _context.User_Policy on u.User_Id equals up.User_Id
                            join p in _context.Policy on up.Policy_Id equals p.Policy_Id
                            join d in _context.Document on u.User_Id equals d.User_Id
                            join dt in _context.Document_Type on d.Doc_Id equals dt.Doc_Id
                            where u.User_Id == userId
                            select new UserInfoDTO
                            {
                                Role_Name = r.Role_Name,
                                User_Name = u.User_Name,
                                User_Surname = u.User_Surname,
                                User_ID_Number = u.User_ID_Number,
                                User_Email = u.User_Email,
                                User_Cell = u.User_Cell,
                                User_Dob = u.User_Dob,
                                User_Gender = u.User_Gender,
                                Street = a.Street,
                                City = a.City,
                                Postal_Code = a.Postal_Code,
                                Policy_Type = p.Policy_Type,
                                Med_Cet = dt.Med_Cet,
                                Passport_Doc = dt.Passport_Doc,
                                Birth_Certificate = dt.Birth_Certificate,
                                Policy_Id = p.Policy_Id,
                                DocType_Id = dt.DocType_Id
                            }).ToList();


            return userData;
        }

        #endregion


    }
}
