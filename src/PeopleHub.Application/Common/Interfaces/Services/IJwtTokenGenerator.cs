using PeopleHub.Application.Common.Models;
using PeopleHub.Domain.Enums;

namespace PeopleHub.Application.Common.Interfaces.Services;

public interface IJwtTokenGenerator
{
    JwtTokenResult GenerateToken(
        Guid userId,
        string email,
        Role role);
}