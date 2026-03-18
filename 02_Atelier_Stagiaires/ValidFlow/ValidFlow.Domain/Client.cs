namespace ValidFlow.Domain
{
    using System.Collections.Generic;
    using ValidFlow.Domain.ValidationRules;

    public record Client
    {
        public required string Name { get; init; }
        public required string Email { get; init; }
        
        public List<string> ValidationErrors { get; init; } = new List<string>();
        
        public Client(string name, string email)
        {
            Name = name;
            Email = email;
        }
        
        public void Validate(IEnumerable<IValidationRule> rules)
        {
            ValidationErrors.Clear();
            
            foreach (var rule in rules)
            {
                if (!rule.IsValid(Name))
                {
                    ValidationErrors.Add($"Name: {rule.ErrorMessage}");
                }
                
                if (!rule.IsValid(Email))
                {
                    ValidationErrors.Add($"Email: {rule.ErrorMessage}");
                }
            }
        }
        
        public bool IsValid => ValidationErrors.Count == 0;
    }
}
