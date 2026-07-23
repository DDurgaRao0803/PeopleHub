using Microsoft.EntityFrameworkCore;

using PeopleHub.Application.Common.Interfaces.Persistence;

using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Domain.Aggregates.User;
using PeopleHub.Domain.Aggregates.Review;
using PeopleHub.Domain.Aggregates.Bidding;
using PeopleHub.Domain.Aggregates.Notifications;
using PeopleHub.Domain.Aggregates.Location;
using PeopleHub.Domain.Aggregates.Payments;

using PeopleHub.Domain.Entities;


namespace PeopleHub.Infrastructure.Persistence.Context;

public sealed class ApplicationDbContext
    : DbContext, IUnitOfWork
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }



    public DbSet<User> Users =>
        Set<User>();


    public DbSet<UserRole> UserRoles =>
        Set<UserRole>();


    public DbSet<ProviderProfile> ProviderProfiles =>
        Set<ProviderProfile>();


    public DbSet<ProviderSkill> ProviderSkills =>
        Set<ProviderSkill>();


    public DbSet<ProviderVerification> ProviderVerifications =>
        Set<ProviderVerification>();


    public DbSet<ServiceCategory> ServiceCategories =>
        Set<ServiceCategory>();


    public DbSet<RefreshToken> RefreshTokens =>
        Set<RefreshToken>();


    public DbSet<ProviderAvailability> ProviderAvailabilities =>
        Set<ProviderAvailability>();


    public DbSet<ServiceRequest> ServiceRequests =>
        Set<ServiceRequest>();


    public DbSet<ProviderBid> ProviderBids =>
        Set<ProviderBid>();


    public DbSet<Review> Reviews =>
        Set<Review>();


    public DbSet<Notification> Notifications =>
        Set<Notification>();


    public DbSet<ProviderLocation> ProviderLocations =>
        Set<ProviderLocation>();


    public DbSet<ProviderService> ProviderServices =>
        Set<ProviderService>();


    // Payments

    public DbSet<PaymentTransaction> PaymentTransactions =>
        Set<PaymentTransaction>();


    public DbSet<Wallet> Wallets =>
        Set<Wallet>();


    public DbSet<WalletTransaction> WalletTransactions =>
        Set<WalletTransaction>();



    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);
    }
}