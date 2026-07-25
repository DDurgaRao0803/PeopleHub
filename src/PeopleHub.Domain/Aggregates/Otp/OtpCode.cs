using PeopleHub.Domain.Enums;

namespace PeopleHub.Domain.Aggregates.Otp;

public sealed class OtpCode
{
    private OtpCode()
    {
    }

    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public string CodeHash { get; private set; } = string.Empty;

    public OtpPurpose Purpose { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }

    public DateTime ExpiresAtUtc { get; private set; }

    public DateTime? VerifiedAtUtc { get; private set; }

    public int FailedAttempts { get; private set; }

    public bool IsVerified => VerifiedAtUtc.HasValue;

    public bool IsExpired => DateTime.UtcNow > ExpiresAtUtc;

    public static OtpCode Create(
        Guid userId,
        string codeHash,
        OtpPurpose purpose,
        TimeSpan validity)
    {
        return new OtpCode
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CodeHash = codeHash,
            Purpose = purpose,
            CreatedAtUtc = DateTime.UtcNow,
            ExpiresAtUtc = DateTime.UtcNow.Add(validity),
            FailedAttempts = 0
        };
    }

    public void MarkVerified()
    {
        VerifiedAtUtc = DateTime.UtcNow;
    }

    public void IncrementFailedAttempts()
    {
        FailedAttempts++;
    }
}