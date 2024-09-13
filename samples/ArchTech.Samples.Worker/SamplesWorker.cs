using ArchTech.Interactors.Base;
using ArchTech.Samples.Worker.Application;
using ArchTech.Samples.Worker.Application.Features.ReactivacaoBeneficio.Ports;
using ArchTech.Samples.Worker.Application.Features.ReactivacaoBeneficio.UseCases;
using ArchTech.Samples.Worker.Application.Features.ReactivacaoBeneficio.Validators;
using ArchTech.Samples.Worker.Consumers;
using ArchTech.Samples.Worker.Messages;
using ArchTech.Samples.Worker.Services;
using ArchTech.Streams.Consumers;
using FluentValidation;

namespace ArchTech.Samples.Worker;

public static class SamplesWorker
{
    public static IHostBuilder ConfigureConsumers(this IHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConfigureServices(services =>
        {
            services.AddSingleton<IValidator<AnalysisInput>, AnalisysValidator>();
            services.AddSingleton<UseCase<AnalysisInput, AnalysisOutput>, AnalysisUseCase>();
            services.AddSingleton<MessageConsumer<string, RequisicaoAnalise>, ReactivaBeneficioMessageConsumer>();
        });
        
        return builder;
    }
    
    public static IHostBuilder ConfigureWorkers(this IHostBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ConfigureServices(services => 
            services.AddHostedService<ReactivaBeneficioService>());
        
        return builder;
    }
}