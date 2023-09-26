using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.DAL.Models;
using Todo.DAL.Specification;

namespace Todo.Core.Specification
{
    public class TodoSpecification : BaseSpecification<TodoItem>
    {

        public TodoSpecification(string userId) : base(todo => todo.UserId == userId)
        {
            AddInclude(x => x.User);

        }
        public TodoSpecification(Guid id , string userId) : base(todo => todo.Id == id && todo.UserId == userId)
        {

            AddInclude(x => x.User);
        }
    }
}
