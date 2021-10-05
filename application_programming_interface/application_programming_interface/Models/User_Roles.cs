using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace application_programming_interface.Models
{
    public class User_Roles
    {
        [Key]
        public int User_Roles_Id { get; set; }
        public int Role_Id { get; set; }
        public int User_Id { get; set; }


        //many to one - roles
        public virtual Roles Role { get; set; }

        //many to one - users
        public virtual Users User { get; set; }
    }
}
