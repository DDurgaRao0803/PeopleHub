using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.SmartMatch.Interfaces;
using PeopleHub.Application.SmartMatch.Models;

namespace PeopleHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SmartMatchController : ControllerBase
{
    private readonly ISmartMatchService _smartMatchService;

    public SmartMatchController(
        ISmartMatchService smartMatchService)
    {
        _smartMatchService = smartMatchService;
    }

    [HttpPost("{serviceRequestId:guid}")]
    public async Task<ActionResult<SmartMatchResponse>> FindBestProvider(
        Guid serviceRequestId,
        CancellationToken cancellationToken)
    {
        var response = await _smartMatchService.FindBestProviderAsync(
            serviceRequestId,
            cancellationToken);

        return Ok(response);
    }
}