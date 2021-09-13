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

        // many to one - policy
        public int Policy_id { get; set; }
        public virtual Policy Policy { get; set; }

        // many to one - queries
        public int Query_id { get; set; }
        public virtual Queries Query { get; set; }

        //many to one - address
        public int Address_id { get; set; }
        public virtual Address Address { get; set; }

        //many to one - user_type
        public int User_type_id { get; set; }
        public virtual User_Type User_Type { get; set; }

        //may to one - medical
        public int Med_Cet_id { get; set; }
        public virtual Medical_Certificate Medical_Certificate { get; set; }

        //many to one - documents
        public int Doc_id { get; set; }
        public virtual Document Document { get; set; }
    }
}
