using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Todo.API.Helpers;
using Todo.Core.Dtos.User;
using Todo.Core.IServices;
using Todo.DAL.Interfaces;
using Todo.DAL.Models;

namespace Todo.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IIdentityManager<User> _user;
        private readonly JWT _jwt;

        public UserService(IIdentityManager<User> user , IOptions<JWT> jwt )
        {
            _user = user ;
            _jwt = jwt.Value ;
        }

        public async Task<AuthDTO> Register(RegisterDTO model)
        {

            if (await _user.FindUserByEmail(model.Email) is not null)
                return new AuthDTO { Message = "Email is already registered!" };

            var user = new User
            {
              
                UserName = model.Name,
                Email = model.Email,
             
            };

            var result = await _user.CreateUser(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthDTO { Message = errors };
            }

            await _user.AddUserRole(user, "User");

            var jwtSecurityToken = await CreateJwtToken(user);


            return new AuthDTO
            {
                Username = user.UserName,
                Email = user.Email,
                IsAuthenticated = false,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                ExpiresOn = jwtSecurityToken.ValidTo,

            };
        }

        public async Task<AuthDTO> Login(LoginDTO model)
        {
            var AuthDTO = new AuthDTO();

            var user = await _user.FindUserByEmail(model.Email);

            if (user is null || !await _user.CheckUserPassword(user, model.Password))
            {
                AuthDTO.Message = "Email or Password is incorrect!";
                return AuthDTO;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            await _user.SetUserToken(user, "Admin", "jwt", token);

            var rolesList = await _user.GetUserRoles(user);

            AuthDTO.IsAuthenticated = true;
            AuthDTO.Token = token;
            AuthDTO.Email = user.Email;
            AuthDTO.Username = user.UserName;
            AuthDTO.ExpiresOn = jwtSecurityToken.ValidTo;
            AuthDTO.Roles = rolesList.ToList();

            return AuthDTO;
        }

        public async Task<AuthUser?> GetUser(string id)
        {
            var userData = new AuthUser();

            var user = await _user.FindUserById(id);
            if (user is null) return null;

            var token = await _user.GetUserToken(user, "Admin", "jwt");
            if (token is null) return userData;

            var rolesList = await _user.GetUserRoles(user);

            userData.Username = user.UserName;
            userData.Email = user.Email;
            userData.Roles = rolesList.ToList();

            return userData; 

        }

        public async Task Logout(string id)
        {
            var user = await _user.FindUserById(id);

            await _user.DeleteUserToken(user, "Admin", "jwt");

        }

        public async Task<bool> DeleteUser(string id)
        {
            var user = await _user.FindUserById(id);
            if(user is null) return false;

            var result = await _user.DeleteUser(user);

            return result.Succeeded; 
        }


        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _user.GetUserClaims(user);
            var roles = await _user.GetUserRoles(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        
         

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            
            return jwtSecurityToken;
        }

    }
}
