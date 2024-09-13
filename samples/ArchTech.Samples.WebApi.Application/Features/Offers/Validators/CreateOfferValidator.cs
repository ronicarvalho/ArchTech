using ArchTech.Samples.WebApi.Application.Features.Offers.Ports;
using FluentValidation;

namespace ArchTech.Samples.WebApi.Application.Features.Offers.Validators;

public class CreateOfferValidator: AbstractValidator<CreateOfferInput>
{
    public CreateOfferValidator() => ValidateInput();

    private void ValidateInput()
    {
        RuleFor(input => input.CorrelationCode)
            .NotEqual(string.Empty)
            .WithMessage("The trackable request cant have a empty correlation identifier");
        
        RuleFor(input => input.TransactionCode)
            .NotEqual(string.Empty)
            .WithMessage("The transaction identifier cant be empty");
        
        RuleFor(input => input.Title)
            .NotEqual(string.Empty)
            .WithMessage("The title of offer cant be empty");
        
        RuleFor(input => input.Description)
            .NotEqual(string.Empty)
            .WithMessage("The description of offer cant be empty");
        
        RuleFor(input => input.Title)
            .MinimumLength(10)
            .WithMessage("The title of offer is too short");
        
        RuleFor(input => input.Title)
            .MaximumLength(120)
            .WithMessage("The title of offer is too long");
        
        RuleFor(input => input.Description)
            .MinimumLength(40)
            .WithMessage("The description of offer is too short");
        
        RuleFor(input => input.Description)
            .MaximumLength(2046)
            .WithMessage("The description of offer is too long");
    }
}