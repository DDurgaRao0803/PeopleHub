namespace PeopleHub.Contracts.Administration;

public sealed class ProviderVerificationSummaryResponse
{
    public Guid ProviderId { get; init; }

    public Guid UserId { get; init; }

    public string FullName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string VerificationStatus { get; init; } = string.Empty;

    public DateTime SubmittedOn { get; init; }
}