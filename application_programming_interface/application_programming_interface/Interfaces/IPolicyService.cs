using application_programming_interface.DTOs;
using application_programming_interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.Interfaces
{
    public interface IPolicyService
    {
        void CreatePolicy(PolicyCreateDTO newPolicy);
        void UpdatePolicyInformation(Policy policy, int policyId);
        void RemovePolicy(int policyId);
    }
}
