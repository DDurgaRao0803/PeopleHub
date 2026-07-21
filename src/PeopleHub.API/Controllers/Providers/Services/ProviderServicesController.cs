using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Providers.Services;
using PeopleHub.Contracts.Providers.Services;

namespace PeopleHub.API.Controllers;

[ApiController]
[Route("api/provider-services")]
public sealed class ProviderServicesController : ControllerBase
{
    private readonly IProviderServiceService _providerServiceService;

    public ProviderServicesController(
        IProviderServiceService providerServiceService)
    {
        _providerServiceService = providerServiceService;
    }

    [HttpPost]
    public async Task<ActionResult<ProviderServiceResponse>> Create(
        [FromBody] CreateProviderServiceRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _providerServiceService.CreateAsync(
                request,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = response.Id },
                response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new
            {
                message = ex.Message
            });
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProviderServiceResponse>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var response = await _providerServiceService.GetByIdAsync(
            id,
            cancellationToken);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpGet("provider/{providerProfileId:guid}")]
    public async Task<ActionResult<IReadOnlyList<ProviderServiceResponse>>> GetProviderServices(
        Guid providerProfileId,
        CancellationToken cancellationToken)
    {
        var response = await _providerServiceService.GetByProviderProfileIdAsync(
            providerProfileId,
            cancellationToken);

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
public async Task<ActionResult<ProviderServiceResponse>> Update(
    Guid id,
    [FromBody] UpdateProviderServiceRequest request,
    CancellationToken cancellationToken)
{
    try
    {
        var response = await _providerServiceService.UpdateAsync(
            id,
            request,
            cancellationToken);

        return Ok(response);
    }
    catch (KeyNotFoundException ex)
    {
        return NotFound(new
        {
            message = ex.Message
        });
    }
}

    [HttpDelete("{id:guid}")]
public async Task<IActionResult> Delete(
    Guid id,
    CancellationToken cancellationToken)
{
    try
    {
        await _providerServiceService.DeleteAsync(
            id,
            cancellationToken);

        return NoContent();
    }
    catch (KeyNotFoundException ex)
    {
        return NotFound(new
        {
            message = ex.Message
        });
    }
}
}