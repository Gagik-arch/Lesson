using Lesson.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Lesson.Middlewares.ExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception ex)
        {
            int status = StatusCodes.Status500InternalServerError;

            string title = ex.Message ?? "An unexpected error occurred";

            var map = new Dictionary<Type, int>
{
    { typeof(ConflictException), StatusCodes.Status409Conflict },
    { typeof(NotFoundException), StatusCodes.Status404NotFound },
    { typeof(BadRequestException), StatusCodes.Status400BadRequest },
    { typeof(UnauthorizedException), StatusCodes.Status401Unauthorized },
};

            Type exceptionType = ex.GetType();

            ProblemDetails problem = new ProblemDetails
            {
                Title = title,
                Status = map[exceptionType],
                Type = $"https://httpstatuses.com/{status}",
            };

            // ✅ Add trace id (VERY useful)
            problem.Extensions["traceId"] = context.TraceIdentifier;

            // ✅ Hide details in production
            if (_env.IsDevelopment())
            {
                problem.Detail = ex.StackTrace;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = status;

            return context.Response.WriteAsJsonAsync(problem);
        }
    }
}
