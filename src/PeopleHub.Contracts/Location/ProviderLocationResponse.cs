namespace PeopleHub.Contracts.Location;

public sealed class ProviderLocationResponse
{
    public Guid ProviderProfileId { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public DateTime UpdatedOnUtc { get; set; }
}