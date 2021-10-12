using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.Atributes;
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

        public SchemaRequestsController(ISchemaRequestService schemaRequestService)
        {
            _schemaRequestService = schemaRequestService;
        }

        //----------------------------------------------------------------------------
        // NOTE --> GetPolicyDetails AND GetAdmissionsTypeDetails in UserController
        //----------------------------------------------------------------------------

        //Admin - medcial scheme request review page
        //  Select
        //      user name, surname, request id, policy type
        //  Select - expand click
        //      all user info, policy request info 
        //  Update - accept
        //      user details update with policy  
        //  Delete - reject - email alternatives
        //      schema_request, 
        //
    }
}
