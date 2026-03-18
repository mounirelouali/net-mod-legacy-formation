namespace DataGuard.Domain.Entities
{
    public record Client
    {
        public required string Id { get; init; }
        public required string Name { get; init; }
        public required string Email { get; init; }
        
        public List<string> ValidationErrors { get; init; } = new List<string>();
        
        public Client(string id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
        
        public bool IsValid() => 
            !string.IsNullOrWhiteSpace(Name) && 
            Name.Length >= 2 &&
            Email.Contains('@');
    }
}