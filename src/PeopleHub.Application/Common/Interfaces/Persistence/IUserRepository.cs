using PeopleHub.Domain.Aggregates.User;
using PeopleHub.Contracts.Users;

namespace PeopleHub.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<(IReadOnlyList<User> Users, int TotalCount)> GetPagedAsync(
    GetUsersRequest request,
    CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task<User?> GetByPhoneNumberAsync(
        string phoneNumber,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByPhoneNumberAsync(
        string phoneNumber,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        User user,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        User user,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        User user,
        CancellationToken cancellationToken = default);
    Task<User?> GetByRefreshTokenHashAsync(
    string tokenHash,
    CancellationToken cancellationToken = default);

}