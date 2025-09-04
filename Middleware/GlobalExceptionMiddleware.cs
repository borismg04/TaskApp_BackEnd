using Models;
using System.Net;
using System.Text.Json;

namespace TaskAppBackEnd.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            var response = new ResponseModel
            {
                success = false,
                message = "An error occurred while processing your request"
            };

            switch (exception)
            {
                case UnauthorizedAccessException:
                    response.statusCode = (int)HttpStatusCode.Unauthorized;
                    response.message = "Unauthorized access";
                    break;
                case ArgumentException:
                    response.statusCode = (int)HttpStatusCode.BadRequest;
                    response.message = "Invalid request parameters";
                    break;
                default:
                    response.statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.StatusCode = response.statusCode;

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}