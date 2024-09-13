using ArchTech.WebApi.Startup;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ArchTech.WebApi.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await context.LoggerExceptionAsync(logger, exception).ConfigureAwait(false);
            await context.HandleExceptionAsync(exception, exception.GetStatusCode()).ConfigureAwait(false);
        }
    }
}