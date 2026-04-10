using Lesson.Data.DTO.Read;
using Lesson.Data.DTO.Write;
using Lesson.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lesson.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodoController(ITodoService service)
        {
            _service = service;
        }

        [HttpPost(Name = "CreateToDo")]
        public async Task<IActionResult> Create([FromBody] TodoWriteDTO todo)
        {
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;

            TodoReadDTO result = await _service.Add(email, todo);

            return CreatedAtRoute(
                      "CreateTodo",
                      new { Author = email },
                      new
                      {
                          Data = result,
                          Message = "User created succesfully"
                      }
            );
        }

        [HttpGet(Name = "GetAllTodos")]
        public async Task<IActionResult> GetAll()
        {
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;

            List<TodoReadDTO> result = await _service.GetAll(email);

            return CreatedAtRoute(
                      "GetAllTodos",
                      new { Author = email },
                         result
            );
        }
    }
}
