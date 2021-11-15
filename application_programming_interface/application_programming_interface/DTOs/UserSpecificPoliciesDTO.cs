using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.DTOs
{
    public class UserSpecificPoliciesDTO
    {
        public int Policy_Id { get; set; }
        public string Policy_Holder { get; set; }
        public string Policy_Type { get; set; }
        public string RequestStatus { get; set; }

    }
}
