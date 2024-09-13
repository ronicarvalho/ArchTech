using ArchTech.Custom.Interfaces;

namespace ArchTech.Samples.WebApi.Application.Features.Offers.Ports;

public class CreateOfferInput: ITrackable
{
    public string CorrelationCode { get; set; } = Guid.NewGuid().ToString();
    public string TransactionCode { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}