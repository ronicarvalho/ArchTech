using ArchTech.Custom.Interfaces;
using ArchTech.Interactors.Core;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ArchTech.Interactors.Base;

public abstract class UseCase<TRequest, TResponse>(
    ILogger<UseCase<TRequest, TResponse>> logger,
    IValidator<TRequest>? validator = null)
    : IUseCase<TRequest, TResponse>
    where TRequest : ITrackable
    where TResponse : notnull
{
    public async Task<Output<TResponse>> ExecuteAsync(TRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting: {RequestName}", typeof(TRequest).Name);

        var output = Output<TResponse>.New();

        if (validator != null)
        {
            var validationResult = await validator!.ValidateAsync(request, cancellationToken).ConfigureAwait(false);
            
            if (!validationResult.IsValid) 
                output.SetResultValidation(validationResult);
        }

        if (output.IsValid)
        {
            var resultValue = await ProcessAsync(request, cancellationToken).ConfigureAwait(false);
            output.SetResultValue(resultValue);
        }

        logger.LogInformation("Finishing: {RequestName}", typeof(TRequest).Name);

        return output;
    }

    protected abstract Task<TResponse> ProcessAsync(TRequest request, CancellationToken cancellationToken);
}