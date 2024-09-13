using ArchTech.Interactors.Base;
using ArchTech.Samples.WebApi.Application.Features.Offers.Ports;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ArchTech.Samples.WebApi.Application.Features.Offers.UseCases;

public class CreateOfferUseCase(ILogger<CreateOfferUseCase> logger, IValidator<CreateOfferInput>? validator = null)
    : UseCase<CreateOfferInput, CreateOfferOutput>(logger, validator)
{
    protected override Task<CreateOfferOutput> ProcessAsync(CreateOfferInput request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Processing the use case to create offer: {Title} {Description}", request.Title, request.Description);
        return Task.FromResult(new CreateOfferOutput() { IsCreated = true, OfferId = Guid.NewGuid() });
    }
}