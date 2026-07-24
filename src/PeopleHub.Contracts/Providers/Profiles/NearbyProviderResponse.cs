namespace PeopleHub.Contracts.Providers.Profiles;

public sealed record NearbyProviderResponse(
    Guid ProviderProfileId,
    Guid UserId,
    string FullName,
    string ServiceCategory,
    decimal Rating,
    bool IsAvailable);