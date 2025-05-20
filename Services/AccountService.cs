using DDDExample.Domain.Entities;
using DDDExample.Domain.ValueObjects;
using DDDExample.Exceptions;
using DDDExample.Infrastructure.Repositories;
using DDDExample.Models;

namespace DDDExample.Services;

public class AccountService(IBankAccountRepository repository, ILogger<AccountService> logger)
{
    public async Task<BankAccount> GetByIdAsync(Guid id)
    {
        logger.LogInformation("Getting account by id: {AccountId}", id);
        var account = await repository.GetByIdAsync(id);
        if (account == null)
        {
            logger.LogWarning("Account not found: {AccountId}", id);
            throw new NotFoundException($"Account {id} not found");
        }
        return account;
    }

    public async Task<Guid> OpenAsync(OpenAccountRequest request)
    {
        logger.LogInformation("Opening new account for owner: {Owner}, currency: {Currency}, initial deposit: {Amount}", request.Owner, request.Currency, request.InitialDeposit);
        var account = new BankAccount(AccountId.NewId(), request.Owner, new Money(request.InitialDeposit, new Currency(request.Currency)));
        await repository.AddAsync(account);
        await repository.SaveChangesAsync();
        return account.Id;
    }

    public async Task DepositAsync(Guid id, TransactionRequest request)
    {
        logger.LogInformation("Depositing {Amount} {Currency} to account {AccountId}", request.Amount, request.Currency, id);
        var account = await repository.GetByIdAsync(id);
        if (account == null)
        {
            logger.LogWarning("Account not found: {AccountId}", id);
            throw new NotFoundException($"Account {id} not found");
        }
        try
        {
            account.Deposit(new Money(request.Amount, new Currency(request.Currency)));
        }
        catch (InvalidOperationException ex)
        {
            logger.LogError(ex, "Deposit failed for account {AccountId}", id);
            throw new BadRequestException(ex.Message);
        }
        await repository.SaveChangesAsync();
    }

    public async Task WithdrawAsync(Guid id, TransactionRequest request)
    {
        logger.LogInformation("Withdrawing {Amount} {Currency} from account {AccountId}", request.Amount, request.Currency, id);
        var account = await repository.GetByIdAsync(id);
        if (account == null)
        {
            logger.LogWarning("Account not found: {AccountId}", id);
            throw new NotFoundException($"Account {id} not found");
        }
        try
        {
            account.Withdraw(new Money(request.Amount, new Currency(request.Currency)));
        }
        catch (InvalidOperationException ex)
        {
            logger.LogError(ex, "Withdraw failed for account {AccountId}", id);
            throw new BadRequestException(ex.Message);
        }
        await repository.SaveChangesAsync();
    }

    public async Task CloseAsync(Guid id)
    {
        logger.LogInformation("Closing account {AccountId}", id);
        var account = await repository.GetByIdAsync(id);
        if (account == null)
        {
            logger.LogWarning("Account not found: {AccountId}", id);
            throw new NotFoundException($"Account {id} not found");
        }
        try
        {
            account.Close();
        }
        catch (InvalidOperationException ex)
        {
            logger.LogError(ex, "Close failed for account {AccountId}", id);
            throw new BadRequestException(ex.Message);
        }
        await repository.SaveChangesAsync();
    }
}
