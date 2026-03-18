namespace ValidFlow.Domain.ValidationRules
{
    public record MandatoryRule : IValidationRule
    {
        public bool IsValid(string? value) => !string.IsNullOrWhiteSpace(value);
        
        public string ErrorMessage => "Ce champ est obligatoire.";
    }
}
