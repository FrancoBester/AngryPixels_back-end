using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace application_programming_interface.Models
{
    public class Roles
    {
        [Key]
        public int Role_id { get; set;}

        public string Role_name { get; set; }

        public string Role_des { get; set; }

        public string Role_code { get; set; }

    }
}
