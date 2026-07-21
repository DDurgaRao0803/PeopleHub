using Microsoft.EntityFrameworkCore;
using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Infrastructure.Persistence.Context;

namespace PeopleHub.Infrastructure.Persistence.Seeders;

public static class ServiceCategorySeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.ServiceCategories.AnyAsync())
        {
            return;
        }

        var categories = new[]
        {
            new ServiceCategory("Plumbing", "Plumbing installation and repairs"),
            new ServiceCategory("Electrical", "Electrical installation and maintenance"),
            new ServiceCategory("Cleaning", "Home and office cleaning"),
            new ServiceCategory("Painting", "Interior and exterior painting"),
            new ServiceCategory("Carpentry", "Furniture and wood work"),
            new ServiceCategory("AC Repair", "Air conditioner installation and repair"),
            new ServiceCategory("Appliance Repair", "Home appliance repair"),
            new ServiceCategory("Pest Control", "Residential and commercial pest control"),
            new ServiceCategory("Gardening", "Garden maintenance and landscaping"),
            new ServiceCategory("Moving", "Packing and moving services"),
            new ServiceCategory("Home Maintenance", "General maintenance services")
        };

        await context.ServiceCategories.AddRangeAsync(categories);
        await context.SaveChangesAsync();
    }
}