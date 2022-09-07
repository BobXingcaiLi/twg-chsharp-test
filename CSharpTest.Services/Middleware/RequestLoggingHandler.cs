
using CSharpTest.Services.Log;
using Microsoft.AspNetCore.Http;

namespace CSharpTest.Services.Middleware
{
    public class RequestLoggingHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogService _logService;

        public RequestLoggingHandler(RequestDelegate next, ILogService logService)
        {
            _next = next;
            _logService = logService;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if(httpContext.Request != null)
            {
                char kind;
                if (httpContext.Request.Path.Value.Contains("search", StringComparison.InvariantCultureIgnoreCase))
                {
                    kind = 'S';
                }
                else if (httpContext.Request.Path.Value.Contains("price", StringComparison.InvariantCultureIgnoreCase))
                {
                    //just guess if price request should be P?
                    kind = 'P';
                }
                else
                {
                    kind = '?'; //unknown?
                }
                //The new generated Rid would be used for the other INSERT command RID key
                var generatedRid = await _logService.LogRequest(kind);

                httpContext.Request.Headers.Add("RID", $"{generatedRid}");
            }

            await _next(httpContext);
        }
    }
}
