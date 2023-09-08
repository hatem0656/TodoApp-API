using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.DAL.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Body { get; set; }
        public bool IsCompleted { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
