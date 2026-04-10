using Lesson.Data;
using Lesson.Data.Mappers;
using Lesson.Repositories.Implementations;
using Lesson.Repositories.Interfaces;
using Lesson.Services.Implementations;
using Lesson.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lesson.Extensions
{
    public static class ServiceGonfigExtensions
    {
        public static IServiceCollection AddDatabase(
                this IServiceCollection services,
                IConfiguration configuration
            )
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DatabaseConnection")
                );
            });

            return services;
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<JwtService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITodoService, TodoService>();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITodoRepository, TodoRepository>();
        }

        public static void RegisterMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(option =>
            {
                option.AddProfile<UserMapper>();
                option.AddProfile<TodoMapper>();
            });
        }
    }
}
