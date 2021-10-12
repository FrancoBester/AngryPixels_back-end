using application_programming_interface.DTOs;
using application_programming_interface.Interfaces;
using application_programming_interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.Services
{
    public class SchemaRequestService : ISchemaRequestService
    {
        private readonly DataContext _context;

        public SchemaRequestService(DataContext context)
        {
            _context = context;
        }

        enum SchemaRequestStatuses
        {
            Pending = 1,
            Approved = 2,
            Declined = 3
        }

        #region Client Policy/Schema Request Related Queries
        //Get Specific Users Policy / Policies
        public IEnumerable<SpecificUserPolicyDTO> GetSpecificUserPolicyDetails(int userId)
        {
            //Query for needed info
            var policyData = (from u in _context.Users
                              join up in _context.User_Policy on u.User_Id equals up.User_Id
                              join p in _context.Policy on up.Policy_Id equals p.Policy_Id
                              where u.User_Id == userId
                              select new SpecificUserPolicyDTO
                              {
                                  UserId = userId,
                                  policyId = p.Policy_Id,
                                  Policy_Type = p.Policy_Type,
                                  Policy_Holder = p.Policy_Holder,
                                  Policy_Date = p.Policy_Date,
                                  Policy_Des = p.Policy_Des,
                                  Policy_Benefits = p.Policy_Benefits,
                                  Adms_Id = (from up in _context.User_Policy
                                             join a in _context.Admissions
                                             on up.Policy_Id equals a.Policy_Id
                                             where a.Policy_Id == p.Policy_Id
                                             select a.Adms_Id).FirstOrDefault(),
                                  Adms_Type = (from up in _context.User_Policy
                                               join a in _context.Admissions
                                               on up.Policy_Id equals a.Policy_Id
                                               where a.Policy_Id == p.Policy_Id
                                               select a.Adms_Type).FirstOrDefault(),
                                  Adms_Doctors = (from up in _context.User_Policy
                                                  join a in _context.Admissions
                                                  on up.Policy_Id equals a.Policy_Id
                                                  where a.Policy_Id == p.Policy_Id
                                                  select a.Adms_Doctors).FirstOrDefault(),
                                  Adms_Hospitals = (from up in _context.User_Policy
                                                    join a in _context.Admissions
                                                    on up.Policy_Id equals a.Policy_Id
                                                    where a.Policy_Id == p.Policy_Id
                                                    select a.Adms_Hospitals).FirstOrDefault()
                              }).ToList();

            return policyData;
        }

        //View all Joinable Policies 
        public IEnumerable<AllPoliciesDTO> GetAllPolicies()
        {
            //Query for needed info
            var policyData = (from p in _context.Policy
                              join a in _context.Admissions on p.Policy_Id equals a.Policy_Id
                              select new AllPoliciesDTO
                              {
                                  PolicyId = p.Policy_Id,
                                  AdmsId = a.Adms_Id,
                                  PolicyType = p.Policy_Type,
                                  AdmsType = a.Adms_Type,
                                  PolicyBenefits = p.Policy_Benefits,
                                  PolicyDescription = p.Policy_Des,
                                  PolicyHolder = p.Policy_Holder
                              }).ToList();

            return policyData;
        }

        //Request to Join a Policy
        public void RequestToJoinSchema(int userId, Schema_Requests newRequest)
        {
            var requestToAdd = new Schema_Requests
            {
                User_Id = userId,
                Status_Id = 1,
                Policy_Id = newRequest.Policy_Id
            };

            _context.Schema_Requests.Add(requestToAdd);
            _context.SaveChanges();
        }

        //Request to Upgrade to new Policy


        //Cancel Policy


        #endregion

        #region Admin/Employee Policy/Schema Request Related Queries

        //Get all Schema Requests for Admin/Employee to View
        public IEnumerable<SchemaRequestDTO> GetAllSchemaRequests(int? pageNumber)
        {
            //Pagination
            int curPage = pageNumber ?? 1;
            int curPageSize = 20;

            //Query for needed info
            var qeuryData = (from u in _context.Users
                             join sr in _context.Schema_Requests on u.User_Id equals sr.User_Id
                             where u.IsActive == true
                             select new SchemaRequestDTO
                             {
                                 UserId = u.User_Id,
                                 PolicyId = sr.Policy_Id,
                                 RequestId = sr.Request_Id,
                                 UserName = u.User_Name,
                                 UserSurname = u.User_Surname,
                                 RequestStatus = ((SchemaRequestStatuses)sr.Status_Id).ToString(),
                                 PolicyType = (from sr in _context.Schema_Requests
                                               join p in _context.Policy
                                               on sr.Policy_Id equals p.Policy_Id
                                               where sr.Policy_Id == p.Policy_Id
                                               select p.Policy_Type).FirstOrDefault(),
                             }).ToList();

            return qeuryData.Skip((curPage - 1) * curPageSize).Take(curPageSize);
        }

        //Decline Client Schema Request
        //Approve Client Schema Request
        #endregion
    }
}
