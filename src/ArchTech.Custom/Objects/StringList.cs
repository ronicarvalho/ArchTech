namespace ArchTech.Custom.Objects;

public class StringList: List<string>
{
    public static StringList EmptyList() => [];
    public static string[] EmptyArray() => Array.Empty<string>();
}