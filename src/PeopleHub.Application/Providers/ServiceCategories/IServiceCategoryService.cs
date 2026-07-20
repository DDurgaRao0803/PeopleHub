using PeopleHub.Domain.Aggregates.Provider;

namespace PeopleHub.Application.Providers.ServiceCategories;

public interface IServiceCategoryService
{
    Task<IReadOnlyList<ServiceCategory>> GetActiveAsync(
        CancellationToken cancellationToken = default);

    Task<ServiceCategory?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<ServiceCategory> CreateAsync(
        string name,
        string? description,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Guid id,
        string name,
        string? description,
        bool isActive,
        CancellationToken cancellationToken = default);
}