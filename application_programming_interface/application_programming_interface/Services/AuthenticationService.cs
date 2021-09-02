﻿using application_programming_interface.Interfaces;
using application_programming_interface.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace application_programming_interface.Services
{
    public class AuthenticationService : IAuthenticationService
    {
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
            claims.Add(new Claim("name", requestDTO.Username));

            claims.Add(new Claim("Role", "User"));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("852852-852852-852852-416534163"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("Issuer", "Audience", claims, notBefore: DateTime.Now,expires:DateTime.Now.AddDays(20), credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
