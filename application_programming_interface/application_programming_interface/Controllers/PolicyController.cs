using application_programming_interface.Interfaces;
using application_programming_interface.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolicyController : Controller
    {
        private readonly IPolicyService _policyService;
        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        //Allow users to create queries
        [Route("~/api/Queries/CreatePolicy")]
        [HttpPost]
        public JsonResult CreatePolicy(Policy newPolicy)
        {
            try
            {
                _policyService.CreatePolicy(newPolicy);

                return new JsonResult("Policy added");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

        }

        //Update Policy
        [Route("~/api/Policy/UpdatePolicyInformation/{policyId}")]
        [HttpPut("{policyId}")]
        public JsonResult UpdatePolicyInformation(Policy policy, int policyId)
        {
            try
            {
                _policyService.UpdatePolicyInformation(policy, policyId);

                return new JsonResult("Policy saved");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }

        //Delete Policy
        [Route("~/api/Policy/RemovePolicy/{policyId}")]
        [HttpPut("{policyId}")]
        public JsonResult RemovePolicy(int policyId)
        {
            try
            {
                _policyService.RemovePolicy(policyId);

                return new JsonResult("Policy removed");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }

    }
}
