using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace application_programming_interface.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionTestController : ControllerBase
    {
        // GET: api/<ConnectionTestController>
        [HttpGet]
        [Route("~/api/ConnectionTest/test")]
        public JsonResult Get()
        {
            return new JsonResult(new { value = "Sucsess" });
        }

    }
}
