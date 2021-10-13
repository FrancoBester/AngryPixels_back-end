using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.DTOs
{
    public class SpecificUserQueriesDTO
    {
        public int QueryId { get; set; }
        public string QueryTitle { get; set; }
        public string QueryStatus { get; set; }
        public string AssistantName { get; set; }
    }
}
