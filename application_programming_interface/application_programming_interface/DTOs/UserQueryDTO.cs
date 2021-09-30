using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.DTOs
{
    public class UserQueryDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<string> Query_Title { get; set; }

        public List<string> Query_Detail { get; set; }
    }
}
