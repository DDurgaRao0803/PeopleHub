using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.SignalR.Client;
using PeopleHub.Contracts.Location;
using PeopleHub.IntegrationTests.Infrastructure;

namespace PeopleHub.IntegrationTests.Location;

public sealed class LocationHubTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;


    private static readonly Guid ProviderProfileId =
        Guid.Parse(
            "11111111-1111-1111-1111-111111111111");



    public LocationHubTests(
        CustomWebApplicationFactory factory)
    {
        _factory = factory;

        factory.ResetDatabase();

        _client = factory.CreateClient();
    }



    [Fact]
    public async Task LocationUpdate_Should_Broadcast_Event()
    {
        var received =
            new TaskCompletionSource<bool>();


        var connection =
    new HubConnectionBuilder()
        .WithUrl(
            "http://localhost/hubs/location",
            options =>
            {
                options.HttpMessageHandlerFactory =
                    _ =>
                        _factory.Server.CreateHandler();
            })
        .Build();



        connection.On<object>(
            "LocationUpdated",
            data =>
            {
                received.SetResult(true);
            });



        await connection.StartAsync();



        await connection.InvokeAsync(
            "JoinProviderTracking",
            ProviderProfileId.ToString());



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



        var completed =
            await Task.WhenAny(
                received.Task,
                Task.Delay(5000));



        completed
            .Should()
            .Be(received.Task);



        await connection.DisposeAsync();
    }
}