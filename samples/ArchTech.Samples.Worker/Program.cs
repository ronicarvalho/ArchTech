using ArchTech.Interactors.Startup;
using ArchTech.Samples.Worker;
using ArchTech.Streams.Startup;
using ArchTech.Worker.Startup;

Host
    .CreateDefaultBuilder(args)
    .ConfigureSerilog()
    .ConfigureInteractors()
    .ConfigureStreams()
    .ConfigureConsumers()
    .ConfigureWorkers()
    .Build()
    .Run();