using DDDExample.Models;
using DDDExample.Services;
using Microsoft.AspNetCore.Mvc;

namespace DDDExample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly AccountService _service;

    public AccountsController(AccountService service)
    {
        _service = service;
    }

    [HttpGet("{id}", Name = "GetAccountById")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var account = await _service.GetByIdAsync(id);
        return Ok(account);
    }

    [HttpPost]
    public async Task<IActionResult> Open([FromBody] OpenAccountRequest request)
    {
        var id = await _service.OpenAsync(request);
        return CreatedAtRoute("GetAccountById", new { id }, id);
    }

    [HttpPost("{id}/deposit")]
    public async Task<IActionResult> Deposit(Guid id, [FromBody] TransactionRequest request)
    {
        await _service.DepositAsync(id, request);
        return Ok();
    }

    [HttpPost("{id}/withdraw")]
    public async Task<IActionResult> Withdraw(Guid id, [FromBody] TransactionRequest request)
    {
        await _service.WithdrawAsync(id, request);
        return Ok();
    }

    [HttpPost("{id}/close")]
    public async Task<IActionResult> Close(Guid id)
    {
        await _service.CloseAsync(id);
        return Ok();
    }
}


