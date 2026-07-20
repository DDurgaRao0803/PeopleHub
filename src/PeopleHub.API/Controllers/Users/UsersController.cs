using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Users;
using PeopleHub.Contracts.Users;
using PeopleHub.Contracts.Common;


namespace PeopleHub.API.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public sealed class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
{
    _userService = userService;
}

    [HttpGet("me")]
    public async Task<ActionResult> GetCurrentUser(
        CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var user = await _userService.GetCurrentUserAsync(
    userId,
    cancellationToken);

        return Ok(user);
    }

    [HttpGet("{id:guid}")]
public async Task<ActionResult<UserResponse>> GetUserById(
    Guid id,
    CancellationToken cancellationToken)
{
    var currentUserIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
    var currentUserRole = User.FindFirstValue(ClaimTypes.Role);

    if (!Guid.TryParse(currentUserIdClaim, out var currentUserId))
    {
        return Unauthorized();
    }

    var isAdmin = string.Equals(
        currentUserRole,
        "Admin",
        StringComparison.OrdinalIgnoreCase);

    if (!isAdmin && currentUserId != id)
    {
        return Forbid();
    }

    var user = await _userService.GetUserByIdAsync(
        id,
        cancellationToken);

    return Ok(user);
}

[Authorize(Roles = "Admin")]
[HttpGet]
public async Task<ActionResult<PagedResponse<UserResponse>>> GetAllUsers(
    [FromQuery] GetUsersRequest request,
    CancellationToken cancellationToken = default)
{
    if (request.PageNumber < 1)
    {
        request = request with { PageNumber = 1 };
    }

    if (request.PageSize < 1)
    {
        request = request with { PageSize = 10 };
    }

    if (request.PageSize > 100)
    {
        request = request with { PageSize = 100 };
    }

    var users = await _userService.GetAllUsersAsync(
        request,
        cancellationToken);

    return Ok(users);
}
[HttpPut("{id:guid}")]
public async Task<ActionResult<UserResponse>> UpdateUser(
    Guid id,
    UpdateUserRequest request,
    CancellationToken cancellationToken)
{
    var currentUserIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
    var currentUserRole = User.FindFirstValue(ClaimTypes.Role);

    if (!Guid.TryParse(currentUserIdClaim, out var currentUserId))
    {
        return Unauthorized();
    }

    var isAdmin = string.Equals(
        currentUserRole,
        "Admin",
        StringComparison.OrdinalIgnoreCase);

    if (!isAdmin && currentUserId != id)
    {
        return Forbid();
    }

    var updatedUser = await _userService.UpdateUserAsync(
        id,
        request,
        cancellationToken);

    return Ok(updatedUser);
}
[HttpDelete("{id:guid}")]
public async Task<IActionResult> DeleteUser(
    Guid id,
    CancellationToken cancellationToken)
{
    var currentUserIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
    var currentUserRole = User.FindFirstValue(ClaimTypes.Role);

    if (!Guid.TryParse(currentUserIdClaim, out var currentUserId))
    {
        return Unauthorized();
    }

    var isAdmin = string.Equals(
        currentUserRole,
        "Admin",
        StringComparison.OrdinalIgnoreCase);

    if (!isAdmin && currentUserId != id)
    {
        return Forbid();
    }

    await _userService.DeleteUserAsync(
        id,
        cancellationToken);

    return NoContent();
}

}