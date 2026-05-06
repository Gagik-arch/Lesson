using AutoMapper;
using Lesson.Data;
using Lesson.Data.DTO.Read;
using Lesson.Data.DTO.Write;
using Lesson.Exceptions;
using Lesson.Models;
using Lesson.Repositories.Interfaces;
using Lesson.Services.Core;
using Lesson.Services.Interfaces;
using Lesson.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace Lesson.Services.Implementations
{
    public class TodoService : BaseService<ITodoRepository>, ITodoService
    {
        public TodoService(
               ITodoRepository repo,
               IMapper mapper,
               AppDbContext context
           )
           : base(repo, mapper, context)
        {
        }

        public async Task<TodoReadDTO> Add(string? email, TodoWriteDTO toDo)
        {
            if (email == null)
            {
                throw new BadRequestException("User not found!");
            }

            Io.CreateDirectory(Constants.TodoUploadPath);

            Todo _toDo = new()
            {
                Author = email,
                Title = toDo.Title,
                Description = toDo.Description,
            };


            if (toDo.Image is not null)
            {
                string imageName = toDo.Image.FileName;

                var filePath = Path.Combine(Constants.TodoUploadPath, imageName);

                if (!File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await toDo.Image.CopyToAsync(stream);
                    }
                }
                _toDo.Image = imageName;
            }

            await _repo.Add(_toDo);

            await _context.SaveChangesAsync();
            string url = Constants.TodoUploadPath.Replace(Constants.Base, "");

            _toDo.Image = $"/{url.Replace("\\", "/")}/{_toDo.Image}";

            return _mapper.Map<Todo, TodoReadDTO>(_toDo);
        }

        public async Task<List<TodoReadDTO>> GetAll(string? email)
        {
            if (email == null)
            {
                throw new BadRequestException("User not found!");
            }

            List<Todo> toDos = await _repo.GetAllByEmail(email);

            return _mapper.Map<List<Todo>, List<TodoReadDTO>>(toDos);
        }
    }
}
