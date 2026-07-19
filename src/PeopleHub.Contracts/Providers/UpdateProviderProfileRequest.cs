namespace PeopleHub.Contracts.Providers;

public sealed class UpdateProviderProfileRequest
{
    public string Bio { get; init; } = string.Empty;

    public int ExperienceYears { get; init; }
}