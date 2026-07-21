using Microsoft.AspNetCore.Mvc;
using PeopleHub.API.Controllers;
using PeopleHub.Application.Providers.Services;
using PeopleHub.Contracts.Providers.Services;

namespace PeopleHub.UnitTests.Providers.Services;

public class ProviderServicesControllerTests
{
    [Fact]
    public async Task GetById_ReturnsNotFound_WhenServiceDoesNotExist()
    {
        var controller = new ProviderServicesController(
            new ProviderServiceNotFoundService());

        var result = await controller.GetById(
            Guid.NewGuid(),
            CancellationToken.None);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Create_ReturnsCreated_WhenServiceIsCreated()
    {
        var controller = new ProviderServicesController(
            new SuccessfulProviderService());

        var request = new CreateProviderServiceRequest
        {
            ProviderProfileId = Guid.NewGuid(),
            ServiceCategoryId = Guid.NewGuid(),
            Title = "AC Repair",
            Description = "Home AC Repair",
            BasePrice = 150,
            EstimatedDurationMinutes = 90
        };

        var result = await controller.Create(
            request,
            CancellationToken.None);

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);

        var response = Assert.IsType<ProviderServiceResponse>(created.Value);

        Assert.Equal(request.Title, response.Title);
        Assert.Equal(request.Description, response.Description);
        Assert.Equal(request.BasePrice, response.BasePrice);
        Assert.Equal(request.EstimatedDurationMinutes, response.EstimatedDurationMinutes);
    }

    [Fact]
    public async Task GetProviderServices_ReturnsOk()
    {
        var controller = new ProviderServicesController(
            new SuccessfulProviderService());

        var providerProfileId = Guid.NewGuid();

        var result = await controller.GetProviderServices(
            providerProfileId,
            CancellationToken.None);

        var ok = Assert.IsType<OkObjectResult>(result.Result);

        var response =
            Assert.IsAssignableFrom<IReadOnlyList<ProviderServiceResponse>>(ok.Value);

        Assert.Single(response);
        Assert.Equal(providerProfileId, response.First().ProviderProfileId);
    }

    private sealed class ProviderServiceNotFoundService : IProviderServiceService
    {
        public Task<ProviderServiceResponse> CreateAsync(
            CreateProviderServiceRequest request,
            CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public Task<ProviderServiceResponse?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
            => Task.FromResult<ProviderServiceResponse?>(null);

        public Task<IReadOnlyList<ProviderServiceResponse>> GetByProviderProfileIdAsync(
            Guid providerProfileId,
            CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<ProviderServiceResponse>>([]);

        public Task<ProviderServiceResponse> UpdateAsync(
            Guid id,
            UpdateProviderServiceRequest request,
            CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public Task DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default)
            => throw new NotImplementedException();
    }

    private sealed class SuccessfulProviderService : IProviderServiceService
    {
        public Task<ProviderServiceResponse> CreateAsync(
            CreateProviderServiceRequest request,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(
                new ProviderServiceResponse
                {
                    Id = Guid.NewGuid(),
                    ProviderProfileId = request.ProviderProfileId,
                    ServiceCategoryId = request.ServiceCategoryId,
                    Title = request.Title,
                    Description = request.Description,
                    BasePrice = request.BasePrice,
                    EstimatedDurationMinutes = request.EstimatedDurationMinutes,
                    IsActive = true
                });
        }

        public Task<ProviderServiceResponse?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult<ProviderServiceResponse?>(
                new ProviderServiceResponse
                {
                    Id = id,
                    ProviderProfileId = Guid.NewGuid(),
                    ServiceCategoryId = Guid.NewGuid(),
                    Title = "AC Repair",
                    Description = "Repair service",
                    BasePrice = 100,
                    EstimatedDurationMinutes = 60,
                    IsActive = true
                });
        }

        public Task<IReadOnlyList<ProviderServiceResponse>> GetByProviderProfileIdAsync(
            Guid providerProfileId,
            CancellationToken cancellationToken = default)
        {
            IReadOnlyList<ProviderServiceResponse> services =
            [
                new ProviderServiceResponse
                {
                    Id = Guid.NewGuid(),
                    ProviderProfileId = providerProfileId,
                    ServiceCategoryId = Guid.NewGuid(),
                    Title = "AC Repair",
                    Description = "Repair service",
                    BasePrice = 100,
                    EstimatedDurationMinutes = 60,
                    IsActive = true
                }
            ];

            return Task.FromResult(services);
        }

        public Task<ProviderServiceResponse> UpdateAsync(
            Guid id,
            UpdateProviderServiceRequest request,
            CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public Task DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default)
            => throw new NotImplementedException();
    }
}