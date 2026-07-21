using PeopleHub.Domain.Common;

namespace PeopleHub.Domain.Aggregates.Review;

public sealed class Review : AuditableEntity
{
    private Review()
    {
        Comment = string.Empty;
    }

    public Review(
        Guid customerId,
        Guid providerProfileId,
        Guid serviceRequestId,
        int rating,
        string comment)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(rating, 1);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(rating, 5);
        ArgumentException.ThrowIfNullOrWhiteSpace(comment);

        CustomerId = customerId;
        ProviderProfileId = providerProfileId;
        ServiceRequestId = serviceRequestId;
        Rating = rating;
        Comment = comment.Trim();
    }

    public Guid CustomerId { get; private set; }

    public Guid ProviderProfileId { get; private set; }

    public Guid ServiceRequestId { get; private set; }

    public int Rating { get; private set; }

    public string Comment { get; private set; }

    public void Update(
        int rating,
        string comment)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(rating, 1);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(rating, 5);
        ArgumentException.ThrowIfNullOrWhiteSpace(comment);

        Rating = rating;
        Comment = comment.Trim();
    }
}