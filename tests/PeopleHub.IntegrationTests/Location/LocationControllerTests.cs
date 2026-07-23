using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using PeopleHub.Contracts.Location;

using PeopleHub.IntegrationTests.Infrastructure;

namespace PeopleHub.IntegrationTests.Location;

public sealed class LocationControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;


    public LocationControllerTests(
        CustomWebApplicationFactory factory)
    {
        factory.ResetDatabase();

        _client = factory.CreateClient();
    }



    private static readonly Guid ProviderProfileId =
        Guid.Parse(
            "11111111-1111-1111-1111-111111111111");



    [Fact]
    public async Task UpdateLocation_Should_Return_Location()
    {
        var request =
            new UpdateLocationRequest
            {
                Latitude = 24.7136m,

                Longitude = 46.6753m
            };



        var response =
            await _client.PostAsJsonAsync(
                "/api/location/update",
                request);



        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);



        var result =
            await response.Content
                .ReadFromJsonAsync<ProviderLocationResponse>();



        result.Should()
            .NotBeNull();


        result!.Latitude
            .Should()
            .Be(24.7136m);


        result.Longitude
            .Should()
            .Be(46.6753m);
    }




    [Fact]
    public async Task GetProviderLocation_Should_Return_Location()
    {
        var updateRequest =
            new UpdateLocationRequest
            {
                Latitude = 24.7136m,

                Longitude = 46.6753m
            };



        await _client.PostAsJsonAsync(
            "/api/location/update",
            updateRequest);



        var response =
            await _client.GetAsync(
                $"/api/location/provider/{ProviderProfileId}");



        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);



        var result =
            await response.Content
                .ReadFromJsonAsync<ProviderLocationResponse>();



        result.Should()
            .NotBeNull();


        result!.ProviderProfileId
            .Should()
            .Be(ProviderProfileId);
    }




    [Fact]
    public async Task UpdateLocation_Should_Replace_Previous_Location()
    {
        await _client.PostAsJsonAsync(
            "/api/location/update",
            new UpdateLocationRequest
            {
                Latitude = 24.7136m,

                Longitude = 46.6753m
            });



        await _client.PostAsJsonAsync(
            "/api/location/update",
            new UpdateLocationRequest
            {
                Latitude = 24.7000m,

                Longitude = 46.6800m
            });



        var response =
            await _client.GetAsync(
                $"/api/location/provider/{ProviderProfileId}");



        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);



        var result =
            await response.Content
                .ReadFromJsonAsync<ProviderLocationResponse>();



        result.Should()
            .NotBeNull();



        result!.Latitude
            .Should()
            .Be(24.7000m);


        result.Longitude
            .Should()
            .Be(46.6800m);
    }
}