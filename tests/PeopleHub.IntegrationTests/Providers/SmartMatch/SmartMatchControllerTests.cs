using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PeopleHub.Application.SmartMatch.Models;
using PeopleHub.Infrastructure.Persistence.Context;
using PeopleHub.IntegrationTests.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace PeopleHub.IntegrationTests.Controllers;

public class SmartMatchControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;
    private readonly ITestOutputHelper _output;

    public SmartMatchControllerTests(
        CustomWebApplicationFactory factory,
        ITestOutputHelper output)
    {
        _factory = factory;
        _output = output;

        _factory.ResetDatabase();
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task FindBestProvider_ShouldReturnOk()
    {
        Guid serviceRequestId = Guid.Empty;

        await _factory.ExecuteDbContextAsync(async context =>
        {
            var seeder = new TestDataSeeder(context);

            var category = await seeder.CreateServiceCategoryAsync();

            var provider = await seeder.CreateVerifiedProviderAsync(category);

            var request = await seeder.CreateServiceRequestAsync(
                provider,
                category);

            serviceRequestId = request.Id;
        });

        var response = await _client.PostAsync(
            $"/api/SmartMatch/{serviceRequestId}",
            null);

        var body = await response.Content.ReadAsStringAsync();
        _output.WriteLine(body);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content
            .ReadFromJsonAsync<SmartMatchResponse>();

        result.Should().NotBeNull();

        result!.CandidateCount.Should().BeGreaterThan(0);

        result.SelectedProviderId.Should().NotBeNull();
    }
}