using PeopleHub.Domain.Aggregates.Provider;

namespace PeopleHub.Application.Common.Interfaces.Persistence;

public interface IProviderRepository
{
    Task<ProviderProfile?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<ProviderProfile?> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProviderProfile>> GetEligibleProvidersAsync(
    Guid serviceCategoryId,
    CancellationToken cancellationToken = default);

    Task AddAsync(
        ProviderProfile provider,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        ProviderProfile provider,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        ProviderProfile provider,
        CancellationToken cancellationToken = default);
}