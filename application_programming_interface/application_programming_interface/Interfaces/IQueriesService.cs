using application_programming_interface.DTOs;
using application_programming_interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.Interfaces
{
    public interface IQueriesService
    {
        IEnumerable<SpecificUserQueriesDTO> GetSpecificUserQueries(int? pageNumber, int userId);
        void CreateQuery(int userId, Queries newQuery);
        IEnumerable<AllUserQueriesDTO> GetAllQueries(int? pageNumber);
        IEnumerable<AllUserQueriesDTO> SearchAllUserQueries(int? pageNumber, string search);
        IEnumerable<AllUserQueriesDTO> GetQueriesByStatus(int? pageNumber, int statusId);
        IEnumerable<QueryDetailsDTO> GetQueryDetails(int queryId);

    }
}
