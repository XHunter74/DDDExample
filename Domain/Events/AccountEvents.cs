namespace DDDExample.Domain.Events;

public sealed class AccountOpenedEvent : DomainEvent
{
    public Guid AccountId { get; }
    public string Owner { get; }
    public decimal InitialDeposit { get; }
    public string Currency { get; }

    public AccountOpenedEvent(Guid accountId, string owner, decimal initialDeposit, string currency)
    {
        AccountId = accountId;
        Owner = owner;
        InitialDeposit = initialDeposit;
        Currency = currency;
    }
}

public sealed class MoneyDepositedEvent : DomainEvent
{
    public Guid AccountId { get; }
    public decimal Amount { get; }
    public string Currency { get; }

    public MoneyDepositedEvent(Guid accountId, decimal amount, string currency)
    {
        AccountId = accountId;
        Amount = amount;
        Currency = currency;
    }
}

public sealed class MoneyWithdrawnEvent : DomainEvent
{
    public Guid AccountId { get; }
    public decimal Amount { get; }
    public string Currency { get; }

    public MoneyWithdrawnEvent(Guid accountId, decimal amount, string currency)
    {
        AccountId = accountId;
        Amount = amount;
        Currency = currency;
    }
}

public sealed class AccountClosedEvent : DomainEvent
{
    public Guid AccountId { get; }

    public AccountClosedEvent(Guid accountId)
    {
        AccountId = accountId;
    }
}
