using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Authentication;
using PeopleHub.Contracts.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;


namespace PeopleHub.API.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(
        IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

[HttpPost("register")]
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status409Conflict)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public async Task<IActionResult> Register(
    [FromBody] RegisterRequest request,
    CancellationToken cancellationToken)
{
    try
    {
        await _authenticationService.RegisterAsync(
            request,
            cancellationToken);

        return StatusCode(StatusCodes.Status201Created);
    }
    catch (InvalidOperationException ex)
    {
        return Conflict(new { message = ex.Message });
    }
}

    [HttpPost("login")]
[ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public async Task<ActionResult<LoginResponse>> Login(
    [FromBody] LoginRequest request,
    CancellationToken cancellationToken)
{
    try
    {
        var response = await _authenticationService.LoginAsync(
            request,
            cancellationToken);

        return Ok(response);
    }
    catch (UnauthorizedAccessException)
    {
        return Unauthorized(new { message = "Invalid email or password." });
    }
}

[HttpPost("refresh")]
[ProducesResponseType(typeof(RefreshTokenResponse), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public async Task<ActionResult<RefreshTokenResponse>> Refresh(
    [FromBody] RefreshTokenRequest request,
    CancellationToken cancellationToken)
{
    try
    {
        var response = await _authenticationService.RefreshTokenAsync(
            request,
            cancellationToken);

        return Ok(response);
    }
    catch (UnauthorizedAccessException ex)
    {
        return Unauthorized(new
        {
            message = ex.Message
        });
    }
}

[HttpPost("logout")]
public async Task<IActionResult> Logout(
    LogoutRequest request,
    CancellationToken cancellationToken)
{
    await _authenticationService.LogoutAsync(
        request,
        cancellationToken);

    return NoContent();
}

[Authorize]
[HttpGet("me")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public IActionResult Me()
{
    return Ok(new
    {
        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                 ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub),

        Email = User.FindFirstValue(ClaimTypes.Email),

        Claims = User.Claims.Select(c => new
        {
            c.Type,
            c.Value
        })
    });
}

[Authorize(Roles = "Admin")]
[HttpGet("admin")]
public IActionResult AdminOnly()
{
    return Ok(new
    {
        Message = "Welcome Admin!"
    });
}

}