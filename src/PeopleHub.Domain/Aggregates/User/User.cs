using PeopleHub.Domain.Common;
using PeopleHub.Domain.Enums;
using PeopleHub.Domain.Exceptions;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Domain.Aggregates.User;

public sealed class User : AuditableEntity
{
    private readonly List<UserRole> _roles = [];
    private readonly List<RefreshToken> _refreshTokens = [];

    private User()
    {
        FirstName = null!;
        LastName = null!;   
        PasswordHash = null!;
        Email = null!;
        PhoneNumber = null!;
    }

    public User(
        string firstName,
        string lastName,
        Email email,
        PhoneNumber phoneNumber,
        string passwordHash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(phoneNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash);

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email;
        PhoneNumber = phoneNumber;
        PasswordHash = passwordHash;

        Status = UserStatus.PendingVerification;
    }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string PasswordHash { get; private set; }

    public Email Email { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }

    public Role Role { get; private set; } = Role.User;

    public UserStatus Status { get; private set; }

    public bool IsEmailVerified { get; private set; }

    public bool IsPhoneVerified { get; private set; }

    public DateTime? LastLoginUtc { get; private set; }

    public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();

    public IReadOnlyCollection<RefreshToken> RefreshTokens =>
        _refreshTokens.AsReadOnly();

    public void Activate()
    {
        if (!IsPhoneVerified)
        {
            throw new DomainException(
                "A user cannot be activated until the phone number is verified.");
        }

        Status = UserStatus.Active;
    }

    public void Suspend()
    {
        Status = UserStatus.Suspended;
    }

    public void VerifyEmail()
    {
        IsEmailVerified = true;
    }

    public void VerifyPhone()
    {
        if (IsPhoneVerified)
        {
            return;
        }

        IsPhoneVerified = true;
    }

    public void Deactivate()
    {
        Status = UserStatus.Inactive;
    }

    public void MarkAsDeleted()
    {
        Status = UserStatus.Deleted;
    }

    public void ChangeFirstName(string firstName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);

        FirstName = firstName.Trim();
    }

    public void ChangeLastName(string lastName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);

        LastName = lastName.Trim();
    }

    public void ChangePassword(string passwordHash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash);

        PasswordHash = passwordHash;
    }

    public void UpdateLastLogin()
    {
        LastLoginUtc = DateTime.UtcNow;
    }

    public void AddRole(UserRoleType role)
    {
        if (_roles.Any(r => r.Role == role))
        {
            return;
        }

        _roles.Add(new UserRole(Id, role));
    }

    public void RemoveRole(UserRoleType role)
    {
        var existingRole = _roles.FirstOrDefault(r => r.Role == role);

        if (existingRole is null)
        {
            return;
        }

        _roles.Remove(existingRole);
    }

    public void AddRefreshToken(RefreshToken refreshToken)
    {
        ArgumentNullException.ThrowIfNull(refreshToken);

        _refreshTokens.Add(refreshToken);
    }

    public void RemoveRefreshToken(RefreshToken refreshToken)
    {
        ArgumentNullException.ThrowIfNull(refreshToken);

        _refreshTokens.Remove(refreshToken);
    }

    public void Update(
    string firstName,
    string lastName)
{
    FirstName = firstName;
    LastName = lastName;
}

    public RefreshToken? GetRefreshToken(string tokenHash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(tokenHash);

        return _refreshTokens.FirstOrDefault(
            x => x.TokenHash == tokenHash);
    }

    public void RevokeRefreshToken(string tokenHash)
    {
        var refreshToken = GetRefreshToken(tokenHash);

        if (refreshToken is null)
        {
            return;
        }

        refreshToken.Revoke();
    }

    public void RemoveExpiredRefreshTokens()
    {
        _refreshTokens.RemoveAll(
            x => x.IsExpired || x.IsRevoked);
    }
}