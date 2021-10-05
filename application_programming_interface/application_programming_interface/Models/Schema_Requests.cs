using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.Models
{
    public class Schema_Requests
    {
        [Key]
        public int Request_Id { get; set; }
        public int User_Id { get; set; }
        public int Policy_Id { get; set; }
        public int Status_Id { get; set; }


        // many to one - users
        public virtual Users User { get; set; }

        //many to one - policy
        public virtual Policy Policy { get; set; }
    }
}
