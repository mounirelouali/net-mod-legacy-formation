namespace ValidFlow.Domain.ValidationRules
{
    public interface IValidationRule
    {
        bool IsValid(string? value);
        string ErrorMessage { get; }
    }
}
