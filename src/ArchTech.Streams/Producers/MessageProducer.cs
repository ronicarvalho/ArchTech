using ArchTech.Streams.Common;
using ArchTech.Streams.Settings;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace ArchTech.Streams.Producers;

public class MessageProducer<TKey, TMessage>(ILogger<MessageProducer<TKey, TMessage>> logger, StreamSettings settings)
    : IMessageProducer<TKey, TMessage>
{
    private readonly IProducer<TKey, TMessage> _producer = KafkaBuilder.BuildProducer<TKey, TMessage>(settings);

    public async Task ProduceAsync(TKey key, TMessage content, string topic, CancellationToken cancellationToken)
    {
        var message = new Message<TKey, TMessage>() { Key = key, Value = content };
        logger.LogInformation($"Producing event {message.Value!} on topic {topic}");
        await _producer.ProduceAsync(topic, message, cancellationToken).ConfigureAwait(false);
    }
}