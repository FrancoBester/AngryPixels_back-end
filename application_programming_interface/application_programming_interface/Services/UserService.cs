using application_programming_interface.DTOs;
using application_programming_interface.Interfaces;
using application_programming_interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.Services
{
    public class UserService: IUserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        public void RegisterUser(UserRegisterDTO user)
        {
            
            try
            {
                var userToAdd = new Users
                {
                    User_Name = user.FirstName,
                    User_Surname = user.LastName,
                    User_Dob = user.DateOfBirth,
                    User_Cell = user.CellPhoneNumber,
                    User_Email = user.Email,
                    User_Gender = user.Gender,
                    User_ID_Number = user.IDnumber,
                    Password_Hash = user.Password,
                    IsActive = true,
                    Address = new Address
                    {
                        City = user.Address.City,
                        Street = user.Address.Street
                    }
                    
                };

                _context.Users.Add(userToAdd);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
            
        }

        #region Client User Dashboard Functionalities 

        //Allow specific user to update their own information (includes Users and Address)
        public void UpdateUserInformation(Users user, int userId)
        {
            var updateUserObj = _context.Users.Where(x => x.User_Id == userId && x.IsActive).SingleOrDefault();
            var updateAdressObj = _context.Address.Where(x => x.Address_Id == updateUserObj.Address_Id).SingleOrDefault();

            if (updateUserObj != null)
            {
                updateUserObj.User_Name = user.User_Name;
                updateUserObj.User_Surname = user.User_Surname;
                updateUserObj.User_Email = user.User_Email;
                updateUserObj.User_Cell = user.User_Cell;
                updateUserObj.User_Gender = user.User_Gender;
                updateUserObj.User_Dob = user.User_Dob;
                updateUserObj.User_ID_Number = user.User_ID_Number;

                updateAdressObj.City = user.Address.City;
                updateAdressObj.Street = user.Address.Street;
                updateAdressObj.Postal_Code = user.Address.Postal_Code;

                _context.Users.Update(updateUserObj);
                _context.SaveChanges();
            }
        }

        //Allow specific user to remove own account
        //Allow admin to also remove user account
        public void RemoveUserAccount(int userId)
        {
            var delObj = _context.Users.Where(x => x.IsActive && x.User_Id == userId).SingleOrDefault();

            if (delObj != null)
            {
                delObj.IsActive = false;
                _context.Users.Update(delObj);
                _context.SaveChanges();
            }
        }

        //TODO:
        //Allow to manage policy info/type


        #endregion

        #region Admin/Employee Dashboard User Functionalities

        //Retreives user information data are displayed on the admin loading page regaring all users with their policies and roles.
            //FirstName/LastName --> When admin clicks it, they can view specific user info.
            //PolicyType --> When admin clicks it, they can view specific policy info.
        public IEnumerable<AdminLoadPageDTO> GetAdminLoadPageData(int? pageNumber)
        {
            //Pagination
            int curPage = pageNumber ?? 1;
            int curPageSize = 20;

            //Query for needed info
            var userData = (from u in _context.Users
                            join ur in _context.User_Roles on u.User_Id equals ur.User_Id
                            join r in _context.Roles on ur.Role_Id equals r.Role_Id
                            where u.IsActive == true && r.Role_Id != 1
                            select new AdminLoadPageDTO
                            {
                                UserId = u.User_Id,
                                FirstName = u.User_Name,
                                LastName = u.User_Surname,
                                RoleName = r.Role_Name,
                                PolicyId = (from up in _context.User_Policy
                                            join p in _context.Policy
                                            on up.Policy_Id equals p.Policy_Id
                                            where up.User_Id == u.User_Id
                                            select p.Policy_Id).FirstOrDefault(),
                                PolicyName = (from up in _context.User_Policy
                                            join p in _context.Policy
                                            on up.Policy_Id equals p.Policy_Id
                                            where up.User_Id == u.User_Id
                                            select p.Policy_Type).FirstOrDefault()
                            }).ToList();

            return userData.Skip((curPage - 1) * curPageSize).Take(curPageSize);
        }

        //Allows Admin users to search for any field values on the admin loading page table
        public IEnumerable<AdminLoadPageDTO> SearchLoadPageData(int? pageNumber, string search)
        {

            //Pagination
            int curPage = pageNumber ?? 1;
            int curPageSize = 20;

            //Query for needed info
            var userData = (from u in _context.Users
                            join ur in _context.User_Roles on u.User_Id equals ur.User_Id
                            join r in _context.Roles on ur.Role_Id equals r.Role_Id
                            where u.IsActive == true && r.Role_Id != 1
                            select new AdminLoadPageDTO
                            {
                                UserId = u.User_Id,
                                FirstName = u.User_Name,
                                LastName = u.User_Surname,
                                RoleName = r.Role_Name,
                                PolicyId = (from up in _context.User_Policy
                                            join p in _context.Policy
                                            on up.Policy_Id equals p.Policy_Id
                                            where up.User_Id == u.User_Id
                                            select p.Policy_Id).FirstOrDefault(),
                                PolicyName = (from up in _context.User_Policy
                                            join p in _context.Policy
                                            on up.Policy_Id equals p.Policy_Id
                                            where up.User_Id == u.User_Id
                                            select p.Policy_Type).FirstOrDefault()
                            }).Where( x => x.FirstName.ToUpper().Contains(search.ToUpper()) ||
                                      x.LastName.ToUpper().Contains(search.ToUpper()) ||
                                      x.RoleName.ToUpper().Contains(search.ToUpper()) ||
                                      x.PolicyName.ToUpper().Contains(search.ToUpper())).ToList();


            return userData.Skip((curPage - 1) * curPageSize).Take(curPageSize);
        }

        //Retreives a specific Client Users information 
        //Use when admin clicks on User_Name or User_Surname in GetAdminLoadPageData(User Controller) <<<AND>>> GetAllUserQueries(Queries Controller)
            // Policy_Id ==> Allow Admins to click on Policy_Type to view specific policy info
            // DocType_Id ==> Allow admins to click on Med_Cet, Passport_Doc, Birth_Certificate to download/view it
        public UserInfoDTO GetUserDetails(int userId)
        {
            var userData = (from u in _context.Users
                         join a in _context.Address on u.Address_Id equals a.Address_Id
                         join ur in _context.User_Roles on u.User_Id equals ur.User_Id
                         join r in _context.Roles on ur.Role_Id equals r.Role_Id
                         where u.User_Id == userId
                         select new UserInfoDTO
                         {
                             User_Name = u.User_Name,
                             User_Surname = u.User_Surname,
                             User_ID_Number = u.User_ID_Number,
                             User_Email = u.User_Email,
                             User_Cell = u.User_Cell,
                             User_Dob = (u.User_Dob).ToShortDateString(),
                             User_Gender = u.User_Gender,
                             Street = a.Street,
                             City = a.City,
                             Postal_Code = a.Postal_Code,
                             Role_Name = r.Role_Name,
                             Policy_Type = (from up in _context.User_Policy
                                            join p in _context.Policy
                                            on up.Policy_Id equals p.Policy_Id
                                            where up.User_Id == userId
                                            select p.Policy_Type).FirstOrDefault(),
                             Policy_Id = (from up in _context.User_Policy
                                          join p in _context.Policy
                                          on up.Policy_Id equals p.Policy_Id
                                          where up.User_Id == userId
                                          select p.Policy_Id).FirstOrDefault()
                         }).FirstOrDefault();

            return userData;
        }

        //Retreives a specific Policy's information with the Admissions Type
            //Use when admin clicks on Policy_Type in GetAdminLoadPageData (User Controller)
            //Adms_Id ==> Allow Admin to click on Adms_Type to view specific Admission type info
        public PolicyInfoDTO GetPolicyDetails(int policyId)
        {
            //Query for needed info
            var policyData = (from p in _context.Policy
                              join a in _context.Admissions on p.Policy_Id equals a.Policy_Id
                              where p.Policy_Id == policyId
                              select new PolicyInfoDTO
                              {
                                  Policy_Type = p.Policy_Type,
                                  Policy_Holder = p.Policy_Holder,
                                  Policy_Date = p.Policy_Date,
                                  Policy_Des = p.Policy_Des,
                                  Policy_Benefits = p.Policy_Benefits,
                                  Adms_Id = a.Adms_Id,
                                  Adms_Type = a.Adms_Type
                              }).FirstOrDefault();

            return policyData;
        }

        //Retreives a specific AddimionType's information
        //Use when admin clicks on Adms_Type in GetPolicyDetails (User Controller)
        public IEnumerable<AdmsInfoDTO> GetAdmissionsTypeDetails(int admsId)
        {
            //Query for needed info
            var policyData = (from a in _context.Admissions
                              where a.Adms_Id == admsId
                              select new AdmsInfoDTO
                              {
                                  Adms_Type = a.Adms_Type,
                                  Adms_Hospitals = a.Adms_Hospitals,
                                  Adms_Doctors = a.Adms_Doctors
                              }).ToList();

            return policyData;
        }

        #endregion

        public ProfileDTO GetProfileInformation(int id)
        {
            var objectToReturn = new ProfileDTO()
            {
                User = GetUserDetails(id),
                Files = (from d in _context.Document
                         where d.Policy_Id == null
                         && d.User_Id == id
                         select new ProfileFileDTO
                         {
                             FileId = d.Doc_Id,
                             FileName = d.File_Name,
                             FileUrl = d.File_Url,
                             FileTypeId = d.Doc_Type_Id
                         }).ToList()
            };

            return objectToReturn;
        }
    }
}
