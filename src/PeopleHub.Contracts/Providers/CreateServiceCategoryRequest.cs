namespace PeopleHub.Contracts.Providers;

public sealed class CreateServiceCategoryRequest
{
    public string Name { get; init; } = string.Empty;

    public string? Description { get; init; }
}