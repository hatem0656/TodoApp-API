using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.DAL.Models;

namespace Todo.Core.Dtos.Todo
{
    public class TodoGetResponse
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Body { get; set; }
        public bool IsCompleted { get; set; }

        public string CreatedBy { get; set; }
    }
}
