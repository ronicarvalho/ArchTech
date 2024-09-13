using ArchTech.Samples.WebApi.Application;

namespace ArchTech.Samples.WebApi;

public static class SamplesWebApi
{
    public static WebApplicationBuilder ConfigureApplication(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.UsingFeatureOffers();
        return builder;
    }
}