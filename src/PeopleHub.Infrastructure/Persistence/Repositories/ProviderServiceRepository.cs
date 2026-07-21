using Microsoft.EntityFrameworkCore;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Infrastructure.Persistence.Context;

namespace PeopleHub.Infrastructure.Persistence.Repositories;

public sealed class ProviderServiceRepository : IProviderServiceRepository
{
    private readonly ApplicationDbContext _context;

    public ProviderServiceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProviderService?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.ProviderServices
            .Include(x => x.ProviderProfile)
            .Include(x => x.ServiceCategory)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<ProviderService>> GetByProviderProfileIdAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ProviderServices
            .Where(x => x.ProviderProfileId == providerProfileId)
            .OrderBy(x => x.Title)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ProviderService>> GetActiveByCategoryAsync(
        Guid serviceCategoryId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ProviderServices
            .Where(x =>
                x.ServiceCategoryId == serviceCategoryId &&
                x.IsActive)
            .OrderBy(x => x.Title)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        ProviderService providerService,
        CancellationToken cancellationToken = default)
    {
        await _context.ProviderServices.AddAsync(
            providerService,
            cancellationToken);
    }

    public Task UpdateAsync(
    ProviderService providerService,
    CancellationToken cancellationToken = default)
{
    _context.ProviderServices.Update(providerService);
    return Task.CompletedTask;
}

public Task DeleteAsync(
    ProviderService providerService,
    CancellationToken cancellationToken = default)
{
    _context.ProviderServices.Remove(providerService);
    return Task.CompletedTask;
}
}