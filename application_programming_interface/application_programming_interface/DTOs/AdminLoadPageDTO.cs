using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.DTOs
{
    public class AdminLoadPageDTO
    {
        public string User_Name { get; set; }
        public string User_Surname { get; set; }
        public string Policy_Type { get; set; }
        public string Role_Name { get; set; }
    }
}
