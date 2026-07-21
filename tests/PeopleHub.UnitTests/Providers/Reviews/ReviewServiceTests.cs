using FluentAssertions;
using Moq;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Contracts.Providers.Reviews;
using PeopleHub.Domain.Aggregates.Review;
using PeopleHub.Domain.Entities;
using PeopleHub.Domain.Enums;
using PeopleHub.Infrastructure.Providers.Reviews;

namespace PeopleHub.UnitTests.Providers.Reviews;

public class ReviewServiceTests
{
    private readonly Mock<IReviewRepository> _reviewRepository = new();
    private readonly Mock<IServiceRequestRepository> _serviceRequestRepository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    private readonly ReviewService _service;

    public ReviewServiceTests()
    {
        _service = new ReviewService(
            _reviewRepository.Object,
            _serviceRequestRepository.Object,
            _unitOfWork.Object);
    }

    [Fact]
    public async Task CreateAsync_Should_Create_Review_When_Request_Is_Valid()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var providerId = Guid.NewGuid();
        var serviceRequestId = Guid.NewGuid();

        var request = new CreateReviewRequest
        {
            CustomerId = customerId,
            ProviderProfileId = providerId,
            ServiceRequestId = serviceRequestId,
            Rating = 5,
            Comment = "Excellent service"
        };

        var serviceRequest = new ServiceRequest(
    customerId,
    providerId,
    Guid.NewGuid(),               // ServiceCategoryId
    "House Cleaning",
    "General cleaning service",
    DateTime.UtcNow);

serviceRequest.Accept();
serviceRequest.Complete();

        _reviewRepository
            .Setup(x => x.ExistsForServiceRequestAsync(
                serviceRequestId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _serviceRequestRepository
            .Setup(x => x.GetByIdAsync(
                serviceRequestId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(serviceRequest);

        // Act
        var result = await _service.CreateAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.CustomerId.Should().Be(customerId);
        result.ProviderProfileId.Should().Be(providerId);
        result.ServiceRequestId.Should().Be(serviceRequestId);
        result.Rating.Should().Be(5);
        result.Comment.Should().Be("Excellent service");

        _reviewRepository.Verify(
            x => x.AddAsync(
                It.IsAny<Review>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _unitOfWork.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}