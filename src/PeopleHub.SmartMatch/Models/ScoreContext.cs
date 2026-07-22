using PeopleHub.Domain.Aggregates.Provider;

namespace PeopleHub.SmartMatch.Models;

public sealed class ScoreContext
{
    public required ProviderProfile Provider { get; init; }

    public decimal DistanceInKm { get; init; }
}