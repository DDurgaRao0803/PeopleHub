using PeopleHub.Domain.Enums;

namespace PeopleHub.Application.Authentication;

public interface IOtpService
{
    Task<string> GenerateAsync(
        Guid userId,
        OtpPurpose purpose,
        CancellationToken cancellationToken = default);

    Task<OtpVerificationResult> VerifyAsync(
    Guid userId,
    string otp,
    OtpPurpose purpose,
    CancellationToken cancellationToken = default);

    Task<string> ResendAsync(
        Guid userId,
        OtpPurpose purpose,
        CancellationToken cancellationToken = default);
}