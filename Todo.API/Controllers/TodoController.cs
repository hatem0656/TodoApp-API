using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Core.Dtos.Todo;
using Todo.Core.IServices;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodo()
        {
            return Ok(await _todoService.GetAllTodo());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTodo(Guid id)
        {
            var todo = await _todoService.GetTodo(id);
            if(todo == null) return NotFound();
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] TodoAddRequest todo)
        {
            var createdTodo = await _todoService.CreateTodo(todo);

            return CreatedAtAction(nameof(GetTodo), new { id = createdTodo.Id  }, createdTodo);
        }

        [HttpPut]
        public IActionResult UpdateTodo([FromBody] TodoUpdated todo)
        {
            return Ok(_todoService.UpdateTodo(todo));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            var deletedId = await _todoService.DeleteTodo(id);
            if (deletedId == Guid.Empty) return NotFound();
            return NoContent();
        }
    }
}

   
