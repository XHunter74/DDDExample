namespace DDDExample.Domain.ValueObjects;

public sealed record AccountId(Guid Value)
{
    public static AccountId NewId() => new(Guid.NewGuid());
}
