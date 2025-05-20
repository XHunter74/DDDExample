namespace DDDExample.Models;

public record OpenAccountRequest(string Owner, decimal InitialDeposit, string Currency);
