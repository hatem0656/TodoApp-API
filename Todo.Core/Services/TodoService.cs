using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Dtos.Todo;
using Todo.Core.IServices;
using Todo.Core.Specification;
using Todo.DAL.Models;
using Todo.DAL.Interfaces;
using Todo.DAL.Specification;
namespace Todo.Core.Services
{
    public class TodoService : ITodoService
    {
        private readonly IBaseRespository<TodoItem> _repo;
        private readonly IMapper _mapper;

        public TodoService(IBaseRespository<TodoItem> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }

        public async Task<IEnumerable<TodoGetResponse>> GetAllTodo()
        {
            var specification = new TodoSpecification();
            var todos = await _repo.GetAllWithSpecs(specification);
            var data = _mapper.Map<IEnumerable<TodoGetResponse>>(todos);
            return data;
        }

        public async Task<TodoGetResponse> GetTodo(Guid id)
        {
            var specification = new TodoSpecification(id);
            var todo = await _repo.GetWithSpecs(specification);
            var data = _mapper.Map<TodoGetResponse>(todo);
            return data;
        }

        public async Task<TodoUpdated> CreateTodo(TodoAddRequest newTodo)
        {
            var todo = _mapper.Map<TodoItem>(newTodo);

            var createdTodo = await _repo.Add(todo);

            var data = _mapper.Map<TodoUpdated>(createdTodo);
            return data;
        }
        public TodoUpdated UpdateTodo(TodoUpdated updatedTodo)
        {
            var todo = _mapper.Map<TodoItem>(updatedTodo);
            _repo.Update(todo);

            return updatedTodo;
        }

        public async Task<Guid> DeleteTodo(Guid id)
        {
            var todo = await _repo.GetById(id);
            if (todo == null) return Guid.Empty;
            _repo.Delete(todo);
            return id;
        }




    }
}
