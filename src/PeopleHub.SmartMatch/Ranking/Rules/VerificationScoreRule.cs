using PeopleHub.Domain.Enums;
using PeopleHub.SmartMatch.Models;
using PeopleHub.SmartMatch.Ranking.Interfaces;

namespace PeopleHub.SmartMatch.Ranking.Rules;

public sealed class VerificationScoreRule : IScoreRule
{
    private const decimal VerificationWeight = 10m;

    public decimal Calculate(ScoreContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        return context.Provider.VerificationStatus == VerificationStatus.Approved
            ? VerificationWeight
            : 0m;
    }
}