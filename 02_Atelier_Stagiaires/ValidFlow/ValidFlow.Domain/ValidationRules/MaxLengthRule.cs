namespace ValidFlow.Domain.ValidationRules
{
    public record MaxLengthRule : IValidationRule
    {
        public int MaxLength { get; init; }
        
        public MaxLengthRule(int maxLength)
        {
            MaxLength = maxLength;
        }
        
        public bool IsValid(string? value) => 
            string.IsNullOrEmpty(value) || value.Length <= MaxLength;
        
        public string ErrorMessage => $"La longueur maximale est {MaxLength} caractères.";
    }
}
