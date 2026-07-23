using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Microsoft.Extensions.DependencyInjection;

using PeopleHub.Application.Payments.Models;
using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Infrastructure.Persistence.Context;
using PeopleHub.IntegrationTests.Infrastructure;


namespace PeopleHub.IntegrationTests.Payments;

public sealed class WalletControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;


    private static readonly Guid ProviderProfileId =
        Guid.Parse(
            "22222222-2222-2222-2222-222222222222");



    public WalletControllerTests(
        CustomWebApplicationFactory factory)
    {
        factory.ResetDatabase();


        using var scope =
            factory.Services.CreateScope();


        var db =
            scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();


        var provider =
            new ProviderProfile(
                ProviderProfileId,
                "Integration test provider",
                5);



        db.ProviderProfiles.Add(provider);


        db.SaveChanges();


        _client =
            factory.CreateClient();
    }



    [Fact]
public async Task GetWallet_Should_ReturnWallet()
{
    await _client.PostAsync(
        $"/api/wallet/{ProviderProfileId}/credit?amount=100&description=Initial%20credit",
        null);



    var response =
        await _client.GetAsync(
            $"/api/wallet/{ProviderProfileId}");



    await PrintErrorIfFailed(response);



    response.StatusCode
        .Should()
        .Be(HttpStatusCode.OK);



    var result =
        await response.Content
            .ReadFromJsonAsync<WalletResponse>();



    result.Should()
        .NotBeNull();



    result!.ProviderProfileId
        .Should()
        .Be(ProviderProfileId);



    result.Balance
        .Should()
        .Be(100);
}



    [Fact]
    public async Task CreditWallet_Should_IncreaseBalance()
    {
        var response =
            await _client.PostAsync(
                $"/api/wallet/{ProviderProfileId}/credit?amount=100&description=Service%20Completed",
                null);



        await PrintErrorIfFailed(response);



        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);



        var wallet =
            await response.Content
                .ReadFromJsonAsync<WalletResponse>();



        wallet.Should()
            .NotBeNull();


        wallet!.Balance
            .Should()
            .Be(100);
    }



    [Fact]
    public async Task DebitWallet_Should_DecreaseBalance()
    {
        var creditResponse =
            await _client.PostAsync(
                $"/api/wallet/{ProviderProfileId}/credit?amount=200&description=Service%20Completed",
                null);



        await PrintErrorIfFailed(creditResponse);



        creditResponse.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);



        var response =
            await _client.PostAsync(
                $"/api/wallet/{ProviderProfileId}/debit?amount=50&description=Withdrawal",
                null);



        await PrintErrorIfFailed(response);



        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);



        var wallet =
            await response.Content
                .ReadFromJsonAsync<WalletResponse>();



        wallet.Should()
            .NotBeNull();


        wallet!.Balance
            .Should()
            .Be(150);
    }



    private static async Task PrintErrorIfFailed(
        HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }


        var error =
            await response.Content.ReadAsStringAsync();


        Console.WriteLine(
            $"API Error ({(int)response.StatusCode}):");


        Console.WriteLine(error);
    }
}