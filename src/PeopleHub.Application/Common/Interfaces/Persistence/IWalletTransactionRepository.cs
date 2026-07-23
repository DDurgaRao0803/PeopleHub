using PeopleHub.Domain.Aggregates.Payments;

namespace PeopleHub.Application.Common.Interfaces.Persistence;

public interface IWalletTransactionRepository
{
    Task AddAsync(
        WalletTransaction transaction,
        CancellationToken cancellationToken = default);


    Task<IReadOnlyList<WalletTransaction>> GetByWalletIdAsync(
        Guid walletId,
        CancellationToken cancellationToken = default);
}