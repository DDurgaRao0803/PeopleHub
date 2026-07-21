using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.SmartMatch.Rules;

namespace PeopleHub.UnitTests.SmartMatch.Rules;

public class SkillRuleTests
{
    [Fact]
    public void IsEligible_ReturnsTrue_WhenProviderHasRequestedSkill()
    {
        // Arrange
        var serviceCategory = new ServiceCategory("Electrical");

        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Experienced Electrician",
            8);

        provider.AddSkill(serviceCategory);

        var rule = new SkillRule();

        // Act
        var result = rule.IsEligible(provider, serviceCategory.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsEligible_ReturnsFalse_WhenProviderDoesNotHaveRequestedSkill()
    {
        // Arrange
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Experienced Electrician",
            8);

        var existingCategory = new ServiceCategory("Electrical");
        provider.AddSkill(existingCategory);

        var requestedCategory = new ServiceCategory("Plumbing");

        var rule = new SkillRule();

        // Act
        var result = rule.IsEligible(provider, requestedCategory.Id);

        // Assert
        Assert.False(result);
    }
}