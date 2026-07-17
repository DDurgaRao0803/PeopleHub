using PeopleHub.Domain.Common;
using PeopleHub.Domain.Enums;
using PeopleHub.Domain.Exceptions;

namespace PeopleHub.Domain.Aggregates.Provider;

public class ProviderVerification : AuditableEntity
{
    private ProviderVerification()
    {
        GovernmentIdNumber = null!;
        FrontImageUrl = null!;
        SelfieImageUrl = null!;
    }

    public ProviderVerification(
        Guid providerProfileId,
        GovernmentIdType governmentIdType,
        string governmentIdNumber,
        string frontImageUrl,
        string? backImageUrl,
        string selfieImageUrl)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(governmentIdNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(frontImageUrl);
        ArgumentException.ThrowIfNullOrWhiteSpace(selfieImageUrl);

        ProviderProfileId = providerProfileId;
        GovernmentIdType = governmentIdType;
        GovernmentIdNumber = governmentIdNumber.Trim();
        FrontImageUrl = frontImageUrl.Trim();
        BackImageUrl = backImageUrl?.Trim();
        SelfieImageUrl = selfieImageUrl.Trim();

        VerificationStatus = VerificationStatus.NotSubmitted;
    }

    public Guid ProviderProfileId { get; private set; }

    public GovernmentIdType GovernmentIdType { get; private set; }

    public string GovernmentIdNumber { get; private set; }

    public string FrontImageUrl { get; private set; }

    public string? BackImageUrl { get; private set; }

    public string SelfieImageUrl { get; private set; }

    public DateTime? SubmittedOnUtc { get; private set; }

    public DateTime? ReviewedOnUtc { get; private set; }

    public Guid? ReviewedByUserId { get; private set; }

    public string? RejectionReason { get; private set; }

    public VerificationStatus VerificationStatus { get; private set; }

    public void Submit()
    {
        if (VerificationStatus != VerificationStatus.NotSubmitted)
        {
            throw new DomainException("Verification has already been submitted.");
        }

        VerificationStatus = VerificationStatus.Pending;
        SubmittedOnUtc = DateTime.UtcNow;
    }

    public void Approve(Guid reviewedByUserId)
    {
        if (VerificationStatus != VerificationStatus.Pending)
        {
            throw new DomainException("Only pending verification can be approved.");
        }

        VerificationStatus = VerificationStatus.Approved;
        ReviewedOnUtc = DateTime.UtcNow;
        ReviewedByUserId = reviewedByUserId;
        RejectionReason = null;
    }

    public void Reject(Guid reviewedByUserId, string reason)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reason);

        if (VerificationStatus != VerificationStatus.Pending)
        {
            throw new DomainException("Only pending verification can be rejected.");
        }

        VerificationStatus = VerificationStatus.Rejected;
        ReviewedOnUtc = DateTime.UtcNow;
        ReviewedByUserId = reviewedByUserId;
        RejectionReason = reason.Trim();
    }
}