using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PeopleHub.Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.TestHost;

namespace PeopleHub.IntegrationTests.Infrastructure;

public sealed class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureTestServices(services =>
{
    services.RemoveAll<ApplicationDbContext>();
    services.RemoveAll<DbContextOptions<ApplicationDbContext>>();

    services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseInMemoryDatabase("IntegrationTests");
    });
});
    }
}