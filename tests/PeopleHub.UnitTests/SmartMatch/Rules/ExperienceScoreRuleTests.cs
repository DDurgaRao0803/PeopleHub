using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.SmartMatch.Models;
using PeopleHub.SmartMatch.Ranking.Rules;

namespace PeopleHub.UnitTests.SmartMatch.Ranking.Rules;

public class ExperienceScoreRuleTests
{
    [Theory]
    [InlineData(0, 0)]
[InlineData(5, 6.25)]
[InlineData(12, 15)]
[InlineData(20, 25)]
    public void Calculate_ReturnsExpectedExperienceScore(
        int experienceYears,
        decimal expectedScore)
    {
        // Arrange
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Provider",
            experienceYears);

        var context = new ScoreContext
        {
            Provider = provider
        };

        var rule = new ExperienceScoreRule();

        // Act
        var score = rule.Calculate(context);

        // Assert
        Assert.Equal(expectedScore, score);
    }
}