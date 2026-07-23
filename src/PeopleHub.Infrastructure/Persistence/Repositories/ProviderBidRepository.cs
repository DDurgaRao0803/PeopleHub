using Microsoft.EntityFrameworkCore;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Domain.Aggregates.Bidding;
using PeopleHub.Infrastructure.Persistence.Context;

namespace PeopleHub.Infrastructure.Persistence.Repositories;

public sealed class ProviderBidRepository
    : IProviderBidRepository
{
    private readonly ApplicationDbContext _context;

    public ProviderBidRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task AddAsync(
        ProviderBid bid,
        CancellationToken cancellationToken = default)
    {
        await _context.ProviderBids.AddAsync(
            bid,
            cancellationToken);
    }


    public async Task<ProviderBid?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.ProviderBids
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }


    public async Task<IReadOnlyList<ProviderBid>> GetByServiceRequestIdAsync(
    Guid serviceRequestId,
    CancellationToken cancellationToken = default)
{
    var bids = await _context.ProviderBids
        .Where(x => x.ServiceRequestId == serviceRequestId)
        .ToListAsync(cancellationToken);


    return bids
        .OrderBy(x => x.Amount)
        .ToList();
}


    public async Task<IReadOnlyList<ProviderBid>> GetByProviderProfileIdAsync(
    Guid providerProfileId,
    CancellationToken cancellationToken = default)
{
    return await _context.ProviderBids
        .Where(x => x.ProviderProfileId == providerProfileId)
        .ToListAsync(cancellationToken);
}


    public Task UpdateAsync(
        ProviderBid bid,
        CancellationToken cancellationToken = default)
    {
        _context.ProviderBids.Update(bid);

        return Task.CompletedTask;
    }
}