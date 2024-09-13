using ArchTech.Custom.Objects;
using FluentValidation.Results;

namespace ArchTech.Interactors.Core;

public class Output<T>
    where T: notnull
{
    private readonly StringList _validationMessages;
    
    public T Result { get; private set; }

    private Output()
    {
        _validationMessages = StringList.EmptyList();
        Result = default!;
    }

    public bool IsValid => !_validationMessages.Any();

    public void SetResultValidation(ValidationResult? validationResults)
    {
        if (validationResults is null) return;
        _validationMessages.AddRange(validationResults.Errors.Select(e => e.ErrorMessage));
    }

    public string[] GetValidationMessages() => 
        !IsValid ? _validationMessages.ToArray() : StringList.EmptyArray();

    public void SetResultValue(T value) => Result = value;

    public static Output<T> New() => new();
}