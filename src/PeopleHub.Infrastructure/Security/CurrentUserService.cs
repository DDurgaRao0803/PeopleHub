using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Common.Interfaces.Services;

namespace PeopleHub.Infrastructure.Security;

public sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? User =>
        _httpContextAccessor.HttpContext?.User;

    public Guid UserId
    {
        get
        {
            var value = User?.FindFirstValue(ClaimTypes.NameIdentifier);

            return Guid.TryParse(value, out var id)
                ? id
                : Guid.Empty;
        }
    }

    public bool IsAuthenticated =>
        User?.Identity?.IsAuthenticated ?? false;

    public string? Email =>
        User?.FindFirstValue(ClaimTypes.Email);

    public string? Role =>
        User?.FindFirstValue(ClaimTypes.Role);
}