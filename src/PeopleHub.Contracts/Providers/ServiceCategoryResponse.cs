namespace PeopleHub.Contracts.Providers;

public sealed record ServiceCategoryResponse(
    Guid Id,
    string Name,
    string? Description,
    bool IsActive);