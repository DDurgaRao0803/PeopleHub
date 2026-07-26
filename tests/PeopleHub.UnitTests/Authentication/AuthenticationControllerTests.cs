using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.API.Controllers;
using PeopleHub.Application.Authentication;
using PeopleHub.Contracts.Authentication;
using PeopleHub.Domain.Enums;


namespace PeopleHub.UnitTests;

public class AuthenticationControllerTests
{
    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenAuthenticationServiceRejectsCredentials()
    {
        var controller = new AuthenticationController(
    new RejectingAuthenticationService(),
    new FakeOtpService());

        var request = new LoginRequest
        {
            Email = "user@example.com",
            Password = "WrongPassword"
        };

        var result = await controller.Login(request, CancellationToken.None);

        var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status401Unauthorized, unauthorized.StatusCode);
    }

    [Fact]
    public async Task Register_ReturnsConflict_WhenAuthenticationServiceRejectsDuplicateUser()
    {
        var controller = new AuthenticationController(
    new RejectingRegistrationService(),
    new FakeOtpService());

        var request = new RegisterRequest
        {
            FirstName = "Durga",
            LastName = "Rao",
            Email = "durga@example.com",
            PhoneNumber = "9876543210",
            Password = "Password@123"
        };

        var result = await controller.Register(request, CancellationToken.None);

        var conflict = Assert.IsType<ConflictObjectResult>(result);

        Assert.Equal(StatusCodes.Status409Conflict, conflict.StatusCode);
    }

    private sealed class RejectingAuthenticationService : IAuthenticationService
    {

        public Task<Guid> RegisterAsync(
    RegisterRequest request,
    CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public Task<LoginResponse> LoginAsync(
            LoginRequest request,
            CancellationToken cancellationToken = default)
            => throw new UnauthorizedAccessException("Invalid email or password.");

        public Task<RefreshTokenResponse> RefreshTokenAsync(
            RefreshTokenRequest request,
            CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public Task LogoutAsync(
            LogoutRequest request,
            CancellationToken cancellationToken = default)
            => throw new NotImplementedException();


        public Task<Guid> ForgotPasswordAsync(
    ForgotPasswordRequest request,
    CancellationToken cancellationToken = default)
    => throw new InvalidOperationException("User not found.");

    public Task ResetPasswordAsync(
    ResetPasswordRequest request,
    CancellationToken cancellationToken = default)
{
    throw new NotImplementedException();
}
            
    }
    
    private sealed class RejectingRegistrationService : IAuthenticationService
    {
        public Task<Guid> RegisterAsync(
            RegisterRequest request,
            CancellationToken cancellationToken = default)
            => throw new InvalidOperationException("A user with this email already exists.");

        public Task<LoginResponse> LoginAsync(
            LoginRequest request,
            CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public Task<RefreshTokenResponse> RefreshTokenAsync(
            RefreshTokenRequest request,
            CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public Task LogoutAsync(
            LogoutRequest request,
            CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        public Task<Guid> ForgotPasswordAsync(
    ForgotPasswordRequest request,
    CancellationToken cancellationToken = default)
    => throw new InvalidOperationException("User not found.");

    public Task ResetPasswordAsync(
    ResetPasswordRequest request,
    CancellationToken cancellationToken = default)
{
    throw new NotImplementedException();
}

    }

    private sealed class ForgotPasswordAuthenticationService : IAuthenticationService
{
    public Task<Guid> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public Task<LoginResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public Task<RefreshTokenResponse> RefreshTokenAsync(
        RefreshTokenRequest request,
        CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public Task LogoutAsync(
        LogoutRequest request,
        CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public Task<Guid> ForgotPasswordAsync(
        ForgotPasswordRequest request,
        CancellationToken cancellationToken = default)
        => Task.FromResult(Guid.NewGuid());

    public Task ResetPasswordAsync(
    ResetPasswordRequest request,
    CancellationToken cancellationToken = default)
{
    return Task.CompletedTask;
}
}

    private sealed class FakeOtpService : IOtpService
{
    public Task<string> GenerateAsync(
        Guid userId,
        OtpPurpose purpose,
        CancellationToken cancellationToken = default)
        => Task.FromResult("123456");

    public Task<OtpVerificationResult> VerifyAsync(
        Guid userId,
        string otp,
        OtpPurpose purpose,
        CancellationToken cancellationToken = default)
        => Task.FromResult(OtpVerificationResult.Success);

    public Task<string> ResendAsync(
        Guid userId,
        OtpPurpose purpose,
        CancellationToken cancellationToken = default)
        => Task.FromResult("123456");
}

[Fact]
public async Task ForgotPassword_ReturnsOk_WhenUserExists()
{
    var controller = new AuthenticationController(
        new ForgotPasswordAuthenticationService(),
        new FakeOtpService());

    var request = new ForgotPasswordRequest
    {
        Email = "user@example.com"
    };

    var result = await controller.ForgotPassword(
        request,
        CancellationToken.None);

    var ok = Assert.IsType<OkObjectResult>(result);

    Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);
}



}