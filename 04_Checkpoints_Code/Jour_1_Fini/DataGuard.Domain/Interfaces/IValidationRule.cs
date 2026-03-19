namespace DataGuard.Domain.Interfaces;

public interface IValidationRule
{
    bool IsValid(string? value);
    string GetErrorMessage(string fieldName);
}