using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace application_programming_interface.Models
{
    public class Queries
    {
        [Key]
        public int Query_id { get; set; }

        public string Query_Title { get; set; }

        public string Query_Detail { get; set; }

        public string Query_Code { get; set; }

        public string Query_Level { get; set; }

        //one to many - users
        public ICollection<Users> Users { get; set; }

    }
}
