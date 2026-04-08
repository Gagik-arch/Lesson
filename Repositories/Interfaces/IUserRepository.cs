using Lesson.Models;
using Lesson.Repositories.core;

namespace Lesson.Repositories.Interfaces;
public interface IUserRepository: IBaseRepository<User>
{
    Task<User?> userByEmail(string email);
}
