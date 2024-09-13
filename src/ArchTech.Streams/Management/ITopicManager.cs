namespace ArchTech.Streams.Management;

public interface ITopicManager
{
    Task CreateTopicAsync(string topic, short replication, int partitions);
    Task CreateTopicsAsync(IEnumerable<string> topics, short replication, int partitions);
    Task RemoveTopicAsync(string topic);
    Task RemoveTopicsAsync(IEnumerable<string> topics);
    TopicMetadata GetTopicEvent(string topic);
    TopicMetadata GetTopicsEvents();
}