using Microsoft.EntityFrameworkCore;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Infrastructure.Persistence.Context;

namespace PeopleHub.Infrastructure.Persistence.Repositories;

public sealed class ProviderRepository : IProviderRepository
{
    private readonly ApplicationDbContext _context;

    public ProviderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProviderProfile?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.ProviderProfiles
            .Include(provider => provider.Skills)
            .FirstOrDefaultAsync(
                provider => provider.Id == id,
                cancellationToken);
    }

    public async Task<ProviderProfile?> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ProviderProfiles
            .Include(provider => provider.Skills)
            .FirstOrDefaultAsync(
                provider => provider.UserId == userId,
                cancellationToken);
    }

    public async Task AddAsync(
        ProviderProfile provider,
        CancellationToken cancellationToken = default)
    {
        await _context.ProviderProfiles.AddAsync(
            provider,
            cancellationToken);
    }

    public Task UpdateAsync(
        ProviderProfile provider,
        CancellationToken cancellationToken = default)
    {
        _context.ProviderProfiles.Update(provider);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(
        ProviderProfile provider,
        CancellationToken cancellationToken = default)
    {
        _context.ProviderProfiles.Remove(provider);

        return Task.CompletedTask;
    }
}