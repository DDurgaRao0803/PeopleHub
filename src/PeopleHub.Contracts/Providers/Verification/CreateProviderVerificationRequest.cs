namespace PeopleHub.Contracts.Providers;

public sealed class CreateProviderVerificationRequest
{
    public GovernmentIdType GovernmentIdType { get; init; }

    public string GovernmentIdNumber { get; init; } = string.Empty;

    public string FrontImageUrl { get; init; } = string.Empty;

    public string? BackImageUrl { get; init; }

    public string SelfieImageUrl { get; init; } = string.Empty;
}