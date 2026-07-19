using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using PeopleHub.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.TestHost;

namespace PeopleHub.IntegrationTests.Infrastructure;

public sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private DbConnection? _connection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = TestAuthenticationHandler.Scheme;
    options.DefaultChallengeScheme = TestAuthenticationHandler.Scheme;
})
.AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
    TestAuthenticationHandler.Scheme,
    _ => { });
            services.RemoveAll<ApplicationDbContext>();
            services.RemoveAll<DbContextOptions<ApplicationDbContext>>();
            services.RemoveAll<DbContextOptions>();
            services.RemoveAll<DbConnection>();
            services.RemoveAll<IConfigureOptions<DbContextOptions<ApplicationDbContext>>>();
            services.RemoveAll<IOptionsChangeTokenSource<DbContextOptions<ApplicationDbContext>>>();
            services.RemoveAll<IDbContextOptionsConfiguration<ApplicationDbContext>>();

            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            services.AddSingleton<DbConnection>(_connection);

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                var connection = sp.GetRequiredService<DbConnection>();
                options.UseSqlite(connection);
            });

            using var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();
        });
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
        base.Dispose(disposing);

        if (disposing)
        {
            _connection?.Dispose();
        }
    }
}