using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.DTOs;
using application_programming_interface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


        //Admin
        //  select
        //      level,code,title,user id/name
        //  Select - user click
        //      all user info
        //  select - tilte click
        //      all querie table


        #region User Dashboard Query Functionalities

        //Allow users to view all of their own queries
        //QueryId --> When clicked user can view that queries details
        [Route("~/Queries/GetSpecificUserQueries/{userId}")]
        [HttpGet("{userId}")]
        public IEnumerable<SpecificUserQueriesDTO> GetSpecificUserQueries(int? pageNumber, int userId)
        {

            //Pagination
            int curPage = pageNumber ?? 1;
            int curPageSize = 20;

            //Query for needed info
            var qeuryData = (from u in _context.Users
                             join uq in _context.Queries on u.User_Id equals uq.User_Id
                             where u.User_Id == userId
                             select new SpecificUserQueriesDTO
                             {
                                 Query_Id = uq.Query_Id,
                                 Query_Title = uq.Query_Title,
                                 Query_Level = uq.Query_Level
                             }).ToList();

            return qeuryData.Skip((curPage - 1) * curPageSize).Take(curPageSize);
        }

        //TODO:
            //Allow user to post query
            //Allow user to remove query


        #endregion


        #region Admin Dashboard Query Functionalities

        //Allow admins to view queries posted by all the users with pagination
        [Route("~/Queries/GetAllUserQueries")]
        [HttpGet]
        public IEnumerable<AllUserQueriesDTO> GetAllUserQueries(int? pageNumber)
        {

            //Pagination
            int curPage = pageNumber ?? 1;
            int curPageSize = 20;

            //Query for needed info
            var qeuryData = (from u in _context.Users
                             join uq in _context.Queries on u.User_Id equals uq.User_Id
                             select new AllUserQueriesDTO
                             {
                                 Query_Id = uq.Query_Id,
                                 Query_Level = uq.Query_Level,
                                 Query_Code = uq.Query_Code,
                                 Query_Title = uq.Query_Title,
                                 User_Id = u.User_Id,
                                 User_Name = u.User_Name
                             }).ToList();

            return qeuryData.Skip((curPage - 1) * curPageSize).Take(curPageSize);
        }

        //Allow admins to search for any of the fields in the Queries Table
        [Route("~/Queries/SearchAllUserQueries")]
        [HttpGet]
        public IEnumerable<AllUserQueriesDTO> SearchAllUserQueries(int? pageNumber, string search)
        {

            //Pagination
            int curPage = pageNumber ?? 1;
            int curPageSize = 20;

            //Query for needed info
            var qeuryData = (from u in _context.Users
                             join uq in _context.Queries on u.User_Id equals uq.User_Id
                             where uq.Query_Level.ToUpper().Contains(search.ToUpper()) ||
                                   uq.Query_Code.ToUpper().Contains(search.ToUpper()) ||
                                   uq.Query_Title.ToUpper().Contains(search.ToUpper()) ||
                                   u.User_Name.ToUpper().Contains(search.ToUpper())
                             select new AllUserQueriesDTO
                             {
                                 Query_Id = uq.Query_Id,
                                 Query_Level = uq.Query_Level,
                                 Query_Code = uq.Query_Code,
                                 Query_Title = uq.Query_Title,
                                 User_Id = u.User_Id,
                                 User_Name = u.User_Name
                             }).ToList();


            return qeuryData.Skip((curPage - 1) * curPageSize).Take(curPageSize);
        }

        //Allow admins to view the details of the chosen query
        //Allows users to view the details of their own query
        [Route("~/Queries/GetQueryDetails/{queryId}")]
        [HttpGet("{queryId}")]
        public IEnumerable<QueryDetailsDTO> GetQueryDetails(int queryId)
        {
            //Query for needed info
            var qeuryData = (from uq in _context.Queries
                            where uq.Query_Id == queryId
                            select new QueryDetailsDTO
                            {
                                Query_Id = uq.Query_Id,
                                Query_Title = uq.Query_Title,
                                Query_Level = uq.Query_Level,
                                Query_Code = uq.Query_Code,
                                Query_Detail = uq.Query_Detail
                            }).ToList();


            return qeuryData;
        }

        //TODO: (Need DB changes gotta check which ones)
            //Medical Scheme Request Review Page
                //-->Retreive all schema requests (Name, Surname, RequestID, PolicyType)
                //-->Allow admin to accept schema request
                //-->Allow admin to reject schema request with alternatives

        #endregion
    }
}
