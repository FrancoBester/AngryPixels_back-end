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
    public class DocumentTypeController : ControllerBase
    {
        private readonly DataContext _context;

        public DocumentTypeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Document_Type> Get()
        {
            return _context.Document_Type.ToList();
        }

        [HttpPost]
        public JsonResult Post([FromBody] Document_Type document_Type)
        {
            try
            {
                _context.Set<Document_Type>().Add(document_Type);
                _context.SaveChanges();
                return new JsonResult("Data saved");
            }
            catch( Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }

        [Route("~/{id}")]
        [HttpPut("{id}")]
        public JsonResult Put(Document_Type document_Type)
        {
            try
            {
                _context.Entry(document_Type).State = EntityState.Modified;
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
                _context.Remove(_context.Document_Type.Single(dt => dt.DocType_Id == id));
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
