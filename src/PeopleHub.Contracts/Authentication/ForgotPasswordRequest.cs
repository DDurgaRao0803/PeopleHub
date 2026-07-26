namespace PeopleHub.Contracts.Authentication;

public sealed class ForgotPasswordRequest
{
    public string Email { get; init; } = string.Empty;
}