using FluentAssertions;
using Moq;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Common.Interfaces.Services;
using PeopleHub.Domain.Entities;
using PeopleHub.Domain.Enums;
using PeopleHub.Infrastructure.Providers.ServiceRequests;
using PeopleHub.Domain.Aggregates.Provider;

namespace PeopleHub.UnitTests.Providers.ServiceRequests;

public class ServiceRequestServiceTests
{
    private readonly Mock<IServiceRequestRepository> _repository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly Mock<IProviderRepository> _providerRepository = new();
    private readonly Mock<ICurrentUserService> _currentUserService = new();

    private readonly ServiceRequestService _service;


    public ServiceRequestServiceTests()
    {
        _service = new ServiceRequestService(
            _repository.Object,
            _unitOfWork.Object,
            _providerRepository.Object,
            _currentUserService.Object);
    }


    [Fact]
    public async Task AcceptAsync_Should_Accept_Request()
    {
        var providerUserId = Guid.NewGuid();

        var provider = new ProviderProfile(
            providerUserId,
            "Test Provider",
            5);

        var request = new ServiceRequest(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Fix AC",
            "AC issue",
            DateTime.UtcNow);

        request.AssignProvider(provider.Id);


        _currentUserService
            .Setup(x => x.UserId)
            .Returns(providerUserId);


        _providerRepository
            .Setup(x => x.GetByUserIdAsync(
                providerUserId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(provider);


        _repository
            .Setup(x => x.GetByIdAsync(
                request.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(request);


        var result = await _service.AcceptAsync(request.Id);


        result.Status.Should()
            .Be(ServiceRequestStatus.Accepted.ToString());


        _repository.Verify(
            x => x.UpdateAsync(
                It.IsAny<ServiceRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);


        _unitOfWork.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }



    [Fact]
    public async Task RejectAsync_Should_Reject_Request()
    {
        var request = new ServiceRequest(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Fix AC",
            "AC issue",
            DateTime.UtcNow);

        request.AssignProvider(Guid.NewGuid());


        _repository
            .Setup(x => x.GetByIdAsync(
                request.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(request);


        var result = await _service.RejectAsync(request.Id);


        result.Status.Should()
            .Be(ServiceRequestStatus.Rejected.ToString());


        _repository.Verify(
            x => x.UpdateAsync(
                It.IsAny<ServiceRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);


        _unitOfWork.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }



    [Fact]
    public async Task CancelAsync_Should_Cancel_Request()
    {
        var request = new ServiceRequest(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Fix AC",
            "AC issue",
            DateTime.UtcNow);

        request.AssignProvider(Guid.NewGuid());


        _repository
            .Setup(x => x.GetByIdAsync(
                request.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(request);


        var result = await _service.CancelAsync(request.Id);


        result.Status.Should()
            .Be(ServiceRequestStatus.Cancelled.ToString());


        _repository.Verify(
            x => x.UpdateAsync(
                It.IsAny<ServiceRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);


        _unitOfWork.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }



    [Fact]
    public async Task CompleteAsync_Should_Complete_Request()
    {
        var request = new ServiceRequest(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Fix AC",
            "AC issue",
            DateTime.UtcNow);

        request.AssignProvider(Guid.NewGuid());

        request.Accept();


        _repository
            .Setup(x => x.GetByIdAsync(
                request.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(request);


        var result = await _service.CompleteAsync(request.Id);


        result.Status.Should()
            .Be(ServiceRequestStatus.Completed.ToString());


        _repository.Verify(
            x => x.UpdateAsync(
                It.IsAny<ServiceRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);


        _unitOfWork.Verify(
            x => x.SaveChangesAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);
    }



    [Fact]
    public async Task AcceptAsync_Should_Throw_When_Request_Not_Found()
    {
        _repository
            .Setup(x => x.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((ServiceRequest?)null);


        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _service.AcceptAsync(Guid.NewGuid()));
    }



    [Fact]
    public async Task RejectAsync_Should_Throw_When_Request_Not_Found()
    {
        _repository
            .Setup(x => x.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((ServiceRequest?)null);


        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _service.RejectAsync(Guid.NewGuid()));
    }



    [Fact]
    public async Task CancelAsync_Should_Throw_When_Request_Not_Found()
    {
        _repository
            .Setup(x => x.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((ServiceRequest?)null);


        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _service.CancelAsync(Guid.NewGuid()));
    }



    [Fact]
    public async Task CompleteAsync_Should_Throw_When_Request_Not_Found()
    {
        _repository
            .Setup(x => x.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((ServiceRequest?)null);


        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _service.CompleteAsync(Guid.NewGuid()));
    }
}