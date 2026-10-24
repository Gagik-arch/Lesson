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
            CreateMap<Todo, TodoReadDTO>()
                .ForMember(
                    dest => dest.Image,
                    opt => opt.MapFrom(src =>
                        src.Image == null
                            ? null
                            : $"/uploads/todos/{src.Image}"
                    )); ;
        }
    }
}
