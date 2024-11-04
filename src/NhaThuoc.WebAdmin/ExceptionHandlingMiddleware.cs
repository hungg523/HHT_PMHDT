using NhaThuoc.Share.Exceptions;
using System.Text.Json;

namespace NhaThuoc.WebAdmin
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (NhaThuocException ex)
            {
                await HandleExceptionAsync(context, ex.StatusCode, ex.Errors);
            }
            catch (Exception)
            {
                await HandleExceptionAsync(context, 500, new List<string> { "An unexpected error occurred." });
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, int statusCode, List<string>? errors)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = new
            {
                IsSuccess = false,
                StatusCode = statusCode,
                Errors = errors ?? new List<string> { "An error occurred." }
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

}