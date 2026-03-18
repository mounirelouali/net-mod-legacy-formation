namespace DataGuard.Domain.ValueObjects
{
    using DataGuard.Domain.Interfaces;

    public record MaxLengthRule : IValidationRule
    {
        public int MaxLength { get; init; }
        
        public MaxLengthRule(int maxLength)
        {
            MaxLength = maxLength;
        }
        
        public bool IsValid(string? value) => value switch
        {
            null or "" => true,
            { Length: var len } when len <= MaxLength => true,
            _ => false
        };
        
        public string GetErrorMessage(string fieldName) => 
            $"Le champ '{fieldName}' ne doit pas dépasser {MaxLength} caractères.";
    }
}
