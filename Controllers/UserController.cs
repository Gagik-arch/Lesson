using Lesson.Data.DTO.Read;
using Lesson.Services.Implementations;
using Lesson.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lesson.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service, JwtService jwtService)
        {
            _service = service;
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me() 
        {
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;

            return Ok(await _service.Me(email));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            UserReadDTO user = await  _service.GetById(id);

            return Ok(user);
    
        }

        [HttpGet(Name = "GetAllUsers")]
        public ActionResult getAll()
        {
            return Ok("Get All USers");
        }
    }
}
