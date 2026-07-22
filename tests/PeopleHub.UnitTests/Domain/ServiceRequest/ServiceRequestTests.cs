using ServiceRequestEntity = PeopleHub.Domain.Entities.ServiceRequest;
using PeopleHub.Domain.Enums;

namespace PeopleHub.UnitTests.Domain.ServiceRequest;

public class ServiceRequestTests
{
    [Fact]
    public void Constructor_Should_Set_Pending_Status()
    {
        var request = new ServiceRequestEntity(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Fix AC",
            "AC not cooling",
            DateTime.UtcNow);

        Assert.Equal(ServiceRequestStatus.Pending, request.Status);
    }

    [Fact]
    public void Accept_Should_Set_Status_To_Accepted()
    {
        var request = new ServiceRequestEntity(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Fix AC",
            "AC not cooling",
            DateTime.UtcNow);

        request.Accept();

        Assert.Equal(ServiceRequestStatus.Accepted, request.Status);
    }

    [Fact]
    public void Reject_Should_Set_Status_To_Rejected()
    {
        var request = new ServiceRequestEntity(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Fix AC",
            "AC not cooling",
            DateTime.UtcNow);

        request.Reject();

        Assert.Equal(ServiceRequestStatus.Rejected, request.Status);
    }

    [Fact]
    public void Cancel_Should_Set_Status_To_Cancelled()
    {
        var request = new ServiceRequestEntity(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Fix AC",
            "AC not cooling",
            DateTime.UtcNow);

        request.Cancel();

        Assert.Equal(ServiceRequestStatus.Cancelled, request.Status);
    }

    [Fact]
    public void Complete_Should_Set_Status_To_Completed()
    {
        var request = new ServiceRequestEntity(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Fix AC",
            "AC not cooling",
            DateTime.UtcNow);

        request.Accept();
        request.Complete();

        Assert.Equal(ServiceRequestStatus.Completed, request.Status);
    }

    [Fact]
    public void Reject_Should_Throw_When_Request_Is_Accepted()
    {
        var request = new ServiceRequestEntity(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Fix AC",
            "AC not cooling",
            DateTime.UtcNow);

        request.Accept();

        Assert.Throws<InvalidOperationException>(() => request.Reject());
    }

    [Fact]
    public void Cancel_Should_Throw_When_Request_Is_Completed()
    {
        var request = new ServiceRequestEntity(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Fix AC",
            "AC not cooling",
            DateTime.UtcNow);

        request.Accept();
        request.Complete();

        Assert.Throws<InvalidOperationException>(() => request.Cancel());
    }
}