using ArchTech.Custom.Extensions;
using ArchTech.WebApi.Configurations;
using ArchTech.WebApi.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ArchTech.WebApi.Startup;

public static partial class WebApiStartup
{
    private const string OpenApiConfiguration = "OpenApiSettings";
    private const string OpenApiConfigurationNotFound = "ServiceSettings must exist in appsettings.json";
    
    public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));
        
        return builder;
    }
    
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddControllers();
        builder.Services.AddLogging();
        builder.Services.AddOpenTelemetry();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.ConfigureVersions();
        
        return builder;
    }
    
    public static WebApplicationBuilder ConfigureOpenApi(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        if (!builder.Configuration.SectionExists(OpenApiConfiguration)) 
            throw new SwaggerGeneratorException(OpenApiConfigurationNotFound);

        var openApiSettings = builder.Configuration
            .GetSection(OpenApiConfiguration).Get<OpenApiSettings>()!;
        
        builder.Services.AddSingleton(openApiSettings);
        builder.Services.ConfigureSwagger(openApiSettings);
        
        return builder;
    }
    
    private static void ConfigureVersions(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });
        
        services.AddApiVersioning(setup =>
        {
            setup.AssumeDefaultVersionWhenUnspecified = true;
            setup.DefaultApiVersion = new ApiVersion(1, 0);
            setup.ReportApiVersions = true;
            setup.ApiVersionReader = new UrlSegmentApiVersionReader();
        });
    }
    
    private static void ConfigureSwagger(this IServiceCollection services, OpenApiSettings openApiSettings)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(openApiSettings);

        services.AddSwaggerGen();
        services.ConfigureOptions<SwaggerConfiguration>();
    }
}