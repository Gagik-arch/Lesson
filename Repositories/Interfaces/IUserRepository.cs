using Lesson.Models;
using Lesson.Repositories.core;

namespace Lesson.Repositories.Interfaces;
public interface IUserRepository: IBaseRepository<User>
{
    Task<User?> UserByEmail(string email);
    Task<User?> UserById(string id);
}
