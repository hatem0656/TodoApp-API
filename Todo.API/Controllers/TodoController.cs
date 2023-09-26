using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo.Core.Dtos.Todo;
using Todo.Core.IServices;

namespace Todo.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly IUserService _userService;
        public TodoController(ITodoService todoService, IUserService userService)
        {
            _todoService = todoService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodo()
        {
            var userId = HttpContext.User.FindFirstValue("uid");

            return Ok(await _todoService.GetAllTodo(userId));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTodo(Guid id)
        {
            var userId = HttpContext.User.FindFirstValue("uid");
        
            var todo = await _todoService.GetTodo(id, userId);
            if(todo == null) return NotFound(new { message = "Todo Item Not Found" });

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

   
