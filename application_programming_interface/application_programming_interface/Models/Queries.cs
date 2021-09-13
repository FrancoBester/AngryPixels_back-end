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

        public string Query_des { get; set; }

        public string Query_code { get; set; }

        public string Query_level { get; set; }

        //one to many - users
        public ICollection<Users> Users { get; set; }

    }
}
