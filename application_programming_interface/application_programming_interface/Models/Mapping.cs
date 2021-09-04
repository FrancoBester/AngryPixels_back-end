using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace application_programming_interface.Models
{
    public class Mapping
    {
        [Key]
        public int Policy_id { get; set; }

        public int Doc_id { get; set; }

        public int Role_id { get; set; }

        public int Med_Cet_id { get; set; }

        public int Query_id {get;set;}

        public int User_Type_id { get; set; }


    }
}
