using PeopleHub.Application.Authentication;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Common.Interfaces.Services;
using PeopleHub.Contracts.Authentication;
using Microsoft.Extensions.Options;
using PeopleHub.Domain.Aggregates.User;
using PeopleHub.Domain.ValueObjects;
using PeopleHub.Infrastructure.Persistence.Context;

namespace PeopleHub.Infrastructure.Authentication;

public sealed class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly JwtOptions _jwtOptions;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _dbContext;

    public AuthenticationService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator,
    IRefreshTokenGenerator refreshTokenGenerator,
    IOptions<JwtOptions> jwtOptions,
    IUnitOfWork unitOfWork,
    ApplicationDbContext dbContext)
{
    _userRepository = userRepository;
    _passwordHasher = passwordHasher;
    _jwtTokenGenerator = jwtTokenGenerator;
    _refreshTokenGenerator = refreshTokenGenerator;
    _jwtOptions = jwtOptions.Value;
    _unitOfWork = unitOfWork;
    _dbContext = dbContext;
}

    public async Task<RefreshTokenResponse> RefreshTokenAsync(
    RefreshTokenRequest request,
    CancellationToken cancellationToken = default)
{
    var tokenHash = _refreshTokenGenerator.ComputeHash(
        request.RefreshToken);

    var user = await _userRepository.GetByRefreshTokenHashAsync(
        tokenHash,
        cancellationToken);

    if (user is null)
    {
        throw new UnauthorizedAccessException("Invalid refresh token.");
    }

    var existingRefreshToken = user.GetRefreshToken(tokenHash);

    if (existingRefreshToken is null)
    {
        throw new UnauthorizedAccessException("Invalid refresh token.");
    }

    if (existingRefreshToken.IsExpired)
    {
        throw new UnauthorizedAccessException("Refresh token has expired.");
    }

    if (existingRefreshToken.IsRevoked)
    {
        throw new UnauthorizedAccessException("Refresh token has been revoked.");
    }

    user.RevokeRefreshToken(tokenHash);

    var jwt = _jwtTokenGenerator.GenerateToken(
        user.Id,
        user.Email.Value);

    var newRefreshToken = _refreshTokenGenerator.GenerateToken();

    var newRefreshTokenHash =
        _refreshTokenGenerator.ComputeHash(newRefreshToken);

    user.AddRefreshToken(
        RefreshToken.Create(
            user.Id,
            newRefreshTokenHash,
            DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiryDays)));

    user.RemoveExpiredRefreshTokens();

    await _userRepository.UpdateAsync(
    user,
    cancellationToken);

Console.WriteLine("===== CHANGE TRACKER =====");

foreach (var entry in _dbContext.ChangeTracker.Entries())
{
    Console.WriteLine(
        $"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
}

Console.WriteLine("==========================");

await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new RefreshTokenResponse
    {
        AccessToken = jwt.AccessToken,
        RefreshToken = newRefreshToken,
        ExpiresAtUtc = jwt.ExpiresAtUtc
    };
}

public async Task RegisterAsync(
    RegisterRequest request,
    CancellationToken cancellationToken = default)
{
    var emailExists = await _userRepository.ExistsByEmailAsync(
        request.Email,
        cancellationToken);

    if (emailExists)
    {
        throw new InvalidOperationException(
            "A user with this email already exists.");
    }

    var phoneExists = await _userRepository.ExistsByPhoneNumberAsync(
    request.PhoneNumber,
    cancellationToken);

if (phoneExists)
{
    throw new InvalidOperationException(
        "A user with this phone number already exists.");
}

    var passwordHash = _passwordHasher.HashPassword(
        request.Password);

    var user = new User(
    request.FirstName,
    request.LastName,
    Email.Create(request.Email),
    PhoneNumber.Create(request.PhoneNumber),
    passwordHash);

    await _userRepository.AddAsync(
        user,
        cancellationToken);

    await _unitOfWork.SaveChangesAsync(
        cancellationToken);
}

public async Task<LoginResponse> LoginAsync(
    LoginRequest request,
    CancellationToken cancellationToken = default)
{
    Console.WriteLine(">>> LoginAsync START");

    var user = await _userRepository.GetByEmailAsync(
        request.Email,
        cancellationToken);

    Console.WriteLine(">>> User loaded");

    if (user is null)
    {
        throw new UnauthorizedAccessException("Invalid email or password.");
    }

    var passwordValid = _passwordHasher.VerifyPassword(
        request.Password,
        user.PasswordHash);

    if (!passwordValid)
    {
        throw new UnauthorizedAccessException("Invalid email or password.");
    }

    Console.WriteLine(">>> Password verified");

    user.UpdateLastLogin();

    Console.WriteLine(">>> Last login updated");

    var jwt = _jwtTokenGenerator.GenerateToken(
        user.Id,
        user.Email.Value);

    Console.WriteLine(">>> JWT generated");

    var refreshToken = _refreshTokenGenerator.GenerateToken();

    var refreshTokenHash =
        _refreshTokenGenerator.ComputeHash(refreshToken);

    var refreshTokenEntity = RefreshToken.Create(
        user.Id,
        refreshTokenHash,
        DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiryDays));

    Console.WriteLine(">>> RefreshToken entity created");

    user.AddRefreshToken(refreshTokenEntity);

    var refreshEntry = _dbContext.Entry(refreshTokenEntity);

Console.WriteLine(
    $"RefreshToken state immediately after AddRefreshToken: {refreshEntry.State}");

    Console.WriteLine(">>> RefreshToken added to User");

    await _userRepository.UpdateAsync(
        user,
        cancellationToken);

        Console.WriteLine(
    $"RefreshToken state after UpdateAsync: {_dbContext.Entry(refreshTokenEntity).State}");

    Console.WriteLine(">>> Repository.UpdateAsync completed");

    Console.WriteLine("===== CHANGE TRACKER =====");

    foreach (var entry in _dbContext.ChangeTracker.Entries())
    {
        Console.WriteLine(
            $"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
    }

    Console.WriteLine("==========================");

    Console.WriteLine(">>> Before SaveChanges");

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    Console.WriteLine(">>> SaveChanges completed");

    return new LoginResponse
    {
        AccessToken = jwt.AccessToken,
        RefreshToken = refreshToken,
        ExpiresAtUtc = jwt.ExpiresAtUtc
    };
}
public async Task LogoutAsync(
    LogoutRequest request,
    CancellationToken cancellationToken = default)
{
    var tokenHash = _refreshTokenGenerator.ComputeHash(
        request.RefreshToken);

    var user = await _userRepository.GetByRefreshTokenHashAsync(
        tokenHash,
        cancellationToken);

    if (user is null)
    {
        return;
    }

    var refreshToken = user.GetRefreshToken(tokenHash);

    if (refreshToken is null)
    {
        return;
    }

    if (refreshToken.IsRevoked)
    {
        return;
    }

    user.RevokeRefreshToken(tokenHash);

    await _userRepository.UpdateAsync(
        user,
        cancellationToken);

    await _unitOfWork.SaveChangesAsync(
        cancellationToken);
}
}