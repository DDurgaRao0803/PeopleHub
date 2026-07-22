using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Administration;
using PeopleHub.Contracts.Users;
using PeopleHub.Contracts.Administration;

namespace PeopleHub.API.Controllers.Administration;

[ApiController]
[Route("api/admin/users")]
[Authorize(Policy = "AdminOnly")]
public sealed class AdminUsersController : ControllerBase
{
    private readonly IAdminUserService _adminUserService;

    public AdminUsersController(
        IAdminUserService adminUserService)
    {
        _adminUserService = adminUserService;
    }

    [HttpGet]
[ProducesResponseType(StatusCodes.Status200OK)]
public async Task<IActionResult> GetUsers(
    [FromQuery] GetUsersRequest request,
    CancellationToken cancellationToken)
{
    var users = await _adminUserService.GetUsersAsync(
        request,
        cancellationToken);

    return Ok(users);
}

[HttpPut("{id:guid}/status")]
[ProducesResponseType(StatusCodes.Status204NoContent)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<IActionResult> UpdateUserStatus(
    Guid id,
    [FromBody] UpdateUserStatusRequest request,
    CancellationToken cancellationToken)
{
    var updated = await _adminUserService.UpdateUserStatusAsync(
        id,
        request,
        cancellationToken);

    if (!updated)
    {
        return NotFound();
    }

    return NoContent();
}

[HttpGet("{id:guid}")]
[ProducesResponseType(typeof(UserDetailsResponse), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<IActionResult> GetUser(
    Guid id,
    CancellationToken cancellationToken)
{
    var user = await _adminUserService.GetUserByIdAsync(
        id,
        cancellationToken);

    if (user is null)
    {
        return NotFound();
    }

    return Ok(user);
}

}