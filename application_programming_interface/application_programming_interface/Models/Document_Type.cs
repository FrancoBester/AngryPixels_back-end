using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.Models
{
    public class Document_Type
    {
        [Key]

        public int DocType_Id { get; set; }

        public string Med_Cet {get;set;}

        public string Passport_Doc { get; set; }

        public string Birth_Certificate { get; set; }

        //many to one - document
        public int Doc_Id { get; set; }
        public virtual Document Document { get; set; }
    }
}
