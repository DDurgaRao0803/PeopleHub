using PeopleHub.Application.Authentication;
using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Common.Interfaces.Services;
using PeopleHub.Domain.Aggregates.Otp;
using PeopleHub.Domain.Enums;

namespace PeopleHub.Infrastructure.Authentication;

public sealed class OtpService : IOtpService
{
    private static readonly TimeSpan OtpValidity = TimeSpan.FromMinutes(5);

    private readonly IOtpRepository _otpRepository;
    private readonly IUserRepository _userRepository;
    private readonly IOtpGenerator _otpGenerator;
    private readonly IOtpHasher _otpHasher;
    private readonly IUnitOfWork _unitOfWork;

    public OtpService(
        IOtpRepository otpRepository,
        IUserRepository userRepository,
        IOtpGenerator otpGenerator,
        IOtpHasher otpHasher,
        IUnitOfWork unitOfWork)
    {
        _otpRepository = otpRepository;
        _userRepository = userRepository;
        _otpGenerator = otpGenerator;
        _otpHasher = otpHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> GenerateAsync(
        Guid userId,
        OtpPurpose purpose,
        CancellationToken cancellationToken = default)
    {
        var otp = _otpGenerator.Generate();

        var otpHash = _otpHasher.Hash(otp);

        var entity = OtpCode.Create(
            userId,
            otpHash,
            purpose,
            OtpValidity);

        await _otpRepository.AddAsync(
            entity,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return otp;
    }

    public async Task<OtpVerificationResult> VerifyAsync(
        Guid userId,
        string otp,
        OtpPurpose purpose,
        CancellationToken cancellationToken = default)
    {
        var existing = await _otpRepository.GetLatestAsync(
            userId,
            purpose,
            cancellationToken);

        if (existing is null)
        {
            return OtpVerificationResult.NotFound;
        }

        if (existing.IsVerified)
        {
            return OtpVerificationResult.AlreadyVerified;
        }

        if (existing.IsExpired)
        {
            return OtpVerificationResult.Expired;
        }

        if (!_otpHasher.Verify(otp, existing.CodeHash))
        {
            existing.IncrementFailedAttempts();

            await _otpRepository.UpdateAsync(
                existing,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return OtpVerificationResult.InvalidOtp;
        }

        var user = await _userRepository.GetByIdAsync(
            userId,
            cancellationToken);

        if (user is null)
        {
            return OtpVerificationResult.NotFound;
        }

        existing.MarkVerified();

        switch (purpose)
{
    case OtpPurpose.Registration:
        user.VerifyEmail();
        break;

    case OtpPurpose.PhoneVerification:
        user.VerifyPhone();
        user.Activate();
        break;

    case OtpPurpose.ForgotPassword:
        // Password reset flow handles the password change.
        break;

    case OtpPurpose.TwoFactorAuthentication:
        // OTP only authenticates the current login session.
        break;

    default:
        throw new ArgumentOutOfRangeException(
            nameof(purpose),
            purpose,
            "Unsupported OTP purpose.");
}

await _otpRepository.UpdateAsync(
    existing,
    cancellationToken);

await _userRepository.UpdateAsync(
    user,
    cancellationToken);

await _unitOfWork.SaveChangesAsync(
    cancellationToken);

return OtpVerificationResult.Success;
}

    public async Task<string> ResendAsync(
        Guid userId,
        OtpPurpose purpose,
        CancellationToken cancellationToken = default)
    {
        return await GenerateAsync(
            userId,
            purpose,
            cancellationToken);
    }
}