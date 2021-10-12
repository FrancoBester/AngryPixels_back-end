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
        void UpdateUserInformation(Users user, int userId);
        void RemoveUserAccount(int userId);
        IEnumerable<AdminLoadPageDTO> GetAdminLoadPageData(int? pageNumber);
        IEnumerable<AdminLoadPageDTO> SearchLoadPageData(int? pageNumber, string search);
        UserInfoDTO GetUserDetails(int userId);
        PolicyInfoDTO GetPolicyDetails(int policyId);
        IEnumerable<AdmsInfoDTO> GetAdmissionsTypeDetails(int admsId);
        ProfileDTO GetProfileInformation(int id);
    }
}
