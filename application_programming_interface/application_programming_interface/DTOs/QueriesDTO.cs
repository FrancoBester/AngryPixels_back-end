using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.DTOs
{
    public class QueriesDTO
    {
        public string Query_Title { get; set; }

        public string Query_Code { get; set; }

        public string Query_Level { get; set; }

        public List<int> User_Id { get; set; }

        public List<string> User_Name { get; set; }
        public List<string> User_Surname { get; set; }
    }
}
