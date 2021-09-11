using application_programming_interface.DTOs;
using application_programming_interface.Models;

namespace application_programming_interface.Interfaces
{
    public interface IAuthenticationService
    {
        string SignIn(SignInRequestDTO requestDTO);
        UserDescriptorDTO GetUser();
    }
}