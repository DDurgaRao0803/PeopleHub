using PeopleHub.Contracts.Authentication;


namespace PeopleHub.Application.Authentication;

public interface IAuthenticationService
{
    Task<Guid> RegisterAsync(
    RegisterRequest request,
    CancellationToken cancellationToken = default);
    
    Task<LoginResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default);

    Task<RefreshTokenResponse> RefreshTokenAsync(
        RefreshTokenRequest request,
        CancellationToken cancellationToken = default);

    Task LogoutAsync(
    LogoutRequest request,
    CancellationToken cancellationToken = default);

    Task<Guid> ForgotPasswordAsync(
    ForgotPasswordRequest request,
    CancellationToken cancellationToken = default);

    Task ResetPasswordAsync(
    ResetPasswordRequest request,
    CancellationToken cancellationToken = default);

    Task ChangePasswordAsync(
    Guid userId,
    ChangePasswordRequest request,
    CancellationToken cancellationToken = default);

}