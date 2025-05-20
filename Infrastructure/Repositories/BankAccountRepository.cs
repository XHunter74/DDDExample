using DDDExample.Domain.Entities;

namespace DDDExample.Infrastructure.Repositories;

public class BankAccountRepository : IRepository<BankAccount>
{
    private readonly BankingDbContext _context;

    public BankAccountRepository(BankingDbContext context)
    {
        _context = context;
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
    }
}
