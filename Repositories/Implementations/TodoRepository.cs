using Lesson.Data;
using Lesson.Models;
using Lesson.Repositories.core;
using Lesson.Repositories.Interfaces;
using Lesson.Helpers;

namespace Lesson.Repositories.Implementations
{
    public class TodoRepository : BaseRepository<Todo>, ITodoRepository
    {
        public TodoRepository(AppDbContext context) : base(context) { }

        public async Task<List<Todo>> GetAllByEmail(string email)
        {
            var filtered = _dbSet.Where(x => x.Author == email).ToList();

            return filtered.Select(todo =>
            {

                if (todo.Image is not null)
                {
                    string url = Constants.TodoUploadPath.Replace(Constants.Base, "");

                    todo.Image = $"/{url.Replace("\\","/")}/{todo.Image}";
                }
                return todo;
            }).ToList();
        }
    }
}