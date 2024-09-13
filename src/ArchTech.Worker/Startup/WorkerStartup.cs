using Microsoft.Extensions.Hosting;
using Serilog;

namespace ArchTech.Worker.Startup;

public static class WorkerStartup
{
    public static IHostBuilder ConfigureSerilog(this IHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.UseSerilog((context, configuration) => 
            configuration.ReadFrom.Configuration(context.Configuration));
        
        return builder;
    }
}