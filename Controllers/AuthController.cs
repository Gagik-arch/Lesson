using Azure;
using Lesson.Data.DTO.Read;
using Lesson.Data.DTO.Write;
using Lesson.Services.Implementations;
using Lesson.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lesson.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;

        public AuthController(IUserService service, JwtService jwtService)
        {
            _service = service;
        }

        [HttpGet(Name = "GetAllUsers")]
        public ActionResult getAll()
        {
            return Ok("Get All USers");
        }

        [HttpPost("signup", Name = "SignupUser")]
        public async Task<IActionResult> SignUp([FromBody] UserWriteDTO user)
        {
            UserReadDTO result = await _service.Create(user);

            return CreatedAtAction(
                "UserCreated",
                 new {
                    Email = result.Email,
                    Message = "User created succesfully"
            });
        }

        [HttpPost("login", Name = "LoginUser")]
        public async Task<IActionResult> Login([FromBody] UserWriteDTO user)
        {
            LoginResponseDTO response = await _service.Login(user);

            return Ok(response);
        }

        [HttpGet("me", Name = "GetUserWithToken")]
        public IActionResult Me()
        {
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;

            return Ok(_service.Me(email));
        }
    }
}
