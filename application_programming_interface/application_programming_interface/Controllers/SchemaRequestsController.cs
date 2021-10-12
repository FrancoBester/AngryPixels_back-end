using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.Atributes;
using application_programming_interface.DTOs;
using application_programming_interface.Interfaces;
using application_programming_interface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace application_programming_interface.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchemaRequestsController : ControllerBase
    {
        private readonly ISchemaRequestService _schemaRequestService;
        private IAuthenticationService _authentication;

        public SchemaRequestsController(ISchemaRequestService schemaRequestService, IAuthenticationService authenticationService)
        {
            _schemaRequestService = schemaRequestService;
            _authentication = authenticationService;
        }

        //----------------------------------------------------------------------------
        // NOTE --> GetPolicyDetails AND GetAdmissionsTypeDetails in UserController
        //----------------------------------------------------------------------------

        #region Client Policy/Schema Request Related Queries
        //View Own Policy
        [Route("~/api/SchemaRequests/GetSpecificUserPolicyDetails/{userId}")]
        [HttpGet("{userId}")]
        public IEnumerable<SpecificUserPolicyDTO> GetSpecificUserPolicyDetails(int userId)
        {
            var PolicyInfo = _schemaRequestService.GetSpecificUserPolicyDetails(userId);

            if (PolicyInfo == null)
            {
                return null;
            }
            else
            {
                return PolicyInfo;
            }
             
        }

        //View All Joinable Policies
                //PolicyType Click --> GetPolicyDetails
                //AdmsType Click --> GetAdmissionsTypeDetails
        [Route("~/api/SchemaRequests/GetAllPolicies")]
        [HttpGet]
        public IEnumerable<AllPoliciesDTO> GetAllPolicies()
        {
            return _schemaRequestService.GetAllPolicies();

        }

        //Allow Users tp Apply to a New Policy
                //Receive PolicyId from front-end on GetAllPolicies Page
        [Route("~/api/SchemaRequests/RequestToJoinSchema/{PolicyId}")]
        [HttpPost("{userId}")]
        [Authentication]
        public JsonResult RequestToJoinSchema(int PolicyId)
        {
            try
            {
                _schemaRequestService.RequestToJoinSchema(PolicyId, _authentication.GetUser().Id);

                return new JsonResult("Request Received.");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

        }

        //Request to Upgrade to new Policy
        //Request to Downgrade to new Policy
        //Cancel Policy

        #endregion

        #region Admin/Employee Policy/Schema Request Related Queries
        //Retreives all Schema Requests
        //UserName or UserSurname Click --> GetUserDetails
        //PolicyType Click --> GetPolicyDetails
        [Route("~/api/SchemaRequests/GetAllSchemaRequests")]
        [HttpGet]
        public IEnumerable<SchemaRequestDTO> GetAllSchemaRequests(int? pageNumber)
        {
            return _schemaRequestService.GetAllSchemaRequests(pageNumber);
        }

        [Route("~/api/SchemaRequests/ViewUserSchemarequest/{requestId}")]
        [HttpGet]
        [Authentication]
        public JsonResult ViewUserSchemarequest(int requestId)
        {
            try
            {
                _schemaRequestService.GetUserSchemaRequest(requestId);

                return new JsonResult("Request Received.");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }

        }

        //Decline Client Schema Request
        //Approve Client Schema Request
        #endregion
    }
}
