namespace PeopleHub.Contracts.Authentication;

public sealed class RefreshTokenRequest
{
    public string RefreshToken { get; init; } = string.Empty;
}