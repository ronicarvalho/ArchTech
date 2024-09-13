using ArchTech.Interactors.Startup;
using ArchTech.Samples.WebApi;
using ArchTech.WebApi.Startup;

WebApplication
    .CreateBuilder(args)
    .ConfigureSerilog()
    .ConfigureServices()
    .ConfigureOpenApi()
    .ConfigureInteractors()
    .ConfigureApplication()
    .Build()
    .UseDefaults()
    .Run();