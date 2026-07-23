using Microsoft.EntityFrameworkCore;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Domain.Aggregates.Location;
using PeopleHub.Infrastructure.Persistence.Context;

namespace PeopleHub.Infrastructure.Location;

public sealed class ProviderLocationRepository
    : IProviderLocationRepository
{
    private readonly ApplicationDbContext _context;


    public ProviderLocationRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }



    public async Task AddAsync(
        ProviderLocation location,
        CancellationToken cancellationToken = default)
    {
        await _context.ProviderLocations
            .AddAsync(
                location,
                cancellationToken);
    }



    public async Task<ProviderLocation?> GetByProviderProfileIdAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ProviderLocations
            .FirstOrDefaultAsync(
                x => x.ProviderProfileId == providerProfileId,
                cancellationToken);
    }



    public Task UpdateAsync(
        ProviderLocation location,
        CancellationToken cancellationToken = default)
    {
        _context.ProviderLocations.Update(location);

        return Task.CompletedTask;
    }
}