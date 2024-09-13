namespace ArchTech.WebApi.Settings;

public class OpenApiSettings
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string TermsOfService { get; init; } = string.Empty;
    public Contact Contact { get; init; } = new();
    public License License { get; init; } = new();
}