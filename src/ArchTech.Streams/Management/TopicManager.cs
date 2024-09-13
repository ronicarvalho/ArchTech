using ArchTech.Streams.Common;
using ArchTech.Streams.Settings;
using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace ArchTech.Streams.Management;

internal sealed class TopicManager(StreamSettings settings) : ITopicManager
{
    private readonly IAdminClient _administrator = KafkaBuilder.BuildAdmin(settings);

    public async Task CreateTopicAsync(string topic, short replication, int partitions)
    {
        var specification = new TopicSpecification
        {
            Name = topic, 
            ReplicationFactor = replication, 
            NumPartitions = partitions
        };

        await _administrator.CreateTopicsAsync(new[] { specification }).ConfigureAwait(false);
    }

    public async Task CreateTopicsAsync(IEnumerable<string> topics, short replication, int partitions)
    {
        var specifications = topics.Select(topic => new TopicSpecification
        {
            Name = topic,
            ReplicationFactor = replication,
            NumPartitions = partitions
        });

        await _administrator.CreateTopicsAsync(specifications).ConfigureAwait(false);
    }

    public async Task RemoveTopicAsync(string topic)
    {
        await _administrator.DeleteTopicsAsync(new[] { topic }, new DeleteTopicsOptions
        {
            OperationTimeout = TimeSpan.FromMilliseconds(2000),
            RequestTimeout = TimeSpan.FromMilliseconds(2000)
        }).ConfigureAwait(false);
    }

    public async Task RemoveTopicsAsync(IEnumerable<string> topics)
    {
        await _administrator.DeleteTopicsAsync(topics, new DeleteTopicsOptions
        {
            OperationTimeout = TimeSpan.FromMilliseconds(2000),
            RequestTimeout = TimeSpan.FromMilliseconds(2000)
        }).ConfigureAwait(false);
    }

    public TopicMetadata GetTopicEvent(string topic)
    {
        var events = _administrator.GetMetadata(topic, TimeSpan.FromMilliseconds(100));
        return TopicMetadata.Create(events?.Topics?.Where(@event => !@event.Error.IsError));
    }

    public TopicMetadata GetTopicsEvents()
    {
        var events = _administrator.GetMetadata(TimeSpan.FromMilliseconds(100));
        return TopicMetadata.Create(events?.Topics);
    }
}