using PeopleHub.SmartMatch.Models;
using PeopleHub.SmartMatch.Ranking.Interfaces;

namespace PeopleHub.SmartMatch.Ranking.Rules;

public sealed class ResponseRateScoreRule : IScoreRule
{
    private const decimal ResponseRateWeight = 15m;

    public decimal Calculate(ScoreContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        return context.Provider.ResponseRate
               / 100m
               * ResponseRateWeight;
    }
}