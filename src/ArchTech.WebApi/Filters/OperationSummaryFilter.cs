using ArchTech.WebApi.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ArchTech.WebApi.Filters;

public sealed class OperationSummaryFilter: IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.MethodInfo.GetCustomAttributes(typeof(OperationSummaryAttribute), false)?.FirstOrDefault() is OperationSummaryAttribute attribute) {
            operation.Summary = attribute.Value;
        }
    }
}