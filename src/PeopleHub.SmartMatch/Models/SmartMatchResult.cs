using PeopleHub.Domain.Aggregates.Provider;

namespace PeopleHub.SmartMatch.Models;

public sealed class SmartMatchResult
{
    public ProviderProfile? SelectedProvider { get; init; }

    public IReadOnlyList<ProviderScore> Scores { get; init; }
        = [];
}