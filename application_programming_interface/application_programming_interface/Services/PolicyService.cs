using application_programming_interface.DTOs;
using application_programming_interface.Interfaces;
using application_programming_interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly DataContext _context;
        public PolicyService(DataContext context)
        {
            _context = context;
        }

        public enum SchemaRequestStatuses
        {
            Pending = 1,
            Approved = 2,
            Declined = 3
        }

        //Create Policy
        public void CreatePolicy(PolicyCreateDTO newPolicy)
        {
            try
            {
                var policyToAdd = new Policy
                {
                    IsActive = true,
                    Policy_Holder = newPolicy.Policy_Holder,
                    Policy_Type = newPolicy.Policy_Type,
                    Policy_Des = newPolicy.Policy_Des,
                    Policy_Benefits = newPolicy.Policy_Benefits,
                    Policy_Date = DateTime.Now.ToString(),
                };

                _context.Policy.Add(policyToAdd);
                _context.SaveChanges();

                //Add the policy admissions info
                _context.Admissions.Add(new Admissions
                {
                    Adms_Doctors = newPolicy.Admissions.Adms_Doctors,
                    Adms_Hospitals = newPolicy.Admissions.Adms_Hospitals,
                    Adms_Type = newPolicy.Admissions.Adms_Type,
                    Policy_Id = policyToAdd.Policy_Id
                });

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

        }

        //Update Policy
        public void UpdatePolicyInformation(PolicyCreateDTO policy, int policyId)
        {
            var updatePolicyObj = _context.Policy.Where(x => x.Policy_Id == policyId && x.IsActive).SingleOrDefault();
            var updateAdmsObj = _context.Admissions.Where(x => x.Policy_Id == policyId).SingleOrDefault();

            if (updatePolicyObj != null && updateAdmsObj != null)
            {
                updatePolicyObj.Policy_Holder = policy.Policy_Holder;
                updatePolicyObj.Policy_Type = policy.Policy_Type;
                updatePolicyObj.Policy_Des = policy.Policy_Des;
                updatePolicyObj.Policy_Benefits = policy.Policy_Benefits;
                updatePolicyObj.Policy_Date = DateTime.Now.ToString();

                _context.Policy.Update(updatePolicyObj);
                _context.SaveChanges();

                updateAdmsObj.Adms_Hospitals = policy.Admissions.Adms_Hospitals;
                updateAdmsObj.Adms_Doctors = policy.Admissions.Adms_Doctors;
                updateAdmsObj.Adms_Type = policy.Admissions.Adms_Type;

                _context.Admissions.Update(updateAdmsObj);
                _context.SaveChanges();
            }

        }

        //Delete Policy
        public void RemovePolicy(int policyId)
        {
            var delObj = _context.Policy.Where(x => x.IsActive && x.Policy_Id == policyId).SingleOrDefault();

            if (delObj != null)
            {
                delObj.IsActive = false;
                _context.Policy.Update(delObj);
                _context.SaveChanges();
            }
        }

        //Get Specific Users Policy / Policies
        public IEnumerable<SpecificUserPolicyDTO> GetSpecificPolicyDetails(int policyId)
        {
            try
            {
                var policyData = (from p in _context.Policy
                                  join a in _context.Admissions on p.Policy_Id equals a.Policy_Id
                                  where p.Policy_Id == policyId
                                  select new SpecificUserPolicyDTO
                                  {
                                      policyId = policyId,
                                      Policy_Type = p.Policy_Type,
                                      Policy_Holder = p.Policy_Holder,
                                      Policy_Date = p.Policy_Date,
                                      Policy_Des = p.Policy_Des,
                                      Policy_Benefits = p.Policy_Benefits,
                                      Adms_Id = a.Adms_Id,
                                      Adms_Type = a.Adms_Type,
                                      Adms_Doctors = a.Adms_Doctors,
                                      Adms_Hospitals = a.Adms_Hospitals
                                  }).ToList();

                return policyData;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
           
        }

        //Get Specific User Policy Information
        public IEnumerable<UserSpecificPoliciesDTO> GetAllSchemaRequests(int userId)
        {
            try
            {
                var qeuryData = (from u in _context.Users
                                 join sr in _context.Schema_Requests on u.User_Id equals sr.User_Id
                                 join p in _context.Policy on sr.Policy_Id equals p.Policy_Id
                                 where u.IsActive == true && u.User_Id == userId
                                 select new UserSpecificPoliciesDTO
                                 {
                                     Policy_Id = sr.Policy_Id,
                                     Policy_Holder = p.Policy_Holder,
                                     Policy_Type = p.Policy_Type,
                                     RequestStatus = ((SchemaRequestStatuses)sr.Status_Id).ToString()
                                 }).ToList();

                return qeuryData;
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
            
        }

    }
}
