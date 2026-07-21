namespace PeopleHub.Application.Common.Interfaces.Services;

public interface ICurrentUserService
{
    Guid UserId { get; }

    bool IsAuthenticated { get; }

    string? Email { get; }

    string? Role { get; }
}