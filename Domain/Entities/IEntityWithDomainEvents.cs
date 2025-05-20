using DDDExample.Domain.Events;

namespace DDDExample.Domain.Entities;

public interface IEntityWithDomainEvents
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
