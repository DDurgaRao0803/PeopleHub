using Microsoft.EntityFrameworkCore;
using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Domain.Aggregates.User;
using PeopleHub.Application.Common.Interfaces.Persistence;


namespace PeopleHub.Infrastructure.Persistence.Context;

public sealed class ApplicationDbContext
    : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<UserRole> UserRoles => Set<UserRole>();

    public DbSet<ProviderProfile> ProviderProfiles => Set<ProviderProfile>();

    public DbSet<ProviderSkill> ProviderSkills => Set<ProviderSkill>();

    public DbSet<ProviderVerification> ProviderVerifications => Set<ProviderVerification>();

    public DbSet<ServiceCategory> ServiceCategories => Set<ServiceCategory>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<ProviderAvailability> ProviderAvailabilities => Set<ProviderAvailability>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}