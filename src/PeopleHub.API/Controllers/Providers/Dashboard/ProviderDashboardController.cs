using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Providers.Dashboard;
using PeopleHub.Contracts.Providers.Dashboard;

namespace PeopleHub.API.Controllers;

[Authorize]
[ApiController]
[Route("api/provider/dashboard")]
public sealed class ProviderDashboardController : ControllerBase
{
    private readonly IProviderDashboardService _dashboardService;

    public ProviderDashboardController(
        IProviderDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ProviderDashboardResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<ProviderDashboardResponse>> GetDashboard(
        CancellationToken cancellationToken)
    {
        var dashboard = await _dashboardService.GetDashboardAsync(
            cancellationToken);

        return Ok(dashboard);
    }
}