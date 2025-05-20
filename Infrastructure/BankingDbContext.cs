using DDDExample.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DDDExample.Infrastructure
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options) { }

        public DbSet<BankAccount> BankAccounts => Set<BankAccount>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Owner).IsRequired();
                entity.Property(e => e.Balance).IsRequired();
                entity.Property(e => e.CurrencyCode).IsRequired();
            });
        }
    }
}
