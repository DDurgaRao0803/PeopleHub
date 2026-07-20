namespace PeopleHub.Contracts.Providers;

public sealed class UpdateServiceCategoryRequest
{
    public string Name { get; init; } = string.Empty;

    public string? Description { get; init; }

    public bool IsActive { get; init; }
}