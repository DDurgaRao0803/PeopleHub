using PeopleHub.Domain.Aggregates.Provider;

namespace PeopleHub.SmartMatch.Models;

public sealed class SmartMatchCandidate
{
    public ProviderProfile Provider { get; init; } = null!;

    public int Score { get; set; }
}