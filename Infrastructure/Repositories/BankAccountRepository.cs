using DDDExample.Domain.Entities;
using DDDExample.Domain.Events;

namespace DDDExample.Infrastructure.Repositories;

public class BankAccountRepository : BaseRepository, IBankAccountRepository
{
    public BankAccountRepository(BankingDbContext context, IDomainEventDispatcher dispatcher)
        : base(context, dispatcher)
    {
    }

    public async Task<BankAccount?> GetByIdAsync(Guid id)
    {
        return await context.BankAccounts.FindAsync(id);
    }

    public async Task AddAsync(BankAccount account)
    {
        await context.BankAccounts.AddAsync(account);
    }
}
