namespace PeopleHub.Contracts.Providers.Profiles;

public sealed class ProviderProfileResponse
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public string Bio { get; init; } = string.Empty;

    public int ExperienceYears { get; init; }

    public string VerificationStatus { get; init; } = string.Empty;
}