using Microsoft.AspNetCore.Http;
namespace CSharpTest.Services.Middleware
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            
            await _next(httpContext);
        }
    }
}
