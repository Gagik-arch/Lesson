using Lesson.Extensions;
using Lesson.Middlewares.ExceptionMiddleware;
using Lesson.Models;
using Microsoft.AspNetCore.Identity;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDataProtection();
builder.Services.AddDatabase(builder.Configuration);

builder.Services.RegisterServices();
builder.Services.RegisterRepositories();
builder.Services.RegisterMappers();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

string? key =  builder.Configuration["JWT:Key"] ?? "";

builder.Services.AddAuthorizationConfig(key);

WebApplication app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.OpenApiConfiguration();
app.UseHsts();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();