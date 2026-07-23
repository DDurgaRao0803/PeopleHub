using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PeopleHub.Contracts.Providers.Dashboard;
using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Domain.Entities;
using PeopleHub.IntegrationTests.Infrastructure;

namespace PeopleHub.IntegrationTests.Providers.Dashboard;

public class ProviderDashboardControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    private static readonly Guid TestUserId =
        Guid.Parse("11111111-1111-1111-1111-111111111111");


    public ProviderDashboardControllerTests(
        CustomWebApplicationFactory factory)
    {
        _factory = factory;

        factory.ResetDatabase();

        _client = factory.CreateClient();
    }


    [Fact]
    public async Task GetDashboard_Should_Return_Provider_Dashboard()
    {
        await _factory.ExecuteDbContextAsync(async context =>
        {
            var provider = new ProviderProfile(
                TestUserId,
                "Experienced electrician",
                10);

            provider.Approve();

            context.ProviderProfiles.Add(provider);

            await context.SaveChangesAsync();
        });


        var response = await _client.GetAsync(
            "/api/provider/dashboard");


        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);


        var result = await response.Content
            .ReadFromJsonAsync<ProviderDashboardResponse>();


        result.Should().NotBeNull();

        result!.VerificationStatus
            .Should()
            .Be("Approved");

        result.CompletedJobs
            .Should()
            .Be(0);
    }


    [Fact]
    public async Task GetDashboard_Should_Return_NotFound_When_Profile_Missing()
    {
        var response = await _client.GetAsync(
            "/api/provider/dashboard");


        response.StatusCode
            .Should()
            .Be(HttpStatusCode.NotFound);
    }

    [Fact]
public async Task GetDashboard_Should_Return_Request_Status_Counts()
{
    await _factory.ExecuteDbContextAsync(async context =>
    {
        var category = new ServiceCategory(
            "Electrical");

        var provider = new ProviderProfile(
            TestUserId,
            "Experienced electrician",
            10);

        provider.Approve();

        provider.AddSkill(category);

        context.ServiceCategories.Add(category);
        context.ProviderProfiles.Add(provider);

        var pending = new ServiceRequest(
            Guid.NewGuid(),
            category.Id,
            "Pending Job",
            "Pending request",
            DateTime.UtcNow);

        pending.AssignProvider(provider.Id);


        var accepted = new ServiceRequest(
            Guid.NewGuid(),
            category.Id,
            "Accepted Job",
            "Accepted request",
            DateTime.UtcNow);

        accepted.AssignProvider(provider.Id);
        accepted.Accept();


        var completed = new ServiceRequest(
            Guid.NewGuid(),
            category.Id,
            "Completed Job",
            "Completed request",
            DateTime.UtcNow);

        completed.AssignProvider(provider.Id);
        completed.Accept();
        completed.Complete();


        var cancelled = new ServiceRequest(
            Guid.NewGuid(),
            category.Id,
            "Cancelled Job",
            "Cancelled request",
            DateTime.UtcNow);

        cancelled.AssignProvider(provider.Id);
        cancelled.Cancel();


        context.ServiceRequests.AddRange(
            pending,
            accepted,
            completed,
            cancelled);

        await context.SaveChangesAsync();
    });


    var response = await _client.GetAsync(
        "/api/provider/dashboard");


    response.StatusCode
        .Should()
        .Be(HttpStatusCode.OK);


    var result = await response.Content
        .ReadFromJsonAsync<ProviderDashboardResponse>();


    result.Should().NotBeNull();

    result!.PendingRequests
        .Should()
        .Be(1);

    result.AcceptedRequests
        .Should()
        .Be(1);

    result.CompletedRequests
        .Should()
        .Be(1);

    result.CancelledRequests
        .Should()
        .Be(1);
    }


}