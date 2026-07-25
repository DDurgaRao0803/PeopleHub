using System.Security.Cryptography;
using PeopleHub.Application.Common.Interfaces.Services;

namespace PeopleHub.Infrastructure.Authentication;

public sealed class OtpGenerator : IOtpGenerator
{
    public string Generate()
    {
        var value = RandomNumberGenerator.GetInt32(
            100000,
            1000000);

        return value.ToString();
    }
}