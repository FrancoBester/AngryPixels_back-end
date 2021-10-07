using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace application_programming_interface.Models
{
    public class Queries
    {
        [Key]
        public int Query_Id { get; set; }
        public string Query_Title { get; set; }
        public string Query_Detail { get; set; }
        public string Query_Code { get; set; }
        public int Query_Level { get; set; }
        public int User_Id { get; set; }
        public string Assistant_Name { get; set; }
        public int Status_Id { get; set; } //1,2,3 (Use enums)


        //many to one - address
        public virtual Users Users { get; set; }

    }
}
