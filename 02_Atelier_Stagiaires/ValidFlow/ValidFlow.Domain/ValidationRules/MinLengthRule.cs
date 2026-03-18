namespace ValidFlow.Domain.ValidationRules
{
    public record MinLengthRule : IValidationRule
    {
        public int MinLength { get; init; }
        
        public MinLengthRule(int minLength)
        {
            MinLength = minLength;
        }
        
        public bool IsValid(string? value) => 
            !string.IsNullOrEmpty(value) && value.Length >= MinLength;
        
        public string ErrorMessage => $"La longueur minimale est {MinLength} caractères.";
    }
}
