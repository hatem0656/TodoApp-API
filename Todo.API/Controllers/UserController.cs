using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Security.Claims;
using Todo.Core.Dtos.User;
using Todo.Core.IServices;
using Todo.DAL.Models;

namespace Todo.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;


        public UserController(IUserService userService)
        {
            _userService = userService;

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDTO user)
        {
            var res = await _userService.Register(user);

            if (res.Message != null) return BadRequest(new { res.Message });

            return Ok(res);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            var res = await _userService.Login(user);

            if (res.Message != null) return BadRequest(new { res.Message });

            return Ok(res);
        }

        [Authorize]
        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = HttpContext.User.FindFirstValue("uid");
            if (userId == null) return NotFound();

            await _userService.Logout(userId);
            return Ok(new { message = "Logged out successfully" });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userId = HttpContext.User.FindFirstValue("uid");
            if (userId == null) return BadRequest(new { message = "Invalid Token" });

            var user = await _userService.GetUser(userId);
            if(user == null) return NotFound();

            if (user.Username == null ) return Unauthorized();

            return Ok(user);
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
          
            var user = await _userService.DeleteUser(id);
            if (!user) return NotFound();

            return Ok(new { message = "User Deleted Successfully" });
        }
    }

       

    
}
