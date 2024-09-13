using System.Text.Json;
using Confluent.Kafka;

namespace ArchTech.Streams.Common;

public class Deserializer<T>: IDeserializer<T>
{
    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context) => 
        JsonSerializer.Deserialize<T>(data)!;
    
    public static Deserializer<T> Create() => new();
}