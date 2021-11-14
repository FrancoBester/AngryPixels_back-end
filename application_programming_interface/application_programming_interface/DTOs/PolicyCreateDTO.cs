using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.DTOs
{
    public class PolicyCreateDTO
    {
        public string Policy_Holder { get; set; }
        public string Policy_Type { get; set; }
        public string Policy_Des { get; set; }
        public string Policy_Date { get; set; }
        public string Policy_Benefits { get; set; }
        public bool IsActive { get; set; }

        public AdmissionsDTO Admissions { get; set; }
    }
}
