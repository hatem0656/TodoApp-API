using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Dtos.User;

namespace Todo.Core.IServices
{
    public interface IUserService
    {
        Task<AuthDTO> Register(RegisterDTO model);
        Task<AuthDTO> Login(LoginDTO model);
       

        //UserGetResponse UpdateUser(UserUpdateRequest request);

        //void DeleteUser(Guid id);
    }
}
