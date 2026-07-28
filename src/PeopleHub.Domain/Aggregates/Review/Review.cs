using PeopleHub.Domain.Common;
using PeopleHub.Domain.Entities;

using ProviderProfile = PeopleHub.Domain.Aggregates.Provider.ProviderProfile;
using UserEntity = PeopleHub.Domain.Aggregates.User.User;

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
        string? comment)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(rating, 1);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(rating, 5);

        CustomerId = customerId;
        ProviderProfileId = providerProfileId;
        ServiceRequestId = serviceRequestId;
        Rating = rating;
        Comment = comment?.Trim() ?? string.Empty;
    }

    public Guid CustomerId { get; private set; }

    public Guid ProviderProfileId { get; private set; }

    public Guid ServiceRequestId { get; private set; }

    public int Rating { get; private set; }

    public string Comment { get; private set; }

    // Navigation Properties
    public UserEntity Customer { get; private set; } = null!;

    public ProviderProfile ProviderProfile { get; private set; } = null!;

    public ServiceRequest ServiceRequest { get; private set; } = null!;

    public void Update(
        int rating,
        string? comment)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(rating, 1);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(rating, 5);

        Rating = rating;
        Comment = comment?.Trim() ?? string.Empty;
    }
}