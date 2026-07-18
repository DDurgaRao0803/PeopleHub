using PeopleHub.Contracts.Authentication;


namespace PeopleHub.Application.Authentication;

public interface IAuthenticationService
{
    Task RegisterAsync(
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

}