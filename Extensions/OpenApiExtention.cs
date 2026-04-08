using Scalar.AspNetCore;

namespace Lesson.Extensions
{
    public static class OpenApiExtention
    {
        public static void OpenApiConfiguration(
            this WebApplication app
            )
        {
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(options =>
                {
                    options.WithTitle("Lesson Api title");
                });
            }
        }
    }
}
