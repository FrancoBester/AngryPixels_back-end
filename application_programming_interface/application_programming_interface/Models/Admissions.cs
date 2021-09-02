using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.Models
{
    public class Admissions
    {
        [Key]
        public int Adms_id { get; set; }

        public string Adms_Doctor { get; set; }

        public string Adms_Hospital { get; set; }

        public string Adms_type { get; set; }

        [ForeignKey("Policy")]
        public int Policy_id { get; set; }
        public Policy Policy { get; set; }

        //many to one - policy
        //public virtual int Policy_id { get; set; }
        //public virtual Policy Policies { get; set; }

        //public  int Policy_id { get; set; }
        //public  Policy Policy { get; set; }
    }
}
