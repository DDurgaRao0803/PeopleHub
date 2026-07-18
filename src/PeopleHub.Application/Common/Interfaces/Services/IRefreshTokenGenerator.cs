namespace PeopleHub.Application.Common.Interfaces.Services;

public interface IRefreshTokenGenerator
{
    string GenerateToken();

    string ComputeHash(string refreshToken);
}