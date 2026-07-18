namespace PeopleHub.Contracts.Authentication;

public sealed class LogoutRequest
{
    public string RefreshToken { get; init; } = string.Empty;
}