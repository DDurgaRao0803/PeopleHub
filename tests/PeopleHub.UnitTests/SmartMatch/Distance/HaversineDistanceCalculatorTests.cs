using PeopleHub.SmartMatch.Distance;

namespace PeopleHub.SmartMatch.Distance;

public class HaversineDistanceCalculatorTests
{
    [Fact]
    public void CalculateDistance_Should_Return_Zero_For_Same_Location()
    {
        var calculator = new HaversineDistanceCalculator();

        var distance = calculator.CalculateDistance(
            17.3850m,
            78.4867m,
            17.3850m,
            78.4867m);

        Assert.Equal(0m, distance);
    }

    [Fact]
    public void CalculateDistance_Should_Be_Symmetric()
    {
        var calculator = new HaversineDistanceCalculator();

        var forward = calculator.CalculateDistance(
            17.3850m,
            78.4867m,
            12.9716m,
            77.5946m);

        var reverse = calculator.CalculateDistance(
            12.9716m,
            77.5946m,
            17.3850m,
            78.4867m);

        Assert.Equal(forward, reverse);
    }

    [Fact]
    public void CalculateDistance_Should_Return_Approximate_Distance_Between_Hyderabad_And_Bengaluru()
    {
        var calculator = new HaversineDistanceCalculator();

        var distance = calculator.CalculateDistance(
            17.3850m,
            78.4867m,
            12.9716m,
            77.5946m);

        Assert.InRange(distance, 495m, 505m);
    }

    [Fact]
    public void CalculateDistance_Should_Return_Positive_Value()
    {
        var calculator = new HaversineDistanceCalculator();

        var distance = calculator.CalculateDistance(
            25m,
            55m,
            26m,
            56m);

        Assert.True(distance > 0);
    }
}