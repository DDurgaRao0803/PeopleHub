using PeopleHub.Domain.Aggregates.Provider;

namespace PeopleHub.SmartMatch.Rules;

public sealed class AvailabilityRule
{
    public bool IsEligible(
        ProviderProfile provider,
        DateTime requestedDate)
    {
        ArgumentNullException.ThrowIfNull(provider);

        var dayOfWeek = requestedDate.DayOfWeek;
        var requestedTime = TimeOnly.FromDateTime(requestedDate);

        return provider.Availabilities.Any(a =>
            a.DayOfWeek == dayOfWeek &&
            requestedTime >= a.StartTime &&
            requestedTime <= a.EndTime);
    }
}