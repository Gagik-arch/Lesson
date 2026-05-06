using Lesson.Data;
using Lesson.Models;
using Lesson.Repositories.core;
using Lesson.Repositories.Interfaces;

namespace Lesson.Repositories.Implementations
{
    public class TodoRepository : BaseRepository<Todo>, ITodoRepository
    {
        public TodoRepository(AppDbContext context) : base(context) { }

        public async Task<List<Todo>> GetAllByEmail(string email)
        {
            return _dbSet.Where(x => x.Author == email).ToList();
        }
    }
}