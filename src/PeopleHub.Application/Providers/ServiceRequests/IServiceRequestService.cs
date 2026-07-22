using PeopleHub.Contracts.Providers.ServiceRequests;

namespace PeopleHub.Application.Providers.ServiceRequests;

public interface IServiceRequestService
{
    Task<ServiceRequestResponse> CreateAsync(
        Guid customerId,
        CreateServiceRequestRequest request,
        CancellationToken cancellationToken = default);

    Task<ServiceRequestResponse?> GetByIdAsync(
        Guid serviceRequestId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ServiceRequestResponse>> GetCustomerRequestsAsync(
        Guid customerId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ServiceRequestResponse>> GetProviderRequestsAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default);

    Task<ServiceRequestResponse> AcceptAsync(
        Guid serviceRequestId,
        CancellationToken cancellationToken = default);

    Task<ServiceRequestResponse> RejectAsync(
    Guid serviceRequestId,
    CancellationToken cancellationToken = default);

Task<ServiceRequestResponse> CancelAsync(
    Guid serviceRequestId,
    CancellationToken cancellationToken = default);

    Task<ServiceRequestResponse> CompleteAsync(
        Guid serviceRequestId,
        CancellationToken cancellationToken = default);
}