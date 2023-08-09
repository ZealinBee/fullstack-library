using Microsoft.AspNetCore.Http;

namespace IntegrifyLibraryApi.Business
{
    public class LoggingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine($"Incoming request: {context.Request.Protocol} {context.Request.Method} {context.Request.Path}");
            // await context.Response.WriteAsync("request should end here");
            await next(context);
        }
    }
}