using System.Text.Json.Serialization;

namespace ArchTech.Samples.Worker.Messages;

public sealed class RequisicaoAnalise
{
    [JsonPropertyName("solicitacao")]
    public string Solicitacao { get; set; }
    [JsonPropertyName("numeroInscricao")]
    public string NumeroInscricao { get; set; } = string.Empty;
    [JsonPropertyName("numeroBeneficio")]
    public int NumeroBeneficio { get; set; }
    [JsonPropertyName("motivo")]
    public string Motivo { get; set; } = string.Empty;
    [JsonPropertyName("vigencia")]
    public int Vigencia { get; set; }
    [JsonPropertyName("dataSolicitacao")]
    public DateTimeOffset DataSolicitacao { get; set; }
}