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
        public int Policy_id { get; set; }

        public string Policy_holder {get;set;}

        public string Policy_type { get; set; }

        public string Policy_des { get; set; }

        public string Policy_date { get; set; }


    }
}
