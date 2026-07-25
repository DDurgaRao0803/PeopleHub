namespace PeopleHub.Contracts.Authentication;

public sealed class VerifyOtpRequest
{
    public Guid UserId { get; set; }

    public string Otp { get; set; } = string.Empty;
}