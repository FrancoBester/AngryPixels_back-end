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

        public string Role_Name { get; set; }

        public string Role_Des { get; set; }

        public string Role_Code { get; set; }

        //one to many - user_roles
        public ICollection<User_Roles> User_Roles { get; set; }

    }
}
