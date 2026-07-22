using PeopleHub.Domain.Aggregates.Provider;

namespace PeopleHub.Application.Common.Interfaces.Persistence;

public interface IProviderVerificationRepository
{
    Task<ProviderVerification?> GetByProviderProfileIdAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProviderVerification>> GetPendingAsync(
    CancellationToken cancellationToken = default);

    Task AddAsync(
        ProviderVerification verification,
        CancellationToken cancellationToken = default);

    void Update(ProviderVerification verification);

    void Remove(ProviderVerification verification);
}