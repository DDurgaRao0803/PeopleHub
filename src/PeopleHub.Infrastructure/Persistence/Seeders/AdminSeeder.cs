using Microsoft.EntityFrameworkCore;
using PeopleHub.Domain.Aggregates.User;
using PeopleHub.Domain.Enums;
using PeopleHub.Domain.ValueObjects;
using PeopleHub.Infrastructure.Persistence.Context;
using PeopleHub.Infrastructure.Security;

namespace PeopleHub.Infrastructure.Persistence.Seeders;

public static class AdminSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        const string adminEmail = "admin@peoplehub.com";

        var exists = await context.Users
            .Include(x => x.Roles)
            .AnyAsync(x => x.Email.Value == adminEmail);

        if (exists)
        {
            return;
        }

        var passwordHash = new PasswordHasher()
    .HashPassword("ChangeMe123!");

var admin = new User(
    firstName: "System",
    lastName: "Administrator",
    email: Email.Create(adminEmail),
    phoneNumber: PhoneNumber.Create("+966500000000"),
    passwordHash: passwordHash);

        admin.VerifyEmail();
        admin.VerifyPhone();
        admin.AddRole(UserRoleType.Admin);
        admin.Activate();

        await context.Users.AddAsync(admin);

        await context.SaveChangesAsync();
    }
}