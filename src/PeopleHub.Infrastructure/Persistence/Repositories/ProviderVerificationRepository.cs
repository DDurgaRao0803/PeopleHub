using Microsoft.EntityFrameworkCore;
using PeopleHub.Application.Interfaces.Repositories;
using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Infrastructure.Persistence.Context;

namespace PeopleHub.Infrastructure.Persistence.Repositories;

public sealed class ProviderVerificationRepository : IProviderVerificationRepository
{
    private readonly ApplicationDbContext _context;

    public ProviderVerificationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProviderVerification?> GetByProviderProfileIdAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ProviderVerifications
            .FirstOrDefaultAsync(
                x => x.ProviderProfileId == providerProfileId,
                cancellationToken);
    }

    public async Task AddAsync(
        ProviderVerification verification,
        CancellationToken cancellationToken = default)
    {
        await _context.ProviderVerifications.AddAsync(
            verification,
            cancellationToken);
    }

    public void Update(ProviderVerification verification)
    {
        _context.ProviderVerifications.Update(verification);
    }

    public void Remove(ProviderVerification verification)
    {
        _context.ProviderVerifications.Remove(verification);
    }
}