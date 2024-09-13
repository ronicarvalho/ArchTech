using ArchTech.Samples.Worker.Application.Features.ReactivacaoBeneficio.Ports;
using FluentValidation;

namespace ArchTech.Samples.Worker.Application.Features.ReactivacaoBeneficio.Validators;

public sealed class AnalisysValidator: AbstractValidator<AnalysisInput>
{
    public AnalisysValidator() => ValidateInput();

    private void ValidateInput()
    {
        RuleFor(input => input.CorrelationCode)
            .NotEqual(string.Empty)
            .WithMessage("The trackable request cant have a empty correlation identifier");
        
        RuleFor(input => input.TransactionCode)
            .NotEqual(string.Empty)
            .WithMessage("The transaction identifier cant be empty");

        RuleFor(input => input.NumeroInscricao)
            .NotEqual(string.Empty)
            .WithMessage("Numero de inscricao nao pode ser vazio");

        RuleFor(input => input.NumeroBeneficio)
            .NotEqual(0)
            .WithMessage("O numero do beneficio nao pode ser zero");
        
        RuleFor(input => input.Motivo)
            .NotEqual(string.Empty)
            .WithMessage("Motivo da solicitacao nao pode ser vazio");
        
        RuleFor(input => input.Vigencia)
            .NotEqual(short.MinValue)
            .WithMessage("Vigencia deve ser informada");
    }
}