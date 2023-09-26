using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Todo.DAL.Interfaces;

namespace Todo.DAL.EF
{
    public class IdentityManager<T> : IIdentityManager<T> where T : IdentityUser
    {
        private readonly UserManager<T> _userManager;
       
        public IdentityManager(UserManager<T> userManager)
        {

            _userManager = userManager;
            
        }
        public async Task<IdentityResult> CreateUser(T user, string password)
        {
           return await _userManager.CreateAsync(user, password); 
        }

        public async Task<IdentityResult> AddUserRole(T user, string roleName)
        {
           return await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> DeleteUser(T user)
        {
            return await _userManager.DeleteAsync(user);
        }


        public async Task<T?> FindUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<T?> FindUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }


        public async Task<IList<Claim>> GetUserClaims(T user)
        {
            return await _userManager.GetClaimsAsync(user);
        }

        public async Task<IList<string>> GetUserRoles(T user)
        {
            return await _userManager.GetRolesAsync(user);
            
        }

        public async Task<bool> CheckUserPassword(T user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> SetUserToken(T user, string provider, string tokenName, string tokenValue)
        {
            return await _userManager.SetAuthenticationTokenAsync(user, provider, tokenName, tokenValue);
        }

        public async Task<string?> GetUserToken(T user, string provider, string tokenName)
        {
            return await _userManager.GetAuthenticationTokenAsync(user, provider, tokenName);
        }  
        public async Task<IdentityResult> DeleteUserToken(T user, string provider, string tokenName)
        {
            return await _userManager.RemoveAuthenticationTokenAsync(user, provider, tokenName);
        }

    }
}
