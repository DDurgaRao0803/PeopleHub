using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.SmartMatch.Ranking;
using PeopleHub.SmartMatch.Models;
using PeopleHub.SmartMatch.Ranking.Interfaces;
using PeopleHub.SmartMatch.Ranking.Rules;

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

        var calculator = new ScoreCalculator(
    new IScoreRule[]
    {
        new ExperienceScoreRule(),
        new RatingScoreRule(),
        new CompletedJobsScoreRule(),
        new VerificationScoreRule(),
        new ResponseRateScoreRule()
    });

        // Act
        var result = calculator.Calculate(
    new ScoreContext
    {
        Provider = provider
    });

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

        var calculator = new ScoreCalculator(
    new IScoreRule[]
    {
        new ExperienceScoreRule(),
        new RatingScoreRule(),
        new CompletedJobsScoreRule(),
        new VerificationScoreRule(),
        new ResponseRateScoreRule()
    });

        // Act
var result = calculator.Calculate(
    new ScoreContext
    {
        Provider = provider
    });

        // Assert
        Assert.Equal(0m, result.Score);
    }
}