using AutoMapper;
using Lesson.Data.DTO.Write;
using Lesson.Data.DTO.Read;
using Lesson.Models;

namespace Lesson.Data.Mappers
{
    public class TodoMapper : Profile
    {
        public TodoMapper()
        {
            CreateMap<TodoReadDTO, TodoWriteDTO>();
            CreateMap<Todo, TodoReadDTO>();
        }
    }
}
