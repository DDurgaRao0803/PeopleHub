using Microsoft.EntityFrameworkCore;
using PeopleHub.Domain.Entities;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Infrastructure.Persistence.Context;

namespace PeopleHub.Infrastructure.Persistence.Repositories;

public class ServiceRequestRepository : IServiceRequestRepository
{
    private readonly ApplicationDbContext _context;

    public ServiceRequestRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        ServiceRequest serviceRequest,
        CancellationToken cancellationToken = default)
    {
        await _context.ServiceRequests.AddAsync(
            serviceRequest,
            cancellationToken);
    }

    public async Task<ServiceRequest?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.ServiceRequests
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IEnumerable<ServiceRequest>> GetByCustomerIdAsync(
    Guid customerId,
    CancellationToken cancellationToken = default)
{
    return await _context.ServiceRequests
        .Where(x => x.CustomerId == customerId)
        .OrderByDescending(x => x.CreatedOnUtc)
        .ToListAsync(cancellationToken);
}

public async Task<IEnumerable<ServiceRequest>> GetByProviderProfileIdAsync(
    Guid providerProfileId,
    CancellationToken cancellationToken = default)
{
    return await _context.ServiceRequests
        .Where(x => x.ProviderProfileId == providerProfileId)
        .OrderByDescending(x => x.CreatedOnUtc)
        .ToListAsync(cancellationToken);
}

    public Task UpdateAsync(
        ServiceRequest serviceRequest,
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}