using PeopleHub.Contracts.Providers.Profiles;
using PeopleHub.Contracts.Providers.Availability;

namespace PeopleHub.Application.Providers;

public interface IProviderProfileService
{
    Task<ProviderProfileResponse> CreateAsync(
        Guid userId,
        CreateProviderProfileRequest request,
        CancellationToken cancellationToken = default);

    Task<ProviderProfileResponse?> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    Task<ProviderProfileResponse> UpdateAsync(
        Guid userId,
        UpdateProviderProfileRequest request,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

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