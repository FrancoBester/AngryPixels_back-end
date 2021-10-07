using application_programming_interface.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace application_programming_interface.Models
{
    public class SignInRequestDTO: IValidation
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public void Validate()
        {
            if (Email == null)
            {
                throw new ValidationException("No username entered.");
            }

            if (Email.Length>50 || Email.Length < 3)
            {
                throw new ValidationException("Username must be longer than 3 charachters and shorter than 20.");
            }
        }
    }
}
