using Microsoft.EntityFrameworkCore;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Infrastructure.Persistence.Context;
using PeopleHub.Contracts.Providers.Search;

namespace PeopleHub.Infrastructure.Persistence.Repositories;

public sealed class ProviderProfileRepository : IProviderProfileRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProviderProfileRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProviderProfile?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.ProviderProfiles
            .Include(p => p.Skills)
            .FirstOrDefaultAsync(
                p => p.Id == id,
                cancellationToken);
    }

    public async Task<ProviderProfile?> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.ProviderProfiles
            .Include(p => p.Skills)
            .FirstOrDefaultAsync(
                p => p.UserId == userId,
                cancellationToken);
    }

    public async Task AddAsync(
        ProviderProfile providerProfile,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.ProviderProfiles.AddAsync(
            providerProfile,
            cancellationToken);
    }

    public Task UpdateAsync(
    ProviderProfile providerProfile,
    CancellationToken cancellationToken = default)
{
    // No action required.
    // The entity is already tracked by EF Core.
    return Task.CompletedTask;
}

    public async Task AddAvailabilityAsync(
        ProviderAvailability availability,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.ProviderAvailabilities.AddAsync(
            availability,
            cancellationToken);
    }

    public Task DeleteAsync(
        ProviderProfile providerProfile,
        CancellationToken cancellationToken = default)
    {
        _dbContext.ProviderProfiles.Remove(providerProfile);

        return Task.CompletedTask;
    }

    public async Task<IReadOnlyList<ProviderProfile>> GetAllAsync(
    CancellationToken cancellationToken = default)
{
    return await _dbContext.ProviderProfiles
        .Include(p => p.Skills)
        .AsNoTracking()
        .ToListAsync(cancellationToken);
}

public async Task<ProviderProfile?> GetByIdWithAvailabilityAsync(
    Guid id,
    CancellationToken cancellationToken = default)
{
    return await _dbContext.ProviderProfiles
        .Include(p => p.Skills)
        .Include(p => p.Availabilities)
        .FirstOrDefaultAsync(
            p => p.Id == id,
            cancellationToken);
}

public async Task<ProviderProfile?> GetByUserIdWithAvailabilityAsync(
    Guid userId,
    CancellationToken cancellationToken = default)
{
    return await _dbContext.ProviderProfiles
        .Include(p => p.Skills)
        .Include(p => p.Availabilities)
        .FirstOrDefaultAsync(
            p => p.UserId == userId,
            cancellationToken);
}

public async Task<IReadOnlyList<ProviderProfile>> GetNearbyAsync(
    CancellationToken cancellationToken = default)
{
    return await _dbContext.ProviderProfiles
        .Include(p => p.Skills)
            .ThenInclude(s => s.ServiceCategory)
        .AsNoTracking()
        .OrderByDescending(p => p.LastActiveUtc)
        .ToListAsync(cancellationToken);
}

}