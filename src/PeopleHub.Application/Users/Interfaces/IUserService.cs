using PeopleHub.Application.Users.Dtos;

namespace PeopleHub.Application.Users.Interfaces;

public interface IUserService
{
    Task<RegisterUserResponse> RegisterAsync(
        RegisterUserRequest request,
        CancellationToken cancellationToken = default);

    Task<LoginResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default);

    Task<UserDto?> GetByIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}