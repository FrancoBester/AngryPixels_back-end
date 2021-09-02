using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace application_programming_interface.Models
{
    public class User_Type
    {
        [Key]
        public int User_Type_id { get; set; }

        public string User_Type_name { get; set; }

        public string User_Type_des { get; set; }

        // many to one - roles
        public virtual Roles Roles { get; set; }
    }
}
