using ArchTech.Streams.Common;
using ArchTech.Streams.Settings;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace ArchTech.Streams.Consumers;

public abstract class MessageConsumer<TKey, TMessage>: IMessageConsumer<TKey, TMessage>
{
    private readonly ILogger<MessageConsumer<TKey, TMessage>> _logger;
    private readonly IConsumer<TKey, TMessage> _consumer;
    private readonly AsyncRetryPolicy _retryPolicy;

    protected MessageConsumer(ILogger<MessageConsumer<TKey, TMessage>> logger, StreamSettings settings)
    {
        _logger = logger;
        _consumer = KafkaBuilder.BuildConsumer<TKey, TMessage>(settings);
        _retryPolicy = ConfigureRetyPolice(settings);
        _consumer.Subscribe(settings.Topic);
    }

    public abstract Task ProcessMessageAsync(TKey key, TMessage message, CancellationToken cancellationToken);

    public async Task ConsumeAsync(CancellationToken cancellationToken)
    {
        var received = _consumer.Consume(cancellationToken);
        
        _logger.LogInformation($"Consuming event from {received.Topic} with message {received.Message.Value!}");
        
        await _retryPolicy.ExecuteAsync(async () =>
        {
            await ProcessMessageAsync(received.Message.Key, received.Message.Value, cancellationToken)
                .ConfigureAwait(false);
        }).ConfigureAwait(false);
    }

    private static AsyncRetryPolicy ConfigureRetyPolice(StreamSettings settings)
    {
        return Policy.Handle<Exception>().WaitAndRetryAsync(
            retryCount: settings.RetryCount,
            sleepDurationProvider: SleepDurationProvider);

        TimeSpan SleepDurationProvider(int i) => 
            TimeSpan.FromSeconds(settings.TimeToRetry);
    }
}