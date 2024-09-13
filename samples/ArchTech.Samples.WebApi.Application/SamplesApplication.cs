using ArchTech.Interactors.Base;
using ArchTech.Samples.WebApi.Application.Features.Offers.Ports;
using ArchTech.Samples.WebApi.Application.Features.Offers.UseCases;
using ArchTech.Samples.WebApi.Application.Features.Offers.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ArchTech.Samples.WebApi.Application;

public static class SamplesApplication
{
    public static void UsingFeatureOffers(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        
        services.AddScoped<IValidator<CreateOfferInput>, CreateOfferValidator>();
        services.AddScoped<UseCase<CreateOfferInput, CreateOfferOutput>, CreateOfferUseCase>();
    }
}