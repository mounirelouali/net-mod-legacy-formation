using DataGuard.Domain.ValueObjects;
using Xunit;

public class MinLengthRuleTests
{
    [Fact]
    public void IsValid_WithNullValue_ShouldReturnFalseAndNotCrash()
    {
        var rule = new MinLengthRule(2);
        bool result = rule.IsValid(null);
        Assert.False(result);
    }
}