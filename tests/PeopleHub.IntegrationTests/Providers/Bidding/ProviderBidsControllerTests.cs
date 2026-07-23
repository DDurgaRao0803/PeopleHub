using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PeopleHub.Contracts.Providers.Bidding;
using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Domain.Aggregates.User;
using PeopleHub.Domain.Entities;
using PeopleHub.Domain.ValueObjects;
using PeopleHub.IntegrationTests.Infrastructure;
using Xunit.Abstractions;

namespace PeopleHub.IntegrationTests.Providers.Bidding;

public sealed class ProviderBidsControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
private readonly HttpClient _client;
private readonly ITestOutputHelper _output;


    private static readonly Guid TestUserId =
        Guid.Parse(
            "11111111-1111-1111-1111-111111111111");


    public ProviderBidsControllerTests(
    CustomWebApplicationFactory factory,
    ITestOutputHelper output)
{
    _factory = factory;
    _output = output;

    factory.ResetDatabase();

    _client = factory.CreateClient();
}



    private async Task<Guid> SeedProviderAndRequestAsync()
    {
        var serviceRequestId = Guid.Empty;


        await _factory.ExecuteDbContextAsync(async context =>
        {
            var user = new User(
                "Test",
                "Provider",
                Email.Create("provider@test.com"),
                PhoneNumber.Create("+966500000001"),
                "password");


            typeof(User)
                .GetProperty("Id")?
                .SetValue(user, TestUserId);



            var category = new ServiceCategory(
                "Electrical");



            var provider = new ProviderProfile(
                user.Id,
                "Professional electrician",
                5);


            provider.Approve();

            provider.AddSkill(category);



            var serviceRequest = new ServiceRequest(
                Guid.NewGuid(),
                category.Id,
                "Repair AC",
                "AC not working",
                DateTime.UtcNow.AddDays(1));



            context.Users.Add(user);

            context.ServiceCategories.Add(category);

            context.ProviderProfiles.Add(provider);

            context.ServiceRequests.Add(serviceRequest);



            await context.SaveChangesAsync();


            serviceRequestId = serviceRequest.Id;
        });


        return serviceRequestId;
    }




    [Fact]
    public async Task SubmitBid_Should_Create_Bid()
    {
        var serviceRequestId =
            await SeedProviderAndRequestAsync();



        var request =
            new CreateProviderBidRequest(
                serviceRequestId,
                150,
                30);



        var response =
            await _client.PostAsJsonAsync(
                $"/api/service-requests/{serviceRequestId}/bids",
                request);



        var body =
            await response.Content.ReadAsStringAsync();


        Console.WriteLine(
            $"SUBMIT BID STATUS: {response.StatusCode}");

        Console.WriteLine(body);



        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Created);



        var result =
            await response.Content
                .ReadFromJsonAsync<ProviderBidResponse>();


        result.Should()
            .NotBeNull();


        result!.Amount
            .Should()
            .Be(150);


        result.Status
            .Should()
            .Be("Submitted");
    }




    [Fact]
    public async Task GetBids_Should_Return_Bids()
    {
        var serviceRequestId =
            await SeedProviderAndRequestAsync();



        var createResponse =
            await _client.PostAsJsonAsync(
                $"/api/service-requests/{serviceRequestId}/bids",
                new CreateProviderBidRequest(
                    serviceRequestId,
                    200,
                    45));



        createResponse.StatusCode
            .Should()
            .Be(HttpStatusCode.Created);



        var response =
            await _client.GetAsync(
                $"/api/service-requests/{serviceRequestId}/bids");



        var body =
    await response.Content.ReadAsStringAsync();


_output.WriteLine(
    $"GET BIDS STATUS: {response.StatusCode}");

_output.WriteLine(body);



        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);



        var result =
            await response.Content
                .ReadFromJsonAsync<
                    IReadOnlyList<ProviderBidResponse>>();


        result.Should()
            .NotBeNull();


        result!
            .Should()
            .HaveCount(1);
    }




    [Fact]
    public async Task AcceptBid_Should_Update_Status()
    {
        var serviceRequestId =
            await SeedProviderAndRequestAsync();



        var createResponse =
            await _client.PostAsJsonAsync(
                $"/api/service-requests/{serviceRequestId}/bids",
                new CreateProviderBidRequest(
                    serviceRequestId,
                    250,
                    60));



        createResponse.StatusCode
            .Should()
            .Be(HttpStatusCode.Created);



        var created =
            await createResponse.Content
                .ReadFromJsonAsync<ProviderBidResponse>();



        var response =
            await _client.PostAsync(
                $"/api/service-requests/{serviceRequestId}/bids/{created!.Id}/accept",
                null);



        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);



        var result =
            await response.Content
                .ReadFromJsonAsync<ProviderBidResponse>();


        result.Should()
            .NotBeNull();


        result!.Status
            .Should()
            .Be("Accepted");
    }
}