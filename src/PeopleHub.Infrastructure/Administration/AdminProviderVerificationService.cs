using PeopleHub.Application.Administration;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Contracts.Administration;

namespace PeopleHub.Infrastructure.Administration;

public sealed class AdminProviderVerificationService
    : IAdminProviderVerificationService
{
    private readonly IProviderVerificationRepository _verificationRepository;

    public AdminProviderVerificationService(
        IProviderVerificationRepository verificationRepository)
    {
        _verificationRepository = verificationRepository;
    }

    public async Task<IReadOnlyList<ProviderVerificationSummaryResponse>>
        GetPendingVerificationsAsync(
            CancellationToken cancellationToken = default)
    {
        var verifications =
            await _verificationRepository.GetPendingAsync(cancellationToken);

        return verifications
            .Select(v => new ProviderVerificationSummaryResponse
            {
                ProviderId = v.ProviderProfileId,
                VerificationStatus = v.VerificationStatus.ToString(),
                SubmittedOn = v.SubmittedOnUtc ?? DateTime.MinValue
            })
            .ToList();
    }
}