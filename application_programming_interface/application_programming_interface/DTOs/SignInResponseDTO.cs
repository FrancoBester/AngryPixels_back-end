using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.DTOs
{
    public class SignInResponseDTO
    {
        public string Token;
        public List<string> Roles;
    }
}
