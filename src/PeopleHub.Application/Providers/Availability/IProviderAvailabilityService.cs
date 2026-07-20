using PeopleHub.Contracts.Providers.Availability;

namespace PeopleHub.Application.Providers.Availability;

public interface IProviderAvailabilityService
{
    Task<IReadOnlyList<ProviderAvailabilityResponse>> GetAvailabilityAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default);

    Task<ProviderAvailabilityResponse> AddAvailabilityAsync(
        Guid providerProfileId,
        CreateProviderAvailabilityRequest request,
        CancellationToken cancellationToken = default);

    Task<ProviderAvailabilityResponse> UpdateAvailabilityAsync(
        Guid providerProfileId,
        Guid availabilityId,
        UpdateProviderAvailabilityRequest request,
        CancellationToken cancellationToken = default);

    Task DeleteAvailabilityAsync(
        Guid providerProfileId,
        Guid availabilityId,
        CancellationToken cancellationToken = default);
}