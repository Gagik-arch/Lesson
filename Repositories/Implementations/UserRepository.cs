using Lesson.Data;
using Lesson.Models;
using Lesson.Repositories.core;
using Lesson.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lesson.Repositories.Implementations;
public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<User?> UserByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> UserById(string id){
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
}