using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.Models
{
    public class Document 
    {
        [Key]
        public int Doc_id { get; set; }

        public string Doc_type { get; set; }

        public string Doc_des { get; set; }
    }
}
