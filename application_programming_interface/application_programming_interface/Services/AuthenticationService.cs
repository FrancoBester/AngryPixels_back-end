using application_programming_interface.DTOs;
using application_programming_interface.Interfaces;
using application_programming_interface.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace application_programming_interface.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly DataContext _context;
        public UserDescriptorDTO User { get; set; }
        public AuthenticationService(DataContext context)
        {
            User = new UserDescriptorDTO();
            _context = context;
        }
        

        public UserDescriptorDTO GetUser()
        {
            return User;
        }

        public SignInResponseDTO SignIn(SignInRequestDTO requestDTO)
        {
            requestDTO.Validate();

            //Check if user exists and if password matches
            var user = (from u in _context.Users
                        where u.User_Email == requestDTO.Email
                        select u
                       ).FirstOrDefault();

            if (user == null)
            {
                throw new ValidationException("User Email Not Found");
            }

            ///WHERE IS PASSWORD IN DB??????
            ///WE SAVE PASSWORD AS MD5 HASH IN DB
            ///var hash = GenerateHash(requestDTO.Password)

            return Authenticate(requestDTO,user.User_Id);

            throw new Exception("Invalid username and password combination.");
        }

        private SignInResponseDTO Authenticate(SignInRequestDTO requestDTO,int id)
        {
            //TODO: redo this to query DB
            var claims = new List<Claim>();

            //Add user details to claims
            claims.Add(new Claim("Email", requestDTO.Email));
            claims.Add(new Claim("ID", id.ToString()));

            var roles = new List<string>() { "User"};
            var rolesAsString = JsonConvert.SerializeObject(roles);

            claims.Add(new Claim("Roles", rolesAsString));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("852852-852852-852852-416534163"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("Issuer", "Audience", claims, notBefore: DateTime.Now,expires:DateTime.Now.AddDays(20), credentials);

            var objectToReturn = new SignInResponseDTO();
            objectToReturn.Token = new JwtSecurityTokenHandler().WriteToken(token);
            objectToReturn.Roles = roles;
            objectToReturn.Id = id;
            objectToReturn.Email = requestDTO.Email;

            return objectToReturn;

        }

        private string GenerateHash(string value)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(value);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }

        }
    }
}
