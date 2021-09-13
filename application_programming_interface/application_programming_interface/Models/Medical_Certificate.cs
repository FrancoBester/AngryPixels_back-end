﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace application_programming_interface.Models
{
    public class Medical_Certificate
    {
        [Key]
        public int Med_Cet_id { get; set; }

        public string Med_Cet_Date { get; set; }

        public string Med_Cet_llink { get; set; }

        //one to many - users
        public ICollection<Users> Users { get; set; }

        //many to one - document
        public int Doc_id { get; set; }
        public virtual Document Document { get; set; }
    }
}
