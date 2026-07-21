namespace PeopleHub.Contracts.Providers.Reviews;

public sealed class CreateReviewRequest
{

    public Guid ProviderProfileId { get; set; }

    public Guid ServiceRequestId { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; } = string.Empty;
}