using System.Reflection;
using ArchTech.Custom.Extensions;
using ArchTech.WebApi.Filters;
using ArchTech.WebApi.Settings;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ArchTech.WebApi.Configurations;

public class SwaggerConfiguration(
    IApiVersionDescriptionProvider provider,
    OpenApiSettings openApiSettings)
    : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly DateTime _creationTime = File.GetCreationTime(Assembly.GetExecutingAssembly().Location);

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            if (options.SwaggerGeneratorOptions.SwaggerDocs.ContainsKey(description.GroupName)) continue;
            options.SwaggerDoc(description.GroupName, GetInformationByDescription(description));
            options.OperationFilter<OperationSummaryFilter>();
            options.OperationFilter<OperationNotesFilter>();
        }
    }

    public void Configure(string? name, SwaggerGenOptions options) =>
        Configure(options);

    private OpenApiInfo GetInformationByDescription(ApiVersionDescription description)
    {
        if (openApiSettings == null) throw new NullReferenceException(nameof(openApiSettings));

        var openApiInfo = new OpenApiInfo
        {
            Title = openApiSettings.Title,
            Version = description.ApiVersion.ToString(),
            Description = DescriptionMessage(description.IsDeprecated),
            Contact = new OpenApiContact(),
            License = new OpenApiLicense()
        };

        if (openApiSettings.TermsOfService.HasValue()) 
            openApiInfo.TermsOfService = openApiSettings.TermsOfService.AsUri();

        if (openApiSettings.Contact.Name.HasValue())
            openApiInfo.Contact.Name = openApiSettings.Contact.Name;
        
        if (openApiSettings.Contact.Email.HasValue())
            openApiInfo.Contact.Email = openApiSettings.Contact.Email;
        
        if (openApiSettings.Contact.Url.HasValue())
            openApiInfo.Contact.Url = openApiSettings.Contact.Url.AsUri();
        
        if (openApiSettings.License.Url.HasValue())
            openApiInfo.License.Url = openApiSettings.License.Url.AsUri();

        openApiInfo.License.Name = LicenseMessage(openApiSettings.License);

        return openApiInfo;
    }

    private string DescriptionMessage(bool deprecated)
    {
        return deprecated
            ? $"{openApiSettings.Description} (deprecated version)"
            : openApiSettings.Description;
    }

    private string LicenseMessage(License license)
    {
        return !license.Name.HasValue()
            ? $"Version generated on {_creationTime:yyyy-MM-dd} at {_creationTime:HH:mm:ss}"
            : $"{license.Name} - Version generated on {_creationTime:yyyy-MM-dd} at {_creationTime:HH:mm:ss}";
    }
}