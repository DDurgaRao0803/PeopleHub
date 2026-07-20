using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PeopleHub.Contracts.Providers.Profiles;
using PeopleHub.IntegrationTests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace PeopleHub.IntegrationTests.Controllers;

public class ProviderProfilesControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public ProviderProfilesControllerTests(
    CustomWebApplicationFactory factory,
    ITestOutputHelper output)
{
    factory.ResetDatabase();

    _client = factory.CreateClient();
    _output = output;
}

    [Fact]
    public async Task Create_ShouldReturnCreated()
    {
        // Arrange
        var request = new CreateProviderProfileRequest
        {
            Bio = "Experienced electrician",
            ExperienceYears = 10
        };

        // Act
        var response = await _client.PostAsJsonAsync(
            "/api/provider-profiles",
            request);

        var body = await response.Content.ReadAsStringAsync();
        _output.WriteLine(body);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var profile = await response.Content
            .ReadFromJsonAsync<ProviderProfileResponse>();

        profile.Should().NotBeNull();
        profile!.Bio.Should().Be(request.Bio);
        profile.ExperienceYears.Should().Be(request.ExperienceYears);
    }

    [Fact]
public async Task Get_ShouldReturnCreatedProfile()
{
    // Arrange
    var request = new CreateProviderProfileRequest
    {
        Bio = "Professional plumber",
        ExperienceYears = 8
    };

    var createResponse = await _client.PostAsJsonAsync(
        "/api/provider-profiles",
        request);

    createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

    // Act
    var response = await _client.GetAsync("/api/provider-profiles");

    var body = await response.Content.ReadAsStringAsync();
    _output.WriteLine(body);

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.OK);

    var profile = await response.Content
        .ReadFromJsonAsync<ProviderProfileResponse>();

    profile.Should().NotBeNull();
    profile!.Bio.Should().Be(request.Bio);
    profile.ExperienceYears.Should().Be(request.ExperienceYears);
}
[Fact]
public async Task Update_ShouldReturnUpdatedProfile()
{
    // Arrange
    var createRequest = new CreateProviderProfileRequest
    {
        Bio = "Electrician",
        ExperienceYears = 5
    };

    var createResponse = await _client.PostAsJsonAsync(
        "/api/provider-profiles",
        createRequest);

    createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

    var updateRequest = new UpdateProviderProfileRequest
    {
        Bio = "Master Electrician",
        ExperienceYears = 12
    };

    // Act
    var updateResponse = await _client.PutAsJsonAsync(
        "/api/provider-profiles",
        updateRequest);

    var body = await updateResponse.Content.ReadAsStringAsync();
    _output.WriteLine(body);

    // Assert
    updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

    var profile = await updateResponse.Content
        .ReadFromJsonAsync<ProviderProfileResponse>();

    profile.Should().NotBeNull();
    profile!.Bio.Should().Be(updateRequest.Bio);
    profile.ExperienceYears.Should().Be(updateRequest.ExperienceYears);
}

[Fact]
public async Task Delete_ShouldRemoveProfile()
{
    // Arrange
    var createRequest = new CreateProviderProfileRequest
    {
        Bio = "Temporary Profile",
        ExperienceYears = 2
    };

    var createResponse = await _client.PostAsJsonAsync(
        "/api/provider-profiles",
        createRequest);

    createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

    // Act
    var deleteResponse = await _client.DeleteAsync(
        "/api/provider-profiles");

    // Assert
    deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

    var getResponse = await _client.GetAsync(
        "/api/provider-profiles");

    getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
}

[Fact]
public async Task Create_WhenProfileAlreadyExists_ShouldReturnBadRequest()
{
    // Arrange
    var request = new CreateProviderProfileRequest
    {
        Bio = "Existing Provider",
        ExperienceYears = 5
    };

    var firstResponse = await _client.PostAsJsonAsync(
        "/api/provider-profiles",
        request);

    firstResponse.StatusCode.Should().Be(HttpStatusCode.Created);

    // Act
    var secondResponse = await _client.PostAsJsonAsync(
        "/api/provider-profiles",
        request);

    var body = await secondResponse.Content.ReadAsStringAsync();
    _output.WriteLine(body);

    // Assert
    secondResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
}
[Fact]
public async Task Get_WhenProfileDoesNotExist_ShouldReturnNotFound()
{
    // Act
    var response = await _client.GetAsync("/api/provider-profiles");

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.NotFound);
}

}