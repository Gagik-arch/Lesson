using AutoMapper;
using Lesson.Data;
using Lesson.Data.DTO.Read;
using Lesson.Data.DTO.Write;
using Lesson.Exceptions;
using Lesson.Models;
using Lesson.Repositories.Interfaces;
using Lesson.Services.Core;
using Lesson.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Lesson.Services.Implementations;

public class UserService : BaseService<IUserRepository>, IUserService
{
    private readonly JwtService _jwtService;

    public UserService(
            IUserRepository repo,
            IMapper mapper,
            AppDbContext context,
            JwtService jwtService
        )
        : base(repo, mapper, context)
    {
        _jwtService = jwtService;
    }

    public async Task<UserReadDTO> Create(UserWriteDTO user)
    {
        if (await _repo.userByEmail(user.Email) != null)
        {
            throw new ConflictException("User with this eamil already exicts");
        }

        PasswordHasher<User> hasher = new PasswordHasher<User>();

        User newUser = new User
        {
            Email = user.Email,
            Password = user.Password,
            CreatedAt = DateTime.UtcNow,
        };

        newUser.Password = hasher.HashPassword(newUser, user.Password);

        await _repo.Add(newUser);
        await _context.SaveChangesAsync();

        return _mapper.Map<User, UserReadDTO>(newUser);
    }

    public async Task<LoginResponseDTO> Login(UserWriteDTO user)
    {
        User? existedUser = await _repo.userByEmail(user.Email);

        if (existedUser == null)
        {
            throw new NotFoundException("User not found!");
        }

        PasswordHasher<User> hasher = new PasswordHasher<User>();

        PasswordVerificationResult result = hasher.VerifyHashedPassword(existedUser, existedUser.Password, user.Password);

        if (result == PasswordVerificationResult.Success)
        {
            string token = _jwtService.GenerateToken(existedUser.Id, existedUser.Email);

            return new LoginResponseDTO
            {
                Token = token,
                User = _mapper.Map<UserReadDTO>(existedUser)
            };
        }

        throw new BadRequestException("Password is wrong!");
    }

    public UserReadDTO Me(string? email)
    {
        if (email == null)
        {
            throw new UnauthorizedException("User not found");
        }

        User? existedUser = _repo.userByEmail(email).Result;

        if (existedUser == null)
        {
            throw new NotFoundException("User not found");
        }

        return _mapper.Map<UserReadDTO>(existedUser);
    }
}