using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace application_programming_interface.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchemaRequestsController : ControllerBase
    {
        private readonly DataContext _context;

        public SchemaRequestsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Schema_Requests> Get()
        {
            return _context.Schema_Requests.ToList();
        }

        [HttpPost]
        public JsonResult Post([FromBody] Schema_Requests schema_Requests)
        {
            try
            {
                _context.Set<Schema_Requests>().Add(schema_Requests);
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
        public JsonResult Put(Schema_Requests schema_Requests)
        {
            try
            {
                _context.Entry(schema_Requests).State = EntityState.Modified;
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
                _context.Remove(_context.Schema_Requests.Single(sr => sr.Request_Id == id));
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
