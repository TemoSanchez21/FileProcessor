using FileProcessor.Domain.Exceptions;

namespace FileProcessor.API.Middlewares;

public class ErrorHandlingMiddle(ILogger<ErrorHandlingMiddle> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch(NotFolderException ex)
        {
            logger.LogError(ex.Message);
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(ex.Message);
        }
        catch(UnableToDecompressException ex)
        {
            logger.LogError(ex.Message);
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(ex.Message);
        }
        catch(UnauthorizedAccessException ex )
        {
            logger.LogError(ex.Message);
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("Something went wrong");
        }
    }
}
