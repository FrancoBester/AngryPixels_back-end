using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.Models;
using application_programming_interface.Services;

namespace application_programming_interface.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAddressService _serve;
        public AddressController(DataContext context, IAddressService addressService)
        {
            _serve = addressService;
            _context = context;
        }

        [HttpGet]
        public  List <Address> Get()
        {
            _serve.WriteMsg("Hello");
            return _context.Address.ToList();
        }
    }
    
}
