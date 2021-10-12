using application_programming_interface.DTOs;
using application_programming_interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.Interfaces
{
    public interface ISchemaRequestService
    {
        IEnumerable<SpecificUserPolicyDTO> GetSpecificUserPolicyDetails(int userId);
        IEnumerable<AllPoliciesDTO> GetAllPolicies();
        public void RequestToJoinSchema(int userId, Schema_Requests newRequest);
        IEnumerable<SchemaRequestDTO> GetAllSchemaRequests(int? pageNumber);
    }
}
