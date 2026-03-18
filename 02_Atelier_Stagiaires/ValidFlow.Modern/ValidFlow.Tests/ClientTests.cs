// ValidFlow.Tests/ClientTests.cs
namespace ValidFlow.Tests;

using ValidFlow.Domain.Entities;
using Xunit;

public class ClientTests
{
    [Fact]
    public void Client_WithValidData_ShouldBeValid()
    {
        // Arrange
        var client = new Client 
        { 
            Id = "CLT-001", 
            Name = "Acme Corporation", 
            Email = "contact@acme.com" 
        };
        
        // Act & Assert
        Assert.True(client.IsValid());
    }
    
    [Theory]
    [InlineData("", "test@email.com")]      // Nom vide
    [InlineData("A", "test@email.com")]     // Nom trop court
    [InlineData("Acme", "invalid-email")]   // Email sans @
    public void Client_WithInvalidData_ShouldBeInvalid(string name, string email)
    {
        // Arrange
        var client = new Client 
        { 
            Id = "CLT-001", 
            Name = name, 
            Email = email 
        };
        
        // Act & Assert
        Assert.False(client.IsValid());
    }
}