using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PeopleHub.Contracts.Providers.Services;
using PeopleHub.IntegrationTests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace PeopleHub.IntegrationTests.Providers.Services;

public class ProviderServicesControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public ProviderServicesControllerTests(
        CustomWebApplicationFactory factory,
        ITestOutputHelper output)
    {
        factory.ResetDatabase();

        _client = factory.CreateClient();
        _output = output;
    }

    [Fact]
    public async Task Create_ShouldReturnNotFound_WhenProviderProfileDoesNotExist()
    {
        var request = new CreateProviderServiceRequest
        {
            ProviderProfileId = Guid.NewGuid(),
            ServiceCategoryId = Guid.NewGuid(),
            Title = "AC Repair",
            Description = "Home AC Repair",
            BasePrice = 150,
            EstimatedDurationMinutes = 90
        };

        var response = await _client.PostAsJsonAsync(
            "/api/provider-services",
            request);

        var body = await response.Content.ReadAsStringAsync();
        _output.WriteLine(body);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetById_WhenServiceDoesNotExist_ShouldReturnNotFound()
    {
        var response = await _client.GetAsync(
            $"/api/provider-services/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetProviderServices_WhenProviderHasNoServices_ShouldReturnOk()
    {
        var providerProfileId = Guid.NewGuid();

        var response = await _client.GetAsync(
            $"/api/provider-services/provider/{providerProfileId}");

        var body = await response.Content.ReadAsStringAsync();
        _output.WriteLine(body);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var services =
            await response.Content.ReadFromJsonAsync<List<ProviderServiceResponse>>();

        services.Should().NotBeNull();
        services.Should().BeEmpty();
    }

    [Fact]
    public async Task Update_WhenServiceDoesNotExist_ShouldReturnNotFound()
    {
        var request = new UpdateProviderServiceRequest
        {
            Title = "Updated Service",
            Description = "Updated Description",
            BasePrice = 250,
            EstimatedDurationMinutes = 120,
            IsActive = true
        };

        var response = await _client.PutAsJsonAsync(
            $"/api/provider-services/{Guid.NewGuid()}",
            request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_WhenServiceDoesNotExist_ShouldReturnNotFound()
    {
        var response = await _client.DeleteAsync(
            $"/api/provider-services/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}