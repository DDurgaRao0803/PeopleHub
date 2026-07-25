using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.Application.Authentication;
using PeopleHub.Contracts.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using PeopleHub.Domain.Enums;

namespace PeopleHub.API.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
private readonly IOtpService _otpService;

    public AuthenticationController(
    IAuthenticationService authenticationService,
    IOtpService otpService)
{
    _authenticationService = authenticationService;
    _otpService = otpService;
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
            var userId = await _authenticationService.RegisterAsync(
    request,
    cancellationToken);

await _otpService.GenerateAsync(
    userId,
    OtpPurpose.Registration,
    cancellationToken);

return StatusCode(
    StatusCodes.Status201Created,
    new
    {
        userId
    });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
    }

    [HttpPost("verify-otp")]
public async Task<IActionResult> VerifyOtp(
    VerifyOtpRequest request,
    CancellationToken cancellationToken)
{
    var result = await _otpService.VerifyAsync(
        request.UserId,
        request.Otp,
        OtpPurpose.Registration,
        cancellationToken);

    return result switch
{
    OtpVerificationResult.Success =>
        Ok(),

    OtpVerificationResult.InvalidOtp =>
        BadRequest(new
        {
            message = "Invalid OTP."
        }),

    OtpVerificationResult.Expired =>
        BadRequest(new
        {
            message = "OTP has expired."
        }),

    OtpVerificationResult.AlreadyVerified =>
        BadRequest(new
        {
            message = "User is already verified."
        }),

    OtpVerificationResult.TooManyAttempts =>
        BadRequest(new
        {
            message = "Too many invalid attempts. Please request a new OTP."
        }),

    OtpVerificationResult.NotFound =>
        NotFound(new
        {
            message = "OTP not found."
        }),

    _ =>
        BadRequest(new
        {
            message = "OTP verification failed."
        })
};
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
            return Unauthorized(new
            {
                message = "Invalid email or password."
            });
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Logout(
        [FromBody] LogoutRequest request,
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
}