using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Common.Interfaces.Services;
using PeopleHub.Infrastructure.Persistence;
using PeopleHub.Infrastructure.Persistence.Context;
using PeopleHub.Infrastructure.Persistence.Repositories;
using PeopleHub.Infrastructure.Security;
using PeopleHub.Contracts.Authentication;
using PeopleHub.Application.Authentication;
using PeopleHub.Infrastructure.Authentication;

namespace PeopleHub.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultConnection' was not found.");

        services.Configure<JwtOptions>(
    configuration.GetSection(JwtOptions.SectionName));
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProviderRepository, ProviderRepository>();
        services.AddScoped<IServiceCategoryRepository, ServiceCategoryRepository>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddSingleton<IRefreshTokenGenerator, RefreshTokenGenerator>();

        return services;
    }
}