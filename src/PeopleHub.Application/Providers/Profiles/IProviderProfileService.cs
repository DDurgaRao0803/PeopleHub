using PeopleHub.Contracts.Providers.Profiles;

namespace PeopleHub.Application.Providers.Profiles;

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

    Task<IReadOnlyList<NearbyProviderResponse>> GetNearbyAsync(
    CancellationToken cancellationToken = default);

}