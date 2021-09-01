using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}
