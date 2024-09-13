using ArchTech.Custom.Interfaces;
using ArchTech.Interactors.Core;

namespace ArchTech.Interactors.Base;

public interface IUseCase<in TRequest, TResponse>
    where TRequest : ITrackable
    where TResponse : notnull
{
    Task<Output<TResponse>> ExecuteAsync(TRequest request, CancellationToken cancellationToken);
}