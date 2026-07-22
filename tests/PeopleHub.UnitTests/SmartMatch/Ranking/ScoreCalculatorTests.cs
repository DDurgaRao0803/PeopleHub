using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.SmartMatch.Ranking;

namespace PeopleHub.UnitTests.SmartMatch.Ranking;

public class ScoreCalculatorTests
{
    [Fact]
    public void Calculate_ReturnsExperienceYearsAsScore()
    {
        // Arrange
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Experienced Electrician",
            12);

        var calculator = new ScoreCalculator();

        // Act
        var result = calculator.Calculate(provider);

        // Assert
        Assert.Equal(provider.Id, result.ProviderId);
        Assert.Equal(15m, result.Score);
    }

    [Fact]
    public void Calculate_ReturnsZero_WhenProviderHasNoExperience()
    {
        // Arrange
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "New Provider",
            0);

        var calculator = new ScoreCalculator();

        // Act
        var result = calculator.Calculate(provider);

        // Assert
        Assert.Equal(0, result.Score);
    }
}