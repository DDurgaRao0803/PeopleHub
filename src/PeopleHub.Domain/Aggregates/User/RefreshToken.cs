using PeopleHub.Domain.Common;

namespace PeopleHub.Domain.Aggregates.User;

public sealed class RefreshToken : Entity
{
    private RefreshToken()
    {
    }

    private RefreshToken(
        Guid userId,
        string tokenHash,
        DateTime expiresAtUtc)
    {
        UserId = userId;
        TokenHash = tokenHash;
        CreatedAtUtc = DateTime.UtcNow;
        ExpiresAtUtc = expiresAtUtc;
    }

    public Guid UserId { get; private set; }

    public string TokenHash { get; private set; } = string.Empty;

    public DateTime CreatedAtUtc { get; private set; }

    public DateTime ExpiresAtUtc { get; private set; }

    public DateTime? RevokedAtUtc { get; private set; }

    public bool IsExpired => DateTime.UtcNow >= ExpiresAtUtc;

    public bool IsRevoked => RevokedAtUtc.HasValue;

    public static RefreshToken Create(
        Guid userId,
        string tokenHash,
        DateTime expiresAtUtc)
    {
        return new RefreshToken(
            userId,
            tokenHash,
            expiresAtUtc);
    }

    public void Revoke()
    {
        RevokedAtUtc = DateTime.UtcNow;
    }
}