using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PeopleHub.Contracts.Administration;
using PeopleHub.IntegrationTests.Infrastructure;
using Xunit;

namespace PeopleHub.IntegrationTests.Administration;

public sealed class AdminDashboardControllerTests
    : IntegrationTestBase
{
    public AdminDashboardControllerTests(
        CustomWebApplicationFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task GetDashboard_ShouldReturnDashboardStatistics()
    {
        // Act
        var response = await Client.GetAsync("/api/admin/dashboard");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var dashboard = await response.Content
            .ReadFromJsonAsync<DashboardResponse>();

        dashboard.Should().NotBeNull();

        dashboard!.TotalUsers.Should().BeGreaterThanOrEqualTo(0);
dashboard.ActiveUsers.Should().BeGreaterThanOrEqualTo(0);
dashboard.Providers.Should().BeGreaterThanOrEqualTo(0);
dashboard.PendingVerifications.Should().BeGreaterThanOrEqualTo(0);
dashboard.ServiceCategories.Should().BeGreaterThanOrEqualTo(0);
dashboard.ServiceRequests.Should().BeGreaterThanOrEqualTo(0);
dashboard.CompletedServiceRequests.Should().BeGreaterThanOrEqualTo(0);
dashboard.Reviews.Should().BeGreaterThanOrEqualTo(0);
    }
}