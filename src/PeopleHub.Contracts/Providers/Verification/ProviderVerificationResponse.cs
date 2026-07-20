
namespace PeopleHub.Contracts.Providers.Verification;

public sealed class ProviderVerificationResponse
{
    public Guid Id { get; init; }

    public GovernmentIdType GovernmentIdType { get; init; }

    public string GovernmentIdNumber { get; init; } = string.Empty;

    public string FrontImageUrl { get; init; } = string.Empty;

    public string? BackImageUrl { get; init; }

    public string SelfieImageUrl { get; init; } = string.Empty;

    public string VerificationStatus { get; init; } = string.Empty;

    public DateTime? SubmittedOnUtc { get; init; }

    public DateTime? ReviewedOnUtc { get; init; }

    public string? RejectionReason { get; init; }
}