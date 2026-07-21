using PeopleHub.Domain.Aggregates.Review;

namespace PeopleHub.Application.Common.Interfaces.Persistence;

public interface IReviewRepository
{
    Task AddAsync(
        Review review,
        CancellationToken cancellationToken = default);

    Task<Review?> GetByIdAsync(
        Guid reviewId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Review>> GetByProviderAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsForServiceRequestAsync(
    Guid serviceRequestId,
    CancellationToken cancellationToken = default);

    void Update(Review review);

    void Remove(Review review);
}