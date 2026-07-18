using Microsoft.AspNetCore.Identity;
using PeopleHub.Application.Common.Interfaces.Services;

namespace PeopleHub.Infrastructure.Security;

public sealed class PasswordHasher : IPasswordHasher
{
    private readonly Microsoft.AspNetCore.Identity.PasswordHasher<object> _passwordHasher;

    public PasswordHasher()
    {
        _passwordHasher = new Microsoft.AspNetCore.Identity.PasswordHasher<object>();
    }

    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(
            new object(),
            password);
    }

    public bool VerifyPassword(
        string password,
        string passwordHash)
    {
        var result = _passwordHasher.VerifyHashedPassword(
            new object(),
            passwordHash,
            password);

        return result != PasswordVerificationResult.Failed;
    }
}