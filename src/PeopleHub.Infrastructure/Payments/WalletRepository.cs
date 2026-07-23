using Microsoft.EntityFrameworkCore;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Domain.Aggregates.Payments;
using PeopleHub.Infrastructure.Persistence.Context;

namespace PeopleHub.Infrastructure.Payments;

public sealed class WalletRepository
    : IWalletRepository
{
    private readonly ApplicationDbContext _context;


    public WalletRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }



    public async Task<Wallet?> GetByProviderProfileIdAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Wallets
            .FirstOrDefaultAsync(
                x => x.ProviderProfileId == providerProfileId,
                cancellationToken);
    }



    public async Task AddAsync(
        Wallet wallet,
        CancellationToken cancellationToken = default)
    {
        await _context.Wallets
            .AddAsync(
                wallet,
                cancellationToken);
    }



    public Task UpdateAsync(
        Wallet wallet,
        CancellationToken cancellationToken = default)
    {
        _context.Wallets.Update(wallet);

        return Task.CompletedTask;
    }
}