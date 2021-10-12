using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.DTOs
{
    public class AllPoliciesDTO
    {
        public int PolicyId { get; set; }
        public string PolicyType { get; set; }
        public string PolicyHolder { get; set; }
        public string PolicyDescription { get; set; }
        public string PolicyBenefits { get; set; }
        public int AdmsId { get; set; }
        public string AdmsType { get; set; }
    }
}
