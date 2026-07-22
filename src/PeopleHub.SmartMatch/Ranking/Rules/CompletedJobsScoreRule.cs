using PeopleHub.SmartMatch.Models;
using PeopleHub.SmartMatch.Ranking.Interfaces;

namespace PeopleHub.SmartMatch.Ranking.Rules;

public sealed class CompletedJobsScoreRule : IScoreRule
{
    private const decimal MaxCompletedJobs = 100m;
    private const decimal CompletedJobsWeight = 20m;

    public decimal Calculate(ScoreContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        return Math.Min(
                   context.Provider.CompletedJobs,
                   MaxCompletedJobs)
               / MaxCompletedJobs
               * CompletedJobsWeight;
    }
}