using PeopleHub.Domain.Aggregates.Provider;

namespace PeopleHub.Application.Interfaces.Repositories;

public interface IProviderVerificationRepository
{
    Task<ProviderVerification?> GetByProviderProfileIdAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        ProviderVerification verification,
        CancellationToken cancellationToken = default);

    void Update(ProviderVerification verification);

    void Remove(ProviderVerification verification);
}