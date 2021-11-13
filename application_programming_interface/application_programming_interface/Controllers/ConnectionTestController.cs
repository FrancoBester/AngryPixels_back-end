using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.Atributes;
using application_programming_interface.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace application_programming_interface.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExceptionFilter]
    public class ConnectionTestController : ControllerBase
    {
        private IAuthenticationService _auth;

        public ConnectionTestController(IAuthenticationService authentication)
        {
            _auth = authentication;
        }

        // GET: api/<ConnectionTestController>
        [HttpGet]
        [Route("~/api/ConnectionTest/test")]
        public JsonResult Get()
        {
            return new JsonResult(new { value = "Sucsess" });
        }

        [HttpGet]
        [Route("~/api/ConnectionTest/Authed")]
        [Authentication(new string[] { "Customer" })]
        public JsonResult GetAuthed()
        {
            return new JsonResult(new { value = "Sucsess",testValue = _auth.GetUser()});
        }
    }
}
