using Microsoft.Extensions.Configuration;

namespace ArchTech.Custom.Extensions;

public static class Configurations
{
    public static bool SectionExists(this IConfiguration configuration, string key) =>
        configuration.GetChildren().Any(item => item.Key == key);
}