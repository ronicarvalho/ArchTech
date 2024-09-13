using System.Text.Json;
using System.Text.Json.Serialization;

namespace ArchTech.Samples.Worker.Converters;

public class EnumJsonConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
{
    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException($"Unexpected token type '{reader.TokenType}' when parsing enum.");

        var enumString = reader.GetString();
        if (Enum.TryParse(reader.GetString(), true, out TEnum result)) return result;
        throw new JsonException($"Unable to convert '{enumString}' to enum type '{typeof(TEnum)}'.");
    }

    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        var enumString = value.ToString();
        writer.WriteStringValue(enumString);
    }
}