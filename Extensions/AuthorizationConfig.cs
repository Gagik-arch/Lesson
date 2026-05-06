using Lesson.Exceptions;
using Lesson.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Lesson.Extensions
{
    public static class AuthorizationConfig
    {
        public static void AddAuthorizationConfig(
                this IServiceCollection services,
                IConfiguration configuration
            )
        {
            byte[] key = Encoding.UTF8.GetBytes(configuration["JWT:Key"] ?? "");

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
        
                  options.Events = new JwtBearerEvents
                  {
                      OnChallenge = async context =>
                      {
                          context.HandleResponse();
                          throw new UnauthorizedException("Invalid or missing authorization!");
                      },

                      OnForbidden = context =>
                      {
                          throw new ForbiddenException("Access denied!");
                      },

                      OnTokenValidated = async context =>
                      {
                          var userService = context.HttpContext.RequestServices
                              .GetRequiredService<IUserService>();

                          string? email = context.Principal?
                              .FindFirst(ClaimTypes.Email)?.Value;

                          var user = await userService.Me(email);

                          if (user == null)
                          {
                              context.Fail("Unauthorized");
                          }
                      }
                  };
              });

   
        }
    }
}
