using System.Security.Cryptography;
using System.Text;
using PeopleHub.Application.Common.Interfaces.Services;

namespace PeopleHub.Infrastructure.Authentication;

public sealed class OtpHasher : IOtpHasher
{
    public string Hash(string otp)
    {
        var bytes = SHA256.HashData(
            Encoding.UTF8.GetBytes(otp));

        return Convert.ToHexString(bytes);
    }

    public bool Verify(
        string otp,
        string hash)
    {
        return Hash(otp) == hash;
    }
}