using System.Net;
using FluentAssertions;
using PeopleHub.IntegrationTests.Infrastructure;

namespace PeopleHub.IntegrationTests.Users;

[Collection("Integration Tests")]
public sealed class GetUsersTests
{
    private readonly HttpClient _client;

    public GetUsersTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
public async Task GetUsers_WithoutAuthentication_ShouldReturnUnauthorized()
{
    _client.DefaultRequestHeaders.Add("X-Test-Anonymous", "true");

    var response = await _client.GetAsync("/api/users");

    response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
}
}