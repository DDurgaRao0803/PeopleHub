using PeopleHub.Domain.Aggregates.Payments;

namespace PeopleHub.Application.Common.Interfaces.Persistence;

public interface IWalletRepository
{
    Task<Wallet?> GetByProviderProfileIdAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default);


    Task AddAsync(
        Wallet wallet,
        CancellationToken cancellationToken = default);


    Task UpdateAsync(
        Wallet wallet,
        CancellationToken cancellationToken = default);
}