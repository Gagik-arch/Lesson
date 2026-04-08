using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Lesson.Extensions
{
    public static class AuthorizationConfig
    {

        public static void AddAuthorizationConfig(this IServiceCollection services, string _key)
        {
            byte[] key = Encoding.UTF8.GetBytes(_key);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
              {
                  options.RequireHttpsMetadata = false;
                  options.SaveToken = true;

                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = false,
                      ValidateAudience = false,
                      ValidateIssuerSigningKey = true,
                      ValidateLifetime = true,
                      IssuerSigningKey = new SymmetricSecurityKey(key),
                  };
              });
        }
    }
}
