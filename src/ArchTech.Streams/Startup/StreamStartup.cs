using ArchTech.Custom.Extensions;
using ArchTech.Streams.Management;
using ArchTech.Streams.Producers;
using ArchTech.Streams.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ArchTech.Streams.Startup;

public static class StreamStartup
{
    private const string StreamsConfiguration = "StreamSettings";
    private const string StreamsConfigurationNotFound = "StreamSettings must exist in appsettings.json";

    public static IHostBuilder ConfigureStreams(this IHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConfigureServices((context, services) =>
        {
            if (!context.Configuration.SectionExists(StreamsConfiguration))
                throw new Exception(StreamsConfigurationNotFound);
            
            services.AddSingleton(context.Configuration
                .GetSection(StreamsConfiguration).Get<StreamSettings>()!);
        });

        return builder;
    }

    public static IServiceCollection EnableStreamProducer<TKey, TMessage>(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddTransient<IMessageProducer<TKey, TMessage>, MessageProducer<TKey, TMessage>>();
        return services;
    }

    public static IServiceCollection EnableTopicManager(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddTransient<ITopicManager, TopicManager>();
        return services;
    }
}