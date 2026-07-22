using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.SmartMatch.Models;
using PeopleHub.SmartMatch.Ranking.Rules;

namespace PeopleHub.UnitTests.SmartMatch.Ranking.Rules;

public class CompletedJobsScoreRuleTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(25, 5)]
    [InlineData(50, 10)]
    [InlineData(100, 20)]
    [InlineData(150, 20)]
    public void Calculate_ReturnsWeightedCompletedJobsScore(
        int completedJobs,
        decimal expectedScore)
    {
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Provider",
            10);

        for (int i = 0; i < completedJobs; i++)
        {
            provider.IncrementCompletedJobs();
        }

        var context = new ScoreContext
        {
            Provider = provider
        };

        var rule = new CompletedJobsScoreRule();

        var score = rule.Calculate(context);

        Assert.Equal(expectedScore, score);
    }
}