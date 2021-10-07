using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.Models;
using Microsoft.EntityFrameworkCore;
using application_programming_interface.Interfaces;
using Microsoft.AspNetCore.Http;
using application_programming_interface.DTOs;

namespace application_programming_interface.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IBlobStorageService _blobStorage;

        public DocumentController(DataContext context, IBlobStorageService blobStorage)
        {
            _context = context;
            _blobStorage = blobStorage;
        }

        //[Route("~/Document/GetAll")]
        //[HttpGet]
        //public BinaryData GetAll()
        //{
        //    #region
        //    //var teset = _context.Document.FromSqlRaw("GetAllDocument").ToList(); //method to use stored procedures in api

        //    //int id_test = 1;
        //    //var test = _context.Document.Where(e => e.Doc_id == id_test).Select(e => e).SingleOrDefault(); //example of link and lambda statments in api

        //    //return _context.Document.ToList();
        //    #endregion
        //    var doc = _blobStorage.GetDocument();
        //    return doc;
        //}

        [Route("~/api/Document/UploadDoc")]
        [HttpPost]
        public void OnPostUpload([FromForm]FileDTO file)
        {
            _blobStorage.UploadDocument(file);
        }

        [Route("~/api/Document/UploadDocForUser")]
        [HttpPost]
        public void UploadDocumentForUser([FromForm] UserDocumentUploadDTO file)
        {
            _blobStorage.UploadDocumentForUser(file);
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
