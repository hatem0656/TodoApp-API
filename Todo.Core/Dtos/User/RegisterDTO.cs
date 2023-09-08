using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Todo.Core.Dtos.User
{
    public class RegisterDTO
    {

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(30)]
        public string Email { get; set; }

        [MaxLength(30)]
        public string Password { get; set; }

    }
}
