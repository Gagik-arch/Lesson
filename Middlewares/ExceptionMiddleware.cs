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
            int status = ex switch
            {
                ConflictException => StatusCodes.Status409Conflict,
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                ForbiddenException => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };

            ProblemDetails problem = new ProblemDetails
            {
                Title = status == 500 ? "Internal Server Error" : (ex.Message ?? "An unexpected error occurred"),
                Status = status,
                Type = $"https://httpstatuses.com/{status}",
            };

            problem.Extensions["traceId"] = context.TraceIdentifier;

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
