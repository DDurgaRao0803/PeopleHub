using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PeopleHub.Contracts.Providers.Profiles;
using PeopleHub.IntegrationTests.Infrastructure;
using Xunit;

namespace PeopleHub.IntegrationTests.Providers;

public sealed class ProviderProfileTests : IntegrationTestBase
{
    public ProviderProfileTests(CustomWebApplicationFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Get_WhenProfileDoesNotExist_ShouldReturnNotFound()
    {
        // Act
        var response = await Client.GetAsync("/api/provider-profiles");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Create_ShouldReturnCreated()
    {
        // Arrange
        var request = new CreateProviderProfileRequest
        {
            Bio = "Experienced healthcare provider.",
            ExperienceYears = 10
        };

        // Act
        var response = await Client.PostAsJsonAsync(
            "/api/provider-profiles",
            request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
public async Task Create_ThenGet_ShouldReturnProviderProfile()
{
    // Arrange
    var request = new CreateProviderProfileRequest
    {
        Bio = "Experienced healthcare provider.",
        ExperienceYears = 10
    };

    // Create
    var createResponse = await Client.PostAsJsonAsync(
        "/api/provider-profiles",
        request);

    createResponse.EnsureSuccessStatusCode();

    // Act
    var getResponse = await Client.GetAsync("/api/provider-profiles");

    // Assert
    getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

    var profile = await getResponse.Content.ReadFromJsonAsync<ProviderProfileResponse>();

    profile.Should().NotBeNull();
    profile!.Bio.Should().Be(request.Bio);
    profile.ExperienceYears.Should().Be(request.ExperienceYears);
}

[Fact]
public async Task Update_ShouldReturnUpdatedProviderProfile()
{
    // Arrange
    var createRequest = new CreateProviderProfileRequest
    {
        Bio = "Initial bio",
        ExperienceYears = 5
    };

    var createResponse = await Client.PostAsJsonAsync(
        "/api/provider-profiles",
        createRequest);

    createResponse.EnsureSuccessStatusCode();

    var updateRequest = new UpdateProviderProfileRequest
    {
        Bio = "Updated bio",
        ExperienceYears = 10
    };

    // Act
    var updateResponse = await Client.PutAsJsonAsync(
        "/api/provider-profiles",
        updateRequest);

    // Assert
    updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

    var profile = await updateResponse.Content
        .ReadFromJsonAsync<ProviderProfileResponse>();

    profile.Should().NotBeNull();
    profile!.Bio.Should().Be("Updated bio");
    profile.ExperienceYears.Should().Be(10);
}

[Fact]
public async Task Delete_ShouldRemoveProviderProfile()
{
    // Arrange
    var createRequest = new CreateProviderProfileRequest
    {
        Bio = "Provider to delete",
        ExperienceYears = 5
    };

    var createResponse = await Client.PostAsJsonAsync(
        "/api/provider-profiles",
        createRequest);

    createResponse.EnsureSuccessStatusCode();

    // Act
    var deleteResponse = await Client.DeleteAsync("/api/provider-profiles");

    // Assert
    deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

    var getResponse = await Client.GetAsync("/api/provider-profiles");

    getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
}
[Fact]
public async Task Create_WhenProfileAlreadyExists_ShouldReturnBadRequest()
{
    // Arrange
    var request = new CreateProviderProfileRequest
    {
        Bio = "Provider Bio",
        ExperienceYears = 5
    };

    var firstResponse = await Client.PostAsJsonAsync(
        "/api/provider-profiles",
        request);

    firstResponse.EnsureSuccessStatusCode();

    // Act
    var secondResponse = await Client.PostAsJsonAsync(
        "/api/provider-profiles",
        request);

    // Assert
    secondResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
}
}