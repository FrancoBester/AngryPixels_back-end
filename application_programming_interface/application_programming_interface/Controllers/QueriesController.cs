using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.DTOs;
using application_programming_interface.Interfaces;
using application_programming_interface.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace application_programming_interface.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueriesController : ControllerBase
    {
        private readonly IQueriesService _queriesService;

        public QueriesController(IQueriesService queriesService)
        {
            _queriesService = queriesService;
        }


        #region User Dashboard Query Functionalities

        //Allow users to view all of their own queries
        //QueryId --> When clicked user can view that queries details
        [Route("~/api/Queries/GetSpecificUserQueries/{userId}")]
        [HttpGet("{userId}")]
        public IEnumerable<SpecificUserQueriesDTO> GetSpecificUserQueries(int? pageNumber, int userId)
        {

            return _queriesService.GetSpecificUserQueries(pageNumber, userId);
        }

        //Allow users to create queries
        [Route("~/api/Queries/CreateQuery/{userId}")]
        [HttpPost("{userId}")]
        public JsonResult CreateQuery(int userId, Queries newQuery)
        {
            try
            {
                _queriesService.CreateQuery(userId, newQuery);

                return new JsonResult("Query added");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
            
        }

        #endregion

        #region Admin Dashboard Query Functionalities

        //Retreives all Queries
        [Route("~/api/Queries/GetAllQueries")]
        [HttpGet]
        public IEnumerable<AllUserQueriesDTO> GetAllQueries(int? pageNumber)
        {
            return _queriesService.GetAllQueries(pageNumber);
        }

        //Allow admins to search for any of the fields in the Queries Table
        [Route("~/api/Queries/SearchAllUserQueries")]
        [HttpGet]
        public IEnumerable<AllUserQueriesDTO> SearchAllUserQueries(int? pageNumber, string search)
        {
            return _queriesService.SearchAllUserQueries(pageNumber, search);
        }

        //Retreives Queries by ID
        // 1 --> Gets all Unresolved Queries
        // 2 --> Gets all Active Queries
        // 3 --> Gets all Resolved Queries
        [Route("~/api/Queries/GetQueriesByStatus")]
        [HttpGet]
        public IEnumerable<AllUserQueriesDTO> GetQueriesByStatus(int? pageNumber, int statusId)
        {
            return _queriesService.GetQueriesByStatus(pageNumber, statusId);
        }

        //Allow admins to view the details of the chosen query
        //Allows users to view the details of their own query
        [Route("~/api/Queries/GetQueryDetails/{queryId}")]
        [HttpGet("{queryId}")]
        public IEnumerable<QueryDetailsDTO> GetQueryDetails(int queryId)
        {
            return _queriesService.GetQueryDetails(queryId);
        }

        //TODO: (Need DB changes gotta check which ones)
        //Medical Scheme Request Review Page
        //-->Retreive all schema requests (Name, Surname, RequestID, PolicyType)
        //-->Allow admin to accept schema request
        //-->Allow admin to reject schema request with alternatives

        #endregion

        #region Maybe
        ////Retreives all Resolved Queries (DONT HAVE TO WORRY ABOUT IT ANYMORE)
        //[Route("~/Queries/GetResolvedQueries")]
        //[HttpGet]
        //public IEnumerable<AllUserQueriesDTO> GetResolvedQueries(int? pageNumber)
        //{
            
        //}

        ////Retreives all Unresolved Queries (NEEDS TO HAVE AN EMPLOYEE ASSIGNED STILL)
        //[Route("~/Queries/GetUnresolvedQueries")]
        //[HttpGet]
        //public IEnumerable<AllUserQueriesDTO> GetUnresolvedQueries(int? pageNumber)
        //{

        //}

        ////Retreives all Active Queries (CURRENTLY BEING ADDRESSED BY EMPLOYEE)
        //[Route("~/Queries/GetActiveQueries")]
        //[HttpGet]
        //public IEnumerable<AllUserQueriesDTO> GetActiveQueries(int? pageNumber)
        //{

        //}

        #endregion
    }
}
