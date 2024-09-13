using System.Text;
using System.Text.Json;
using Confluent.Kafka;

namespace ArchTech.Streams.Common;

public class Serializer<T>: ISerializer<T>
{
    public byte[] Serialize(T data, SerializationContext context) =>
        Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data));

    public static Serializer<T> Create() => new();
}