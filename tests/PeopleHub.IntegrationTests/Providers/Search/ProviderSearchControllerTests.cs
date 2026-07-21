using System.Net;

using FluentAssertions;

using PeopleHub.IntegrationTests.Infrastructure;

namespace PeopleHub.IntegrationTests.Providers.Search;

public sealed class ProviderSearchControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProviderSearchControllerTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Search_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/providers/search");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}