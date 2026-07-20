namespace PeopleHub.Contracts.Providers.Availability;

public sealed record UpdateProviderAvailabilityRequest(
    DayOfWeek DayOfWeek,
    TimeOnly StartTime,
    TimeOnly EndTime);