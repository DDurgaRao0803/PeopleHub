using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.SmartMatch.Rules;

namespace PeopleHub.UnitTests.SmartMatch.Rules;

public class VerificationRuleTests
{
    [Fact]
    public void IsEligible_ReturnsTrue_WhenProviderIsApproved()
    {
        // Arrange
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Experienced electrician",
            10);

        provider.Approve();

        var rule = new VerificationRule();

        // Act
        var result = rule.IsEligible(provider);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsEligible_ReturnsFalse_WhenProviderIsNotApproved()
    {
        // Arrange
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Experienced electrician",
            10);

        var rule = new VerificationRule();

        // Act
        var result = rule.IsEligible(provider);

        // Assert
        Assert.False(result);
    }
}