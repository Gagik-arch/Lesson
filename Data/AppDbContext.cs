using Lesson.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos{ get; set; }
        public IConfiguration _config { get; set; }

        public AppDbContext(
                DbContextOptions<AppDbContext> options,
                IConfiguration config
            ) : base(options)
        {
            _config = config;
        }
    }
}
