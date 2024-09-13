namespace ArchTech.Streams.Management;

public sealed class TopicMetadata
{
    private TopicMetadata(IEnumerable<Confluent.Kafka.TopicMetadata> items) => Items = items;
    public IEnumerable<Confluent.Kafka.TopicMetadata> Items { get; set; }
    public static TopicMetadata Create(IEnumerable<Confluent.Kafka.TopicMetadata>? items) =>
        new(items ?? Array.Empty<Confluent.Kafka.TopicMetadata>());
}