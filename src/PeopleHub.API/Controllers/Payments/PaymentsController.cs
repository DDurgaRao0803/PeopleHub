using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Payments;

namespace PeopleHub.API.Controllers.Payments;

[ApiController]
[Route("api/payments")]
[Authorize]
public sealed class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;


    public PaymentsController(
        IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }



    [HttpPost]
    public async Task<IActionResult> Create(
        Guid serviceRequestId,
        Guid customerId,
        decimal amount,
        CancellationToken cancellationToken)
    {
        var result =
            await _paymentService.CreatePaymentAsync(
                serviceRequestId,
                customerId,
                amount,
                cancellationToken);


        return Ok(result);
    }



    [HttpPost("{paymentId}/complete")]
    public async Task<IActionResult> Complete(
        Guid paymentId,
        CancellationToken cancellationToken)
    {
        var result =
            await _paymentService.CompletePaymentAsync(
                paymentId,
                cancellationToken);


        return Ok(result);
    }



    [HttpPost("{paymentId}/fail")]
    public async Task<IActionResult> Fail(
        Guid paymentId,
        CancellationToken cancellationToken)
    {
        var result =
            await _paymentService.FailPaymentAsync(
                paymentId,
                cancellationToken);


        return Ok(result);
    }



    [HttpPost("{paymentId}/refund")]
    public async Task<IActionResult> Refund(
        Guid paymentId,
        CancellationToken cancellationToken)
    {
        var result =
            await _paymentService.RefundPaymentAsync(
                paymentId,
                cancellationToken);


        return Ok(result);
    }
}