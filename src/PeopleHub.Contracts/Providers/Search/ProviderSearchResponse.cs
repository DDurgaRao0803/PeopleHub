namespace PeopleHub.Contracts.Providers.Search;

public sealed class ProviderSearchResponse
{
    public Guid ProviderProfileId { get; set; }

    public Guid UserId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string ServiceCategory { get; set; } = string.Empty;

    public bool IsVerified { get; set; }

    public string Bio { get; set; } = string.Empty;

    public int ExperienceYears { get; set; }
}