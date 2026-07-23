namespace PeopleHub.Contracts.Providers.Bidding;

public sealed record CreateProviderBidRequest(
    Guid ServiceRequestId,
    decimal Amount,
    int EstimatedMinutes);