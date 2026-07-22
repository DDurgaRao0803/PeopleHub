using PeopleHub.Application.Administration;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Contracts.Administration;
using PeopleHub.Contracts.Users;
using PeopleHub.Domain.Enums;

namespace PeopleHub.Infrastructure.Administration;

public sealed class AdminUserService : IAdminUserService
{
    private readonly IUserRepository _userRepository;

    public AdminUserService(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IReadOnlyList<UserSummaryResponse>> GetUsersAsync(
    GetUsersRequest request,
    CancellationToken cancellationToken = default)
    {
        var (users, _) = await _userRepository.GetPagedAsync(
    request,
    cancellationToken);

        return users
            .Select(user => new UserSummaryResponse
            {
                UserId = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email.Value,
                Role = user.Role.ToString(),
                Status = user.Status.ToString(),
                CreatedAt = user.CreatedOnUtc
            })
            .ToList();
    }
    
    public async Task<UserDetailsResponse?> GetUserByIdAsync(
    Guid id,
    CancellationToken cancellationToken = default)
{
    var user = await _userRepository.GetByIdAsync(
        id,
        cancellationToken);

    if (user is null)
    {
        return null;
    }

    return new UserDetailsResponse
    {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email.Value,
        Role = user.Role.ToString(),
        Status = user.Status.ToString(),
        CreatedAt = user.CreatedOnUtc,
        LastLoginUtc = user.LastLoginUtc
    };
}
public async Task<bool> UpdateUserStatusAsync(
    Guid id,
    UpdateUserStatusRequest request,
    CancellationToken cancellationToken = default)
{
    var user = await _userRepository.GetByIdAsync(
        id,
        cancellationToken);

    if (user is null)
    {
        return false;
    }

    if (!Enum.TryParse<UserStatus>(
    request.Status,
    true,
    out var status))
{
    throw new ArgumentException("Invalid user status.");
}

    switch (status)
{
    case UserStatus.Active:
        user.Activate();
        break;

    case UserStatus.Suspended:
        user.Suspend();
        break;

    case UserStatus.Inactive:
        user.Deactivate();
        break;

    case UserStatus.Deleted:
        user.MarkAsDeleted();
        break;

    case UserStatus.PendingVerification:
        throw new InvalidOperationException(
            "Cannot manually change a user back to PendingVerification.");

    default:
        throw new ArgumentOutOfRangeException(nameof(status));
}

    await _userRepository.UpdateAsync(
        user,
        cancellationToken);

    return true;
}

}