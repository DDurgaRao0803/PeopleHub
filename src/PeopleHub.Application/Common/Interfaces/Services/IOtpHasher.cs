namespace PeopleHub.Application.Common.Interfaces.Services;

public interface IOtpHasher
{
    string Hash(string otp);

    bool Verify(
        string otp,
        string hash);
}