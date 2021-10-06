using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.DTOs
{
    public class UserInfoDTO
    {
        public string Role_Name { get; set; }
        public string User_Name { get; set; }
        public string User_Surname { get; set; }
        public string User_ID_Number { get; set; }
        public string User_Email { get; set; }
        public string User_Cell { get; set; }
        public DateTime User_Dob { get; set; }
        public string User_Gender { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Postal_Code { get; set; }
        public int Policy_Id { get; set; }
        public string Policy_Type { get; set; }

    }
}
