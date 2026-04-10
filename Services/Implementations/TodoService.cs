using AutoMapper;
using Lesson.Data;
using Lesson.Data.DTO.Read;
using Lesson.Data.DTO.Write;
using Lesson.Exceptions;
using Lesson.Models;
using Lesson.Repositories.Interfaces;
using Lesson.Services.Core;
using Lesson.Services.Interfaces;
using System.Diagnostics;

namespace Lesson.Services.Implementations
{
    public class TodoService:BaseService<ITodoRepository>, ITodoService
    {
        public TodoService(
               ITodoRepository repo,
               IMapper mapper,
               AppDbContext context
           )
           : base(repo, mapper, context)
        {
        }

        public async Task<TodoReadDTO> Add(string? email,TodoWriteDTO toDo) 
        {
            if (email == null) {
                throw new BadRequestException("User not found!");
            }

            Todo _toDo = new()
            {
                Author = email,
                Title = toDo.Title,
                Description = toDo.Description,
            };

            await _repo.Add(_toDo);
            await _context.SaveChangesAsync();

            return _mapper.Map<Todo, TodoReadDTO>(_toDo);
        }

        public async Task<List<TodoReadDTO>> GetAll(string? email) {
            if (email == null)
            {
                throw new BadRequestException("User not found!");
            }

            List<Todo> toDos = await _repo.GetAllByEmail(email);

            return _mapper.Map<List<Todo>, List<TodoReadDTO>>(toDos);
        }
    }
}
