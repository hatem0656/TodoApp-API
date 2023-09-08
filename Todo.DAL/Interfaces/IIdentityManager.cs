using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Todo.DAL.Interfaces
{
    public interface IIdentityManager<T> where T : IdentityUser
    {
        Task<IdentityResult> CreateUser(T user , string password);
        Task<IdentityResult> AddUserRole (T user , string roleName);
        Task<T?> FindUserById(string id);
        Task<T?> FindUserByEmail(string email);
        Task<IList<Claim>> GetUserClaims(T user);
        Task<IList<string>> GetUserRoles (T user);
        Task<bool> CheckUserPassword (T user , string password);


    }
}
