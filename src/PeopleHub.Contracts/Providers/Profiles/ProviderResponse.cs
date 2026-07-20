namespace PeopleHub.Contracts.Providers.Profiles;

public sealed record ProviderResponse(
    Guid Id,
    string BusinessName,
    string ContactPerson,
    string Email,
    string PhoneNumber,
    Guid ServiceCategoryId,
    bool IsActive);