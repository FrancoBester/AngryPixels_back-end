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


        //many to one - address
        public int Address_Id { get; set; }
        public virtual Address Address { get; set; }

        //many to one - query
        public int Query_Id { get; set; }
        public virtual Queries Query { get; set; }

        //one to many - user_roles
        public ICollection<User_Roles> User_Roles { get; set; }

        //one to many - schema_requests
        public ICollection<Schema_Requests> Schema_Requests { get; set; }

        //one to many - user_policy
        public ICollection<User_Policy> User_Policies { get; set; }

        //one to many - Document
        public ICollection<Document> Documents { get; set; }
        
    }
}
