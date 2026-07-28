using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PeopleHub.Contracts.Providers.Reviews;
using PeopleHub.IntegrationTests.Infrastructure;
using Xunit;
using Xunit.Abstractions;
using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Domain.Aggregates.Review;
using PeopleHub.Domain.Aggregates.User;
using PeopleHub.Domain.Entities;
using PeopleHub.Domain.ValueObjects;


namespace PeopleHub.IntegrationTests.Controllers;

public sealed class ReviewControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
private readonly CustomWebApplicationFactory _factory;
private readonly ITestOutputHelper _output;

public ReviewControllerTests(
    CustomWebApplicationFactory factory,
    ITestOutputHelper output)
{
    _factory = factory;

    _factory.ResetDatabase();

    _client = factory.CreateClient();
    _output = output;
}

    [Fact]
public async Task Create_ShouldReturnCreated()
{
    // Arrange
    var customer = new User(
    "John",
    "Customer",
    Email.Create("customer@test.com"),
    PhoneNumber.Create("+966500000001"),
    "PasswordHash");

var provider = new User(
    "Jane",
    "Provider",
    Email.Create("provider@test.com"),
    PhoneNumber.Create("+966500000002"),
    "PasswordHash");

    var providerProfile = new ProviderProfile(
        provider.Id,
        "Experienced Electrician",
        10);

    var serviceRequest = new ServiceRequest(
        customer.Id,
        Guid.NewGuid(),
        "Electrical Repair",
        "Fix wall socket",
        DateTime.UtcNow);

    serviceRequest.AssignProvider(providerProfile.Id);
    serviceRequest.Accept();
    serviceRequest.Complete();

    await _factory.ExecuteDbContextAsync(async context =>
    {
        context.Users.Add(customer);
        context.Users.Add(provider);
        context.ProviderProfiles.Add(providerProfile);
        context.ServiceRequests.Add(serviceRequest);

        await context.SaveChangesAsync();
    });

    var request = new CreateReviewRequest
    {
        ProviderProfileId = providerProfile.Id,
        ServiceRequestId = serviceRequest.Id,
        Rating = 5,
        Comment = "Excellent service"
    };

    // Act
    var response = await _client.PostAsJsonAsync(
        "/api/provider-reviews",
        request);

    var body = await response.Content.ReadAsStringAsync();
    _output.WriteLine(body);

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.Created);

    var review = await response.Content
        .ReadFromJsonAsync<ReviewResponse>();

    review.Should().NotBeNull();
    review!.ProviderProfileId.Should().Be(providerProfile.Id);
    review.ServiceRequestId.Should().Be(serviceRequest.Id);
    review.Rating.Should().Be(5);
    review.Comment.Should().Be("Excellent service");
}

    [Fact]
public async Task GetProviderReviews_ShouldReturnOk()
{
    // Arrange
    var customer = new User(
    "John",
    "Customer",
    Email.Create("customer@test.com"),
    PhoneNumber.Create("+966500000001"),
    "PasswordHash");

var provider = new User(
    "Jane",
    "Provider",
    Email.Create("provider@test.com"),
    PhoneNumber.Create("+966500000002"),
    "PasswordHash");

    var providerProfile = new ProviderProfile(
        provider.Id,
        "Experienced Electrician",
        10);

    var serviceRequest = new ServiceRequest(
        customer.Id,
        Guid.NewGuid(),
        "Electrical Repair",
        "Fix wall socket",
        DateTime.UtcNow);

    serviceRequest.AssignProvider(providerProfile.Id);
    serviceRequest.Accept();
    serviceRequest.Complete();

    var review = new Review(
        customer.Id,
        providerProfile.Id,
        serviceRequest.Id,
        5,
        "Excellent service");

    await _factory.ExecuteDbContextAsync(async context =>
    {
        context.Users.Add(customer);
        context.Users.Add(provider);
        context.ProviderProfiles.Add(providerProfile);
        context.ServiceRequests.Add(serviceRequest);
        context.Reviews.Add(review);

        await context.SaveChangesAsync();
    });

    // Act
    var response = await _client.GetAsync(
        $"/api/provider-reviews/provider/{providerProfile.Id}");

    var body = await response.Content.ReadAsStringAsync();
    _output.WriteLine(body);

    // Assert
    response.StatusCode.Should().Be(HttpStatusCode.OK);

    var reviews = await response.Content
        .ReadFromJsonAsync<List<ReviewResponse>>();

    reviews.Should().NotBeNull();
    reviews.Should().ContainSingle();

    reviews![0].Rating.Should().Be(5);
}

    [Fact]
    public async Task Create_WithInvalidRating_ShouldReturnBadRequest()
    {
        // Arrange
        var request = new CreateReviewRequest
        {
            Rating = 6,
            Comment = "Invalid rating"
        };

        // Act
        var response = await _client.PostAsJsonAsync(
            "/api/provider-reviews",
            request);

        var body = await response.Content.ReadAsStringAsync();
        _output.WriteLine(body);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetProviderReviews_WhenProviderHasNoReviews_ShouldReturnOk()
    {
        // Arrange
        var providerProfileId =
            Guid.NewGuid();

        // Act
        var response = await _client.GetAsync(
            $"/api/provider-reviews/provider/{providerProfileId}");

        var body = await response.Content.ReadAsStringAsync();
        _output.WriteLine(body);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var reviews = await response.Content
            .ReadFromJsonAsync<List<ReviewResponse>>();

        reviews.Should().NotBeNull();
        reviews.Should().BeEmpty();
    }
}