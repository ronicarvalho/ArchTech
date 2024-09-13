using System.Net;
using ArchTech.Streams.Settings;
using Confluent.Kafka;

namespace ArchTech.Streams.Common;

internal static class KafkaBuilder
{
    // public 
    
    public static IAdminClient BuildAdmin(StreamSettings settings) =>
        new AdminClientBuilder(CreateAdminConfig(settings)).Build();

    public static IProducer<TKey, TMessage> BuildProducer<TKey, TMessage>(StreamSettings settings) =>
        new ProducerBuilder<TKey, TMessage>(CreateProducerConfig(settings))
            .SetValueSerializer(Serializer<TMessage>.Create())
            .Build();
    
    public static IConsumer<TKey, TMessage> BuildConsumer<TKey, TMessage>(StreamSettings settings) =>
        new ConsumerBuilder<TKey, TMessage>(CreateConsumerConfig(settings))
            .SetValueDeserializer(Deserializer<TMessage>.Create())
            .Build();
    
    // private

    private static AdminClientConfig CreateAdminConfig(StreamSettings settings) => new() 
    {
        BootstrapServers = settings.BootstrapServers,
        SaslMechanism = settings.SaslMechanism,
        SecurityProtocol = settings.SecurityProtocol,
        SaslUsername = settings.Username,
        SaslPassword = settings.Password
    };
    
    private static ProducerConfig CreateProducerConfig(StreamSettings settings) => new()
    {
        ClientId = Dns.GetHostName(),
        BootstrapServers = settings.BootstrapServers,
        SaslMechanism = settings.SaslMechanism,
        SecurityProtocol = settings.SecurityProtocol,
        SaslUsername = settings.Username,
        SaslPassword = settings.Password
    };
    
    private static ConsumerConfig CreateConsumerConfig(StreamSettings settings) => new()
    {
        ClientId = Dns.GetHostName(),
        GroupId = settings.ConsumerGroup,
        BootstrapServers = settings.BootstrapServers,
        SaslMechanism = settings.SaslMechanism,
        SecurityProtocol = settings.SecurityProtocol,
        SaslUsername = settings.Username,
        SaslPassword = settings.Password,
        AutoOffsetReset = AutoOffsetReset.Earliest,
        EnableAutoOffsetStore = true
    };
}