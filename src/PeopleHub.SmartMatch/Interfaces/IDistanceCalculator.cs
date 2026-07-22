namespace PeopleHub.SmartMatch.Interfaces;

public interface IDistanceCalculator
{
    decimal CalculateDistance(
        decimal latitude1,
        decimal longitude1,
        decimal latitude2,
        decimal longitude2);
}