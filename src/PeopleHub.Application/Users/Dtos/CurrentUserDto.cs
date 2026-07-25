namespace PeopleHub.Application.Users.Dtos;

public sealed class CurrentUserDto
{
    public Guid Id { get; init; }

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string? PhoneNumber { get; init; }

    public string? ProfileImageUrl { get; init; }

    public bool IsEmailVerified { get; init; }

    public bool IsPhoneVerified { get; init; }

    public DateTime CreatedOnUtc { get; init; }
}