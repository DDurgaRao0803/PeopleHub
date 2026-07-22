using PeopleHub.Contracts.Administration;
using PeopleHub.Contracts.Users;

namespace PeopleHub.Application.Administration;

public interface IAdminUserService
{
    Task<IReadOnlyList<UserSummaryResponse>> GetUsersAsync(
        GetUsersRequest request,
        CancellationToken cancellationToken = default);

    Task<UserDetailsResponse?> GetUserByIdAsync(
    Guid id,
    CancellationToken cancellationToken = default);

    Task<bool> UpdateUserStatusAsync(
    Guid id,
    UpdateUserStatusRequest request,
    CancellationToken cancellationToken = default);

}