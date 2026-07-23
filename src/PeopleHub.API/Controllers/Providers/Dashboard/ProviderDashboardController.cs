using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Providers.Dashboard;
using PeopleHub.Contracts.Providers.Dashboard;

namespace PeopleHub.API.Controllers;

[ApiController]
[Authorize]
[Route("api/provider/dashboard")]
public class ProviderDashboardController : ControllerBase
{
    private readonly IProviderDashboardService _dashboardService;

    public ProviderDashboardController(
        IProviderDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }


    [HttpGet]
    public async Task<ActionResult<ProviderDashboardResponse>> GetDashboard(
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _dashboardService.GetDashboardAsync(
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
}