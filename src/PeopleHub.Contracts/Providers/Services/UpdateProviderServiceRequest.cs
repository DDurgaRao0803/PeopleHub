namespace PeopleHub.Contracts.Providers.Services;

public sealed class UpdateProviderServiceRequest
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal BasePrice { get; set; }

    public int EstimatedDurationMinutes { get; set; }

    public bool IsActive { get; set; }
}