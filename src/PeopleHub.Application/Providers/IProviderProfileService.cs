using PeopleHub.Contracts.Providers;

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
}