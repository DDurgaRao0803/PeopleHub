namespace PeopleHub.Contracts.Location;

public sealed class UpdateLocationRequest
{
    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }
}