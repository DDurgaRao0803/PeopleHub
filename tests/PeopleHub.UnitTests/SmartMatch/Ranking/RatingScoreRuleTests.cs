using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.SmartMatch.Models;
using PeopleHub.SmartMatch.Ranking.Rules;

namespace PeopleHub.UnitTests.SmartMatch.Ranking.Rules;

public class RatingScoreRuleTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(2.5, 15)]
    [InlineData(4.5, 27)]
    [InlineData(5, 30)]
    public void Calculate_ReturnsWeightedRatingScore(
        decimal rating,
        decimal expectedScore)
    {
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Provider",
            10);

        provider.UpdateRating(rating);

        var context = new ScoreContext
        {
            Provider = provider
        };

        var rule = new RatingScoreRule();

        var score = rule.Calculate(context);

        Assert.Equal(expectedScore, score);
    }
}