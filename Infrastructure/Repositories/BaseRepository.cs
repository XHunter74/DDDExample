using DDDExample.Domain.Events;

namespace DDDExample.Infrastructure.Repositories;

public abstract class BaseRepository
{
    protected readonly BankingDbContext context;
    private readonly IDomainEventDispatcher _dispatcher;

    public BaseRepository(BankingDbContext context, IDomainEventDispatcher dispatcher)
    {

        this.context = context;
        this._dispatcher = dispatcher;
    }
    public async Task SaveChangesAsync()
    {
        await context.DispatchDomainEventsAsync(_dispatcher);
        await context.SaveChangesAsync();
    }
}
