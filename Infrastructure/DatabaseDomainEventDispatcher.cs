using System.Text.Json;
using DDDExample.Domain.Events;

namespace DDDExample.Infrastructure;

public class DatabaseDomainEventDispatcher : IDomainEventDispatcher
{
    private readonly BankingDbContext _context;
    public DatabaseDomainEventDispatcher(BankingDbContext context)
    {
        _context = context;
    }

    public async Task DispatchAsync(IEnumerable<IDomainEvent> events)
    {
        foreach (var domainEvent in events)
        {
            var eventEntity = new Event
            {
                Type = domainEvent.GetType().FullName!,
                Data = JsonSerializer.Serialize(domainEvent, domainEvent.GetType()),
                OccurredOn = domainEvent.OccurredOn
            };
            await _context.Events.AddAsync(eventEntity);
        }
        await _context.SaveChangesAsync();
    }
}
