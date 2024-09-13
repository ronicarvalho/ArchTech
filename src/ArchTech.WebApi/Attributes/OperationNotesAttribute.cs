namespace ArchTech.WebApi.Attributes;

public sealed class OperationNotesAttribute(string value) : Attribute
{
    public string Value { get; } = value;
}