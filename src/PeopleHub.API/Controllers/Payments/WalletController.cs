using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Payments;

namespace PeopleHub.API.Controllers.Payments;

[ApiController]
[Route("api/wallet")]
[Authorize]
public sealed class WalletController : ControllerBase
{
    private readonly IWalletService _walletService;


    public WalletController(
        IWalletService walletService)
    {
        _walletService = walletService;
    }



    [HttpGet("{providerProfileId}")]
    public async Task<IActionResult> GetWallet(
        Guid providerProfileId,
        CancellationToken cancellationToken)
    {
        var result =
            await _walletService.GetWalletAsync(
                providerProfileId,
                cancellationToken);


        return Ok(result);
    }



    [HttpPost("{providerProfileId}/credit")]
    public async Task<IActionResult> Credit(
        Guid providerProfileId,
        decimal amount,
        string description,
        CancellationToken cancellationToken)
    {
        var result =
            await _walletService.CreditAsync(
                providerProfileId,
                amount,
                description,
                cancellationToken);


        return Ok(result);
    }



    [HttpPost("{providerProfileId}/debit")]
    public async Task<IActionResult> Debit(
        Guid providerProfileId,
        decimal amount,
        string description,
        CancellationToken cancellationToken)
    {
        var result =
            await _walletService.DebitAsync(
                providerProfileId,
                amount,
                description,
                cancellationToken);


        return Ok(result);
    }
}