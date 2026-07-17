using PeopleHub.Domain.Common;
using PeopleHub.Domain.Enums;

namespace PeopleHub.Domain.Aggregates.User;

public sealed class UserRole : Entity
{
    private UserRole()
    {
    }

    public UserRole(Guid userId, UserRoleType role)
    {
        UserId = userId;
        Role = role;
        AssignedOnUtc = DateTime.UtcNow;
    }

    public Guid UserId { get; private set; }

    public UserRoleType Role { get; private set; }

    public DateTime AssignedOnUtc { get; private set; }
}