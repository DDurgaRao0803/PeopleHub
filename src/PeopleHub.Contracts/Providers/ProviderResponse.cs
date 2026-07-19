namespace PeopleHub.Contracts.Providers;

public sealed record ProviderResponse(
    Guid Id,
    string BusinessName,
    string ContactPerson,
    string Email,
    string PhoneNumber,
    Guid ServiceCategoryId,
    bool IsActive);