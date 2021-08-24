using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Test> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Test
            {
                id = 1,
                name = "Franco"
            })
            .ToArray();
        }
    }
}
