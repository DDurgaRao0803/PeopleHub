using System.Net;
using FluentAssertions;
using PeopleHub.IntegrationTests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace PeopleHub.IntegrationTests.Providers.ServiceRequests;

public class ServiceRequestsControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public ServiceRequestsControllerTests(
        CustomWebApplicationFactory factory,
        ITestOutputHelper output)
    {
        factory.ResetDatabase();

        _client = factory.CreateClient();
        _output = output;
    }

    [Fact]
    public async Task GetById_WhenRequestDoesNotExist_ShouldReturnNotFound()
    {
        var response = await _client.GetAsync(
            $"/api/service-requests/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetCustomerRequests_WhenCustomerHasNoRequests_ShouldReturnOk()
    {
        var customerId = Guid.NewGuid();

        var response = await _client.GetAsync(
            $"/api/service-requests/customer/{customerId}");

        var body = await response.Content.ReadAsStringAsync();

        _output.WriteLine($"Status: {response.StatusCode}");
        _output.WriteLine(body);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetProviderRequests_WhenProviderHasNoRequests_ShouldReturnOk()
    {
        var providerId = Guid.NewGuid();

        var response = await _client.GetAsync(
            $"/api/service-requests/provider/{providerId}");

        var body = await response.Content.ReadAsStringAsync();

        _output.WriteLine($"Status: {response.StatusCode}");
        _output.WriteLine(body);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Accept_WhenRequestDoesNotExist_ShouldReturnNotFound()
    {
        var response = await _client.PutAsync(
            $"/api/service-requests/{Guid.NewGuid()}/accept",
            null);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Reject_WhenRequestDoesNotExist_ShouldReturnNotFound()
    {
        var response = await _client.PostAsync(
            $"/api/service-requests/{Guid.NewGuid()}/reject",
            null);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Complete_WhenRequestDoesNotExist_ShouldReturnNotFound()
    {
        var response = await _client.PutAsync(
            $"/api/service-requests/{Guid.NewGuid()}/complete",
            null);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}