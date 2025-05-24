using System.Net;

namespace WalkingAPI.Middlewares;

public class ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
{

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            var errorId = Guid.NewGuid();
            logger.LogError(e, $"Error occured:{errorId}: {e.Message}");
            
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            
            var error = new { errorId, message = "Something went wrong! We are working on it." };

            await context.Response.WriteAsJsonAsync(error);
        }
    }
}