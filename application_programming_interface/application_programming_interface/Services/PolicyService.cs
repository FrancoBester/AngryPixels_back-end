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
        public void CreatePolicy(Policy newPolicy)
        {
            var policyToAdd = new Policy
            {
                Policy_Holder = newPolicy.Policy_Holder,
                Policy_Type = newPolicy.Policy_Type,
                Policy_Des = newPolicy.Policy_Des,
                Policy_Benefits = newPolicy.Policy_Benefits,
                Policy_Date = DateTime.Now.ToString()
            };

            _context.Policy.Add(policyToAdd);
            _context.SaveChanges();
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
