using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Contracts.Providers.Search;

namespace PeopleHub.Application.Common.Interfaces.Persistence;

public interface IProviderProfileRepository
{
    Task<ProviderProfile?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<ProviderProfile?> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ProviderProfile>> GetAllAsync(
    CancellationToken cancellationToken = default);

    Task AddAsync(
        ProviderProfile providerProfile,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        ProviderProfile providerProfile,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        ProviderProfile providerProfile,
        CancellationToken cancellationToken = default);

    Task<ProviderProfile?> GetByIdWithAvailabilityAsync(
    Guid id,
    CancellationToken cancellationToken = default);

Task<ProviderProfile?> GetByUserIdWithAvailabilityAsync(
    Guid userId,
    CancellationToken cancellationToken = default);

    Task AddAvailabilityAsync(
        ProviderAvailability availability,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProviderProfile>> GetNearbyAsync(
    CancellationToken cancellationToken = default);
}