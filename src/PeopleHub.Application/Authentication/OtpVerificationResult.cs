namespace PeopleHub.Application.Authentication;

public enum OtpVerificationResult
{
    Success = 0,

    InvalidOtp = 1,

    Expired = 2,

    AlreadyVerified = 3,

    TooManyAttempts = 4,

    NotFound = 5
}