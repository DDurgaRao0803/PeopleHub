
using PeopleHub.SmartMatch.Models;
using PeopleHub.SmartMatch.Ranking.Rules;
using PeopleHub.SmartMatch.Ranking.Interfaces;

namespace PeopleHub.SmartMatch.Ranking;

public sealed class ScoreCalculator
{

    private readonly IReadOnlyList<IScoreRule> _scoreRules;

public ScoreCalculator(IEnumerable<IScoreRule> scoreRules)
{
    _scoreRules = scoreRules.ToList();
}

    public ProviderScore Calculate(ScoreContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

var provider = context.Provider;

        decimal totalScore =
    _scoreRules.Sum(rule => rule.Calculate(context));

        return new ProviderScore
        {
            ProviderId = provider.Id,
            Score = totalScore
        };
    }
}