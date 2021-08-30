using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.Models
{
    public class SignInRequestDTO
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
