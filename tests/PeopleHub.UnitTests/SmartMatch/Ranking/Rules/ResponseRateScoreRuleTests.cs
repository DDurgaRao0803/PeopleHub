using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.SmartMatch.Models;
using PeopleHub.SmartMatch.Ranking.Rules;

namespace PeopleHub.UnitTests.SmartMatch.Ranking.Rules;

public class ResponseRateScoreRuleTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(50, 7.5)]
    [InlineData(80, 12)]
    [InlineData(100, 15)]
    public void Calculate_ReturnsWeightedResponseRateScore(
        decimal responseRate,
        decimal expectedScore)
    {
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Provider",
            5);

        provider.UpdateResponseRate(responseRate);

        var context = new ScoreContext
        {
            Provider = provider
        };

        var rule = new ResponseRateScoreRule();

        var score = rule.Calculate(context);

        Assert.Equal(expectedScore, score);
    }
}