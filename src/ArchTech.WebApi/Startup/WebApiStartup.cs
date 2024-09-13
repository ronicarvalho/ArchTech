using System.Net;
using System.Text.Json;
using ArchTech.Custom.Extensions;
using ArchTech.WebApi.Custom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArchTech.WebApi.Startup;

public static partial class WebApiStartup
{
    private const int BadRequest = (int) HttpStatusCode.BadRequest;
    private const int InternalServerError = (int) HttpStatusCode.InternalServerError;
    
    private const string ProblemContentType = "application/problem+json";
    private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";
    
    private const string ForbiddenErrorRequestMessage = "You dont have permission to access this resource.";
    private const string UnauthorizedErrorRequestMessage = "You are not authorized to access this resource.";

    private const string MozillaWebDocs = "https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/";

    private static readonly string[] NonProductiveEnvironments = { "Development", "Quality", "Staging" };

    internal static async Task ForbiddenRequestAsync(this HttpContext context) =>
        await context.HandleExceptionAsync(new HttpRequestException(ForbiddenErrorRequestMessage, null, HttpStatusCode.Forbidden), 
            StatusCodes.Status403Forbidden).ConfigureAwait(false);

    internal static async Task UnauthorizedRequestAsync(this HttpContext context) =>
        await context.HandleExceptionAsync(new HttpRequestException(UnauthorizedErrorRequestMessage, null, HttpStatusCode.Unauthorized), 
            StatusCodes.Status401Unauthorized).ConfigureAwait(false); 
    
    internal static Task<bool> HasApplicationTokenAsync(this HttpContext context, string applicationToken) =>
        Task.FromResult(context.Request.Headers.ContainsKey(applicationToken));
    
    internal static Task<string> GetApplicationTokenAsync(this HttpContext context, string applicationToken) =>
        Task.FromResult(context.Request.Headers[applicationToken].ToString());

    internal static Task LoggerExceptionAsync(this HttpContext context, ILogger logger, Exception exception)
    {
        if (!logger.IsEnabled(LogLevel.Error)) return Task.CompletedTask;

        var message = exception.InnerException is null ? exception.Message
            : $"{exception.Message} <<INNER>> {exception.InnerException.Message}";

        logger.LogError(exception, "{Method} {Request} {Message}",
            context.Request.Method, context.Request.Path.Value, message);

        return Task.CompletedTask;
    }
    
    internal static async Task HandleExceptionAsync(this HttpContext context, Exception exception, int statusCode, CancellationToken cancellationToken = default)
    {
        var payload = exception.DetailsForResponse(context, statusCode);

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = ProblemContentType;

        await context.Response
            .WriteAsync(JsonSerializer.Serialize(payload), cancellationToken)
            .ConfigureAwait(false);
    }
    
    internal static int GetStatusCode(this Exception exception) => exception switch
    {
        ArgumentException => BadRequest, 
        _ => InternalServerError
    };
    
    private static ProblemDetails DetailsForResponse(this Exception exception, HttpContext context,  int httpCode)
    {
        var production = !Environment.GetEnvironmentVariable(
            AspNetCoreEnvironment)!.In(NonProductiveEnvironments);
        
        var descriptor = production 
            ? HttpCodeMapper.DescriptorByHttpCode(httpCode)
            : HttpCodeMapper.DescriptorByCustomData("Exception", exception.Message);
        
        return new ProblemDetails
        {
            Type = $"{MozillaWebDocs}{httpCode}",
            Title = descriptor.Title,
            Detail = descriptor.Detail,
            Status = httpCode,
            Instance = context.Request.RouteValues.ToString(),
            Extensions = { { "traceId", $"{context.TraceIdentifier}" } }
        };
    }
}