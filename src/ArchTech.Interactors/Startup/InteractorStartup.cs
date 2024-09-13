using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ArchTech.Interactors.Startup;

public static class InteractorStartup
{
    public static WebApplicationBuilder ConfigureInteractors(this WebApplicationBuilder builder, string assemblyName = "Application")
    {
        ArgumentNullException.ThrowIfNull(builder);

        var assembly = ApplicationAssemblyInDomain(assemblyName) ?? ApplicationAssemblyInFileSystem(assemblyName);
        if (assembly is null) return builder;

        builder.Services.AddMediatR(config => 
            config.RegisterServicesFromAssembly(assembly));
        
        return builder;
    }
    
    public static IHostBuilder ConfigureInteractors(this IHostBuilder builder, string assemblyName = "Application")
    {
        ArgumentNullException.ThrowIfNull(builder);

        var assembly = ApplicationAssemblyInDomain(assemblyName) ?? ApplicationAssemblyInFileSystem(assemblyName);
        if (assembly is null) return builder;

        builder.ConfigureServices(services => 
            services.AddMediatR(config => 
                config.RegisterServicesFromAssembly(assembly)));
        
        return builder;
    }
    
    private static Assembly? ApplicationAssemblyInDomain(string assemblyName = "Application") =>
        AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(assembly => assembly.GetName().Name!.EndsWith(assemblyName));

    private static Assembly? ApplicationAssemblyInFileSystem(string assemblyName = "Application")
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var files = Directory.GetFiles(path!, "*.dll");
        var assembly = files.SingleOrDefault(file => file.EndsWith($"{assemblyName}.dll"));
        return assembly != null ? Assembly.LoadFrom(assembly) : null;
    }
}