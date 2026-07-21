namespace PeopleHub.Contracts.Providers.Reviews;

public sealed class ReviewResponse
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public Guid ProviderProfileId { get; set; }

    public Guid ServiceRequestId { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; } = string.Empty;
}