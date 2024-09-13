using ArchTech.Custom.Extensions;
using ArchTech.Interactors.Base;
using ArchTech.Samples.WebApi.Application.Features.Offers.Ports;
using ArchTech.Samples.WebApi.Controllers.v1.Transports;
using ArchTech.WebApi.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace ArchTech.Samples.WebApi.Controllers.v1;

[ApiVersion("1.0"), ApiController]
[Route("api/v{version:apiVersion}/offers")]
public class OffersController : ControllerBase
{
    private readonly ILogger<OffersController> _logger;
    private readonly UseCase<CreateOfferInput, CreateOfferOutput> _useCaseCreateOffer;

    public OffersController(ILogger<OffersController> logger,
        UseCase<CreateOfferInput, CreateOfferOutput> useCaseCreateOffer)
    {
        _logger = logger;
        _useCaseCreateOffer = useCaseCreateOffer;
    }

    [HttpPost]
    [OperationSummary("Register Offers")]
    [OperationNotes("Needs the create offers request")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterOffer([FromBody] CreateOfferRequest request, CancellationToken cancellationToken)
    {
        var input = request.Adapt<CreateOfferInput>();

        var output = await _useCaseCreateOffer
            .ExecuteAsync(input, cancellationToken)
            .ConfigureAwait(false);

        if (output.IsValid) return Accepted(output.Result);
        return BadRequest(output.GetValidationMessages());
    }
}