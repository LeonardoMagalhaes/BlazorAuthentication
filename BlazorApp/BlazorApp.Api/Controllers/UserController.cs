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
        public ActionResult<List<UserResponseDTO>> GetUsers()
        {
            var response = _userService.GetUsers().Select(u => new UserResponseDTO
            {
                Id = u.Id.ToString(),
                Email = u.Email,
                Role = u.Role
            }).ToList();
            return response;
        }

        [HttpGet("{id}")]
        public ActionResult<UserResponseDTO> GetUserById(string id)
        {
            var user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound("User not found!");
            }

            var response = new UserResponseDTO
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                Role = user.Role
            };
            return Json(response);
        }

        [HttpPost]
        public ActionResult<UserResponseDTO> CreateUser(UserRequestDTO user)
        {
            try
            {
                var response = _userService.CreateUser(user);

                if (response is null)
                {
                    return BadRequest("User already exists!");
                }

                return Ok(new UserResponseDTO
                {
                    Id = response.Id.ToString(),
                    Email = response.Email,
                    Role = response.Role
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return BadRequest("User already exists!");
            }
        }
    }
}
