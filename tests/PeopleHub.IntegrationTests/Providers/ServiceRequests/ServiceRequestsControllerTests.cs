using System.Net;
using FluentAssertions;
using PeopleHub.IntegrationTests.Infrastructure;
using Xunit;
using Xunit.Abstractions;
using System.Net.Http.Json;
using PeopleHub.Contracts.Providers.ServiceRequests;

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

    [Fact]
public async Task Create_ShouldReturnCreated()
{
    // Arrange
    var request = new CreateServiceRequestRequest(
        Guid.NewGuid(),
        Guid.NewGuid(),
        "Plumbing Service",
        "Kitchen sink leakage",
        DateTime.UtcNow.AddDays(1));

    // Act
    var response = await _client.PostAsJsonAsync(
        "/api/service-requests",
        request);

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.Created);

    var created = await response.Content
        .ReadFromJsonAsync<ServiceRequestResponse>();

    created.Should().NotBeNull();

    created!.Id.Should().NotBe(Guid.Empty);
    created.CustomerId.Should().NotBe(Guid.Empty);
    created.ProviderProfileId.Should().BeNull();
    created.ServiceCategoryId.Should().Be(request.ServiceCategoryId);
    created.Title.Should().Be(request.Title);
    created.Description.Should().Be(request.Description);
    created.Status.Should().Be("Pending");
}

[Fact]
public async Task GetById_ShouldReturnCreatedRequest()
{
    // Arrange
    var request = new CreateServiceRequestRequest(
        Guid.NewGuid(),
        Guid.NewGuid(),
        "Electrical Repair",
        "Repair ceiling fan",
        DateTime.UtcNow.AddDays(2));

    var createResponse = await _client.PostAsJsonAsync(
        "/api/service-requests",
        request);

    createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

    var created = await createResponse.Content
        .ReadFromJsonAsync<ServiceRequestResponse>();

    // Act
    var response = await _client.GetAsync(
        $"/api/service-requests/{created!.Id}");

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.OK);

    var result = await response.Content
        .ReadFromJsonAsync<ServiceRequestResponse>();

    result.Should().NotBeNull();

    result!.Id.Should().Be(created.Id);
    result.Title.Should().Be(request.Title);
    result.Description.Should().Be(request.Description);
    result.ProviderProfileId.Should().BeNull();
    result.ServiceCategoryId.Should().Be(request.ServiceCategoryId);
    result.Status.Should().Be("Pending");
}


}