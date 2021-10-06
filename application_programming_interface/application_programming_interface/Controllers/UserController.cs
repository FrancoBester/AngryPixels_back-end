using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.Models;
using Microsoft.EntityFrameworkCore;
using application_programming_interface.DTOs;
using application_programming_interface.Interfaces;

namespace application_programming_interface.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //DATABASE CHANGES NEEDED
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        [Route("~/api/Users/Register")]
        [HttpPost]
        public JsonResult RegisterUser(UserRegisterDTO user)
        {
            try
            {
                //Add julle code hier om n user te add
                //(CAREL -->>)(NIE SEKER hoe om address stuff te add en hoe om die password hash te add nie)
                _userService.RegisterUser(user);

                return new JsonResult("data saved");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }


        #region Client User Functionalities 

        [Route("~/api/Users/GetUserLoadPageData")]
        [HttpGet]
        public IEnumerable<UserQueryDTO> GetUserLoadPageData(int? pageNumber, int userId)
        {
            return _userService.GetUserLoadPageData(pageNumber, userId);
        }

        //Allow specific user to update their own information (includes Users and Address)
        [Route("~/api/Users/UpdateUserInformation/{userId}")]
        [HttpPut("{userId}")]
        public JsonResult UpdateUserInformation(Users user, int userId)
        {
            try
            {
                _userService.UpdateUserInformation(user, userId);     

                return new JsonResult("data saved");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //NEEDS TO BE TESTED STILL
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //Allow specific user to remove own account
        //Allow admin to also remove user account
        [Route("~/api/Users/RemoveUserAccount/{userId}")]
        [HttpDelete("{userId}")]
        public JsonResult RemoveUserAccount(int userId) 
        {
            try
            {
                _userService.RemoveUserAccount(userId);

                return new JsonResult("Record removed");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }

        [Route("~/api/Users/GetProfileInformation/{userId}")]
        public JsonResult GetProfiledata(int id)
        {
            return new JsonResult("Quack");
        }

        //TODO:
            //Allow to manage policy info/type


        #endregion

        #region Admin Dashboard User Functionalities

        //Retreives user information data are displayed on the admin loading page regaring all users with their policies and roles.
        //FirstName/LastName --> When admin clicks it, they can view specific user info.
        //PolicyType --> When admin clicks it, they can view specific policy info.
        [Route("~/Users/GetAdminLoadPageData")]
        [HttpGet]
        public IEnumerable<AdminLoadPageDTO> GetAdminLoadPageData(int? pageNumber)
        {
            return _userService.GetAdminLoadPageData(pageNumber);
        }

        //Allows Admin users to search for any field values on the admin loading page table
        [Route("~/Users/SearchLoadPageData")]
        [HttpGet]
        public IEnumerable<AdminLoadPageDTO> SearchLoadPageData(int? pageNumber, string search)
        {
            return _userService.SearchLoadPageData(pageNumber, search);
        }

        //Retreives a specific Client Users information 
            //Use when admin clicks on User_Name or User_Surname in GetAdminLoadPageData(User Controller) <<<AND>>> GetAllUserQueries(Queries Controller)
                // Policy_Id ==> Allow Admins to click on Policy_Type to view specific policy info
                // DocType_Id ==> Allow admins to click on Med_Cet, Passport_Doc, Birth_Certificate to download/view it
        [Route("~/Users/GetUserDetails/{userId}")]
        [HttpGet("{userId}")]
        public IEnumerable<UserInfoDTO> GetUserDetails(int userId)
        {
            return _userService.GetUserDetails(userId);
        }

        //Retreives a specific Policy's information with the Admissions Type
            //Use when admin clicks on Policy_Type in GetAdminLoadPageData (User Controller)
                //Adms_Id ==> Allow Admin to click on Adms_Type to view specific Admission type info
        [Route("~/Users/GetPolicyDetails/{policyId}")]
        [HttpGet("{policyId}")]
        public IEnumerable<PolicyInfoDTO> GetPolicyDetails(int policyId)
        {
            return _userService.GetPolicyDetails(policyId);
        }

        //Retreives a specific AddimionType's information
            //Use when admin clicks on Adms_Type in GetPolicyDetails (User Controller)
        [Route("~/Users/GetAdmsTypeDetails/{admsId}")]
        [HttpGet("{admsId}")]
        public IEnumerable<AdmsInfoDTO> GetAdmsTypeDetails(int admsId)
        {
            return _userService.GetAdmsTypeDetails(admsId);
        }

        #endregion


    }
}
