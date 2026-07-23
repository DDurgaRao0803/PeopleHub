using PeopleHub.Domain.Common;
using PeopleHub.Domain.Enums;

namespace PeopleHub.Domain.Aggregates.Bidding;

public sealed class ProviderBid : AuditableEntity
{
    private ProviderBid()
    {
    }


    public ProviderBid(
        Guid serviceRequestId,
        Guid providerProfileId,
        decimal amount,
        int estimatedMinutes)
    {
        if (serviceRequestId == Guid.Empty)
        {
            throw new ArgumentException(
                "Service request id is required.");
        }

        if (providerProfileId == Guid.Empty)
        {
            throw new ArgumentException(
                "Provider profile id is required.");
        }

        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount));
        }

        if (estimatedMinutes <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(estimatedMinutes));
        }


        ServiceRequestId = serviceRequestId;

        ProviderProfileId = providerProfileId;

        Amount = amount;

        EstimatedMinutes = estimatedMinutes;

        Status = ProviderBidStatus.Submitted;
    }


    public Guid ServiceRequestId { get; private set; }


    public Guid ProviderProfileId { get; private set; }


    public decimal Amount { get; private set; }


    public int EstimatedMinutes { get; private set; }


    public ProviderBidStatus Status { get; private set; }



    public void Accept()
    {
        if (Status != ProviderBidStatus.Submitted)
        {
            throw new InvalidOperationException(
                "Only submitted bids can be accepted.");
        }

        Status = ProviderBidStatus.Accepted;
    }


    public void Reject()
    {
        if (Status != ProviderBidStatus.Submitted)
        {
            throw new InvalidOperationException(
                "Only submitted bids can be rejected.");
        }

        Status = ProviderBidStatus.Rejected;
    }


    public void Expire()
    {
        Status = ProviderBidStatus.Expired;
    }
}