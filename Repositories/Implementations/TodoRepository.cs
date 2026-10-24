using Lesson.Data;
using Lesson.Models;
using Lesson.Repositories.core;
using Lesson.Repositories.Interfaces;
using Lesson.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Lesson.Repositories.Implementations
{
    public class TodoRepository : BaseRepository<Todo>, ITodoRepository
    {
        public TodoRepository(AppDbContext context) : base(context) { }

        public async Task<List<Todo>> GetAllByEmail(string email)
        {
            List<Todo> todos = await _dbSet
              .AsNoTracking()
              .Where(todo => todo.Author == email)
              .ToListAsync();
        
            return todos;
        }
    }
}