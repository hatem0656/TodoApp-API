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

        public TodoSpecification()
        {
            AddInclude(x => x.User);

        }
        public TodoSpecification(Guid id) : base(todo => todo.Id == id)
        {

            AddInclude(x => x.User);
        }
    }
}
