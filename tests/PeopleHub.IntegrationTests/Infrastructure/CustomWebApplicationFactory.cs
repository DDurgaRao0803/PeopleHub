using System.Data.Common;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Infrastructure.Payments;
using PeopleHub.Infrastructure.Persistence.Context;


namespace PeopleHub.IntegrationTests.Infrastructure;

public sealed class CustomWebApplicationFactory 
    : WebApplicationFactory<Program>
{
    private DbConnection? _connection;


    protected override void ConfigureWebHost(
        IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");


        builder.ConfigureServices(services =>
        {
            // Replace authentication with test authentication

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    TestAuthenticationHandler.Scheme;

                options.DefaultChallengeScheme =
                    TestAuthenticationHandler.Scheme;
            })
            .AddScheme<
                AuthenticationSchemeOptions,
                TestAuthenticationHandler>(
                    TestAuthenticationHandler.Scheme,
                    _ => { });



            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "AdminOnly",
                    policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireRole("Admin");
                    });
            });



            // Remove SQL Server DbContext

            var descriptorsToRemove =
                services
                    .Where(d =>
                        d.ServiceType ==
                            typeof(DbContextOptions<ApplicationDbContext>)
                        ||
                        d.ServiceType ==
                            typeof(ApplicationDbContext))
                    .ToList();



            foreach (var descriptor in descriptorsToRemove)
            {
                services.Remove(descriptor);
            }



            // SQLite in-memory database

            _connection =
                new SqliteConnection(
                    "DataSource=:memory:");

            _connection.Open();



            services.AddSingleton<DbConnection>(
                _connection);



            services.AddDbContext<ApplicationDbContext>(
                (sp, options) =>
                {
                    var connection =
                        sp.GetRequiredService<DbConnection>();

                    options.UseSqlite(connection);
                });



            // Payment repositories required for tests

            services.AddScoped<
                IWalletRepository,
                WalletRepository>();


            services.AddScoped<
                IWalletTransactionRepository,
                WalletTransactionRepository>();
        });
    }



    protected override IHost CreateHost(
        IHostBuilder builder)
    {
        var host =
            base.CreateHost(builder);



        using var scope =
            host.Services.CreateScope();



        var context =
            scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();


        context.Database.EnsureDeleted();

        context.Database.EnsureCreated();



        return host;
    }



    public void ResetDatabase()
    {
        using var scope =
            Services.CreateScope();



        var context =
            scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();


        context.Database.EnsureDeleted();

        context.Database.EnsureCreated();
    }



    public async Task ExecuteDbContextAsync(
        Func<ApplicationDbContext, Task> action)
    {
        using var scope =
            Services.CreateScope();



        var context =
            scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();


        await action(context);
    }



    protected override void Dispose(
        bool disposing)
    {
        if (disposing)
        {
            _connection?.Dispose();
        }


        base.Dispose(disposing);
    }
}