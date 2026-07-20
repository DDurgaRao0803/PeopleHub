using PeopleHub.Contracts.Providers.Verification;

namespace PeopleHub.Application.Providers;

public interface IProviderVerificationService
{
    Task<ProviderVerificationResponse> CreateAsync(
        Guid providerProfileId,
        CreateProviderVerificationRequest request,
        CancellationToken cancellationToken = default);

    Task<ProviderVerificationResponse?> GetByProviderProfileIdAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default);
}