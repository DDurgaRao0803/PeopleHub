namespace PeopleHub.Contracts.Providers.Availability;

public sealed record CreateProviderAvailabilityRequest(
    DayOfWeek DayOfWeek,
    TimeOnly StartTime,
    TimeOnly EndTime);