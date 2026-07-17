using PeopleHub.Domain.Interfaces;

namespace PeopleHub.Domain.Common;

public abstract class Entity : IEntity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; protected set; }
}