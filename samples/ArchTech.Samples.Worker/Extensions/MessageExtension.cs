using ArchTech.Samples.Worker.Converters;
using ArchTech.Samples.Worker.Messages;

namespace ArchTech.Samples.Worker.Extensions;

public static class MessageExtension
{
    public static Solicitacao RequestType(this string value) =>
        Enum.TryParse<Solicitacao>(value, ignoreCase: true, out var result) ? result : default;
}