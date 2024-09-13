using Confluent.Kafka;

namespace ArchTech.Streams.Settings;

public sealed class StreamSettings
{
    public string BootstrapServers { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public string ConsumerGroup { get; set; } = string.Empty;
    public SaslMechanism SaslMechanism { get; set; } = SaslMechanism.Plain;
    public SecurityProtocol SecurityProtocol { get; set; } = SecurityProtocol.Plaintext;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public short RetryCount { get; set; } = short.MinValue;
    public short TimeToRetry { get; set; } = short.MinValue;
}