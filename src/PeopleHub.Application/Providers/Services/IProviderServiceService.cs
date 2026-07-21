using PeopleHub.Contracts.Providers.Services;

namespace PeopleHub.Application.Providers.Services;

/// <summary>
/// Provides business operations for provider services.
/// </summary>
public interface IProviderServiceService
{
    Task<ProviderServiceResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProviderServiceResponse>> GetByProviderProfileIdAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default);

    Task<ProviderServiceResponse> CreateAsync(
        CreateProviderServiceRequest request,
        CancellationToken cancellationToken = default);

    Task<ProviderServiceResponse> UpdateAsync(
        Guid id,
        UpdateProviderServiceRequest request,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}