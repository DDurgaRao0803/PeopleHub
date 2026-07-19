using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Providers;
using PeopleHub.Contracts.Providers;

namespace PeopleHub.API.Controllers;

[ApiController]
[Route("api/provider-profiles")]
[Authorize]
public sealed class ProviderProfilesController : ControllerBase
{
    private readonly IProviderProfileService _providerProfileService;

    public ProviderProfilesController(
        IProviderProfileService providerProfileService)
    {
        _providerProfileService = providerProfileService;
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

    private bool TryGetUserId(out Guid userId)
    {
        var value = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.TryParse(value, out userId);
    }
}