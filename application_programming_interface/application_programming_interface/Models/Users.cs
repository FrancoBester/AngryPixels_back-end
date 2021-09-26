using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using application_programming_interface.DTOs;

namespace application_programming_interface.Models
{
    public class Users
    {
        [Key]
        public int User_Id { get; set; }

        public string User_Name { get; set; }

        public string User_Surname { get; set; }

        public string User_ID_Number { get; set; }

        public string User_Email { get; set; }

        public string User_Cell { get; set; }

        public string User_Dob { get; set; }

        public string User_Gender { get; set; }

        public string User_Age { get; set; }

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
        public ICollection<User_Policy> User_Policy { get; set; }

        //one to many - Document
        public ICollection<Document> Documents { get; set; }
        
    }
}