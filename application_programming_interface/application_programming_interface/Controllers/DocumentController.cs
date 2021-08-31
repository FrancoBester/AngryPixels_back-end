using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.Models;

namespace application_programming_interface.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly ILogger<DocumentController> _logger;
        private readonly DataContext _context;

        //public DocumentController(ILogger<DocumentController> logger)
        //{
        //    _logger = logger;
        //}

       

        public DocumentController(DataContext context)
        {
            _context = context;
        }

        //public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()1 

        [HttpGet]
        public  List <Document> Get()
        {
            return _context.Document.ToList();
        }
    }
    
}
