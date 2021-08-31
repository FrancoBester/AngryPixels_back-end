using application_programming_interface.Atributes;
using application_programming_interface.Interfaces;
using application_programming_interface.Models;
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

        private IAuthenticationService _authentication;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authentication = authenticationService;
        }


        // GET: api/<AuthenticationController>
        [HttpPost]
        public JsonResult LogIn(SignInRequestDTO requestDTO)
        {
            //generate token
            //return token or throw exeption
            try
            {
                return new JsonResult(new { Token = _authentication.SignIn(requestDTO) });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
