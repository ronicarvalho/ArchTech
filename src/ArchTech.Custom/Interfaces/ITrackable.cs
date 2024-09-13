namespace ArchTech.Custom.Interfaces;

public interface ITrackable
{
    public string CorrelationCode { get; set; }
    public string TransactionCode { get; set; }
}