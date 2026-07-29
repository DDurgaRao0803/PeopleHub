using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Providers.Availability;
using PeopleHub.Contracts.Providers.Availability;

namespace PeopleHub.API.Controllers.Providers.Availability;

[ApiController]
[Route("api/providers/profiles/{providerProfileId:guid}/availability")]
public sealed class ProviderAvailabilityController : ControllerBase
{
    private readonly IProviderAvailabilityService _providerAvailabilityService;

    public ProviderAvailabilityController(
        IProviderAvailabilityService providerAvailabilityService)
    {
        _providerAvailabilityService = providerAvailabilityService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProviderAvailabilityResponse>>> GetAvailability(
        Guid providerProfileId,
        CancellationToken cancellationToken)
    {
        var response = await _providerAvailabilityService.GetAvailabilityAsync(
            providerProfileId,
            cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ProviderAvailabilityResponse>> CreateAvailability(
        Guid providerProfileId,
        [FromBody] CreateProviderAvailabilityRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response =
                await _providerAvailabilityService.AddAvailabilityAsync(
                    providerProfileId,
                    request,
                    cancellationToken);

            return CreatedAtAction(
                nameof(GetAvailability),
                new { providerProfileId },
                response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new
            {
                message = ex.Message
            });
        }
    }

    [HttpPut("{availabilityId:guid}")]
    public async Task<ActionResult<ProviderAvailabilityResponse>> UpdateAvailability(
        Guid providerProfileId,
        Guid availabilityId,
        [FromBody] UpdateProviderAvailabilityRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response =
                await _providerAvailabilityService.UpdateAvailabilityAsync(
                    providerProfileId,
                    availabilityId,
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

    [HttpDelete("{availabilityId:guid}")]
    public async Task<IActionResult> DeleteAvailability(
        Guid providerProfileId,
        Guid availabilityId,
        CancellationToken cancellationToken)
    {
        try
        {
            await _providerAvailabilityService.DeleteAvailabilityAsync(
                providerProfileId,
                availabilityId,
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