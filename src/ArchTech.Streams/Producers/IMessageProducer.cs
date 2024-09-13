namespace ArchTech.Streams.Producers;

public interface IMessageProducer<in TKey, in TMessage>
{
    Task ProduceAsync(TKey key, TMessage content, string topic, CancellationToken cancellationToken);
}