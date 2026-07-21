using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PeopleHub.Contracts.Providers.Reviews;
using PeopleHub.IntegrationTests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace PeopleHub.IntegrationTests.Providers.Reviews;

public sealed class ReviewControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public ReviewControllerTests(
        CustomWebApplicationFactory factory,
        ITestOutputHelper output)
    {
        factory.ResetDatabase();

        _client = factory.CreateClient();
        _output = output;
    }

    [Fact]
    public async Task Create_ShouldReturnCreated()
    {
        // TODO
    }

    [Fact]
    public async Task GetProviderReviews_ShouldReturnOk()
    {
        // TODO
    }
}