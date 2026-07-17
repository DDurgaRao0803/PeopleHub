using PeopleHub.Domain.Common;
using PeopleHub.Domain.Enums;
using PeopleHub.Domain.ValueObjects;
using PeopleHub.Domain.Exceptions;

namespace PeopleHub.Domain.Entities;

public sealed class User : AuditableEntity
{
    private readonly List<UserRole> _roles = [];

    private User()
{
    FirstName = null!;
    LastName = null!;
    Email = null!;
    PhoneNumber = null!;
}

    public User(
    string firstName,
    string lastName,
    Email email,
    PhoneNumber phoneNumber)
{
    ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
    ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
    ArgumentNullException.ThrowIfNull(email);
    ArgumentNullException.ThrowIfNull(phoneNumber);

    FirstName = firstName.Trim();
    LastName = lastName.Trim();
    Email = email;
    PhoneNumber = phoneNumber;

    Status = UserStatus.PendingVerification;
}

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public Email Email { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }

    public UserStatus Status { get; private set; }

    public bool IsEmailVerified { get; private set; }

    public bool IsPhoneVerified { get; private set; }

    public DateTime? LastLoginUtc { get; private set; }

    public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();

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
}