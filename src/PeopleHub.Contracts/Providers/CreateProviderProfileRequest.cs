namespace PeopleHub.Contracts.Providers;

public sealed class CreateProviderProfileRequest
{
    public string Bio { get; init; } = string.Empty;

    public int ExperienceYears { get; init; }
}