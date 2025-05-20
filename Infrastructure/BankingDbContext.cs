using DDDExample.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DDDExample.Domain.Events;

namespace DDDExample.Infrastructure;

public class BankingDbContext : DbContext
{
    public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options) { }

    public DbSet<BankAccount> BankAccounts => Set<BankAccount>();
    public DbSet<Event> Events => Set<Event>();

    public async Task DispatchDomainEventsAsync(IDomainEventDispatcher dispatcher)
    {
        var domainEntities = ChangeTracker.Entries()
            .Where(e => e.Entity is not null && e.Entity is BaseDomainEntity)
            .Select(e => e.Entity as BaseDomainEntity)
            .Where(e => e != null && e.DomainEvents.Any())
            .ToList();

        foreach (var entity in domainEntities)
        {
            await dispatcher.DispatchAsync(entity!.DomainEvents);
            entity.ClearDomainEvents();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankAccount>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Owner).IsRequired();
            entity.Property(e => e.Balance).IsRequired();
            entity.Property(e => e.CurrencyCode).IsRequired();
        });
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.Data).IsRequired();
            entity.Property(e => e.OccurredOn).IsRequired();
            entity.ToTable("Events");
        });
    }
}

