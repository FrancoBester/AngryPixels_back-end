using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.Models;
using Microsoft.EntityFrameworkCore;

namespace application_programming_interface.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly DataContext _context;

        public DocumentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public  IEnumerable<Document> Get()
        {
            //var teset = _context.Document.FromSqlRaw("GetAllDocument").ToList(); //method to use stored procedures in api

            //int id_test = 1;
            //var test = _context.Document.Where(e => e.Doc_id == id_test).Select(e => e).SingleOrDefault(); //example of link and lambda statments in api

            return _context.Document.ToList();
        }

        [HttpPost]
        public JsonResult Post(Document doc)
        {
            try
            {
                Console.WriteLine(doc);
                _context.Add<Document>(doc);
                _context.SaveChanges();
                return new JsonResult("Data added");
            }
            catch( Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            
        }

        [Route("~/{id}")]
        [HttpPut("{id}")]
        public JsonResult Put(Document document)
        {
            try
            {
                _context.Entry(document).State = EntityState.Modified;
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
                _context.Remove(_context.Document.Single(d => d.Doc_Id == id));
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
