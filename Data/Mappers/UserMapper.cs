using AutoMapper;
using Lesson.Data.DTO.Write;
using Lesson.Data.DTO.Read;
using Lesson.Models;

namespace Lesson.Data.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserReadDTO, UserWriteDTO>();
            CreateMap<User, UserReadDTO>();
        }
    }
}
