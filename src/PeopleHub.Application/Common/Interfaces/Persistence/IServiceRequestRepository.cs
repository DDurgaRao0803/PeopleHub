using PeopleHub.Domain.Entities;

namespace PeopleHub.Application.Common.Interfaces.Persistence;

public interface IServiceRequestRepository
{
    Task AddAsync(
        ServiceRequest serviceRequest,
        CancellationToken cancellationToken = default);

    Task<ServiceRequest?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<ServiceRequest>> GetByCustomerIdAsync(
        Guid customerId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<ServiceRequest>> GetByProviderProfileIdAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        ServiceRequest serviceRequest,
        CancellationToken cancellationToken = default);

}