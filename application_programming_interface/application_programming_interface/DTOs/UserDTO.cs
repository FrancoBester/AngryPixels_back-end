using application_programming_interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.DTOs
{
    public class UserDTO
    {
        //--------------------------------
        //VIR EXAMPLE MOENI DELETE NIE
        //--------------------------------
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //Vir 1 item uit table
        public List<string> Roles { get; set; }

        //Vir hele table
        public List<Policy> Policies { get; set; }
    }
}
