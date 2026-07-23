using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PeopleHub.Contracts.Notifications;
using PeopleHub.Domain.Enums;
using PeopleHub.IntegrationTests.Infrastructure;
using Xunit;

namespace PeopleHub.IntegrationTests.Notifications;

public sealed class NotificationsControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;


    public NotificationsControllerTests(
        CustomWebApplicationFactory factory)
    {
        factory.ResetDatabase();

        _client = factory.CreateClient();
    }



    [Fact]
    public async Task Create_ShouldReturnCreated()
    {
        var request =
            new CreateNotificationRequest
            {
                Type = (int)NotificationType.ProviderSelected,

                Title = "Provider Selected",

                Message = "Your provider has been selected."
            };



        var response =
            await _client.PostAsJsonAsync(
                "/api/notifications",
                request);



        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Created);



        var result =
            await response.Content
                .ReadFromJsonAsync<NotificationResponse>();



        result.Should()
            .NotBeNull();



        result!.Title
            .Should()
            .Be("Provider Selected");


        result.IsRead
            .Should()
            .BeFalse();
    }



    [Fact]
    public async Task Get_ShouldReturnNotifications()
    {
        var createRequest =
            new CreateNotificationRequest
            {
                Type = (int)NotificationType.BidSubmitted,

                Title = "New Bid",

                Message = "A provider submitted a bid."
            };



        await _client.PostAsJsonAsync(
            "/api/notifications",
            createRequest);



        var response =
            await _client.GetAsync(
                "/api/notifications");



        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);



        var result =
            await response.Content
                .ReadFromJsonAsync<
                    IReadOnlyList<NotificationResponse>>();




        result.Should()
            .NotBeNull();


        result!
            .Should()
            .HaveCount(1);
    }



    [Fact]
    public async Task MarkRead_ShouldReturnNoContent()
    {
        var createRequest =
            new CreateNotificationRequest
            {
                Type = (int)NotificationType.ServiceCompleted,

                Title = "Completed",

                Message = "Your service is completed."
            };



        var createResponse =
            await _client.PostAsJsonAsync(
                "/api/notifications",
                createRequest);



        var created =
            await createResponse.Content
                .ReadFromJsonAsync<NotificationResponse>();



        created.Should()
            .NotBeNull();



        var response =
            await _client.PutAsync(
                $"/api/notifications/{created!.Id}/read",
                null);



        response.StatusCode
            .Should()
            .Be(HttpStatusCode.NoContent);
    }
}