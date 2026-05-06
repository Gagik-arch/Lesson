using Lesson.Data.DTO.Read;
using Lesson.Data.DTO.Write;

namespace Lesson.Services.Interfaces;
public interface IUserService
{
    Task<UserReadDTO> Create(UserWriteDTO newUser);

    Task<LoginResponseDTO> Login(UserWriteDTO newUser);

    Task<UserReadDTO> Me(string? email);

    Task<UserReadDTO> GetById(string id);
    
    Task ChangePassword(ChangePasswordDTO newDetails);
}
