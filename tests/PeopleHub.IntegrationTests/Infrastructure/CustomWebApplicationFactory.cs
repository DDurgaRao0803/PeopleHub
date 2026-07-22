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
using Microsoft.AspNetCore.Authorization;

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

            services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Admin");
    });
});

        // Remove the application's DbContext registrations so we can replace the
        // provider for tests. Remove any descriptors for ApplicationDbContext or
        // its DbContextOptions to avoid multiple database providers being
        // registered in the same service provider.
        var descriptorsToRemove = services.Where(d =>
            d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>) ||
            d.ServiceType == typeof(ApplicationDbContext) ||
            d.ImplementationType == typeof(ApplicationDbContext)
        ).ToList();

        foreach (var d in descriptorsToRemove)
        {
            services.Remove(d);
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

    public async Task ExecuteDbContextAsync(
    Func<ApplicationDbContext, Task> action)
{
    using var scope = Services.CreateScope();

    var context = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    await action(context);
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