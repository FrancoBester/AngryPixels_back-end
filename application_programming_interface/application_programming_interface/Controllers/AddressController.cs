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

        [Route("~/Address/GetAll")]
        [HttpGet]
        public IEnumerable<Address> GetAll()
        {
            _serve.WriteMsg("Hello");
            return _context.Address.ToList();
        }

        [Route("~/Address/Create")]
        [HttpPost]
        public JsonResult Create([FromBody] Address address)
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

        [Route("~/Address/Update/{id}")]
        [HttpPut("{id}")]
        public JsonResult Update(Address address)
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


        [Route("~/Address/Delete/{id}")]
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
