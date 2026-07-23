using PeopleHub.Domain.Aggregates.Location;

namespace PeopleHub.Application.Common.Interfaces.Persistence;

public interface IProviderLocationRepository
{
    Task AddAsync(
        ProviderLocation location,
        CancellationToken cancellationToken = default);


    Task<ProviderLocation?> GetByProviderProfileIdAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default);


    Task UpdateAsync(
        ProviderLocation location,
        CancellationToken cancellationToken = default);
}