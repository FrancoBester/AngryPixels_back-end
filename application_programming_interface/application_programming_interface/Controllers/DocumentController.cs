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

        [Route("~/Document/GetAll")]
        [HttpGet]
        public  IEnumerable<Document> GetAll()
        {
            //var teset = _context.Document.FromSqlRaw("GetAllDocument").ToList(); //method to use stored procedures in api

            //int id_test = 1;
            //var test = _context.Document.Where(e => e.Doc_id == id_test).Select(e => e).SingleOrDefault(); //example of link and lambda statments in api

            return _context.Document.ToList();
        }

        [Route("~/Document/Create")]
        [HttpPost]
        public JsonResult Create(Document doc)
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

        [Route("~/Document/Update/{id}")]
        [HttpPut("{id}")]
        public JsonResult Update(Document document)
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


        [Route("~/Document/Delete/{id}")]
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
