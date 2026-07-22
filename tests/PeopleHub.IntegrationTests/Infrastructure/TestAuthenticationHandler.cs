using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace PeopleHub.IntegrationTests.Infrastructure;

public sealed class TestAuthenticationHandler
    : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public new const string Scheme = "Test";

    public TestAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
{
    if (Request.Headers.ContainsKey("X-Test-Anonymous"))
    {
        return Task.FromResult(AuthenticateResult.NoResult());
    }

    var role = Request.Headers.TryGetValue(
        "X-Test-Role",
        out var headerRole)
        ? headerRole.ToString()
        : "Admin";

    var claims = new List<Claim>
    {
        new Claim(
            ClaimTypes.NameIdentifier,
            "11111111-1111-1111-1111-111111111111"),

        new Claim(
            ClaimTypes.Name,
            "Integration Test User"),

        new Claim(
            ClaimTypes.Role,
            role)
    };

    var identity = new ClaimsIdentity(
        claims,
        Scheme);

    var principal = new ClaimsPrincipal(identity);

    var ticket = new AuthenticationTicket(
        principal,
        Scheme);

    return Task.FromResult(
        AuthenticateResult.Success(ticket));
}
}