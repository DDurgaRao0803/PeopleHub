using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Providers.Services;
using PeopleHub.Contracts.Providers.Services;
using PeopleHub.Domain.Aggregates.Provider;

namespace PeopleHub.Infrastructure.Providers.Services;

public sealed class ProviderServiceService : IProviderServiceService
{
    private readonly IProviderServiceRepository _providerServiceRepository;
    private readonly IProviderProfileRepository _providerProfileRepository;
    private readonly IServiceCategoryRepository _serviceCategoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProviderServiceService(
        IProviderServiceRepository providerServiceRepository,
        IProviderProfileRepository providerProfileRepository,
        IServiceCategoryRepository serviceCategoryRepository,
        IUnitOfWork unitOfWork)
    {
        _providerServiceRepository = providerServiceRepository;
        _providerProfileRepository = providerProfileRepository;
        _serviceCategoryRepository = serviceCategoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProviderServiceResponse> CreateAsync(
        CreateProviderServiceRequest request,
        CancellationToken cancellationToken = default)
    {
        var provider = await _providerProfileRepository.GetByIdAsync(
            request.ProviderProfileId,
            cancellationToken);

        if (provider is null)
        {
            throw new KeyNotFoundException("Provider profile not found.");
        }

        var category = await _serviceCategoryRepository.GetByIdAsync(
            request.ServiceCategoryId,
            cancellationToken);

        if (category is null)
        {
            throw new KeyNotFoundException("Service category not found.");
        }

        var service = new ProviderService(
            request.ProviderProfileId,
            request.ServiceCategoryId,
            request.Title,
            request.Description,
            request.BasePrice,
            request.EstimatedDurationMinutes);

        await _providerServiceRepository.AddAsync(
            service,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Map(service);
    }

    public async Task<ProviderServiceResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var service = await _providerServiceRepository.GetByIdAsync(
            id,
            cancellationToken);

        return service is null
            ? null
            : Map(service);
    }

    public async Task<IReadOnlyList<ProviderServiceResponse>> GetByProviderProfileIdAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default)
    {
        var services = await _providerServiceRepository.GetByProviderProfileIdAsync(
            providerProfileId,
            cancellationToken);

        return services
            .Select(Map)
            .ToList();
    }

    public async Task<ProviderServiceResponse> UpdateAsync(
        Guid id,
        UpdateProviderServiceRequest request,
        CancellationToken cancellationToken = default)
    {
        var service = await _providerServiceRepository.GetByIdAsync(
            id,
            cancellationToken)
            ?? throw new KeyNotFoundException("Provider service not found.");

        service.UpdateDetails(
            request.Title,
            request.Description,
            request.EstimatedDurationMinutes);

        service.ChangePrice(request.BasePrice);

        if (request.IsActive)
            service.Activate();
        else
            service.Deactivate();

        await _providerServiceRepository.UpdateAsync(
            service,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Map(service);
    }

    public async Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var service = await _providerServiceRepository.GetByIdAsync(
            id,
            cancellationToken)
            ?? throw new KeyNotFoundException("Provider service not found.");

        await _providerServiceRepository.DeleteAsync(
            service,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static ProviderServiceResponse Map(
        ProviderService service)
    {
        return new ProviderServiceResponse
        {
            Id = service.Id,
            ProviderProfileId = service.ProviderProfileId,
            ServiceCategoryId = service.ServiceCategoryId,
            Title = service.Title,
            Description = service.Description,
            BasePrice = service.BasePrice,
            EstimatedDurationMinutes = service.EstimatedDurationMinutes,
            IsActive = service.IsActive
        };
    }
}