namespace ArchTech.Samples.Worker.Application.Features.ReactivacaoBeneficio.Ports;

public sealed class AnalysisOutput
{
    public bool IsCreated { get; set; }
    public Guid OfferId { get; set; }
}