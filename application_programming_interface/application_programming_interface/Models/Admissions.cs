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
        public int Adms_Id { get; set; }

        public string Adms_Doctors { get; set; }

        public string Adms_Hospitals { get; set; }

        public string Adms_Type { get; set; }
        
        //many to one - policy
        public int Policy_Id { get; set; }
        public virtual Policy Policy { get; set; }
    }
}
