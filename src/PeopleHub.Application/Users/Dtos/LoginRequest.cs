namespace PeopleHub.Application.Users.Dtos;

public sealed class LoginRequest
{
    public string EmailOrPhone { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;
}