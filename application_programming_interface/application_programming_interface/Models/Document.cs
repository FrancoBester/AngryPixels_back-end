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
        public int Doc_Id { get; set; }

        //many to one - users
        public int User_Id { get; set; }
        public virtual Users User { get; set; }

        //many to one - policy
        public int Policy_Id { get; set; }
        public virtual Policy Policy { get; set; }

        //one to many 
        public ICollection<Document_Type> Document_Types { get; set; }
    }
}
