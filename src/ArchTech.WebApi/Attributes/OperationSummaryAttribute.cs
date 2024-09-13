namespace ArchTech.WebApi.Attributes;

public sealed class OperationSummaryAttribute(string value) : Attribute
{
    public string Value { get; } = value;
}