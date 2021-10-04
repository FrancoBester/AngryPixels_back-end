using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.DTOs
{
    public class UserRegisterDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CellPhoneNumber { get; set; }
        public string Gender { get; set; }
        public string IDnumber { get; set; }
        //public string Street { get; set; }
        //public string City { get; set; }
        //public string PostalCode { get; set; }
    }
}
