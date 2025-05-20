using DDDExample.Domain.ValueObjects;
using System;

namespace DDDExample.Domain.Entities
{
    public class BankAccount
    {
        public Guid Id { get; private set; }
        public string Owner { get; private set; }
        public bool IsClosed { get; private set; }
        public decimal Balance { get; private set; }
        public string CurrencyCode { get; private set; }

        private BankAccount() { }

        public BankAccount(AccountId id, string owner, Money initialDeposit)
        {
            Id = id.Value;
            Owner = owner;
            IsClosed = false;
            Balance = initialDeposit.Amount;
            CurrencyCode = initialDeposit.Currency.Code;
        }

        public void Deposit(Money amount)
        {
            if (IsClosed) throw new InvalidOperationException("Account is closed.");
            if (amount.Currency.Code != CurrencyCode) throw new InvalidOperationException("Currency mismatch.");
            Balance += amount.Amount;
        }

        public void Withdraw(Money amount)
        {
            if (IsClosed) throw new InvalidOperationException("Account is closed.");
            if (amount.Currency.Code != CurrencyCode) throw new InvalidOperationException("Currency mismatch.");
            if (Balance < amount.Amount) throw new InvalidOperationException("Insufficient funds.");
            Balance -= amount.Amount;
        }

        public void Close()
        {
            if (IsClosed) throw new InvalidOperationException("Account already closed.");
            IsClosed = true;
        }
    }
}
