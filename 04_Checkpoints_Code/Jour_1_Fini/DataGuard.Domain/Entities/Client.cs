namespace DataGuard.Domain.Entities;

public record Client(string id, string name, string email)
{
    public required string Id { get; init; } = id;
    public required string Name { get; init; } = name;
    public required string Email { get; init; } = email;
    
    public List<string> ValidationErrors { get; init; } = [];
    
    public bool IsValid() => 
        !string.IsNullOrWhiteSpace(Name) && 
        Name.Length >= 2 &&
        Email.Contains('@');
}