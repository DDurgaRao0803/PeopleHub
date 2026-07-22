using PeopleHub.SmartMatch.Models;
using PeopleHub.SmartMatch.Ranking.Interfaces;

namespace PeopleHub.SmartMatch.Ranking.Rules;

public sealed class RatingScoreRule : IScoreRule
{
    private const decimal MaxRating = 5m;
    private const decimal RatingWeight = 30m;

    public decimal Calculate(ScoreContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        return context.Provider.AverageRating
               / MaxRating
               * RatingWeight;
    }
}