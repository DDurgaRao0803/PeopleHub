using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Authentication;
using PeopleHub.Contracts.Authentication;
using Microsoft.AspNetCore.Authorization;

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
    await _authenticationService.RegisterAsync(
        request,
        cancellationToken);

    return StatusCode(StatusCodes.Status201Created);
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
    var response = await _authenticationService.LoginAsync(
        request,
        cancellationToken);

    return Ok(response);
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
    var response = await _authenticationService.RefreshTokenAsync(
        request,
        cancellationToken);

    return Ok(response);
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
}