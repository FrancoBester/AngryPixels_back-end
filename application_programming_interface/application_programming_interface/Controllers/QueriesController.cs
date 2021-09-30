using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using application_programming_interface.DTOs;

namespace application_programming_interface.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueriesController : ControllerBase
    {
        private readonly DataContext _context;

        public QueriesController(DataContext context)
        {
            _context = context;
        }

        [Route("~/Queries/GetAll")]
        [HttpGet]
        public IEnumerable<Queries> Get()
        {
            return _context.Queries.ToList();
        }

        [Route("~/Queries/Create")]
        [HttpPost]
        public JsonResult Post([FromBody] Queries queries)
        {
            try
            {
                _context.Set<Queries>().Add(queries);
                _context.SaveChanges();
                return new JsonResult("Data saved");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }


        [Route("~/Queries/Edit/{id}")]
        [HttpPut("{id}")]
        public JsonResult Put(Queries queries)
        {
            try
            {
                _context.Entry(queries).State = EntityState.Modified;
                _context.SaveChanges();
                return new JsonResult("Data saved");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }

        [Route("~/Queries/Delete/{id}")]
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            try
            {
                _context.Remove(_context.Queries.Single(q => q.Query_Id == id));
                _context.SaveChanges();
                return new JsonResult("Record removed");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.InnerException);
            }
        }

        [Route("~/Queries/GetAdminLoadPage")]
        [HttpGet]
        public IEnumerable<QueriesDTO> GetAdminLoadPageData(int? pageNumber)
        {
            int curPage = pageNumber ?? 1;
            int curPageSize = 10;


            var queryData = (from query in _context.Queries
                             select new QueriesDTO
                             {
                                 Query_Title = query.Query_Title,
                                 Query_Code = query.Query_Code,
                                 Query_Level = query.Query_Level,
                                 User_Id = (from u in _context.Users
                                            where u.Query_Id == query.Query_Id
                                            select u.User_Id
                                                ).ToList(),
                                 User_Name = (from u in _context.Users
                                              where u.Query_Id == query.Query_Id
                                              select u.User_Name
                                                ).ToList(),
                                 User_Surname = (from u in _context.Users
                                                 where u.Query_Id == query.Query_Id
                                                 select u.User_Surname
                                                ).ToList(),
                             }).ToList();

            return queryData.Skip((curPage - 1) * curPageSize).Take(curPageSize);
        }
        //Admin
        //  select
        //      level,code,title,user id/name
        //  Select - user click
        //      all user info
        //  select - tilte click
        //      all querie table
    }
}
