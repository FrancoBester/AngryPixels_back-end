using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.DTOs
{
    public class SchemaRequestDTO
    {
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string PolicyType { get; set; }
        public string RequestStatus{ get; set; }
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public int PolicyId { get; set; }
    }
}
