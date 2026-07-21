using PeopleHub.Domain.Aggregates.Provider;

namespace PeopleHub.Application.Common.Interfaces.Persistence;

/// <summary>
/// Repository for Provider Services.
/// </summary>
public interface IProviderServiceRepository
{
    Task<ProviderService?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProviderService>> GetByProviderProfileIdAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProviderService>> GetActiveByCategoryAsync(
        Guid serviceCategoryId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        ProviderService providerService,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
    ProviderService providerService,
    CancellationToken cancellationToken = default);

Task DeleteAsync(
    ProviderService providerService,
    CancellationToken cancellationToken = default);
}