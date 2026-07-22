using PeopleHub.SmartMatch.Models;
using PeopleHub.SmartMatch.Ranking.Interfaces;

namespace PeopleHub.SmartMatch.Ranking.Rules;

public sealed class ExperienceScoreRule : IScoreRule
{
    private const decimal MaxExperienceYears = 20m;
    private const decimal ExperienceWeight = 25m;

    public decimal Calculate(ScoreContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        return Math.Min(context.Provider.ExperienceYears, MaxExperienceYears)
               / MaxExperienceYears
               * ExperienceWeight;
    }
}