using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.SmartMatch.Rules;

namespace PeopleHub.UnitTests.SmartMatch.Rules;

public class AvailabilityRuleTests
{
    [Fact]
    public void IsEligible_ReturnsTrue_WhenProviderIsAvailable()
    {
        // Arrange
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Experienced Electrician",
            8);

        provider.AddAvailability(
            DayOfWeek.Monday,
            new TimeOnly(9, 0),
            new TimeOnly(17, 0));

        var rule = new AvailabilityRule();

        var requestedDate = new DateTime(
            2026,
            7,
            20,   // Monday
            10,
            0,
            0);

        // Act
        var result = rule.IsEligible(provider, requestedDate);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsEligible_ReturnsFalse_WhenProviderIsUnavailable()
    {
        // Arrange
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Experienced Electrician",
            8);

        provider.AddAvailability(
            DayOfWeek.Monday,
            new TimeOnly(9, 0),
            new TimeOnly(17, 0));

        var rule = new AvailabilityRule();

        var requestedDate = new DateTime(
            2026,
            7,
            20,   // Monday
            18,
            0,
            0);

        // Act
        var result = rule.IsEligible(provider, requestedDate);

        // Assert
        Assert.False(result);
    }
}