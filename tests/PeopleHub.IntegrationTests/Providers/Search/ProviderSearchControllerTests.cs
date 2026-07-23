using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using PeopleHub.Contracts.Common;
using PeopleHub.Contracts.Providers.Search;
using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Domain.Aggregates.User;
using PeopleHub.Domain.Entities;
using PeopleHub.Domain.ValueObjects;
using PeopleHub.IntegrationTests.Infrastructure;

namespace PeopleHub.IntegrationTests.Providers.Search;

public sealed class ProviderSearchControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;


    public ProviderSearchControllerTests(
        CustomWebApplicationFactory factory)
    {
        _factory = factory;

        factory.ResetDatabase();

        _client = factory.CreateClient();
    }


    [Fact]
    public async Task Search_Should_Return_Providers()
    {
        await _factory.ExecuteDbContextAsync(async context =>
        {
            var user = new User(
                "John",
                "Electrician",
                Email.Create("john.electric@test.com"),
                PhoneNumber.Create("+966500000001"),
                "test-password");


            var category = new ServiceCategory(
                "Electrical");


            var provider = new ProviderProfile(
                user.Id,
                "Expert electrician",
                10);


            provider.Approve();

            provider.AddSkill(category);


            context.Users.Add(user);

            context.ServiceCategories.Add(category);

            context.ProviderProfiles.Add(provider);


            await context.SaveChangesAsync();
        });


        var response = await _client.GetAsync(
            "/api/providers/search");


        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);


        var result = await response.Content
            .ReadFromJsonAsync<
                PagedResponse<ProviderSearchResponse>>();


        result.Should()
            .NotBeNull();


        result!.TotalCount
            .Should()
            .Be(1);


        result.Items
            .Should()
            .ContainSingle();


        result.Items[0].Bio
            .Should()
            .Be("Expert electrician");
    }



    [Fact]
    public async Task Search_With_VerifiedOnly_Should_Return_Only_Verified_Providers()
    {
        await _factory.ExecuteDbContextAsync(async context =>
        {
            var category = new ServiceCategory(
                "Plumbing");


            var approvedUser = new User(
                "Approved",
                "Provider",
                Email.Create("approved@test.com"),
                PhoneNumber.Create("+966500000002"),
                "test-password");


            var pendingUser = new User(
                "Pending",
                "Provider",
                Email.Create("pending@test.com"),
                PhoneNumber.Create("+966500000003"),
                "test-password");



            var approvedProvider = new ProviderProfile(
                approvedUser.Id,
                "Approved plumber",
                8);


            approvedProvider.Approve();

            approvedProvider.AddSkill(category);



            var pendingProvider = new ProviderProfile(
                pendingUser.Id,
                "New plumber",
                2);


            pendingProvider.AddSkill(category);



            context.Users.AddRange(
                approvedUser,
                pendingUser);


            context.ServiceCategories.Add(category);


            context.ProviderProfiles.AddRange(
                approvedProvider,
                pendingProvider);


            await context.SaveChangesAsync();
        });


        var response = await _client.GetAsync(
            "/api/providers/search?verifiedOnly=true");


        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);



        var result = await response.Content
            .ReadFromJsonAsync<
                PagedResponse<ProviderSearchResponse>>();



        result.Should()
            .NotBeNull();


        result!.Items
            .Should()
            .OnlyContain(x => x.IsVerified);
    }



    [Fact]
    public async Task Search_With_Pagination_Should_Return_Page_Data()
    {
        await _factory.ExecuteDbContextAsync(async context =>
        {
            for (var i = 0; i < 3; i++)
            {
                var user = new User(
                    $"Provider{i}",
                    "Test",
                    Email.Create($"provider{i}@test.com"),
                    PhoneNumber.Create($"+96650000000{i + 4}"),
                    "test-password");


                var provider = new ProviderProfile(
                    user.Id,
                    $"Provider {i}",
                    i + 1);


                provider.Approve();


                context.Users.Add(user);

                context.ProviderProfiles.Add(provider);
            }


            await context.SaveChangesAsync();
        });



        var response = await _client.GetAsync(
            "/api/providers/search?page=1&pageSize=1");



        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);



        var result = await response.Content
            .ReadFromJsonAsync<
                PagedResponse<ProviderSearchResponse>>();



        result.Should()
            .NotBeNull();



        result!.PageNumber
            .Should()
            .Be(1);



        result.PageSize
            .Should()
            .Be(1);



        result.Items
            .Count
            .Should()
            .Be(1);



        result.TotalCount
            .Should()
            .Be(3);
    }
}