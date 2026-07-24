using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Providers.Profiles;
using PeopleHub.Contracts.Providers.Profiles;
using PeopleHub.Contracts.Providers.Availability;
using PeopleHub.Application.Providers.Availability;

namespace PeopleHub.API.Controllers;

[ApiController]
[Route("api/provider-profiles")]
[Authorize]
public sealed class ProviderProfilesController : ControllerBase
{
    private readonly IProviderProfileService _providerProfileService;

    private readonly IProviderAvailabilityService _providerAvailabilityService;

    public ProviderProfilesController(
    IProviderProfileService providerProfileService,
    IProviderAvailabilityService providerAvailabilityService)
{
    _providerProfileService = providerProfileService;
    _providerAvailabilityService = providerAvailabilityService;
}

    [HttpPost]
    public async Task<ActionResult<ProviderProfileResponse>> Create(
        CreateProviderProfileRequest request,
        CancellationToken cancellationToken)
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        try
        {
            var response = await _providerProfileService.CreateAsync(
                userId,
                request,
                cancellationToken);

            return CreatedAtAction(
                nameof(Get),
                null,
                response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    [HttpGet]
    public async Task<ActionResult<ProviderProfileResponse>> Get(
        CancellationToken cancellationToken)
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        var profile = await _providerProfileService.GetByUserIdAsync(
            userId,
            cancellationToken);

        if (profile is null)
        {
            return NotFound();
        }

        return Ok(profile);
    }

    [HttpGet("nearby")]
[AllowAnonymous]
public async Task<ActionResult<IReadOnlyList<NearbyProviderResponse>>> GetNearby(
    CancellationToken cancellationToken)
{
    var response = await _providerProfileService.GetNearbyAsync(
        cancellationToken);

    return Ok(response);
}

    [HttpPut]
    public async Task<ActionResult<ProviderProfileResponse>> Update(
        UpdateProviderProfileRequest request,
        CancellationToken cancellationToken)
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        var response = await _providerProfileService.UpdateAsync(
            userId,
            request,
            cancellationToken);

        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(
        CancellationToken cancellationToken)
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        await _providerProfileService.DeleteAsync(
            userId,
            cancellationToken);

        return NoContent();
    }

    // -------------------------
    // Availability Endpoints
    // -------------------------

    [HttpGet("{providerProfileId:guid}/availability")]
    public async Task<ActionResult<IReadOnlyList<ProviderAvailabilityResponse>>> GetAvailability(
        Guid providerProfileId,
        CancellationToken cancellationToken)
    {
        var response = await _providerAvailabilityService.GetAvailabilityAsync(
    providerProfileId,
    cancellationToken);

        return Ok(response);
    }

    [HttpPost("{providerProfileId:guid}/availability")]
    public async Task<ActionResult<ProviderAvailabilityResponse>> AddAvailability(
        Guid providerProfileId,
        CreateProviderAvailabilityRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _providerAvailabilityService.AddAvailabilityAsync(
    providerProfileId,
    request,
    cancellationToken);

        return Ok(response);
    }

    [HttpPut("{providerProfileId:guid}/availability/{availabilityId:guid}")]
    public async Task<ActionResult<ProviderAvailabilityResponse>> UpdateAvailability(
        Guid providerProfileId,
        Guid availabilityId,
        UpdateProviderAvailabilityRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _providerAvailabilityService.UpdateAvailabilityAsync(
    providerProfileId,
    availabilityId,
    request,
    cancellationToken);

        return Ok(response);
    }

    [HttpDelete("{providerProfileId:guid}/availability/{availabilityId:guid}")]
    public async Task<IActionResult> DeleteAvailability(
        Guid providerProfileId,
        Guid availabilityId,
        CancellationToken cancellationToken)
    {
        await _providerAvailabilityService.DeleteAvailabilityAsync(
    providerProfileId,
    availabilityId,
    cancellationToken);

        return NoContent();
    }

    private bool TryGetUserId(out Guid userId)
    {
        var value = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.TryParse(value, out userId);
    }
}