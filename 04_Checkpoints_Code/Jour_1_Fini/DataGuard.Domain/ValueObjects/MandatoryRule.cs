namespace DataGuard.Domain.ValueObjects;

using DataGuard.Domain.Interfaces;

public record MandatoryRule : IValidationRule
{
    public bool IsValid(string? value) => !string.IsNullOrWhiteSpace(value);
    
    public string GetErrorMessage(string fieldName) => 
        $"Le champ '{fieldName}' est obligatoire.";
}
