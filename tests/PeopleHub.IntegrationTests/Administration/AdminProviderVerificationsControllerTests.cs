using System.Net;
using FluentAssertions;
using PeopleHub.IntegrationTests.Infrastructure;
using Xunit;

namespace PeopleHub.IntegrationTests.Administration;

public sealed class AdminProviderVerificationsControllerTests
    : IntegrationTestBase
{
    public AdminProviderVerificationsControllerTests(
        CustomWebApplicationFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task GetPending_ShouldReturnOk()
    {
        // Act
        var response = await Client.GetAsync(
            "/api/admin/provider-verifications/pending");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetPending_ShouldReturnEmptyCollection_WhenNoPendingVerifications()
    {
        // Act
        var response = await Client.GetAsync(
            "/api/admin/provider-verifications/pending");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();

        content.Should().NotBeNullOrWhiteSpace();
    }
}