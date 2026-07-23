using Microsoft.EntityFrameworkCore;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Domain.Aggregates.Payments;
using PeopleHub.Infrastructure.Persistence.Context;

namespace PeopleHub.Infrastructure.Payments;

public sealed class WalletTransactionRepository
    : IWalletTransactionRepository
{
    private readonly ApplicationDbContext _context;


    public WalletTransactionRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }



    public async Task AddAsync(
        WalletTransaction transaction,
        CancellationToken cancellationToken = default)
    {
        await _context.WalletTransactions
            .AddAsync(
                transaction,
                cancellationToken);
    }



    public async Task<IReadOnlyList<WalletTransaction>> GetByWalletIdAsync(
        Guid walletId,
        CancellationToken cancellationToken = default)
    {
        return await _context.WalletTransactions
            .Where(x => x.WalletId == walletId)
            .OrderByDescending(x => x.CreatedOnUtc)
            .ToListAsync(cancellationToken);
    }
}