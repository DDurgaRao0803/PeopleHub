using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Interfaces.Repositories;
using PeopleHub.Application.Providers;
using PeopleHub.Contracts.Providers.Verification;
using PeopleHub.Domain.Aggregates.Provider;
using DomainGovernmentIdType = PeopleHub.Domain.Enums.GovernmentIdType;

namespace PeopleHub.Infrastructure.Providers;


public sealed class ProviderVerificationService : IProviderVerificationService
{
    private readonly IProviderVerificationRepository _verificationRepository;
    private readonly IProviderProfileRepository _providerProfileRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProviderVerificationService(
        IProviderVerificationRepository verificationRepository,
        IProviderProfileRepository providerProfileRepository,
        IUnitOfWork unitOfWork)
    {
        _verificationRepository = verificationRepository;
        _providerProfileRepository = providerProfileRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProviderVerificationResponse> CreateAsync(
        Guid providerProfileId,
        CreateProviderVerificationRequest request,
        CancellationToken cancellationToken = default)
    {
        var profile = await _providerProfileRepository.GetByIdAsync(
            providerProfileId,
            cancellationToken);

        if (profile is null)
        {
            throw new KeyNotFoundException(
                "Provider profile not found.");
        }

        var existing = await _verificationRepository.GetByProviderProfileIdAsync(
            providerProfileId,
            cancellationToken);

        if (existing is not null)
        {
            throw new InvalidOperationException(
                "Provider verification already exists.");
        }

        var verification = new ProviderVerification(
            providerProfileId,
            (DomainGovernmentIdType)request.GovernmentIdType,
            request.GovernmentIdNumber,
            request.FrontImageUrl,
            request.BackImageUrl,
            request.SelfieImageUrl);

        await _verificationRepository.AddAsync(
            verification,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Map(verification);
    }

    public async Task<ProviderVerificationResponse?> GetByProviderProfileIdAsync(
    Guid providerProfileId,
    CancellationToken cancellationToken = default)
{
    var verification = await _verificationRepository.GetByProviderProfileIdAsync(
        providerProfileId,
        cancellationToken);

    return verification is null ? null : Map(verification);
}

    public async Task DeleteAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default)
    {
        var verification =
            await _verificationRepository.GetByProviderProfileIdAsync(
                providerProfileId,
                cancellationToken)
            ?? throw new KeyNotFoundException(
                "Provider verification not found.");

        _verificationRepository.Remove(verification);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static ProviderVerificationResponse Map(
        ProviderVerification verification)
    {
        return new ProviderVerificationResponse
        {
            Id = verification.Id,
            GovernmentIdType =
                (PeopleHub.Contracts.Providers.Verification.GovernmentIdType)
                verification.GovernmentIdType,
            GovernmentIdNumber = verification.GovernmentIdNumber,
            FrontImageUrl = verification.FrontImageUrl,
            BackImageUrl = verification.BackImageUrl,
            SelfieImageUrl = verification.SelfieImageUrl,
            VerificationStatus = verification.VerificationStatus.ToString(),
            SubmittedOnUtc = verification.SubmittedOnUtc,
            ReviewedOnUtc = verification.ReviewedOnUtc,
            RejectionReason = verification.RejectionReason
        };
    }
}