using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lesson.Services.Implementations
{
    public class JwtService
    {
        private readonly string _secret;

        public JwtService(IConfiguration configuration)
        {
            _secret = configuration["JWT:Key"] ?? "";
        }

        public string GenerateToken(string id, string email)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));

            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
            };

            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
