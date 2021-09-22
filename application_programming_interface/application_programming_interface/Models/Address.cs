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
        public int Address_id { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Postal_Code { get; set; }

        //one to many - users
        public ICollection<Users> Users { get; set; }
    }
}
