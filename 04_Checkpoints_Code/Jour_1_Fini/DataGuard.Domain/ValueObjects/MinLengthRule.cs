namespace DataGuard.Domain.ValueObjects;

using DataGuard.Domain.Interfaces;

public record MinLengthRule(int minLength) : IValidationRule
{
    public int MinLength { get; init; } = minLength;
    
    public bool IsValid(string? value) => value switch
    {
        null or "" => false,
        { Length: var len } when len >= MinLength => true,
        _ => false
    };
    
    public string GetErrorMessage(string fieldName) => 
        $"Le champ '{fieldName}' doit contenir au moins {MinLength} caractères.";
}