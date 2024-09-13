using ArchTech.Custom.Interfaces;

namespace ArchTech.Samples.Worker.Application.Features.ReactivacaoBeneficio.Ports;

public sealed class AnalysisInput: ITrackable
{
    public string CorrelationCode { get; set; } = Guid.NewGuid().ToString();
    public string TransactionCode { get; set; } = Guid.NewGuid().ToString();
    public string NumeroInscricao { get; set; } = string.Empty;
    public long NumeroBeneficio { get; set; }
    public string Motivo { get; set; } = string.Empty;
    public short Vigencia { get; set; }
}