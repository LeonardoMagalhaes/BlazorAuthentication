using BlazorApp.Api.Models;
using BlazorApp.Api.Services;
using BlazorApp.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public ActionResult<string> Login([FromBody] AuthenticateUserRequestDTO user)
        {
            var token = _userService.AuthenticateUser(user.Email, user.Password);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new AuthenticateUserResponseDTO<AuthenticateUserRequestDTO> { Token = token, User = user });
        }

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            return _userService.GetUsers();
        }

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public ActionResult<User> GetUser(string id)
        {
            var user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Json(user);
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            _userService.CreateUser(user);

            return Ok();
        }
    }
}
