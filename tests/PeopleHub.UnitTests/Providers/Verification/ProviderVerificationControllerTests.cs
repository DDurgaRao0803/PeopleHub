using Microsoft.AspNetCore.Mvc;
using PeopleHub.Api.Controllers;
using PeopleHub.Application.Providers.Verification;
using PeopleHub.Contracts.Providers.Verification;
using PeopleHub.Application.Common.Interfaces.Services;

namespace PeopleHub.UnitTests;

public class ProviderVerificationControllerTests
{
    [Fact]
    public async Task Get_ReturnsNotFound_WhenVerificationDoesNotExist()
    {
        var providerProfileId = Guid.NewGuid();

        var controller = new ProviderVerificationController(
    new VerificationNotFoundService(),
    new FakeCurrentUserService());

        var result = await controller.Get(
            providerProfileId,
            CancellationToken.None);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Create_ReturnsCreated_WhenVerificationIsCreated()
    {
        var providerProfileId = Guid.NewGuid();

        var controller = new ProviderVerificationController(
    new SuccessfulProviderVerificationService(),
    new FakeCurrentUserService());

        var request = new CreateProviderVerificationRequest
        {
            GovernmentIdType = GovernmentIdType.Aadhaar,
            GovernmentIdNumber = "123456789012",
            FrontImageUrl = "front.jpg",
            BackImageUrl = "back.jpg",
            SelfieImageUrl = "selfie.jpg"
        };

        var result = await controller.Create(
            providerProfileId,
            request,
            CancellationToken.None);

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);

        var response =
            Assert.IsType<ProviderVerificationResponse>(created.Value);

        Assert.Equal(
            request.GovernmentIdNumber,
            response.GovernmentIdNumber);

        Assert.Equal(
            request.GovernmentIdType,
            response.GovernmentIdType);
    }

    private sealed class VerificationNotFoundService
    : IProviderVerificationService
{
    public Task<ProviderVerificationResponse> CreateAsync(
        Guid providerProfileId,
        CreateProviderVerificationRequest request,
        CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public Task<ProviderVerificationResponse?> GetByProviderProfileIdAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default)
        => Task.FromResult<ProviderVerificationResponse?>(null);

    public Task DeleteAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public Task<ProviderVerificationResponse> ApproveAsync(
        Guid providerProfileId,
        Guid reviewedByUserId,
        CancellationToken cancellationToken = default)
        => throw new KeyNotFoundException();

    public Task<ProviderVerificationResponse> RejectAsync(
        Guid providerProfileId,
        Guid reviewedByUserId,
        string reason,
        CancellationToken cancellationToken = default)
        => throw new KeyNotFoundException();
}

    private sealed class SuccessfulProviderVerificationService
    : IProviderVerificationService
{
    public Task<ProviderVerificationResponse> CreateAsync(
        Guid providerProfileId,
        CreateProviderVerificationRequest request,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(
            new ProviderVerificationResponse
            {
                Id = Guid.NewGuid(),
                GovernmentIdType = request.GovernmentIdType,
                GovernmentIdNumber = request.GovernmentIdNumber,
                FrontImageUrl = request.FrontImageUrl,
                BackImageUrl = request.BackImageUrl,
                SelfieImageUrl = request.SelfieImageUrl,
                VerificationStatus = "Pending"
            });
    }

    public Task<ProviderVerificationResponse?> GetByProviderProfileIdAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public Task DeleteAsync(
        Guid providerProfileId,
        CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public Task<ProviderVerificationResponse> ApproveAsync(
        Guid providerProfileId,
        Guid reviewedByUserId,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(
            new ProviderVerificationResponse
            {
                Id = Guid.NewGuid(),
                VerificationStatus = "Approved",
                ReviewedOnUtc = DateTime.UtcNow
            });
    }

    public Task<ProviderVerificationResponse> RejectAsync(
        Guid providerProfileId,
        Guid reviewedByUserId,
        string reason,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(
            new ProviderVerificationResponse
            {
                Id = Guid.NewGuid(),
                VerificationStatus = "Rejected",
                ReviewedOnUtc = DateTime.UtcNow,
                RejectionReason = reason
            });
    }
}

 private sealed class FakeCurrentUserService : ICurrentUserService
{
    public Guid UserId => Guid.NewGuid();

    public bool IsAuthenticated => true;

    public string? Email => "admin@peoplehub.com";

    public string? Role => "Admin";
} 

[Fact]
public async Task Approve_ReturnsOk_WhenVerificationIsApproved()
{
    var providerProfileId = Guid.NewGuid();

    var controller = new ProviderVerificationController(
        new SuccessfulProviderVerificationService(),
        new FakeCurrentUserService());

    var result = await controller.Approve(
        providerProfileId,
        CancellationToken.None);

    var ok = Assert.IsType<OkObjectResult>(result.Result);

    var response =
        Assert.IsType<ProviderVerificationResponse>(ok.Value);

    Assert.Equal("Approved", response.VerificationStatus);
}


[Fact]
public async Task Reject_ReturnsOk_WhenVerificationIsRejected()
{
    var providerProfileId = Guid.NewGuid();

    var controller = new ProviderVerificationController(
        new SuccessfulProviderVerificationService(),
        new FakeCurrentUserService());

    var request = new RejectProviderVerificationRequest
    {
        Reason = "Invalid ID document"
    };

    var result = await controller.Reject(
        providerProfileId,
        request,
        CancellationToken.None);

    var ok = Assert.IsType<OkObjectResult>(result.Result);

    var response =
        Assert.IsType<ProviderVerificationResponse>(ok.Value);

    Assert.Equal("Rejected", response.VerificationStatus);
    Assert.Equal(request.Reason, response.RejectionReason);
}


}