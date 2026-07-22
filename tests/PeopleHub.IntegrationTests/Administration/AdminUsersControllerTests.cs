using System.Net;
using FluentAssertions;
using PeopleHub.Contracts.Administration;
using PeopleHub.IntegrationTests.Infrastructure;
using Xunit;
using System.Net.Http.Json;

namespace PeopleHub.IntegrationTests.Administration;

public sealed class AdminUsersControllerTests
    : IntegrationTestBase
{
    public AdminUsersControllerTests(
        CustomWebApplicationFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task GetUsers_ShouldReturnOk()
    {
        var response = await Client.GetAsync("/api/admin/users");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetUser_ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        var response = await Client.GetAsync(
            $"/api/admin/users/{Guid.NewGuid()}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateUserStatus_ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        var request = new UpdateUserStatusRequest
        {
            Status = "Inactive"
        };

        var response = await Client.PutAsJsonAsync(
            $"/api/admin/users/{Guid.NewGuid()}/status",
            request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}