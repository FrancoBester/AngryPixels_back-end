using application_programming_interface.DTOs;
using application_programming_interface.Interfaces;
using application_programming_interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace application_programming_interface.Services
{
    public class QueriesService : IQueriesService
    {
        private readonly DataContext _context;

        public QueriesService(DataContext context)
        {
            _context = context;
        }

        enum QueryStatuses
        {
            Unresolved = 1,
            Active = 2,
            Resolved = 3
        }

        #region User Dashboard Query Functionalities

        public string GenerateQueryCode()
        {
            //Add maybe check vir duplicates in DB
            StringBuilder builder = new StringBuilder();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(6)
                .ToList().ForEach(e => builder.Append(e));
            string RefCode = builder.ToString();

            return RefCode;
        }

        //Allow users to view all of their own queries
        //QueryId --> When clicked user can view that queries details
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
                                 Query_Status = ((QueryStatuses)uq.Status_Id).ToString(),
                                 Assistant_Name = uq.Assistant_Name
                             }).ToList();

            return qeuryData.Skip((curPage - 1) * curPageSize).Take(curPageSize);
        }

        //Allow users to create queries
        public void CreateQuery(int userId, Queries newQuery)
        {
                var queryToAdd = new Queries
                {
                    User_Id = userId,
                    Status_Id = 1,
                    Assistant_Name = "None",
                    Query_Title = newQuery.Query_Title,
                    Query_Level = newQuery.Query_Level,
                    Query_Detail = newQuery.Query_Detail,
                    Query_Code = GenerateQueryCode()
                };

                _context.Queries.Add(queryToAdd);
                _context.SaveChanges();
        }

        //Allow users to mark their query as resolved (Problem has been solved)
        public void MarkQueryAsResolved(int queryId)
        {
            var updateQueryObj= _context.Queries.Where(x => x.Query_Id == queryId).SingleOrDefault();

            if (updateQueryObj != null)
            {
                updateQueryObj.Status_Id = 3;

                _context.Queries.Update(updateQueryObj);
                _context.SaveChanges();
            }
        }

        #endregion

        #region Admin Dashboard Query Functionalities

        //Retreives all Queries
        public IEnumerable<AllUserQueriesDTO> GetAllQueries(int? pageNumber)
        {

            //Pagination
            int curPage = pageNumber ?? 1;
            int curPageSize = 20;

            //Query for needed info
            var qeuryData = (from u in _context.Users
                             join uq in _context.Queries on u.User_Id equals uq.User_Id
                             where u.IsActive
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

        //Allow admins/employees to search for any of the fields in the Queries Table
        public IEnumerable<AllUserQueriesDTO> SearchAllUserQueries(int? pageNumber, string search)
        {

            //Pagination
            int curPage = pageNumber ?? 1;
            int curPageSize = 20;

            //Query for needed info
            var qeuryData = (from u in _context.Users
                             join uq in _context.Queries on u.User_Id equals uq.User_Id
                             where u.IsActive &&
                                  (uq.Query_Level).ToString() == search ||
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

        //Retreives Queries by ID
        // 1 --> Gets all Unresolved Queries
        // 2 --> Gets all Active Queries
        // 3 --> Gets all Resolved Queries
        public IEnumerable<AllUserQueriesDTO> GetQueriesByStatus(int? pageNumber, int statusId)
        {

            //Pagination
            int curPage = pageNumber ?? 1;
            int curPageSize = 20;

            //Query for needed info
            var qeuryData = (from u in _context.Users
                             join uq in _context.Queries on u.User_Id equals uq.User_Id
                             where uq.Status_Id == statusId && u.IsActive
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

        //Allow admins/employee to view the details of the chosen query
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
                                 Query_Detail = uq.Query_Detail,
                                 Query_Status = ((QueryStatuses)uq.Status_Id).ToString(),
                                 Assistant_Name = uq.Assistant_Name
                             }).ToList();


            return qeuryData;
        }

        //Allow employees to be assigned to a query
        public void AssignEmployeeToQuery(int empId, int queryId)
        {
            var assingQueryObj = _context.Queries.Where(x => x.Query_Id == queryId).SingleOrDefault();
            var employeeObj = _context.Users.Where(x => x.User_Id == empId && x.IsActive).SingleOrDefault();

            if (assingQueryObj != null && employeeObj != null)
            {
                assingQueryObj.Assistant_Name = $"{employeeObj.User_Name} {employeeObj.User_Surname}";
                assingQueryObj.Status_Id = 2;

                _context.Queries.Update(assingQueryObj);
                _context.SaveChanges();
            };
        }

        #endregion

        #region Maybe
        //Retreives all Resolved Queries (DONT HAVE TO WORRY ABOUT IT ANYMORE)
        //public IEnumerable<AllUserQueriesDTO> GetResolvedQueries(int? pageNumber)
        //{

        //    //Pagination
        //    int curPage = pageNumber ?? 1;
        //    int curPageSize = 20;

        //    //Query for needed info
        //    var qeuryData = (from u in _context.Users
        //                     join uq in _context.Queries on u.User_Id equals uq.User_Id
        //                     where uq.Status_Id == 3
        //                     select new AllUserQueriesDTO
        //                     {
        //                         Query_Id = uq.Query_Id,
        //                         Query_Level = uq.Query_Level,
        //                         Query_Code = uq.Query_Code,
        //                         Query_Title = uq.Query_Title,
        //                         User_Id = u.User_Id,
        //                         User_Name = u.User_Name
        //                     }).ToList();

        //    return qeuryData.Skip((curPage - 1) * curPageSize).Take(curPageSize);
        //}

        ////Retreives all Unresolved Queries (NEEDS TO HAVE AN EMPLOYEE ASSIGNED STILL)
        //public IEnumerable<AllUserQueriesDTO> GetUnresolvedQueries(int? pageNumber)
        //{

        //    //Pagination
        //    int curPage = pageNumber ?? 1;
        //    int curPageSize = 20;

        //    //Query for needed info
        //    var qeuryData = (from u in _context.Users
        //                     join uq in _context.Queries on u.User_Id equals uq.User_Id
        //                     where uq.Status_Id == 1
        //                     select new AllUserQueriesDTO
        //                     {
        //                         Query_Id = uq.Query_Id,
        //                         Query_Level = uq.Query_Level,
        //                         Query_Code = uq.Query_Code,
        //                         Query_Title = uq.Query_Title,
        //                         User_Id = u.User_Id,
        //                         User_Name = u.User_Name
        //                     }).ToList();

        //    return qeuryData.Skip((curPage - 1) * curPageSize).Take(curPageSize);
        //}

        ////Retreives all Active Queries (CURRENTLY BEING ADDRESSED BY EMPLOYEE)
        //public IEnumerable<AllUserQueriesDTO> GetActiveQueries(int? pageNumber)
        //{

        //    //Pagination
        //    int curPage = pageNumber ?? 1;
        //    int curPageSize = 20;

        //    //Query for needed info
        //    var qeuryData = (from u in _context.Users
        //                     join uq in _context.Queries on u.User_Id equals uq.User_Id
        //                     where uq.Status_Id == 2
        //                     select new AllUserQueriesDTO
        //                     {
        //                         Query_Id = uq.Query_Id,
        //                         Query_Level = uq.Query_Level,
        //                         Query_Code = uq.Query_Code,
        //                         Query_Title = uq.Query_Title,
        //                         User_Id = u.User_Id,
        //                         User_Name = u.User_Name
        //                     }).ToList();

        //    return qeuryData.Skip((curPage - 1) * curPageSize).Take(curPageSize);
        //}

        #endregion
    }
}
