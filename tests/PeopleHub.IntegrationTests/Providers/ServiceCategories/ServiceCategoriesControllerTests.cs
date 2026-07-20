using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PeopleHub.Contracts.Providers;
using PeopleHub.IntegrationTests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace PeopleHub.IntegrationTests.Controllers;

public class ServiceCategoriesControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public ServiceCategoriesControllerTests(
        CustomWebApplicationFactory factory,
        ITestOutputHelper output)
    {
        _client = factory.CreateClient();
        _output = output;
    }

    [Fact]
    public async Task GetAll_ShouldReturnSuccessStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/api/service-categories");

        var body = await response.Content.ReadAsStringAsync();
        _output.WriteLine(body);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Create_ShouldReturnCreated()
    {
        // Arrange
        var uniqueName = $"Plumbing-{Guid.NewGuid():N}";

        var request = new CreateServiceCategoryRequest
        {
            Name = uniqueName,
            Description = "Plumbing services"
        };

        // Act
        var response = await _client.PostAsJsonAsync(
            "/api/service-categories",
            request);

        var body = await response.Content.ReadAsStringAsync();

        _output.WriteLine(body);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();
        body.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task GetById_ShouldReturnCreatedCategory()
    {
        // Arrange
        var uniqueName = $"Electrical-{Guid.NewGuid():N}";

        var request = new CreateServiceCategoryRequest
        {
            Name = uniqueName,
            Description = "Electrical services"
        };

        var createResponse = await _client.PostAsJsonAsync(
            "/api/service-categories",
            request);

        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        createResponse.Headers.Location.Should().NotBeNull();

        // Act
        var response = await _client.GetAsync(createResponse.Headers.Location!);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var category = await response.Content.ReadFromJsonAsync<ServiceCategoryResponse>();

        category.Should().NotBeNull();
        category!.Name.Should().Be(uniqueName);
        category.Description.Should().Be("Electrical services");
        category.IsActive.Should().BeTrue();
    }

    [Fact]
    public async Task Update_ShouldReturnNoContent_AndUpdateCategory()
    {
        // Arrange
        var uniqueName = $"Electrical-{Guid.NewGuid():N}";

        var createRequest = new CreateServiceCategoryRequest
        {
            Name = uniqueName,
            Description = "Electrical services"
        };

        var createResponse = await _client.PostAsJsonAsync(
            "/api/service-categories",
            createRequest);

        createResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        createResponse.Headers.Location.Should().NotBeNull();

        var locationId = Guid.Parse(
            createResponse.Headers.Location!.Segments[^1]);

        var updateRequest = new UpdateServiceCategoryRequest
        {
            Name = $"{uniqueName}-Updated",
            Description = "Updated electrical services",
            IsActive = true
        };

        // Act
        var updateResponse = await _client.PutAsJsonAsync(
            $"/api/service-categories/{locationId}",
            updateRequest);

        // Assert
        updateResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var updated = await _client.GetFromJsonAsync<ServiceCategoryResponse>(
            $"/api/service-categories/{locationId}");

        updated.Should().NotBeNull();
        updated!.Id.Should().Be(locationId);
        updated.Name.Should().Be($"{uniqueName}-Updated");
        updated.Description.Should().Be("Updated electrical services");
        updated.IsActive.Should().BeTrue();
    }
}