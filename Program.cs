using Lesson.Extensions;
using Lesson.Middlewares.ExceptionMiddleware;

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

app.OpenApiConfiguration();
app.UseHsts();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();