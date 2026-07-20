using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleHub.API.Controllers;
using PeopleHub.Application.Authentication;
using PeopleHub.Contracts.Authentication;
using PeopleHub.Contracts.Users;

namespace PeopleHub.UnitTests;

public class AuthenticationControllerTests
{
    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenAuthenticationServiceRejectsCredentials()
    {
        var controller = new AuthenticationController(new RejectingAuthenticationService());

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
        var controller = new AuthenticationController(new RejectingRegistrationService());

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
        public Task RegisterAsync(
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

        public Task<CurrentUserResponse> GetCurrentUserAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
            => throw new NotImplementedException();
    }

    private sealed class RejectingRegistrationService : IAuthenticationService
    {
        public Task RegisterAsync(
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

        public Task<CurrentUserResponse> GetCurrentUserAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
            => throw new NotImplementedException();
    }
}