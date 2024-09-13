using ArchTech.Custom.Extensions;
using ArchTech.Interactors.Base;
using ArchTech.Samples.Worker.Application.Features.ReactivacaoBeneficio.Ports;
using ArchTech.Samples.Worker.Messages;
using ArchTech.Streams.Consumers;
using ArchTech.Streams.Settings;

namespace ArchTech.Samples.Worker.Consumers;

public sealed class ReactivaBeneficioMessageConsumer: MessageConsumer<string, RequisicaoAnalise>
{
    private readonly ILogger<ReactivaBeneficioMessageConsumer> _logger;
    private readonly UseCase<AnalysisInput, AnalysisOutput> _useCaseAnalysis;
    
    public ReactivaBeneficioMessageConsumer(
        ILogger<ReactivaBeneficioMessageConsumer> logger,
        UseCase<AnalysisInput, AnalysisOutput> useCaseAnalysis,
        StreamSettings settings) : base(logger, settings)
    {
        _logger = logger;
        _useCaseAnalysis = useCaseAnalysis;
    }

    public override async Task ProcessMessageAsync(string key, RequisicaoAnalise message, CancellationToken cancellationToken)
    {
        //var input = message.Adapt<AnalysisInput>(key);
        
        _logger.LogInformation("Processing message {key}", key);
        //_logger.LogInformation("Sending input message {input} to analysis use case", input);
        
        //await _useCaseAnalysis.ExecuteAsync(input, cancellationToken).ConfigureAwait(false);
        await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
    }
}