namespace PeopleHub.Contracts.Providers.Bidding;

public sealed record ProviderBidResponse(
    Guid Id,
    Guid ServiceRequestId,
    Guid ProviderProfileId,
    decimal Amount,
    int EstimatedMinutes,
    string Status);