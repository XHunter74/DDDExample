namespace DDDExample.Domain.ValueObjects;

public sealed record Currency(string Code)
{
    public static Currency USD => new("USD");
    public static Currency EUR => new("EUR");
    // Add more currencies as needed
}
