using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.DTOs
{
    public class QueryDetailsDTO
    {
        public int Query_Id { get; set; }
        public string Query_Title { get; set; }
        public string Query_Detail { get; set; }
        public string Query_Code { get; set; }
        public string Query_Level { get; set; }
    }
}
