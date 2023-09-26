﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Dtos.Todo;

namespace Todo.Core.IServices
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoGetResponse>> GetAllTodo(string userId);

        Task<TodoGetResponse> GetTodo(Guid id, string userId);

        Task<TodoUpdated> CreateTodo(TodoAddRequest request);

        TodoUpdated UpdateTodo(TodoUpdated request);

        Task<Guid> DeleteTodo(Guid id);


    }
}
