using PeopleHub.Contracts.Common;
using PeopleHub.Contracts.Users;

namespace PeopleHub.Application.Users;

public interface IUserService
{
    Task<CurrentUserResponse> GetCurrentUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<UserResponse> GetUserByIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<PagedResponse<UserResponse>> GetAllUsersAsync(
    GetUsersRequest request,
    CancellationToken cancellationToken = default);

    Task<UserResponse> UpdateUserAsync(
        Guid userId,
        UpdateUserRequest request,
        CancellationToken cancellationToken = default);

    Task DeleteUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}