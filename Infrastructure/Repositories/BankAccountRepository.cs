using DDDExample.Domain.Entities;
using DDDExample.Domain.Events;

namespace DDDExample.Infrastructure.Repositories;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly BankingDbContext _context;
    private readonly IDomainEventDispatcher _dispatcher;

    public BankAccountRepository(BankingDbContext context, IDomainEventDispatcher dispatcher)
    {
        _context = context;
        _dispatcher = dispatcher;
    }

    public async Task<BankAccount?> GetByIdAsync(Guid id)
    {
        return await _context.BankAccounts.FindAsync(id);
    }

    public async Task AddAsync(BankAccount account)
    {
        await _context.BankAccounts.AddAsync(account);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
        await _context.DispatchDomainEventsAsync(_dispatcher);
    }
}
