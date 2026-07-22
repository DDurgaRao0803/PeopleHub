using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.SmartMatch.Models;
using PeopleHub.SmartMatch.Ranking.Rules;

namespace PeopleHub.UnitTests.SmartMatch.Ranking.Rules;

public class VerificationScoreRuleTests
{
    [Fact]
    public void Calculate_ReturnsZero_WhenProviderIsNotApproved()
    {
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Provider",
            5);

        var context = new ScoreContext
        {
            Provider = provider
        };

        var rule = new VerificationScoreRule();

        var score = rule.Calculate(context);

        Assert.Equal(0m, score);
    }

    [Fact]
    public void Calculate_ReturnsVerificationWeight_WhenProviderIsApproved()
    {
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Provider",
            5);

        provider.Approve();

        var context = new ScoreContext
        {
            Provider = provider
        };

        var rule = new VerificationScoreRule();

        var score = rule.Calculate(context);

        Assert.Equal(10m, score);
    }
}