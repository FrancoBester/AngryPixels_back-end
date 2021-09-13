using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.Models
{
    public class Address
    {
        [Key]
        public int Addr_id { get; set; }

        public string Addr_street { get; set; }

        public string Addr_city { get; set; }

        public string Addr_code { get; set; }

        //one to many - users
        public ICollection<Users> Users { get; set; }
    }
}
