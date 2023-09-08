using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Core.Dtos.User;
using Todo.Core.IServices;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
      

        public UserController(IUserService userService )
        {
            _userService = userService;
         
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDTO user)
        {  
            return Ok(await _userService.Register(user));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            return Ok(await _userService.Login(user));
        }
    }
}
