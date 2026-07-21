namespace PeopleHub.Contracts.Providers.Services;

public sealed class CreateProviderServiceRequest
{
    public Guid ProviderProfileId { get; set; }

    public Guid ServiceCategoryId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal BasePrice { get; set; }

    public int EstimatedDurationMinutes { get; set; }
}