using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.DAL.Models;

namespace Todo.Core.Dtos.User
{
    public class UserUpdateRequest
    {


        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(30)]
        public string Email { get; set; }

        [MinLength(6)]
        public string Password { get; set; }
    }
}
