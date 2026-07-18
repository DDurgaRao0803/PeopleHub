using Microsoft.EntityFrameworkCore;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Domain.Aggregates.User;
using PeopleHub.Infrastructure.Persistence.Context;
namespace PeopleHub.Infrastructure.Persistence.Repositories;
using PeopleHub.Domain.Enums;
using PeopleHub.Contracts.Users;

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(
    Guid id,
    CancellationToken cancellationToken = default)
{
    return await _context.Users
        .AsNoTracking()
        .Include(u => u.Roles)
        .FirstOrDefaultAsync(
            u => u.Id == id &&
                 u.Status != UserStatus.Deleted,
            cancellationToken);
}

    public async Task<User?> GetByEmailAsync(
    string email,
    CancellationToken cancellationToken = default)
{
    return await _context.Users
        .Include(user => user.Roles)
        .Include(user => user.RefreshTokens)
        .FirstOrDefaultAsync(
            user => user.Email.Value == email,
            cancellationToken);
}

    public async Task<User?> GetByPhoneNumberAsync(
        string phoneNumber,
        CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .Include(user => user.Roles)
            .FirstOrDefaultAsync(
                user => user.PhoneNumber.Value == phoneNumber,
                cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        return await _context.Users.AnyAsync(
            user => user.Email.Value == email,
            cancellationToken);
    }

    public async Task<bool> ExistsByPhoneNumberAsync(
        string phoneNumber,
        CancellationToken cancellationToken = default)
    {
        return await _context.Users.AnyAsync(
            user => user.PhoneNumber.Value == phoneNumber,
            cancellationToken);
    }

    public async Task AddAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(
            user,
            cancellationToken);
    }

    public async Task<User?> GetByRefreshTokenHashAsync(
    string tokenHash,
    CancellationToken cancellationToken = default)
{
    ArgumentException.ThrowIfNullOrWhiteSpace(tokenHash);

    return await _context.Users
        .Include(u => u.RefreshTokens)
        .FirstOrDefaultAsync(
            u => u.RefreshTokens.Any(rt => rt.TokenHash == tokenHash),
            cancellationToken);
}

    public Task UpdateAsync(
    User user,
    CancellationToken cancellationToken = default)
{
    // The entity was loaded from this DbContext, so it is already tracked.
    return Task.CompletedTask;
}

    public Task DeleteAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        _context.Users.Remove(user);

        return Task.CompletedTask;
    }


public async Task<(IReadOnlyList<User> Users, int TotalCount)> GetPagedAsync(
    GetUsersRequest request,
    CancellationToken cancellationToken = default)
{
    var query = _context.Users
    .AsNoTracking()
    .Include(user => user.Roles)
    .Where(user => user.Status != UserStatus.Deleted);

if (!string.IsNullOrWhiteSpace(request.FirstName))
{
    query = query.Where(user =>
        user.FirstName.Contains(request.FirstName));
}
if (!string.IsNullOrWhiteSpace(request.LastName))
{
    query = query.Where(user =>
        user.LastName.Contains(request.LastName));
}
if (!string.IsNullOrWhiteSpace(request.Email))
{
    query = query.Where(user =>
        user.Email.Value.Contains(request.Email));
}
if (!string.IsNullOrWhiteSpace(request.Status) &&
    Enum.TryParse<UserStatus>(
        request.Status,
        true,
        out var status))
{
    query = query.Where(user => user.Status == status);
}

if (!string.IsNullOrWhiteSpace(request.Role) &&
    Enum.TryParse<Role>(
        request.Role,
        true,
        out var role))
{
    query = query.Where(user => user.Role == role);
}

    var totalCount = await query.CountAsync(cancellationToken);

    query = (request.SortBy?.ToLowerInvariant(), request.SortDirection?.ToLowerInvariant()) switch
{
    ("firstname", "descending") => query.OrderByDescending(user => user.FirstName),
    ("lastname", "ascending") => query.OrderBy(user => user.LastName),
    ("lastname", "descending") => query.OrderByDescending(user => user.LastName),
    ("email", "ascending") => query.OrderBy(user => user.Email.Value),
    ("email", "descending") => query.OrderByDescending(user => user.Email.Value),
    ("status", "ascending") => query.OrderBy(user => user.Status),
    ("status", "descending") => query.OrderByDescending(user => user.Status),
    ("role", "ascending") => query.OrderBy(user => user.Role),
    ("role", "descending") => query.OrderByDescending(user => user.Role),
    (_, "descending") => query.OrderByDescending(user => user.FirstName),
    _ => query.OrderBy(user => user.FirstName)
};

var users = await query
    .Skip((request.PageNumber - 1) * request.PageSize)
    .Take(request.PageSize)
    .ToListAsync(cancellationToken);

return (users, totalCount);
}

}