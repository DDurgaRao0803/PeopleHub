using PeopleHub.Contracts.Administration;

namespace PeopleHub.Application.Administration;

public interface IAdminProviderVerificationService
{
    Task<IReadOnlyList<ProviderVerificationSummaryResponse>>
        GetPendingVerificationsAsync(
            CancellationToken cancellationToken = default);
}