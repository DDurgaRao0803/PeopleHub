using Microsoft.EntityFrameworkCore;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Domain.Aggregates.User;
using PeopleHub.Infrastructure.Persistence.Context;

namespace PeopleHub.Infrastructure.Persistence.Repositories;

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
            .Include(user => user.Roles)
            .FirstOrDefaultAsync(
                user => user.Id == id,
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

    
}