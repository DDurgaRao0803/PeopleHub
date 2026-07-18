namespace PeopleHub.Application.Users.Dtos;

public sealed class RegisterUserResponse
{
    public Guid UserId { get; init; }

    public string Message { get; init; } = string.Empty;
}