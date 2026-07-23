using PeopleHub.Domain.Common;

namespace PeopleHub.Domain.Aggregates.Location;

public sealed class ProviderLocation : AuditableEntity
{
    private ProviderLocation()
    {
    }


    public ProviderLocation(
        Guid providerProfileId,
        decimal latitude,
        decimal longitude)
    {
        ValidateCoordinates(
            latitude,
            longitude);


        ProviderProfileId = providerProfileId;

        Latitude = latitude;

        Longitude = longitude;

        UpdatedOnUtc = DateTime.UtcNow;
    }



    public Guid ProviderProfileId { get; private set; }



    public decimal Latitude { get; private set; }



    public decimal Longitude { get; private set; }



    public DateTime UpdatedOnUtc { get; private set; }



    public void UpdateLocation(
        decimal latitude,
        decimal longitude)
    {
        ValidateCoordinates(
            latitude,
            longitude);


        Latitude = latitude;

        Longitude = longitude;

        UpdatedOnUtc = DateTime.UtcNow;
    }



    private static void ValidateCoordinates(
        decimal latitude,
        decimal longitude)
    {
        if (latitude < -90m || latitude > 90m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(latitude));
        }


        if (longitude < -180m || longitude > 180m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(longitude));
        }
    }
}