using application_programming_interface.Atributes;
using application_programming_interface.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace application_programming_interface.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        // GET: api/<AuthenticationController>
        [HttpPost]
        public JsonResult LogIn([FromBody]SignInRequestDTO requestDTO)
        {
            //generate token
            //return token or throw exeption
            try
            {
                return new JsonResult(new { });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
