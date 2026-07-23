using Lesson.Extensions;
using Lesson.Helpers;
using Lesson.Middlewares.ExceptionMiddleware;
using Microsoft.Extensions.FileProviders;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDataProtection();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRedis(builder.Configuration);
builder.Services.AddAuthorizationConfig(builder.Configuration);

builder.Services.RegisterMappers();
builder.Services.RegisterServices();
builder.Services.RegisterRepositories();

WebApplication app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

Io.CreateDirectory(Constants.TodoUploadPath);

app.OpenApiConfiguration();
app.UseHsts();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Constants.TodoUploadPath),
    RequestPath = $"/{Constants.TodoUploadPath.Replace(Constants.Base, "").Replace("\\", "/")}"
});

app.Run();