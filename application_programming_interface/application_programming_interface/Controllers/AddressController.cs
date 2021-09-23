using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.Models;
using application_programming_interface.Services;
using Microsoft.EntityFrameworkCore;


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
        public IEnumerable<Address> Get()
        {
            _serve.WriteMsg("Hello");
            return _context.Address.ToList();
        }

        [HttpPost]
        public JsonResult Post([FromBody] Address address)
        {
            try
            {
                _context.Set<Address>().Add(address);
                _context.SaveChanges();
                return new JsonResult("Data saved");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }


        [Route("~/{id}")]
        [HttpPut("{id}")]
        public JsonResult Put(Address address)
        {
            try
            {
                _context.Entry(address).State = EntityState.Modified;
                _context.SaveChanges();
                return new JsonResult("Data saved");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }


        [Route("~/{id}")]
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            try
            {
                _context.Remove(_context.Address.Single(a => a.Address_Id == id));
                _context.SaveChanges();
                return new JsonResult("Record removed");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }
    }
    
}
