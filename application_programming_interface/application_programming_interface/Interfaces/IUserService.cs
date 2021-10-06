using application_programming_interface.DTOs;
using application_programming_interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(UserRegisterDTO user);
        IEnumerable<UserQueryDTO> GetUserLoadPageData(int? pageNumber, int id);
        void UpdateUserInformation(Users user, int userId);
        void RemoveUserAccount(int userId);
        IEnumerable<AdminLoadPageDTO> GetAdminLoadPageData(int? pageNumber);
        IEnumerable<AdminLoadPageDTO> SearchLoadPageData(int? pageNumber, string search);
        IEnumerable<UserInfoDTO> GetUserDetails(int userId);
        IEnumerable<PolicyInfoDTO> GetPolicyDetails(int policyId);
        IEnumerable<AdmsInfoDTO> GetAdmsTypeDetails(int admsId);
    }
}
