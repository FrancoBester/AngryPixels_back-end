using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace application_programming_interface.Models
{
    public class User_Policy
    {
        [Key]
        public int User_Policy_Id { get; set; }
        public int User_Id { get; set; }
        public int Policy_Id { get; set; }


        //many to one - users
        public virtual Users User { get; set; }

        //many to one - policy
        public virtual Policy Policy { get; set; }
    }
}
