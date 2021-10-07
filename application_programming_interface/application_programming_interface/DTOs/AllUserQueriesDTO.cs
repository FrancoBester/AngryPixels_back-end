using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.DTOs
{
    public class AllUserQueriesDTO
    {
        public int Query_Id { get; set; }
        public string Query_Title { get; set; }
        public string Query_Code { get; set; }
        public int Query_Level { get; set; }
        public int User_Id { get; set; }
        public string User_Name { get; set; }
    }
}
