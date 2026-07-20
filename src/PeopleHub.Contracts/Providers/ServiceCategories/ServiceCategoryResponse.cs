namespace PeopleHub.Contracts.Providers.ServiceCategories;

public sealed record ServiceCategoryResponse(
    Guid Id,
    string Name,
    string? Description,
    bool IsActive);