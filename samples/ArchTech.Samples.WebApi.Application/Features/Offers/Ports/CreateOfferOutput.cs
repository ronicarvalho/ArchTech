namespace ArchTech.Samples.WebApi.Application.Features.Offers.Ports;

public sealed class CreateOfferOutput
{
    public bool IsCreated { get; set; }
    public Guid OfferId { get; set; }
}