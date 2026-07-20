namespace PeopleHub.Contracts.Providers.Availability;

public sealed record ProviderAvailabilityResponse(
    Guid Id,
    DayOfWeek DayOfWeek,
    TimeOnly StartTime,
    TimeOnly EndTime);