using ArchTech.Custom.Interfaces;

namespace ArchTech.Samples.WebApi.Controllers.v1.Transports;

public class CreateOfferRequest: IAdaptable
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}