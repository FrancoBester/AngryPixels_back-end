using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.Models;

namespace application_programming_interface.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PolicyController : ControllerBase
    {
        private readonly DataContext _context;

        public PolicyController(DataContext context)
        {
            //var test = _context.Policy.Select(x => x.Users).First();
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Policy> Get()
        {
            return _context.Policy.ToList();
        }

        [HttpPost]
        public JsonResult Post(Policy policy)
        {
            try
            {
                _context.Add<Policy>(policy);
                _context.SaveChanges();
                return new JsonResult("data saved");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }
    }
}
