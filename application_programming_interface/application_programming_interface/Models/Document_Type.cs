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
        public string Doc_Name {get;set;}

        //many to one - document
        public virtual Document Document { get; set; }
    }
}
