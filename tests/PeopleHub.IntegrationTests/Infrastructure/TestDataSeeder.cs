using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Domain.Entities;
using PeopleHub.Infrastructure.Persistence.Context;

namespace PeopleHub.IntegrationTests.Infrastructure;

public sealed class TestDataSeeder
{
    private readonly ApplicationDbContext _context;

    // Fixed test date:
    // Monday, 20 July 2026 at 10:00 UTC
    private static readonly DateTime TestRequestedDate = new(
        2026,
        7,
        20,
        10,
        0,
        0,
        DateTimeKind.Utc);

    public TestDataSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceCategory> CreateServiceCategoryAsync(
        string name = "Electrical",
        string? description = "Test Category",
        CancellationToken cancellationToken = default)
    {
        var category = new ServiceCategory(
            name,
            description);

        _context.ServiceCategories.Add(category);

        await _context.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<ProviderProfile> CreateProviderProfileAsync(
        Guid? userId = null,
        string bio = "Experienced Provider",
        int experienceYears = 5,
        CancellationToken cancellationToken = default)
    {
        var profile = new ProviderProfile(
            userId ?? Guid.NewGuid(),
            bio,
            experienceYears);

        _context.ProviderProfiles.Add(profile);

        await _context.SaveChangesAsync(cancellationToken);

        return profile;
    }

    public async Task<ProviderProfile> CreateVerifiedProviderAsync(
        ServiceCategory serviceCategory,
        Guid? userId = null,
        CancellationToken cancellationToken = default)
    {
        var provider = new ProviderProfile(
            userId ?? Guid.NewGuid(),
            "Experienced Test Provider",
            5);

        provider.Approve();

        provider.AddSkill(serviceCategory);

        provider.AddAvailability(
            TestRequestedDate.DayOfWeek,
            TimeOnly.FromDateTime(TestRequestedDate).AddHours(-1),
            TimeOnly.FromDateTime(TestRequestedDate).AddHours(1));

        _context.ProviderProfiles.Add(provider);

        await _context.SaveChangesAsync(cancellationToken);

        return provider;
    }

    public async Task<ServiceRequest> CreateServiceRequestAsync(
    ProviderProfile providerProfile,
    ServiceCategory serviceCategory,
    Guid? customerId = null,
    string title = "Test Service Request",
    string description = "Integration test request",
    CancellationToken cancellationToken = default)
{
    var serviceRequest = new ServiceRequest(
        customerId ?? Guid.NewGuid(),
        serviceCategory.Id,
        title,
        description,
        TestRequestedDate);

    serviceRequest.AssignProvider(providerProfile.Id);

    _context.ServiceRequests.Add(serviceRequest);

    await _context.SaveChangesAsync(cancellationToken);

    return serviceRequest;
}
}