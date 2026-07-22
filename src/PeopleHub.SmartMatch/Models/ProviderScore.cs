namespace PeopleHub.SmartMatch.Models;

public sealed class ProviderScore
{
    public Guid ProviderId { get; init; }

    public decimal Score { get; init; }
}