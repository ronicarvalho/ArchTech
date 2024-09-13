using ArchTech.Interactors.Base;
using ArchTech.Samples.Worker.Application.Features.ReactivacaoBeneficio.Ports;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ArchTech.Samples.Worker.Application.Features.ReactivacaoBeneficio.UseCases;

public sealed class AnalysisUseCase(ILogger<AnalysisUseCase> logger, IValidator<AnalysisInput>? validator = null)
    : UseCase<AnalysisInput, AnalysisOutput>(logger, validator)
{
    protected override Task<AnalysisOutput> ProcessAsync(AnalysisInput request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Processing the use case to create offer: {Title} {Description}", request.NumeroInscricao, request.NumeroBeneficio);
        return Task.FromResult(new AnalysisOutput());
    }
}