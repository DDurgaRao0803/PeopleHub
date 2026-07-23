using PeopleHub.Application.Providers.ServiceRequests;
using PeopleHub.Contracts.Providers.ServiceRequests;
using PeopleHub.Domain.Entities;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Common.Interfaces.Services;

namespace PeopleHub.Infrastructure.Providers.ServiceRequests;

public class ServiceRequestService : IServiceRequestService
{
    private readonly IServiceRequestRepository _serviceRequestRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProviderRepository _providerRepository;
    private readonly ICurrentUserService _currentUserService;

    public ServiceRequestService(
        IServiceRequestRepository serviceRequestRepository,
        IUnitOfWork unitOfWork,
        IProviderRepository providerRepository,
        ICurrentUserService currentUserService)
    {
        _serviceRequestRepository = serviceRequestRepository;
        _unitOfWork = unitOfWork;
        _providerRepository = providerRepository;
        _currentUserService = currentUserService;
    }


    public async Task<ServiceRequestResponse> CreateAsync(
        Guid customerId,
        CreateServiceRequestRequest request,
        CancellationToken cancellationToken = default)
    {
        var serviceRequest = new ServiceRequest(
            customerId,
            request.ServiceCategoryId,
            request.Title,
            request.Description,
            request.RequestedDate);

        await _serviceRequestRepository.AddAsync(
            serviceRequest,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return MapToResponse(serviceRequest);
    }


    public async Task<ServiceRequestResponse?> GetByIdAsync(
        Guid serviceRequestId,
        CancellationToken cancellationToken = default)
    {
        var serviceRequest = await _serviceRequestRepository.GetByIdAsync(
            serviceRequestId,
            cancellationToken);

        if (serviceRequest is null)
        {
            return null;
        }

        return MapToResponse(serviceRequest);
    }


    public async Task<IReadOnlyList<ServiceRequestResponse>> GetCustomerRequestsAsync(
        Guid customerId,
        CancellationToken cancellationToken = default)
    {
        var requests = await _serviceRequestRepository.GetByCustomerIdAsync(
            customerId,
            cancellationToken);

        return requests
            .Select(MapToResponse)
            .ToList();
    }


    public async Task<IReadOnlyList<ServiceRequestResponse>> GetProviderRequestsAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default)
    {
        var requests = await _serviceRequestRepository.GetByProviderProfileIdAsync(
            providerProfileId,
            cancellationToken);

        return requests
            .Select(MapToResponse)
            .ToList();
    }


    public async Task<ServiceRequestResponse> AcceptAsync(
        Guid serviceRequestId,
        CancellationToken cancellationToken = default)
    {
        var serviceRequest = await _serviceRequestRepository.GetByIdAsync(
            serviceRequestId,
            cancellationToken);

        if (serviceRequest is null)
        {
            throw new KeyNotFoundException(
                "Service request not found.");
        }

        await ValidateProviderOwnershipAsync(
            serviceRequest,
            cancellationToken);

        serviceRequest.Accept();

        await _serviceRequestRepository.UpdateAsync(
            serviceRequest,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return MapToResponse(serviceRequest);
    }


    public async Task<ServiceRequestResponse> RejectAsync(
        Guid serviceRequestId,
        CancellationToken cancellationToken = default)
    {
        var serviceRequest = await _serviceRequestRepository.GetByIdAsync(
            serviceRequestId,
            cancellationToken);

        if (serviceRequest is null)
        {
            throw new KeyNotFoundException(
                "Service request not found.");
        }

        serviceRequest.Reject();

        await _serviceRequestRepository.UpdateAsync(
            serviceRequest,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return MapToResponse(serviceRequest);
    }


    public async Task<ServiceRequestResponse> CancelAsync(
        Guid serviceRequestId,
        CancellationToken cancellationToken = default)
    {
        var serviceRequest = await _serviceRequestRepository.GetByIdAsync(
            serviceRequestId,
            cancellationToken);

        if (serviceRequest is null)
        {
            throw new KeyNotFoundException(
                "Service request not found.");
        }

        serviceRequest.Cancel();

        await _serviceRequestRepository.UpdateAsync(
            serviceRequest,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return MapToResponse(serviceRequest);
    }


    public async Task<ServiceRequestResponse> CompleteAsync(
        Guid serviceRequestId,
        CancellationToken cancellationToken = default)
    {
        var serviceRequest = await _serviceRequestRepository.GetByIdAsync(
            serviceRequestId,
            cancellationToken);

        if (serviceRequest is null)
        {
            throw new KeyNotFoundException(
                "Service request not found.");
        }

        serviceRequest.Complete();

        await _serviceRequestRepository.UpdateAsync(
            serviceRequest,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return MapToResponse(serviceRequest);
    }


    private async Task ValidateProviderOwnershipAsync(
        ServiceRequest serviceRequest,
        CancellationToken cancellationToken)
    {
        if (serviceRequest.ProviderProfileId is null)
        {
            throw new InvalidOperationException(
                "Service request has no assigned provider.");
        }

        var provider = await _providerRepository.GetByUserIdAsync(
            _currentUserService.UserId,
            cancellationToken);

        if (provider is null ||
            provider.Id != serviceRequest.ProviderProfileId)
        {
            throw new UnauthorizedAccessException(
                "Provider is not assigned to this request.");
        }
    }


    private static ServiceRequestResponse MapToResponse(
        ServiceRequest serviceRequest)
    {
        return new ServiceRequestResponse(
            serviceRequest.Id,
            serviceRequest.CustomerId,
            serviceRequest.ProviderProfileId,
            serviceRequest.ServiceCategoryId,
            serviceRequest.Title,
            serviceRequest.Description,
            serviceRequest.RequestedDate,
            serviceRequest.Status.ToString());
    }
}