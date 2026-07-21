using PeopleHub.Contracts.Providers.Verification;

namespace PeopleHub.Application.Providers.Verification;

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

    Task<ProviderVerificationResponse> ApproveAsync(
    Guid providerProfileId,
    Guid reviewedByUserId,
    CancellationToken cancellationToken = default);

Task<ProviderVerificationResponse> RejectAsync(
    Guid providerProfileId,
    Guid reviewedByUserId,
    string reason,
    CancellationToken cancellationToken = default);
}