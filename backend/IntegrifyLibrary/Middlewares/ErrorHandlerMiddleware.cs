namespace IntegrifyLibrary.Middlewares
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await context.Response.WriteAsJsonAsync(
                    new { message = ex.Message, stackTrace = ex.StackTrace }
                );
            }
        }
    }
}