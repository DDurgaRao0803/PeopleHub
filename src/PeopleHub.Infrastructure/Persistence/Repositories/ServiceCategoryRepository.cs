using Microsoft.EntityFrameworkCore;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Infrastructure.Persistence.Context;

namespace PeopleHub.Infrastructure.Persistence.Repositories;

public sealed class ServiceCategoryRepository : IServiceCategoryRepository
{
    private readonly ApplicationDbContext _context;

    public ServiceCategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceCategory?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.ServiceCategories
            .FirstOrDefaultAsync(
                category => category.Id == id,
                cancellationToken);
    }

    public async Task<ServiceCategory?> GetByNameAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        return await _context.ServiceCategories
            .FirstOrDefaultAsync(
                category => category.Name == name,
                cancellationToken);
    }

    public async Task<IReadOnlyList<ServiceCategory>> GetActiveAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.ServiceCategories
            .Where(category => category.IsActive)
            .OrderBy(category => category.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        ServiceCategory category,
        CancellationToken cancellationToken = default)
    {
        await _context.ServiceCategories.AddAsync(
            category,
            cancellationToken);
    }

    public Task UpdateAsync(
        ServiceCategory category,
        CancellationToken cancellationToken = default)
    {
        _context.ServiceCategories.Update(category);

        return Task.CompletedTask;
    }
}