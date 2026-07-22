using PeopleHub.SmartMatch.Interfaces;

namespace PeopleHub.SmartMatch.Distance;

public sealed class HaversineDistanceCalculator : IDistanceCalculator
{
    private const double EarthRadiusKm = 6371d;

    public decimal CalculateDistance(
        decimal latitude1,
        decimal longitude1,
        decimal latitude2,
        decimal longitude2)
    {
        var lat1 = DegreesToRadians((double)latitude1);
        var lon1 = DegreesToRadians((double)longitude1);
        var lat2 = DegreesToRadians((double)latitude2);
        var lon2 = DegreesToRadians((double)longitude2);

        var deltaLat = lat2 - lat1;
        var deltaLon = lon2 - lon1;

        var a =
            Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
            Math.Cos(lat1) * Math.Cos(lat2) *
            Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

        var c = 2 * Math.Atan2(
            Math.Sqrt(a),
            Math.Sqrt(1 - a));

        var distance = EarthRadiusKm * c;

        return Math.Round((decimal)distance, 2);
    }

    private static double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180d;
    }
}