using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace application_programming_interface.Models
{
    public class Policy
    {
        [Key]
        public int Policy_Id { get; set; }
        public string Policy_Holder {get;set;}
        public string Policy_Type { get; set; }
        public string Policy_Des { get; set; }
        public string Policy_Date { get; set; }
        public string Policy_Benefits { get; set; }
        public bool IsActive { get; set; }


        //one to many - schema_requests
        public ICollection<Schema_Requests> Schema_Requests { get; set; }

        //one to many - user_policy
        public ICollection<User_Policy> User_Policies { get; set; }

        //one to many - Document
        public ICollection<Document> Documents { get; set; }

        //one to many - admissions
        public ICollection<Admissions> Admissions { get; set; }

    }
}
