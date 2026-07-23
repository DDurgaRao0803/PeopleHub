using PeopleHub.Domain.Aggregates.Bidding;

namespace PeopleHub.Application.Common.Interfaces.Persistence;

public interface IProviderBidRepository
{
    Task AddAsync(
        ProviderBid bid,
        CancellationToken cancellationToken = default);


    Task<ProviderBid?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);


    Task<IReadOnlyList<ProviderBid>> GetByServiceRequestIdAsync(
        Guid serviceRequestId,
        CancellationToken cancellationToken = default);


    Task<IReadOnlyList<ProviderBid>> GetByProviderProfileIdAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default);


    Task UpdateAsync(
        ProviderBid bid,
        CancellationToken cancellationToken = default);
}