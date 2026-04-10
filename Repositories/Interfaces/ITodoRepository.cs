using Lesson.Models;
using Lesson.Repositories.core;

namespace Lesson.Repositories.Interfaces
{
    public interface ITodoRepository : IBaseRepository<Todo>
    {
        Task<List<Todo>> GetAllByEmail(string email);
    }
}
