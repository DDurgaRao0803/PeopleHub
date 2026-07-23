using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PeopleHub.Application.SmartMatch.Models;
using PeopleHub.Domain.Aggregates.Bidding;
using PeopleHub.Domain.Enums;
using PeopleHub.Infrastructure.Persistence.Context;
using PeopleHub.IntegrationTests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace PeopleHub.IntegrationTests.Controllers;

public class SmartMatchControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;
    private readonly ITestOutputHelper _output;


    public SmartMatchControllerTests(
        CustomWebApplicationFactory factory,
        ITestOutputHelper output)
    {
        _factory = factory;
        _output = output;

        _factory.ResetDatabase();

        _client = _factory.CreateClient();
    }



    [Fact]
    public async Task FindBestProvider_Should_SelectBestBid()
    {
        Guid serviceRequestId = Guid.Empty;
        Guid provider1Id = Guid.Empty;
        Guid provider2Id = Guid.Empty;



        await _factory.ExecuteDbContextAsync(async context =>
        {
            var seeder =
                new TestDataSeeder(context);



            var category =
                await seeder.CreateServiceCategoryAsync();



            var provider1 =
                await seeder.CreateVerifiedProviderAsync(
                    category);



            var provider2 =
                await seeder.CreateVerifiedProviderAsync(
                    category);



            var request =
                await seeder.CreateServiceRequestAsync(
                    provider1,
                    category);



            var bid1 =
                new ProviderBid(
                    request.Id,
                    provider1.Id,
                    300,
                    60);



            var bid2 =
                new ProviderBid(
                    request.Id,
                    provider2.Id,
                    150,
                    30);



            context.ProviderBids.AddRange(
                bid1,
                bid2);



            await context.SaveChangesAsync();



            serviceRequestId = request.Id;

            provider1Id = provider1.Id;

            provider2Id = provider2.Id;
        });



        var response =
            await _client.PostAsync(
                $"/api/SmartMatch/{serviceRequestId}",
                null);



        var body =
            await response.Content.ReadAsStringAsync();


        _output.WriteLine(body);



        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);



        var result =
            await response.Content
                .ReadFromJsonAsync<SmartMatchResponse>();


        result.Should()
            .NotBeNull();



        result!
            .SelectedProviderId
            .Should()
            .Be(provider2Id);



        await _factory.ExecuteDbContextAsync(async context =>
        {
            var bids =
                context.ProviderBids
                    .Where(x =>
                        x.ServiceRequestId == serviceRequestId)
                    .ToList();



            var winningBid =
                bids.First(
                    x => x.ProviderProfileId == provider2Id);



            var losingBid =
                bids.First(
                    x => x.ProviderProfileId == provider1Id);



            winningBid.Status
                .Should()
                .Be(ProviderBidStatus.Accepted);



            losingBid.Status
                .Should()
                .Be(ProviderBidStatus.Rejected);



            var request =
                await context.ServiceRequests
                    .FindAsync(serviceRequestId);



            request.Should()
                .NotBeNull();



            request!
                .ProviderProfileId
                .Should()
                .Be(provider2Id);
        });
    }
}