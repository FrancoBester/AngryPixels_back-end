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

namespace application_programming_interface.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        public AuthenticationService()
        {
            User = new UserDescriptorDTO();
        }
        public UserDescriptorDTO User { get; set; }

        public UserDescriptorDTO GetUser()
        {
            return User;
        }

        public string SignIn(SignInRequestDTO requestDTO)
        {
            requestDTO.Validate();

            //mock authenication code
            if (requestDTO.Username == MockData.MockData.User.User_name)
            {
                //user matches
                return Authenticate(requestDTO);
            }

            throw new Exception("Invalid username and password combination.");
        }

        private string Authenticate(SignInRequestDTO requestDTO)
        {
            //TODO: redo this to query DB
            var claims = new List<Claim>();

            //Add user details to claims
            claims.Add(new Claim("Name", requestDTO.Username));

            var roles = new List<string>() { "User" };
            var rolesAsString = JsonConvert.SerializeObject(roles);

            claims.Add(new Claim("Roles", rolesAsString));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("852852-852852-852852-416534163"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("Issuer", "Audience", claims, notBefore: DateTime.Now,expires:DateTime.Now.AddDays(20), credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
