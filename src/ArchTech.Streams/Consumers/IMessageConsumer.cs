namespace ArchTech.Streams.Consumers;

public interface IMessageConsumer<in TKey, in TMessage>
{
    Task ProcessMessageAsync(TKey key, TMessage message, CancellationToken cancellationToken);
}