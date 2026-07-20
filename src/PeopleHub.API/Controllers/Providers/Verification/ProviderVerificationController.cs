using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Providers.Verification;
using PeopleHub.Contracts.Providers.Verification;

namespace PeopleHub.Api.Controllers;

[ApiController]
[Route("api/provider-verifications")]
[Authorize]
public sealed class ProviderVerificationController : ControllerBase
{
    private readonly IProviderVerificationService _service;

    public ProviderVerificationController(
        IProviderVerificationService service)
    {
        _service = service;
    }

    [HttpPost("{providerProfileId:guid}")]
    public async Task<ActionResult<ProviderVerificationResponse>> Create(
    Guid providerProfileId,
    [FromBody] CreateProviderVerificationRequest request,
    CancellationToken cancellationToken)
    {
        try
        {
            var response = await _service.CreateAsync(
                providerProfileId,
                request,
                cancellationToken);

            return CreatedAtAction(
                nameof(Get),
                new { providerProfileId },
                response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{providerProfileId:guid}")]
    public async Task<ActionResult<ProviderVerificationResponse>> Get(
        Guid providerProfileId,
        CancellationToken cancellationToken)
    {
        var response = await _service.GetByProviderProfileIdAsync(
            providerProfileId,
            cancellationToken);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpDelete("{providerProfileId:guid}")]
    public async Task<IActionResult> Delete(
        Guid providerProfileId,
        CancellationToken cancellationToken)
    {
        try
        {
            await _service.DeleteAsync(
                providerProfileId,
                cancellationToken);

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}