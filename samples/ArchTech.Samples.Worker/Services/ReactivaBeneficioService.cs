using ArchTech.Samples.Worker.Messages;
using ArchTech.Streams.Consumers;

namespace ArchTech.Samples.Worker.Services;

public sealed class ReactivaBeneficioService: BackgroundService
{
    private readonly ILogger<ReactivaBeneficioService> _logger;
    private readonly MessageConsumer<string, RequisicaoAnalise> _consumer;
    
    public ReactivaBeneficioService(
        ILogger<ReactivaBeneficioService> logger, 
        MessageConsumer<string, RequisicaoAnalise> consumer)
    {
        _logger = logger;
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Event received running at: {time}", DateTimeOffset.Now);
            await _consumer.ConsumeAsync(stoppingToken).ConfigureAwait(false);
            await Task.Delay(100, stoppingToken);
        }
    }
}