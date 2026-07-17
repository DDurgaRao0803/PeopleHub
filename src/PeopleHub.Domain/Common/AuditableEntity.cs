namespace PeopleHub.Domain.Common;

public abstract class AuditableEntity : Entity
{
    public DateTime CreatedOnUtc { get; protected set; }

    public Guid? CreatedBy { get; protected set; }

    public DateTime? LastModifiedOnUtc { get; protected set; }

    public Guid? LastModifiedBy { get; protected set; }
}