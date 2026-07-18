using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PeopleHub.Contracts.Authentication;
using PeopleHub.IntegrationTests.Infrastructure;

namespace PeopleHub.IntegrationTests.Authentication;

public sealed class RegisterTests : IntegrationTestBase
{
    public RegisterTests(CustomWebApplicationFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Register_WithValidRequest_ShouldReturnCreated()
    {
        // Arrange
        var request = new RegisterRequest
        {
            FirstName = "John",
            LastName = "Doe",
            Email = $"john{Guid.NewGuid():N}@test.com",
            Password = "Password@123",
            PhoneNumber = "1234567890"
        };

        // Act
var response = await Client.PostAsJsonAsync(
    "/api/auth/register",
    request);

if (response.StatusCode != HttpStatusCode.Created)
{
    var body = await response.Content.ReadAsStringAsync();

    throw new Xunit.Sdk.XunitException(
$"""
Status Code: {(int)response.StatusCode} ({response.StatusCode})

Response Body:
{body}
""");
}
    }
}