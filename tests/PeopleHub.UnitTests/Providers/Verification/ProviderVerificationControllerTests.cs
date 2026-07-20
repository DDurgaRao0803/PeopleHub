using Microsoft.AspNetCore.Mvc;
using PeopleHub.Api.Controllers;
using PeopleHub.Application.Providers;
using PeopleHub.Contracts.Providers;

namespace PeopleHub.UnitTests;

public class ProviderVerificationControllerTests
{
    [Fact]
    public async Task Get_ReturnsNotFound_WhenVerificationDoesNotExist()
    {
        var providerProfileId = Guid.NewGuid();

        var controller = new ProviderVerificationController(
            new VerificationNotFoundService());

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
            new SuccessfulProviderVerificationService());

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
    }
}