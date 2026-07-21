using PeopleHub.Contracts.Providers.Reviews;

namespace PeopleHub.Application.Providers.Reviews;

public interface IReviewService
{
    Task<ReviewResponse> CreateAsync(
        CreateReviewRequest request,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ReviewResponse>> GetProviderReviewsAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default);
}