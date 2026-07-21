using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Domain.Entities;
using PeopleHub.SmartMatch.Services;

namespace PeopleHub.UnitTests.SmartMatch.Services;

public class SmartMatchEngineTests
{
    [Fact]
    public void FindBestMatch_ReturnsHighestScoringEligibleProvider()
    {
        // Arrange
        var category = new ServiceCategory("Electrical");

        var provider1 = new ProviderProfile(
            Guid.NewGuid(),
            "Junior Electrician",
            5);

        provider1.Approve();
        provider1.AddSkill(category);
        provider1.AddAvailability(
            DayOfWeek.Monday,
            new TimeOnly(9, 0),
            new TimeOnly(17, 0));

        var provider2 = new ProviderProfile(
            Guid.NewGuid(),
            "Senior Electrician",
            15);

        provider2.Approve();
        provider2.AddSkill(category);
        provider2.AddAvailability(
            DayOfWeek.Monday,
            new TimeOnly(9, 0),
            new TimeOnly(17, 0));

        var serviceRequest = new ServiceRequest(
    Guid.NewGuid(),          // CustomerId
    Guid.NewGuid(),          // ProviderProfileId
    category.Id,             // ServiceCategoryId
    "Electrical Repair",     // Title
    "Repair electrical wiring", // Description
    new DateTime(2026, 7, 20, 10, 0, 0));

        var engine = new SmartMatchEngine();

        // Act
        var result = engine.FindBestMatch(
            serviceRequest,
            [provider1, provider2]);

        // Assert
        Assert.NotNull(result.SelectedProvider);
        Assert.Equal(provider2.Id, result.SelectedProvider!.Id);
        Assert.Equal(2, result.Scores.Count);
    }

    [Fact]
    public void FindBestMatch_ReturnsNull_WhenNoProviderIsEligible()
    {
        // Arrange
        var category = new ServiceCategory("Electrical");

        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Electrician",
            10);

        // Not approved

        var serviceRequest = new ServiceRequest(
    Guid.NewGuid(),          // CustomerId
    Guid.NewGuid(),          // ProviderProfileId
    category.Id,             // ServiceCategoryId
    "Electrical Repair",     // Title
    "Repair electrical wiring", // Description
    new DateTime(2026, 7, 20, 10, 0, 0));

        var engine = new SmartMatchEngine();

        // Act
        var result = engine.FindBestMatch(
            serviceRequest,
            [provider]);

        // Assert
        Assert.Null(result.SelectedProvider);
        Assert.Empty(result.Scores);
    }
}