using application_programming_interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.DTOs
{
    public class AdminLoadPageDTO
    {
        public int UserId{ get; set; }
        public int PolicyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }
        public string PolicyName { get; set; }
        //public List<string> Roles { get; set; }
        //public List<string> Policies { get; set; }
    }
}
