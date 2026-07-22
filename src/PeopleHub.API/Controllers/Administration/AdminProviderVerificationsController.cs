using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Administration;

namespace PeopleHub.API.Controllers.Administration;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/admin/provider-verifications")]
public sealed class AdminProviderVerificationsController : ControllerBase
{
    private readonly IAdminProviderVerificationService _service;

    public AdminProviderVerificationsController(
        IAdminProviderVerificationService service)
    {
        _service = service;
    }

    [HttpGet("pending")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPending(
        CancellationToken cancellationToken)
    {
        var result = await _service.GetPendingVerificationsAsync(
            cancellationToken);

        return Ok(result);
    }
}