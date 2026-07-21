namespace PeopleHub.Contracts.Providers.Search;

public sealed class SearchProvidersRequest
{
    public string? Keyword { get; set; }

    public Guid? ServiceCategoryId { get; set; }

    public bool VerifiedOnly { get; set; }

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 20;
}