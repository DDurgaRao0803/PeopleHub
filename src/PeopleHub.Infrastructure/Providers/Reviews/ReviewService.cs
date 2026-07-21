using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Providers.Reviews;
using PeopleHub.Contracts.Providers.Reviews;
using PeopleHub.Domain.Aggregates.Review;
using PeopleHub.Domain.Enums;

namespace PeopleHub.Infrastructure.Providers.Reviews;

public sealed class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IServiceRequestRepository _serviceRequestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReviewService(
        IReviewRepository reviewRepository,
        IServiceRequestRepository serviceRequestRepository,
        IUnitOfWork unitOfWork)
    {
        _reviewRepository = reviewRepository;
        _serviceRequestRepository = serviceRequestRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ReviewResponse> CreateAsync(
        CreateReviewRequest request,
        CancellationToken cancellationToken = default)
    {
        var alreadyReviewed =
            await _reviewRepository.ExistsForServiceRequestAsync(
                request.ServiceRequestId,
                cancellationToken);

        if (alreadyReviewed)
        {
            throw new InvalidOperationException(
                "A review already exists for this service request.");
        }

        var serviceRequest =
            await _serviceRequestRepository.GetByIdAsync(
                request.ServiceRequestId,
                cancellationToken);

        if (serviceRequest is null)
        {
            throw new InvalidOperationException(
                "Service request not found.");
        }

        if (serviceRequest.Status != ServiceRequestStatus.Completed)
        {
            throw new InvalidOperationException(
                "Reviews can only be submitted after the service request is completed.");
        }

        if (serviceRequest.CustomerId != request.CustomerId)
        {
            throw new InvalidOperationException(
                "Only the customer who created the service request can submit a review.");
        }

        if (serviceRequest.ProviderProfileId != request.ProviderProfileId)
        {
            throw new InvalidOperationException(
                "Provider does not match the service request.");
        }

        var review = new Review(
            request.CustomerId,
            request.ProviderProfileId,
            request.ServiceRequestId,
            request.Rating,
            request.Comment);

        await _reviewRepository.AddAsync(
            review,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Map(review);
    }

    public async Task<IReadOnlyList<ReviewResponse>> GetProviderReviewsAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default)
    {
        var reviews = await _reviewRepository.GetByProviderAsync(
            providerProfileId,
            cancellationToken);

        return reviews
            .Select(Map)
            .ToList();
    }

    private static ReviewResponse Map(Review review)
    {
        return new ReviewResponse
        {
            Id = review.Id,
            CustomerId = review.CustomerId,
            ProviderProfileId = review.ProviderProfileId,
            ServiceRequestId = review.ServiceRequestId,
            Rating = review.Rating,
            Comment = review.Comment
        };
    }
}