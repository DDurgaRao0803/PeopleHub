using System.Data.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using PeopleHub.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.TestHost;
using System.Linq;

namespace PeopleHub.IntegrationTests.Infrastructure;

public sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private DbConnection? _connection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
{
    builder.UseEnvironment("Testing");

    builder.ConfigureServices(services =>
    {
        // Replace authentication with test authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = TestAuthenticationHandler.Scheme;
            options.DefaultChallengeScheme = TestAuthenticationHandler.Scheme;
        })
        .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
            TestAuthenticationHandler.Scheme,
            _ => { });

        // Remove the application's DbContext registration
        var descriptor = services.SingleOrDefault(
            d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

        if (descriptor is not null)
        {
            services.Remove(descriptor);
        }

        // Configure SQLite in-memory
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        services.AddSingleton<DbConnection>(_connection);

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var connection = sp.GetRequiredService<DbConnection>();
            options.UseSqlite(connection);
        });
    });
}

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var host = base.CreateHost(builder);

        using (var scope = host.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        return host;
    }

    public void ResetDatabase()
    {
        using var scope = Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _connection?.Dispose();
        }

        base.Dispose(disposing);
    }
}