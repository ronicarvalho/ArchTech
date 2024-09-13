using System.Security.Cryptography;
using System.Text;

namespace ArchTech.Custom.Extensions;

public static class Strings
{
    public static bool In(this string value, params string[] args) => 
        args.Any(item => item.ToLower().Equals(value.ToLower()));

    public static bool HasValue(this string value) =>
        !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);

    public static Uri AsUri(this string value) => new(value);

    public static async Task<string> Hashing(this string value, CancellationToken cancellationToken = default)
    {
        using var sha256 = SHA256.Create();
        
        var input = Encoding.ASCII.GetBytes(value);
        var bytes = await sha256.ComputeHashAsync(new MemoryStream(input), cancellationToken).ConfigureAwait(false);
        var builder = new StringBuilder();

        foreach (var t in bytes)
            builder.Append(t.ToString("x2"));

        return builder.ToString();
    }
}