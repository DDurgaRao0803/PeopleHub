using System.Security.Cryptography;
using System.Text;
using PeopleHub.Application.Common.Interfaces.Services;

namespace PeopleHub.Infrastructure.Security;

public sealed class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string GenerateToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);

        return Convert.ToBase64String(bytes);
    }

    public string ComputeHash(string refreshToken)
    {
        var bytes = Encoding.UTF8.GetBytes(refreshToken);

        var hash = SHA256.HashData(bytes);

        return Convert.ToHexString(hash);
    }
}