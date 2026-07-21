namespace PeopleHub.Contracts.Providers.Verification;

public sealed class RejectProviderVerificationRequest
{
    public string Reason { get; set; } = string.Empty;
}