using Lesson.Data.DTO.Read;
using Lesson.Data.DTO.Write;
using Lesson.Models;
using Lesson.Repositories.core;
using Lesson.Services.Core;

namespace Lesson.Services.Interfaces;
public interface ITodoService
{
    Task<TodoReadDTO> Add(string? email, TodoWriteDTO todo);

    Task<List<TodoReadDTO>> GetAll(string? email);
}
