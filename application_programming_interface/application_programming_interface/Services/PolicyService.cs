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
        public void UpdatePolicyInformation(Policy policy, int policyId)
        {
            var updatePolicyObj = _context.Policy.Where(x => x.Policy_Id == policyId && x.IsActive).SingleOrDefault();

            if (updatePolicyObj != null)
            {
                updatePolicyObj.Policy_Holder = policy.Policy_Holder;
                updatePolicyObj.Policy_Type = policy.Policy_Type;
                updatePolicyObj.Policy_Des = policy.Policy_Des;
                updatePolicyObj.Policy_Benefits = policy.Policy_Benefits;
                updatePolicyObj.Policy_Date = DateTime.Now.ToString();

                _context.Policy.Update(updatePolicyObj);
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

    }
}
