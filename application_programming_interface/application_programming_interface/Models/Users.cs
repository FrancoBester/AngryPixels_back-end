using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace application_programming_interface.Models
{
    public class Users
    {
        [Key]
        public int User_id { get; set; }

        public string User_name { get; set; }

        public string User_surname { get; set; }

        public string User_ID_Number { get; set; }

        public int Address_id { get; set; }
    }
}
