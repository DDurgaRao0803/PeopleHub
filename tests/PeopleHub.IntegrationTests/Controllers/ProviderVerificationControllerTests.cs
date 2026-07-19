using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PeopleHub.Contracts.Providers;
using PeopleHub.IntegrationTests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace PeopleHub.IntegrationTests.Controllers;

public class ProviderVerificationControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public ProviderVerificationControllerTests(
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
    var profileRequest = new CreateProviderProfileRequest
    {
        Bio = "Experienced Electrician",
        ExperienceYears = 10
    };

    var profileResponse = await _client.PostAsJsonAsync(
        "/api/provider-profiles",
        profileRequest);

    profileResponse.StatusCode.Should().Be(HttpStatusCode.Created);

    var profile = await profileResponse.Content
        .ReadFromJsonAsync<ProviderProfileResponse>();

    profile.Should().NotBeNull();

    var verificationRequest = new CreateProviderVerificationRequest
{
    GovernmentIdType = GovernmentIdType.Passport,
    GovernmentIdNumber = "P123456789",
    FrontImageUrl = "https://example.com/front.jpg",
    BackImageUrl = "https://example.com/back.jpg",
    SelfieImageUrl = "https://example.com/selfie.jpg"
};

    // Act
    var response = await _client.PostAsJsonAsync(
        $"/api/provider-verifications/{profile!.Id}",
        verificationRequest);

    var body = await response.Content.ReadAsStringAsync();
    _output.WriteLine(body);

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.Created);

    var verification = await response.Content
        .ReadFromJsonAsync<ProviderVerificationResponse>();

    verification.Should().NotBeNull();
verification!.GovernmentIdType.Should().Be(verificationRequest.GovernmentIdType);
verification.GovernmentIdNumber.Should().Be(verificationRequest.GovernmentIdNumber);
verification.FrontImageUrl.Should().Be(verificationRequest.FrontImageUrl);
verification.BackImageUrl.Should().Be(verificationRequest.BackImageUrl);
verification.SelfieImageUrl.Should().Be(verificationRequest.SelfieImageUrl);
}

[Fact]
public async Task Get_ShouldReturnVerification()
{
    // Arrange
    var profileRequest = new CreateProviderProfileRequest
    {
        Bio = "Experienced Electrician",
        ExperienceYears = 10
    };

    var profileResponse = await _client.PostAsJsonAsync(
        "/api/provider-profiles",
        profileRequest);

    profileResponse.StatusCode.Should().Be(HttpStatusCode.Created);

    var profile = await profileResponse.Content
        .ReadFromJsonAsync<ProviderProfileResponse>();

    profile.Should().NotBeNull();

    var verificationRequest = new CreateProviderVerificationRequest
    {
        GovernmentIdType = GovernmentIdType.Passport,
        GovernmentIdNumber = "P123456789",
        FrontImageUrl = "https://example.com/front.jpg",
        BackImageUrl = "https://example.com/back.jpg",
        SelfieImageUrl = "https://example.com/selfie.jpg"
    };

    var createResponse = await _client.PostAsJsonAsync(
        $"/api/provider-verifications/{profile!.Id}",
        verificationRequest);

    createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

    // Act
    var response = await _client.GetAsync(
        $"/api/provider-verifications/{profile.Id}");

    var body = await response.Content.ReadAsStringAsync();
    _output.WriteLine(body);

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.OK);

    var verification = await response.Content
        .ReadFromJsonAsync<ProviderVerificationResponse>();

    verification.Should().NotBeNull();
    verification!.GovernmentIdType.Should().Be(verificationRequest.GovernmentIdType);
    verification.GovernmentIdNumber.Should().Be(verificationRequest.GovernmentIdNumber);
    verification.FrontImageUrl.Should().Be(verificationRequest.FrontImageUrl);
    verification.BackImageUrl.Should().Be(verificationRequest.BackImageUrl);
    verification.SelfieImageUrl.Should().Be(verificationRequest.SelfieImageUrl);
}

[Fact]
public async Task Get_WhenVerificationDoesNotExist_ShouldReturnNotFound()
{
    // Arrange
    var providerProfileId = Guid.NewGuid();

    // Act
    var response = await _client.GetAsync(
        $"/api/provider-verifications/{providerProfileId}");

    var body = await response.Content.ReadAsStringAsync();
    _output.WriteLine(body);

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.NotFound);
}


[Fact]
public async Task Delete_ShouldReturnNoContent()
{
    // Arrange
    var profileRequest = new CreateProviderProfileRequest
    {
        Bio = "Experienced Electrician",
        ExperienceYears = 10
    };

    var profileResponse = await _client.PostAsJsonAsync(
        "/api/provider-profiles",
        profileRequest);

    profileResponse.StatusCode.Should().Be(HttpStatusCode.Created);

    var profile = await profileResponse.Content
        .ReadFromJsonAsync<ProviderProfileResponse>();

    profile.Should().NotBeNull();

    var verificationRequest = new CreateProviderVerificationRequest
    {
        GovernmentIdType = GovernmentIdType.Passport,
        GovernmentIdNumber = "P123456789",
        FrontImageUrl = "https://example.com/front.jpg",
        BackImageUrl = "https://example.com/back.jpg",
        SelfieImageUrl = "https://example.com/selfie.jpg"
    };

    var createResponse = await _client.PostAsJsonAsync(
        $"/api/provider-verifications/{profile!.Id}",
        verificationRequest);

    createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

    // Act
    var deleteResponse = await _client.DeleteAsync(
        $"/api/provider-verifications/{profile.Id}");

    var body = await deleteResponse.Content.ReadAsStringAsync();
    _output.WriteLine(body);

    // Assert
    deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

    // Verify it was actually deleted
    var getResponse = await _client.GetAsync(
        $"/api/provider-verifications/{profile.Id}");

    getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
}

[Fact]
public async Task Delete_WhenVerificationDoesNotExist_ShouldReturnNotFound()
{
    // Arrange
    var providerProfileId = Guid.NewGuid();

    // Act
    var response = await _client.DeleteAsync(
        $"/api/provider-verifications/{providerProfileId}");

    var body = await response.Content.ReadAsStringAsync();
    _output.WriteLine(body);

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.NotFound);
}

[Fact]
public async Task Create_WhenVerificationAlreadyExists_ShouldReturnBadRequest()
{
    // Arrange
    var profileRequest = new CreateProviderProfileRequest
    {
        Bio = "Experienced Electrician",
        ExperienceYears = 10
    };

    var profileResponse = await _client.PostAsJsonAsync(
        "/api/provider-profiles",
        profileRequest);

    profileResponse.StatusCode.Should().Be(HttpStatusCode.Created);

    var profile = await profileResponse.Content
        .ReadFromJsonAsync<ProviderProfileResponse>();

    profile.Should().NotBeNull();

    var verificationRequest = new CreateProviderVerificationRequest
    {
        GovernmentIdType = GovernmentIdType.Passport,
        GovernmentIdNumber = "P123456789",
        FrontImageUrl = "https://example.com/front.jpg",
        BackImageUrl = "https://example.com/back.jpg",
        SelfieImageUrl = "https://example.com/selfie.jpg"
    };

    var firstResponse = await _client.PostAsJsonAsync(
        $"/api/provider-verifications/{profile!.Id}",
        verificationRequest);

    firstResponse.StatusCode.Should().Be(HttpStatusCode.Created);

    // Act
    var secondResponse = await _client.PostAsJsonAsync(
        $"/api/provider-verifications/{profile.Id}",
        verificationRequest);

    var body = await secondResponse.Content.ReadAsStringAsync();
    _output.WriteLine(body);

    // Assert
    secondResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
}

}