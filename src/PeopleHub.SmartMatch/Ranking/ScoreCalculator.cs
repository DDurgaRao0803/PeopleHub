using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.SmartMatch.Models;

namespace PeopleHub.SmartMatch.Ranking;

public sealed class ScoreCalculator
{
    public ProviderScore Calculate(ProviderProfile provider)
    {
        ArgumentNullException.ThrowIfNull(provider);

        return new ProviderScore
        {
            ProviderId = provider.Id,
            Score = provider.ExperienceYears
        };
    }
}