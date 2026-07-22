using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PeopleHub.Contracts.Authentication;
using PeopleHub.IntegrationTests.Infrastructure;

namespace PeopleHub.IntegrationTests.Authentication;

public sealed class LoginTests : IntegrationTestBase
{
    public LoginTests(CustomWebApplicationFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Login_WithInvalidRequest_ShouldReturnBadRequest()
    {
        // Arrange
        var request = new LoginRequest
        {
            Email = string.Empty,
            Password = string.Empty
        };

        // Act
        var response = await Client.PostAsJsonAsync(
            "/api/auth/login",
            request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var body = await response.Content.ReadAsStringAsync();

        body.Should().NotBeNullOrWhiteSpace();
    }
}