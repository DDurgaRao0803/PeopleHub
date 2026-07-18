using PeopleHub.Application.Common.Models;

namespace PeopleHub.Application.Common.Interfaces.Services;

public interface IJwtTokenGenerator
{
    JwtTokenResult GenerateToken(
        Guid userId,
        string email);
}