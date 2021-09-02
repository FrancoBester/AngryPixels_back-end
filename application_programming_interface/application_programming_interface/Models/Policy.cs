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

        //one to many - admissions
        //public virtual List<Admissions> Admissions { get; set; }
        //public IEnumerable<Admissions> Admissions { get; set; }

        public ICollection<Admissions> Admissions { get; set; }

        //one to many - users
        public virtual List<Users> Users { get; set; }

    }
}
