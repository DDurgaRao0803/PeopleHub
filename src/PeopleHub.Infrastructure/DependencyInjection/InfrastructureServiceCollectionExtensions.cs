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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PeopleHub.Application.Users;
using PeopleHub.Infrastructure.Users;
using PeopleHub.Application.Providers.Profiles;
using PeopleHub.Infrastructure.Providers.Profiles;
using Microsoft.Extensions.Logging;
using PeopleHub.Application.Providers.Availability;
using PeopleHub.Application.Providers.Verification;
using PeopleHub.Application.Providers.ServiceCategories;
using PeopleHub.Application.Providers.ServiceRequests;
using PeopleHub.Infrastructure.Providers.Availability;
using PeopleHub.Infrastructure.Providers.Verification;
using PeopleHub.Infrastructure.Providers.ServiceCategories;
using PeopleHub.Infrastructure.Providers.ServiceRequests;
using PeopleHub.SmartMatch.Interfaces;
using PeopleHub.SmartMatch.Services;
using PeopleHub.Application.SmartMatch.Interfaces;
using PeopleHub.Infrastructure.Providers.SmartMatch;
using PeopleHub.Application.Providers.Reviews;
using PeopleHub.Infrastructure.Providers.Reviews;
using PeopleHub.Application.Providers.Search;
using PeopleHub.Infrastructure.Providers.Search;
using PeopleHub.Application.Providers.Services;
using PeopleHub.Infrastructure.Providers.Services;
using PeopleHub.Application.Providers.Dashboard;
using PeopleHub.Infrastructure.Providers.Dashboard;
using PeopleHub.Application.Administration;
using PeopleHub.Infrastructure.Administration;
using PeopleHub.SmartMatch.Ranking;
using PeopleHub.SmartMatch.Ranking.Interfaces;
using PeopleHub.SmartMatch.Ranking.Rules;
using PeopleHub.Application.Providers.Bidding;
using PeopleHub.Infrastructure.Providers.Bidding;
using PeopleHub.Application.Notifications;
using PeopleHub.Infrastructure.Notifications;
using PeopleHub.Application.Location;
using PeopleHub.Infrastructure.Location;


namespace PeopleHub.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration,
    Action<DbContextOptionsBuilder>? configureDbContext = null)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultConnection' was not found.");

        services.Configure<JwtOptions>(
    configuration.GetSection(JwtOptions.SectionName));

    var jwtOptions = configuration
    .GetSection(JwtOptions.SectionName)
    .Get<JwtOptions>()
    ?? throw new InvalidOperationException("JWT configuration is missing.");

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,

            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"JWT Authentication Failed: {context.Exception}");
                return Task.CompletedTask;
            },

            OnTokenValidated = context =>
            {
                Console.WriteLine("JWT Token Validated Successfully");
                return Task.CompletedTask;
            }
        };
    });
        
        services.AddDbContext<ApplicationDbContext>(options =>
{
    if (configureDbContext is not null)
    {
        // Tests completely control the provider.
        configureDbContext(options);
        return;
    }

    options.UseSqlServer(connectionString);

options.EnableSensitiveDataLogging();
options.EnableDetailedErrors();
options.LogTo(Console.WriteLine, LogLevel.Information);
});

    services.AddHttpContextAccessor();

services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProviderProfileRepository, ProviderProfileRepository>();
        services.AddScoped<IProviderVerificationRepository, ProviderVerificationRepository>();
        services.AddScoped<IProviderRepository, ProviderRepository>();
        services.AddScoped<IServiceCategoryRepository, ServiceCategoryRepository>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton<IRefreshTokenGenerator, RefreshTokenGenerator>();
        services.AddScoped<IProviderProfileService, ProviderProfileService>();
        services.AddScoped<IProviderVerificationService, ProviderVerificationService>();
        services.AddScoped<IProviderServiceService, ProviderServiceService>();
        services.AddScoped<IServiceCategoryService, ServiceCategoryService>();
        services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();
        services.AddScoped<IProviderBidRepository, ProviderBidRepository>();
        services.AddScoped<IProviderServiceRepository, ProviderServiceRepository>();
        services.AddScoped<IServiceRequestService, ServiceRequestService>();
        services.AddScoped<IProviderBidService, ProviderBidService>();
        services.AddScoped<ISmartMatchService, SmartMatchService>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<
    IProviderLocationRepository,
    ProviderLocationRepository>();

services.AddScoped<
    ILocationService,
    LocationService>();



services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<ISmartMatchEngine, SmartMatchEngine>();
        services.AddScoped<IScoreRule, ExperienceScoreRule>();
services.AddScoped<IScoreRule, RatingScoreRule>();
services.AddScoped<IScoreRule, CompletedJobsScoreRule>();
services.AddScoped<IScoreRule, VerificationScoreRule>();
services.AddScoped<IScoreRule, ResponseRateScoreRule>();

services.AddScoped<ScoreCalculator>();
        services.AddScoped<IProviderAvailabilityService, ProviderAvailabilityService>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IProviderSearchRepository, ProviderSearchRepository>();
        services.AddScoped<IProviderSearchService, ProviderSearchService>();
        services.AddScoped<IAdminDashboardService, AdminDashboardService>();
        services.AddScoped<IAdminUserService, AdminUserService>();
        services.AddScoped<IAdminProviderVerificationService, AdminProviderVerificationService>();
        services.AddScoped<IProviderDashboardService, ProviderDashboardService>();
        
        

        return services;
    }
}