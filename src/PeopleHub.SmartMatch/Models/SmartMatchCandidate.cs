using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Domain.Aggregates.Bidding;

namespace PeopleHub.SmartMatch.Models;

public sealed class SmartMatchCandidate
{
    public ProviderProfile Provider { get; init; } = null!;


    public ProviderBid Bid { get; init; } = null!;


    public decimal Score { get; set; }
}