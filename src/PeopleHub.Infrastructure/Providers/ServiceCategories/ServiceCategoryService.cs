using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Providers.ServiceCategories;
using PeopleHub.Domain.Aggregates.Provider;

namespace PeopleHub.Infrastructure.Providers.ServiceCategories;

public sealed class ServiceCategoryService : IServiceCategoryService
{
    private readonly IServiceCategoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ServiceCategoryService(
        IServiceCategoryRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<ServiceCategory>> GetActiveAsync(
        CancellationToken cancellationToken = default)
    {
        return await _repository.GetActiveAsync(cancellationToken);
    }

    public async Task<ServiceCategory?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<ServiceCategory> CreateAsync(
        string name,
        string? description,
        CancellationToken cancellationToken = default)
    {
        var existing = await _repository.GetByNameAsync(name, cancellationToken);

        if (existing is not null)
        {
            throw new InvalidOperationException(
                $"Service category '{name}' already exists.");
        }

        var category = new ServiceCategory(name, description);

        await _repository.AddAsync(category, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task UpdateAsync(
    Guid id,
    string name,
    string? description,
    bool isActive,
    CancellationToken cancellationToken = default)
{
    Console.WriteLine("================================");
    Console.WriteLine($"Service received id: {id}");
    Console.WriteLine($"Repository type: {_repository.GetType().FullName}");

    var category = await _repository.GetByIdAsync(id, cancellationToken);

    if (category is null)
    {
        Console.WriteLine("Repository returned NULL");
        throw new KeyNotFoundException("Service category not found.");
    }

    Console.WriteLine($"Repository returned entity: {category.Id}");

    category.Rename(name);
    category.UpdateDescription(description);

    if (isActive)
    {
        category.Activate();
    }
    else
    {
        category.Deactivate();
    }

    await _repository.UpdateAsync(category, cancellationToken);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    Console.WriteLine("Update completed successfully");
    Console.WriteLine("================================");
}
}