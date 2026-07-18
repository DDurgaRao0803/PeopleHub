using PeopleHub.Domain.Aggregates.Provider;

namespace PeopleHub.Application.Common.Interfaces.Persistence;

public interface IServiceCategoryRepository
{
    Task<ServiceCategory?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<ServiceCategory?> GetByNameAsync(
        string name,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ServiceCategory>> GetActiveAsync(
        CancellationToken cancellationToken = default);

    Task AddAsync(
        ServiceCategory category,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        ServiceCategory category,
        CancellationToken cancellationToken = default);
}