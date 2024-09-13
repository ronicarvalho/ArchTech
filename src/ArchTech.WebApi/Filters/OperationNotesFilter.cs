using ArchTech.WebApi.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ArchTech.WebApi.Filters;

public sealed class OperationNotesFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.MethodInfo.GetCustomAttributes(typeof(OperationNotesAttribute), false)?.FirstOrDefault() is OperationNotesAttribute attribute) {
            operation.Description = attribute.Value;
        }          
    }
}