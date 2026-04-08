using Lesson.Data.DTO.Read;
using Lesson.Data.DTO.Write;

namespace Lesson.Services.Interfaces;

public interface IUserService
{
   Task<UserReadDTO> Create(UserWriteDTO newUser );

    Task<LoginResponseDTO> Login (UserWriteDTO newUser);

    UserReadDTO Me(string? email);
}
