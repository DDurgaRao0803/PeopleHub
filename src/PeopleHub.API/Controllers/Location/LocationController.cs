using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Location;
using PeopleHub.Contracts.Location;

namespace PeopleHub.API.Controllers.Location;

[ApiController]
[Route("api/location")]
[Authorize]
public sealed class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;


    public LocationController(
        ILocationService locationService)
    {
        _locationService = locationService;
    }



    [HttpPost("update")]
    [ProducesResponseType(
        typeof(ProviderLocationResponse),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<ProviderLocationResponse>> Update(
        [FromBody] UpdateLocationRequest request,
        CancellationToken cancellationToken)
    {
        var providerProfileId =
            GetUserId();


        var result =
            await _locationService.UpdateLocationAsync(
                providerProfileId,
                request,
                cancellationToken);


        return Ok(result);
    }



    [HttpGet("provider/{providerProfileId:guid}")]
    [ProducesResponseType(
        typeof(ProviderLocationResponse),
        StatusCodes.Status200OK)]
    [ProducesResponseType(
        StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProviderLocationResponse>> GetProviderLocation(
        Guid providerProfileId,
        CancellationToken cancellationToken)
    {
        var result =
            await _locationService.GetProviderLocationAsync(
                providerProfileId,
                cancellationToken);


        if (result is null)
        {
            return NotFound();
        }


        return Ok(result);
    }



    private Guid GetUserId()
    {
        var claim =
            User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier);


        if (claim is null)
        {
            throw new UnauthorizedAccessException(
                "User id missing.");
        }


        return Guid.Parse(claim.Value);
    }
}