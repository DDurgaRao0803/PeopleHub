using PeopleHub.Contracts.Providers;

namespace PeopleHub.Application.Interfaces.Services;

public interface IProviderVerificationService
{
    Task<ProviderVerificationResponse?> GetAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default);

    Task<ProviderVerificationResponse> CreateAsync(
        Guid providerProfileId,
        CreateProviderVerificationRequest request,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default);
}