using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core.Dtos.User
{
    public class AuthUser
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }

    }
}
