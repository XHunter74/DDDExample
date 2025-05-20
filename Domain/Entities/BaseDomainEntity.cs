using DDDExample.Domain.Events;

namespace DDDExample.Domain.Entities;

public abstract class BaseDomainEntity : IEntityWithDomainEvents
{
    protected readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();
}
